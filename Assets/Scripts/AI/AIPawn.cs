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
		base.Turn ();

		NodeBehaviour bestCover = GetBestCoverWithinReach();

		if(currentNode != bestCover){
			Command move = Factory.GetCommand(Commands.Move, this);
			move.target = bestCover.GetComponent<Targetable>();
			move.Execute();
		}

		if (Weapon.actionCost < ActionPoints) {
			Command attack = Factory.GetCommand(Commands.Attack, this);
			if(attack.validTargets > 0){
				attack.target = GetBestTarget(attack.validTargets);
				attack.Execute();
			}
		}
	}

	NodeBehaviour GetBestCoverWithinReach()
	{
		NodeBehaviour previousBest = currentNode;
	
		List<Pawn> enemies = visibleEnemies;

		int bestScore = 0;

		foreach (Pawn enemy in enemies){
			bestScore += (int)Pawn.GetCoverAtNode(previousBest, enemy);
		}
		bestScore /= enemies.Count;

		Dictionary<NodeBehaviour, float> scoringTables = new Dictionary<NodeBehaviour, float>();
		scoringTables.Add(previousBest,bestScore);

		for(int counter = 0; counter <= ActionPoints; counter++){
			foreach(NodeBehaviour node in Pathfinder.NodesWithinSteps(currentNode, 3)){
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

			if(currentBest == previousBest){
				return currentBest;
			}
		}
        return null;
	}

	Pawn GetBestTarget(List<Pawn> validTargets){

		Pawn bestTarget;
		double bestHitchance = 0;

		foreach (Pawn target in validTargets) {
			int effectiveRange = Weapon.range;
			if (owner.transform.position.y - target.transform.position.y > 0) {
				effectiveRange += (int)((owner.transform.position.y - target.transform.position.y) / 2f);
			}
			double hitChance = 1 - (1 - 0.5) * (Vector3.Distance (owner.transform.position, target.transform.position) - 1) / (effectiveRange - 1);

			if(bestHitchance < hitChance){
				bestHitchance = hitChance;
				bestTarget = target;
			}
		}

		return bestTarget;
	}
}

