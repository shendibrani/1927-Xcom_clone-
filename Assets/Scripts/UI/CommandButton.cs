using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class CommandButton : MonoBehaviour
{
	public Skill skill { get; private set; }
	private Button button;

	// Use this for initialization
	void Start ()
	{
		button = GetComponent<Button> ();
		button.onClick.AddListener(CacheCommand);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void CacheCommand()
	{
		if (SelectionManager.selected == null || skill == null) return;

		SelectionManager.command = Factory.GetCommand(skill.abilityCommand, SelectionManager.selected.GetComponent<Pawn>());
	}

	public void Clear()
	{
		skill = null;
		//button.image = skill.voidIcon;
	}

	public void Set(Skill s)
	{
		skill = s;
        Debug.Log(skill.name);
		//button.image = skill.icon;
	}
}

