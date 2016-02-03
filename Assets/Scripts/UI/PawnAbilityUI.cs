using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class PawnAbilityUI : MenuCanvas {
    //recieves a message from selection manager detailing which pawn is selected
    //message recieved populates this, calls the menumanager change
    CommandButton[] buttonList;

    [SerializeField]
    CharacterTabUI characterDisplay;

    void Start()
    {
        buttonList = GetComponentsInChildren<CommandButton>();
        SelectionManager.instance.SelectionChange += OnSelectionChange;
    }

    public override void setMenu()
    {
        base.setMenu();
    }

    void OnSelectionChange(Selectable previous, Selectable current)
    {
        if (current != null && current.GetComponent<Pawn>() != null && current.GetComponent<Pawn>().owner == TurnManager.instance.turnPlayer)
        {
            List<Skill> tmpList = current.GetComponent<Pawn>().skillList;
            if (characterDisplay != null) characterDisplay.PopulateUI(current.GetComponent<Pawn>().character);
            foreach (CommandButton b in buttonList)
            {
                b.Clear();
            }
            for (int i = 0; i < tmpList.Count; i++)
            {
                //Debug.Log(tmpList[i].abilityCommand.ToString());
                if (tmpList.Count > buttonList.Length)
                {
                    Debug.LogError("Skill List is longer than list of buttons");
                    break;
                }
                buttonList[i].Set(tmpList[i]);
                if (tmpList[i].abilityCommand != Commands.Attack || tmpList[i].abilityCommand != Commands.Move)
                {
                    if (buttonList[i].gameObject.GetComponentInChildren<tipSkillTool>() != null) {
                        buttonList[i].gameObject.GetComponentInChildren<tipSkillTool>().Populate(tmpList[i]);
                    }
                }
            }
        }    
    }
}
