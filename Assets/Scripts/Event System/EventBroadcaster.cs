using UnityEngine;
using System.Collections;

public class EventBroadcaster {

	public static EventBroadcaster instance {
		get {
			if(_instance == null){
				_instance = new EventBroadcaster();
			}
			return _instance;
		}
	}
	
	private static EventBroadcaster _instance;

	private EventBroadcaster(){}

	public delegate void BroadcastedEvent (EventType type, EventData data);

	public event BroadcastedEvent BroadcastEvent;

	public static void Broacast(EventType type, EventData data){
		instance.BroadcastEvent.Invoke (type, data);
	}
}

