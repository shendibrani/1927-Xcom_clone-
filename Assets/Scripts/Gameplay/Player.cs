using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField]
    bool debug;

    [SerializeField]
    protected List<Pawn> pawns;

    [SerializeField]
    bool _isPlayer;

	public bool isPlayer { get { return _isPlayer;}}

    List<Character> characterList { get { return CharacterStaticStorage.instance.fullCharacterList; } }

    public List<Pawn> Pawns
    {
        get { return pawns; }
    }

    Command cachedCommand = null;

    public Sprite playerSymbol;

    public void OnLevelLoaded()
    {
        Initilisation();
        TurnManager.instance.SetFree();
    }

    public void Start()
    {
        Initilisation();
        TurnManager.instance.SetFree();
    }
    // Use this for initialization of level on loading
    public void Initilisation()
    {
        for (int i = 0; i < pawns.Count; i++)
        {
            pawns[i].owner = this;
			if ( _isPlayer && characterList != null && characterList.Count > 0)
            {
                if (i < characterList.Count && characterList[i] != null){
					if(debug) Debug.Log("Init from character list");
					pawns[i].Initalise(characterList[i]);	
				}
            }
            else
            {
                if (_isPlayer){
					if(debug) Debug.Log("Init character from factory");
                    pawns[i].Initalise(Factory.GetCharacter());
				}
                else{
					if(debug) Debug.Log("Init enemy from factory");
                    pawns[i].Initalise(Factory.GetEnemy());
				}
            }
        }
    }

    public bool Owns(Pawn p)
    {
        return pawns.Contains(p);
    }

	public void AddPawn(Pawn p)
	{
		if(p.owner == null){
			p.owner = this;
			pawns.Add(p);
		}
	}

    bool Move(Pawn p, NodeBehaviour target)
    {
        if (pawns.Contains(p))
        {
            p.move = new MoveCommand(p);
            if (debug) Debug.Log(p.move);
            bool result = p.move.Execute();
            if (result)
            {
                if (debug) Debug.Log("Pathfinder Successful");
            }
            else if (debug) { Debug.Log("Pathfinder Failed"); }
            return result;
        }
        return false;
    }

    bool Attack(Pawn p, Pawn target)
    {
        if (pawns.Contains(p))
        {
            p.attack = new AttackCommand(p);
            bool result = p.attack.Execute();
            if (result)
            {
                if (debug) Debug.Log("Attack Successful");
            }
            else if (debug) { Debug.Log("Attack Failed"); }
            return result;
        }

        return false;
    }

    public virtual void Turn()
    {
		Debug.Log (name + " is executing its turn.");
        foreach (Pawn p in pawns)
        {
			Debug.Log (p.name);
            p.Turn();
        }
    }
}

