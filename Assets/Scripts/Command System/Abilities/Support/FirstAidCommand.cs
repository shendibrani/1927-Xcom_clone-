using UnityEngine;
using System.Collections;

public class FirstAidCommand : Command {

    int actionCost = 2;
    int firstAidRange = 6;

    public FirstAidCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "FirstAidCommand";
    }

    public override bool Execute()
    {
        if (!CheckTarget() || !CheckCost(actionCost))
            return false;

        Pawn tPawn = target.GetComponent<Pawn>();

        owner.GetComponent<Health>().Heal(owner.GetComponent<Health>().maxHealth / 5);

        Debug.Log(owner + " Executes " + name);

        return true;
    }

    public override bool IsValidTarget(Targetable t)
    {
        Pawn p = t.GetComponent<Pawn>();
        return (p != null) && (p.owner == owner.owner) && (Vector3.Distance(owner.transform.position, p.transform.position) < firstAidRange);
    }
}
