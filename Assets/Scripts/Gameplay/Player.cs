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

    // Use this for initialization of level on loading
    public void Initilisation()
    {
        for (int i = 0; i < pawns.Count; i++)
        {
            pawns[i].owner = this;
            if (characterList[i] != null)
            { pawns[i].Initalise(characterList[i]); }
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
                    if (SelectionManager.hovered.GetComponent<NodeBehaviour>() != null)
                    {
                        if (Move(SelectionManager.selected.GetComponent<Pawn>(), SelectionManager.hovered.GetComponent<NodeBehaviour>()))
                        {
                            TurnManager.instance.SetBusy();
                        }
                    }
                    else if (SelectionManager.hovered.GetComponent<Pawn>() != null)
                    {
                        if (Attack(SelectionManager.selected.GetComponent<Pawn>(), SelectionManager.hovered.GetComponent<Pawn>()))
                        {
                            //TurnManager.instance.SetBusy();
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
            p.move = new MoveCommand(p, target);
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
            p.attack = new AttackCommand(p, target);
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

