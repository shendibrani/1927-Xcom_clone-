using UnityEngine;
using System.Collections;

public class Targetable : MonoBehaviour
{
	public delegate void TargetingEvent(Pawn targeter);

	public TargetingEvent IsValidTarget;
	public TargetingEvent IsTargeted;
}

