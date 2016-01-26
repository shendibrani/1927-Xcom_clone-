using UnityEngine;
using System.Collections;

public class AddEffect : MonoBehaviour
{
	Effects effect;
	Pawn target;

	public void Execute()
	{
		target.EffectList.Add(Factory.GetEffect(effect, target));
	}
}

