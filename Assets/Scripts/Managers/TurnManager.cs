using System;
using System.Collections.Generic;

public class TurnManager
{
	public static TurnManager instance {
		get {
			if(_instance == null){
				_instance = new TurnManager();
			}
			return _instance;
		}
	}
	
	private static TurnManager _instance;

	public int turn { get; private set; }

	private List<Player> turnOrder;

	public Player turnPlayer{
		get{
			return turnOrder[(turn%turnOrder.Count + turnOrder.Count)%turnOrder.Count];
		}
	}

	public delegate void VoidVoidDelegate();

	public event VoidVoidDelegate TurnEnd, TurnStart;

	private TurnManager()
	{
		turnOrder = new List<Player> ();
	}

	public void NextTurn(){
		TurnEnd.Invoke ();
		turn++;
		TurnStart.Invoke ();
	}

	public void AddPlayer(Player p){
		if (!turnOrder.Contains (p)) {
			turnOrder.Add (p);
		}
	}

	public void RemovePlayer(Player p){
		turnOrder.Remove (p);
	}
}
