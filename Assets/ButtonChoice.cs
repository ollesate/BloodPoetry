using UnityEngine;
using System.Collections;

public enum ChoiceType
{
    Fate,
    Sacrifice,
    Soldier
}

public enum ChoiceColor
{
    Red,
    Green,
    Blue
}

public class ButtonChoice : MonoBehaviour {

    public Sprite fateSprite;
    public Sprite sacrificeSprite;
    public Sprite soldierSprite;

	void Start () {
        SelectSprite( ChoiceType.Fate );
	}
	
	void Update () {
	
	}

    public void SelectSprite(ChoiceType type)
    {
        switch ( type )
        {
            case ChoiceType.Fate:
                GetComponent<SpriteRenderer>().sprite = fateSprite;
                break;
            case ChoiceType.Sacrifice:
                GetComponent<SpriteRenderer>().sprite = sacrificeSprite;
                break;
            case ChoiceType.Soldier:
                GetComponent<SpriteRenderer>().sprite = soldierSprite;
                break;
        }
    }
}
