using UnityEngine;

public class SelectionManager
{
	public static Selectable selected {
		get {
			return instance._selected;
		}

		set {
			if (instance._selected != null){
				instance._selected.OnDeselect();
			}
			if(instance.SelectionChange != null){
				instance.SelectionChange.Invoke(instance._selected, value);
			}
			instance._selected = value;
			instance._selected.OnSelect();

			//Debug.Log("Selection");
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

	private static SelectionManager _instance;

	public delegate void SelectionDelegate(Selectable previous, Selectable current);

	public event SelectionDelegate SelectionChange;

	private SelectionManager ()
	{
		SelectionChange += HighlightingManager.instance.ComputeHighlightChange;
	}
}