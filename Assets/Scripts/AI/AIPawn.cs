using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPawn : Pawn
{
	public List<Pawn> visibleEnemies{
		get{
			return sightList.FindAll(x => x.owner != this.owner);
		}
	}

	public override void Turn ()
	{
		if(!isDead){
			base.Turn ();

			NodeBehaviour bestCover = GetBestCoverWithinReach();
			Debug.Log (bestCover);

			if(currentNode != bestCover && bestCover != null){
				Command move = Factory.GetCommand(Commands.Move, this);
				move.target = bestCover.GetComponent<Targetable>();
				move.Execute();
			}

			StartCoroutine(WaitForFree());

			if (weapon.actionCost < ActionPoints) {
				Command attack = Factory.GetCommand(Commands.Attack, this);
				if(attack.validTargets.Count > 0){
					Pawn bestTarget = GetBestTarget(attack.validTargets);
					if(bestTarget != null){
						attack.target = bestTarget.GetComponent<Targetable>();
						attack.Execute();
					}
				}
			}

			StartCoroutine(WaitForFree());
		}
	}

	NodeBehaviour GetBestCoverWithinReach()
	{
		NodeBehaviour previousBest = currentNode;
	
		List<Pawn> enemies = visibleEnemies;

		float bestScore = 0;

		foreach (Pawn enemy in enemies){
			bestScore += (int) Pawn.GetCoverAtNode(previousBest, enemy);
		}
		bestScore /= enemies.Count;

		Dictionary<NodeBehaviour, float> scoringTables = new Dictionary<NodeBehaviour, float>();
		scoringTables.Add(previousBest,bestScore);

		for(int counter = 1; counter <= ActionPoints; counter++){
			foreach(NodeBehaviour node in Pathfinder.NodesWithinSteps(currentNode, 3*counter)){
				if(!scoringTables.ContainsKey(node)){
					float coverScore = 0;
					foreach (Pawn enemy in enemies){
						coverScore += (float)Pawn.GetCoverAtNode(node, enemy);
					}
					coverScore /= enemies.Count;
					scoringTables.Add(node, coverScore);
				}
			}

			NodeBehaviour currentBest = previousBest;

			foreach (NodeBehaviour node in scoringTables.Keys){
				if(scoringTables[currentBest] < scoringTables[node]){
					currentBest = node;
				}
			}

			if(currentBest == previousBest && (int)scoringTables[currentBest] > 0){
				return currentBest;
			}

			previousBest = currentBest;
		}

        return null;
	}

	Pawn GetBestTarget(List<Targetable> validTargets){

		Pawn bestTarget = null;
		double bestHitchance = 0;
		int effectiveRange = weapon.range;

		foreach (Targetable target in validTargets) {
			if(target.GetComponent<Pawn>() != null){
				if (owner.transform.position.y - target.transform.position.y > 0) {
					effectiveRange += (int)((owner.transform.position.y - target.transform.position.y) / 2f);
				}
				double hitChance = 1 - (1 - 0.5) * (Vector3.Distance (owner.transform.position, target.transform.position) - 1) / (effectiveRange - 1);

				if(bestHitchance < hitChance){
					bestHitchance = hitChance;
					bestTarget = target.GetComponent<Pawn>();
				}
			}
		}
		return bestTarget;
	}

	IEnumerator WaitForFree(){
		while (TurnManager.instance.busy) {
			yield return null; // wait until next frame
		}
	}
}

