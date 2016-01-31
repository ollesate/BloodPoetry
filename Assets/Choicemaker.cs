using UnityEngine;

public class Choicemaker : MonoBehaviour {

    /* Enums */
    public enum Choice
    {
        None,
        Blue,
        Green,
        Red
    }

    /* Fields */
    public bool isUsing;

    Player player;

    ButtonChoice[] choiceButtons;

    /* Methods */
	void Start () {
        player = gameObject.GetComponentInParent<Player>();
        player.choicemaker = this;
        isUsing = false;
        choiceButtons = GetComponentsInChildren<ButtonChoice>();
	}

    public void StartUsing(ChoiceType type)
    {
        foreach (var btn in choiceButtons)
        {
            btn.SelectSprite( type );
        }
        isUsing = true;
        GetComponent<Fading>().FadeIn();
    }

    public void StopUsing()
    {
        isUsing = false;
        GetComponent<Fading>().FadeOut();
    }

    public Choice Poll() {
        if ( player.GetDown( ButtonAction.Blue ) ) {
            StopUsing();
            return Choice.Blue;
        }
        else if ( player.GetDown( ButtonAction.Green ) ) {
            StopUsing();
            return Choice.Green;
        }
        else if ( player.GetDown( ButtonAction.Red ) ) {
            StopUsing();
            return Choice.Red;
        }
        return Choice.None;
	}
}
