using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TextDisplay : MonoBehaviour {

    [SerializeField]
    Pawn pawn;
    [SerializeField]
    Text nameText;
    [SerializeField]
    Text APText;
    [SerializeField]
    Text levelText;
    [SerializeField]
    Text healthText;
    [SerializeField]
    Text weaponText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        APText.text = pawn.ActionPoints + "/" + pawn.MaxActionPointsPerTurn;
        nameText.text = pawn.character.name;
        levelText.text = pawn.character.level.ToString();
        healthText.text = pawn.GetComponent<Health>().health.ToString();
        weaponText.text = pawn.weapon.name;
	}
}
