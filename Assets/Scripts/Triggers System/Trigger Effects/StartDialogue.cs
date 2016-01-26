using UnityEngine;
using System.Collections;

public class StartDialogue : MonoBehaviour
{
	[SerializeField] Dialogue dialogue;

	public void InitDialogue()
	{
		DialogueDisplay.instance.StartDialogue(dialogue);
	}
}

