using UnityEngine;

public class SelectionManager
{
	public static Selectable selected {
		get {
			return instance._selected;
		}

		set {
			if(selected != value){

				if (instance._selected != null){
					instance._selected.OnDeselect();
				}

				if(instance.SelectionChange != null){
					instance.SelectionChange.Invoke(instance._selected, value);
				}

				instance._selected = value;

				if (instance._selected != null){
					instance._selected.OnSelect();
				}
			}
		}
	}

	private Selectable _selected;

	public static Selectable hovered {
		get {
			return instance._hovered;
		}
		
		set {
			instance._hovered = value;
			//Debug.Log("Hovering");
		}
	}
	
	private Selectable _hovered;

	public static SelectionManager instance {
		get {
			if(_instance == null){
				_instance = new SelectionManager();
			}
			return _instance;
		}
	}

	public static Targetable target {
		get{
			if (command != null) {
				return instance._command.target;
			}
			return null;
		}
		set {
			if(command != null){

				command.target = value;
				
				HighlightingManager.instance.RefreshHighlighting();
			}
		}
	}

	private Command _command;
	
	public static Command command {
		get{ return instance._command; }
		
		set {
			if(value == null){
				instance._command = null;
			} else if(selected.GetComponent<Pawn>() != null && selected.GetComponent<Pawn>().owner.isPlayer){
				instance._command = value;
				HighlightingManager.instance.RefreshHighlighting();
			}
		}
	}

	public static void ReturnToSelectionMode()
	{
		command = null;
	}

	public static void Clear()
	{
		command = null;
		selected = null;
	}

	public static bool Execute()
	{
		if (command == null) return false;
		bool result = command.Execute ();
		command = null;
		return result;
	}

	public bool isInTargetingMode 
	{
		get 
		{
			return command != null;
		}
	}

	private static SelectionManager _instance;

	public delegate void SelectionDelegate(Selectable previous, Selectable current);

	public event SelectionDelegate SelectionChange;

	private SelectionManager ()
	{
		SelectionChange += HighlightingManager.instance.ComputeHighlightChange;
	}
}