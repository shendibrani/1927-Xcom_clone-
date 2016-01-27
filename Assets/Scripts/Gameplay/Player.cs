using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField]
    bool debug;

    [SerializeField]
    List<Pawn> pawns;

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
            if (characterList != null)
            {
                if (i <= characterList.Count - 1 & characterList[i] != null)
                { pawns[i].Initalise(characterList[i]); }
            }
            else
            {
                pawns[i].Initalise(new Character());
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

    public void Turn()
    {
        foreach (Pawn p in pawns)
        {
            p.Turn();
        }
    }
}

