using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueDisplay : MonoBehaviour 
{
	#region Singleton

	public static DialogueDisplay instance {
		get {
			if(_instance == null){
				_instance = GameObject.FindObjectOfType<DialogueDisplay>();
				if(_instance == null){
					Debug.LogError("There is not Line of Sight manager instance in the scene.");
				}
			}
			return _instance;
		}
	}
	
	private static DialogueDisplay _instance;

	#endregion

	[SerializeField] DialogueLine currentLine;

	[SerializeField] bool showCharacterByCharacter;
	[SerializeField] float letterDelayInSeconds;
	float timer;

	[SerializeField] Text dialogueField;
	[SerializeField] Image portrait;

	[SerializeField] Vector2 centralPortraitPosition;
	[SerializeField] float portraitOffsetFromCenter;

	[SerializeField] Sprite[] portraits;



	void Update () 
	{
		if (showCharacterByCharacter && currentLine.line.Length > 0) 
		{
			dialogueField.text += currentLine.line[0];
			currentLine.line = currentLine.line.Remove(0,1);
		}

		if (Input.GetKeyUp(KeyCode.Return) || Input.GetMouseButtonUp(0))
		{
			FinishCurrentLine();
		}
	}

	public void SetLine(DialogueLine line)
	{
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
}

[System.Serializable]
public struct DialogueLine 
{
	public int characterID;
	public string line;
	public PortraitPositions portraitPosition;
}

public enum PortraitPositions{
	Left, Center, Right
}

public class Dialogue 
{
	DialogueLine[] lines;

	int currentLine;

	public DialogueLine GetNextLine()
	{
		DialogueLine line = lines[currentLine];
		currentLine++;
		return line;
	}
}