using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	[SerializeField] SpawnParameters[] spawnList;
	[SerializeField] Transform position;
	[SerializeField] Player player;

	// Use this for initialization
	void Start () {
		foreach (SpawnParameters sp in spawnList) {
			if(sp.enemyPrefab.GetComponent<Pawn>() == null){
				Debug.LogError("The prefab does not have a pawn component.");
			}
		}
	}
	
	public void Spawn ()
	{
		if(position == null){
			position = transform;
		}
		foreach (SpawnParameters sp in spawnList){
			NodeBehaviour startingNode;

			if(sp.startingNode != null){
				startingNode = sp.startingNode;
			} else {
				startingNode = NodeBehaviour.GetClosestFreeSpawnNode(position.position);
			}

			GameObject enemyPawn = GameObject.Instantiate(sp.enemyPrefab, startingNode.offsetPosition, Quaternion.identity) as GameObject;
			startingNode.currentObject = enemyPawn.GetComponent<Targetable>();

			player.AddPawn(enemyPawn.GetComponent<Pawn>());
		}
	}

}
[System.Serializable]
public struct SpawnParameters 
{
	public GameObject enemyPrefab;
	public NodeBehaviour startingNode;
}