using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillMenu : MonoBehaviour 
{
	Skill[] currentSkills;
	CommandButton[] buttons;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void UpdateState(Selectable previous, Selectable current)
	{
		if (current == null || current.GetComponent<Pawn> () == null) {
			Hide();
			return;
		}

		currentSkills = current.GetComponent<Pawn> ().skillList.ToArray ();

		if (currentSkills.Length > buttons.Length) {
			throw new UnityException ("Too many skillz in this pawn, yo");
		} else {
			foreach (CommandButton b in buttons){
				b.Clear();
			}
			for (int counter = 0; counter< currentSkills.Length; counter++){
				buttons[counter].Set (currentSkills[counter]);
			}
		}

		Show ();
	}

	void Hide()
	{

	}

	void Show()
	{

	}
}
