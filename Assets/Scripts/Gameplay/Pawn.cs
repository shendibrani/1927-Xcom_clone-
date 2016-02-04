using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GridNavMeshWrapper))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Targetable))]
public class Pawn : MonoBehaviour
{
    public Player owner;

	[SerializeField] bool debug;

    //unique id for the pawn to determine who is doing what
    int pawnID;

    public Character character { get; private set; }
    //public Character character; //a reference to character, only used to initilise the pawn/update the character after level (could be stored in player for mission.) maybe use a passer

    public Weapon weapon
    {
        get;
        private set;
    }//either an instance of weapon or a reference (flywheel)

    //Weapon weapon;

    int actionPointsPerTurn = 3;
    public int MaxActionPointsPerTurn { get { return actionPointsPerTurn; } }

    int actionPoints = 3;
    int movement;
    public int accuracy
    {
        get;
        private set;
    }

    #region Properties
    public int ActionPoints
    {
        get { return actionPointsPerTurn + actionPointsMod - actionPointsSpent; }
        private set { actionPoints = value; }
    }

    public int Movement
    {
        get { return (ActionPoints * STEPSPERPOINT); }
        set { movement = value; }
    }

    public int Accuracy
    {
        get { return accuracy + accuracyMod; }
        private set { accuracy = value; }
    }

    public const int STEPSPERPOINT = 3;

    [HideInInspector]
    public int actionPointsSpent;

    public int actionPointsMod
    {
        get
        {
            int tmp = 0;
            if (currentNode != null && currentNode.tileEffect != null)
            {
                tmp += currentNode.tileEffect.actionPointMod;
            }
            foreach (PawnEffect e in EffectList)
            {
                tmp += e.actionPointMod;
            }
            return tmp;
        }
    }
    public int accuracyMod
    {
        get
        {
            int tmp = 0;
            if (currentNode != null && currentNode.tileEffect != null)
            {
                tmp += currentNode.tileEffect.accuracyMod;
            }
            foreach (PawnEffect e in EffectList)
            {
                tmp += e.accuracyMod;
            }
            return tmp;
        }
    }
    public double accuracyMulti
    {
        get
        {
            double tmp = 0;
            if (currentNode != null && currentNode.tileEffect != null)
            {
                tmp += currentNode.tileEffect.accuracyMulti;
            }
            foreach (PawnEffect e in EffectList)
            {
                tmp += e.accuracyMulti;
            }
            return tmp;
        }
    }
    public double hitMod
    {
        get
        {
            double tmp = 0;
            if (currentNode != null && currentNode.tileEffect != null)
            {
                tmp += currentNode.tileEffect.hitMod;
            }
            foreach (PawnEffect e in EffectList)
            {
                tmp += e.hitMod;
            }
            return tmp;
        }
    }
    public double hitMulti
    {
        get
        {
            double tmp = 0;
            if (currentNode != null && currentNode.tileEffect != null)
            {
                tmp += currentNode.tileEffect.hitMulti;
            }
            foreach (PawnEffect e in EffectList)
            {
                tmp += e.hitMulti;
            }
            return tmp;
        }
    }
    public double damageMod
    {
        get
        {
            double tmp = 0;
            if (currentNode != null && currentNode.tileEffect != null)
            {
                tmp += currentNode.tileEffect.damageMod;
            }
            foreach (PawnEffect e in EffectList)
            {
                tmp += e.damageMod;
            }
            return tmp;
        }
    }
    public double damageMulti
    {
        get
        {
            double tmp = 0;
            if (currentNode != null && currentNode.tileEffect != null)
            {
                tmp += currentNode.tileEffect.damageMulti;
            }
            foreach (PawnEffect e in EffectList)
            {
                tmp += e.damageMulti;
            }
            return tmp;
        }
    }
    public double critChanceMod
    {
        get
        {
            double tmp = 0;
            if (currentNode != null && currentNode.tileEffect != null)
            {
                tmp += currentNode.tileEffect.critChanceMod;
            }
            foreach (PawnEffect e in EffectList)
            {
                tmp += e.critChanceMod;
            }
            return tmp;
        }
    }

