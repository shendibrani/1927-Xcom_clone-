using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(UIDrawerBehaviour))]
public class DialogueDisplay : MonoBehaviour 
{
	#region Singleton

	public static DialogueDisplay instance {
		get {
			if(_instance == null){
				_instance = GameObject.FindObjectOfType<DialogueDisplay>();
				if(_instance == null){
					Debug.LogError("There is not DialogueDisplay instance in the scene.");
				}
			}
			return _instance;
		}
	}
	
	private static DialogueDisplay _instance;

	#endregion

	DialogueLine currentLine;
	Dialogue currentDialogue;

	[SerializeField] bool showCharacterByCharacter;
	[SerializeField] float letterDelayInSeconds;
	float timer;

	[SerializeField] Text dialogueField;
	[SerializeField] Image portrait;

	[SerializeField] Vector2 centralPortraitPosition;
	[SerializeField] float portraitOffsetFromCenter;

	[SerializeField] Sprite[] portraits;



	void FixedUpdate () 
	{
		if (currentDialogue != null) {

			if (showCharacterByCharacter && currentLine.line.Length > 0) {
				dialogueField.text += currentLine.line [0];
				currentLine.line = currentLine.line.Remove (0, 1);
			}

			if (currentLine.line == string.Empty && (Input.GetKeyUp (KeyCode.Return) || Input.GetMouseButtonUp (0))) {
				SetLine( currentDialogue.GetNextLine());
			} else if (Input.GetKeyUp (KeyCode.Return) || Input.GetMouseButtonUp (0)) {
				FinishCurrentLine ();
			}


		}
	}

	public void SetLine(DialogueLine line)
	{
		if (line == null) {
			GetComponent<UIDrawerBehaviour>().Hide();
			enabled=false;
			currentDialogue.DialogueEnd.Invoke();
			return;
		}

		dialogueField.text = string.Empty;

		portrait.sprite = portraits [line.characterID];
		currentLine = line;
		switch (line.portraitPosition){
		case PortraitPositions.Left:
			portrait.rectTransform.anchoredPosition = centralPortraitPosition + Vector2.left*portraitOffsetFromCenter;
			break;
		case PortraitPositions.Right:
			portrait.rectTransform.anchoredPosition = centralPortraitPosition + Vector2.right*portraitOffsetFromCenter;
			break;
		case PortraitPositions.Center:
			portrait.rectTransform.anchoredPosition = centralPortraitPosition;
			break;
		}

		if (!showCharacterByCharacter) {
			dialogueField.text = line.line;
		}

	}

	public void FinishCurrentLine()
	{
		dialogueField.text += currentLine.line;
		currentLine.line = string.Empty;
	}

	public void StartDialogue(Dialogue d)
	{
		enabled = true;
		currentDialogue = d;
		SetLine( currentDialogue.GetNextLine());
		GetComponent<UIDrawerBehaviour>().Show();
	}
	
}

[System.Serializable]
public class DialogueLine 
{
	public int characterID;
	public string line;
	public PortraitPositions portraitPosition;
}

public enum PortraitPositions{
	Left, Center, Right
}

[System.Serializable]
public class Dialogue 
{
	[SerializeField] DialogueLine[] lines;

	public UnityEvent DialogueEnd;

	int currentLine = 0;

	public DialogueLine GetNextLine()
	{
		if(currentLine >= lines.Length){
			return null;
		}

		DialogueLine line = lines[currentLine];
		currentLine++;
		return line;
	}
}