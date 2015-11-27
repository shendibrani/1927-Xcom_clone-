using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField]
    bool debug;

    [SerializeField]
    List<Pawn> pawns;

    List<Character> characterList { get { return CharacterStaticStorage.instance.transferCharacterList; } }

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
                if (characterList[i] != null)
                { pawns[i].Initalise(characterList[i]); }
            }
            else
            {
                pawns[i].Initalise(new Character());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!TurnManager.instance.busy && TurnManager.instance.turnPlayer == this)
        {
			if(debug) Debug.Log(SelectionManager.selected);
            if (Input.GetMouseButtonUp(1) && SelectionManager.selected != null)
            {
				if(debug) Debug.Log("Right Click");
                if (SelectionManager.selected.GetComponent<Pawn>() != null)
                {
                    //default movement atm
                    cachedCommand = Factory.GetCommand(Commands.Move, SelectionManager.selected.GetComponent<Pawn>());
                    //highlight all options for movement (validtargets?);=
                   
                    if (SelectionManager.hovered.GetComponent<Targetable>() != null)
                    {
                        if (cachedCommand.IsValidTarget(SelectionManager.hovered.GetComponent<Targetable>()))
                        {
                            TurnManager.instance.SetBusy();
                            cachedCommand.Execute();
                        }
                    }
                }
            }
        }
    }

    public bool Owns(Pawn p)
    {
        return pawns.Contains(p);
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

