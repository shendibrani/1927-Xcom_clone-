using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class PawnAbilityUI : MenuCanvas {
    //recieves a message from selection manager detailing which pawn is selected
    //message recieved populates this, calls the menumanager change
    Button[] buttonList;

    void Start()
    {
        buttonList = GetComponentsInChildren<Button>();
    }

    public override void setMenu()
    {
        PopulateButtons();
        base.setMenu();
    }

    void PopulateButtons()
    {
        List<Skill> tmpList = SelectionManager.selected.GetComponent<Pawn>().skillList;
        for (int i = 0; i < tmpList.Count; i++)
        {
            if (tmpList.Count > buttonList.Length)
            {
                Debug.LogError("Skill List is longer than list of buttons");
                break;
            }
            buttonList[i].GetComponent<CommandButton>().Set(tmpList[i]);
        }
    }

}
