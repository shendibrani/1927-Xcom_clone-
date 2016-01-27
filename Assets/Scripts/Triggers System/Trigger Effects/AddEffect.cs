using UnityEngine;
using System.Collections;

public class AddEffect : MonoBehaviour
{
	[SerializeField] Effects effect;
	[SerializeField] Pawn target;

	public void Execute (Pawn p)
	{
		if(target == null){
			target = p;
		}
		target.EffectList.Add(Factory.GetEffect(effect, target));
	}
}

