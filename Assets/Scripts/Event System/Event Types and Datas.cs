using UnityEngine;

public enum EventType {
	Damage,
	Movement
}

public abstract class EventData {}

public class DamageEventData : EventData 
{
	public GameObject sender { get; private set; }
	public GameObject target { get; private set; }
	public int damage { get; private set; }

	public DamageEventData (GameObject pSender, GameObject pTarget, int pDamage)
	{
		sender = pSender;
		target = pTarget;
		damage = pDamage;
	}
}

public class MoveEvent : EventData 
{
	public GameObject sender { get; private set; }
	public NodeBehaviour startPoint { get; private set; }
	public NodeBehaviour endPoint { get; private set; }
	
	public MoveEvent (GameObject pSender, NodeBehaviour pStartPoint, NodeBehaviour pEndPoint)
	{
		sender = pSender;
		startPoint = pStartPoint;
		endPoint = pEndPoint;
	}
}