    List<PawnEffect> effectList;
    public List<PawnEffect> EffectList
    {
        get
        {
            if (effectList == null) effectList = new List<PawnEffect>();
            return effectList;
        }
    }

    public static int sightRange = 10;

    public List<Pawn> sightList
    {
        get { return LineOfSightManager.GetSightList(this); }
    }

    public List<Targetable> targetsList
    {
        get
        {
            List<Targetable> temp = new List<Targetable>();
            foreach (Pawn p in sightList)
            {
                temp.Add(p.GetComponent<Targetable>());
            }
            foreach (DestroyableProp d in FindObjectsOfType<DestroyableProp>())
            {
                if (LineOfSightManager.CheckSight(this, d))
                {
                    temp.Add(d.GetComponent<Targetable>());
                }
            }
            foreach (NodeBehaviour n in GameObject.FindObjectsOfType<NodeBehaviour>())
            {
                if (Vector3.Distance(this.transform.position, n.position) <= sightRange)
                {
                    temp.Add(n.GetComponent<Targetable>());
                }
            }
            return temp;
        }
    }

    #endregion

    public Command move;
    public Command attack;
    public List<Skill> skillList;

    [SerializeField]
    public List<Commands> tmpList;
    [SerializeField]
    public Weapons weap;

    void Start()
    {
        skillList = new List<Skill>();
        for (int i = 0; i < tmpList.Count; i++)
        {
            skillList.Add(new Skill("Skill", "Description", tmpList[i]));
        }
        if (currentNode == null)
        {
            Debug.Log("Pawn" + gameObject + " is not attached to a node");
        }
       
    }

    public void Initalise(Character pCharacter)
    {
       // Debug.Log("Pawn " + gameObject + "initalised");
        character = pCharacter;
        weapon = WeaponData.instance.universalWeaponList[weap];
        //Weapon = pCharacter.assignedWeapon;
        accuracy = pCharacter.accuracy;
        actionPoints = pCharacter.actionPoints;
        actionPointsPerTurn = pCharacter.actionPoints;

		Debug.Log("Pawn " + gameObject + " name:" + character.name);

		GetComponent<CharacterVisualsSpawn> ().Initialize (weapon.weaponEnum);

        //skillList = new List<Skill>(pCharacter.skillList);
    }

    public virtual void Turn()
    {
        actionPointsSpent = 0;

        if (currentNode.tileEffect != null)
        {
            currentNode.tileEffect.Turn();
        }
        for (int i = EffectList.Count - 1; i >= 0; i--)
        {
            EffectList[i].Turn();
        }
    }

    public NodeBehaviour currentNode
    {
        get { return GetComponent<GridNavMeshWrapper>().currentNode; }
    }

    public List<NodeBehaviour> reachableNodes
    {
        get { return Pathfinder.NodesWithinSteps(currentNode, Movement); }
    }

    public override string ToString()
    {
        return name;
    }

    public CoverState GetCoverState(Pawn other)
    {
		if (debug) Debug.Log ("Node: " + currentNode + " Pawn: " + other);
		return GetCoverAtNode(currentNode, other);
    }

	public static CoverState GetCoverAtNode(NodeBehaviour node, Pawn other)
	{
		Vector3 direction = other.currentNode.position - node.position;
		direction.Normalize();

		RaycastHit hit;

		if (Physics.Raycast(node.offsetPosition + (Vector3.up * 1.5f), direction, out hit, 1f))
		{
			if (hit.collider.gameObject != other.gameObject) return CoverState.Full;
		}
		if (Physics.Raycast(node.offsetPosition + (Vector3.up * 0.5f), direction, out hit, 1f))
		{
			if (hit.collider.gameObject != other.gameObject) return CoverState.Half;
		}
		
		return CoverState.None;
	}

    #region Callbacks

    #endregion

	void OnDrawGizmos()
	{
		if (weapon != null) {
			UnityEditor.Handles.color = Color.green;
			UnityEditor.Handles.DrawWireDisc (transform.position, transform.up, weapon.range);
		}
	}
}

public enum CoverState
{
    None = 0, Half = 1, Full = 2
}

[System.Serializable]
public class PawnEvent : UnityEvent<Pawn>{}
