using UnityEngine;  

public class Player : MonoBehaviour {

    /* Enums */
    public enum State
    {
        Idle,
        ChoosingFate,
            ChoosingSoldierFate,
                RecruitingBlowgunner,
                RecruitingClubman,
                RecruitingSpearman,
            ChoosingDivineFate,
                SacrificingForFertility,
                SacrificingForFun,
                SacrificingForPower,
        Throwing,
        NUM
    }

    /* Fields */
    public bool isHuman;
    public int playerIndex;
    public Choicemaker choicemaker;

    ProperInput input;
    State state;
    SacrificeThrow throwing;

    /* Lifetime Methods */
    void Start () {
        input = new ProperInput( playerIndex );
        state = State.Idle;
        throwing = GetComponentInChildren<SacrificeThrow>();
	}

    void Update() {

        // Update queue.


        // Update state.
        Debug.Log( state );
        switch ( state )
        {
            case State.Idle:
                {
                    // Check queue for people.
                    state = State.ChoosingFate;
                }
                break;
            case State.ChoosingFate:
                {
                    // Choose fate
                    if (!choicemaker.isUsing)
                        choicemaker.StartUsing();
                    
                    Choicemaker.Choice choice = choicemaker.Poll();
                    if (choice != Choicemaker.Choice.None)
                    {
                        switch (choice)
                        {
                            case Choicemaker.Choice.Blue:
                                state = State.ChoosingSoldierFate;
                                break;
                            case Choicemaker.Choice.Green:
                                state = State.Throwing;
                                break;
                            case Choicemaker.Choice.Red:
                                state = State.ChoosingDivineFate;
                                break;
                        }
                    }
                }
                break;
            case State.Throwing:
                {
                    // Start throwing person.
                    throwing.StartUsing();
                }
                break;

            /* Divine fates */
            case State.ChoosingDivineFate:
                {
                    // Update display of choice-thingy; show sacrifice paths.
                    if ( !choicemaker.isUsing )
                        choicemaker.StartUsing();

                    Choicemaker.Choice choice = choicemaker.Poll();
                    if ( choice != Choicemaker.Choice.None )
                    {
                        switch ( choice )
                        {
                            case Choicemaker.Choice.Blue:
                                state = State.SacrificingForPower;
                                break;
                            case Choicemaker.Choice.Green:
                                state = State.SacrificingForFun;
                                break;
                            case Choicemaker.Choice.Red:
                                state = State.SacrificingForFertility;
                                break;
                        }
                    }
                }
                break;
            case State.SacrificingForFertility:
                {
                    // Increase City Fertility
                    state = State.Idle;
                }
                break;
            case State.SacrificingForFun:
                {
                    // Maybe do something?
                    state = State.Idle;
                }
                break;
            case State.SacrificingForPower:
                {
                    // Increase Soldier Strength.
                    state = State.Idle;
                }
                break;

                /* Soldier fates */
            case State.ChoosingSoldierFate:
                {
                    // Update display of choice-thingy; show soldier paths.
                    if ( !choicemaker.isUsing )
                        choicemaker.StartUsing();

                    Choicemaker.Choice choice = choicemaker.Poll();
                    if ( choice != Choicemaker.Choice.None )
                    {
                        switch ( choice )
                        {
                            case Choicemaker.Choice.Blue:
                                state = State.RecruitingBlowgunner;
                                break;
                            case Choicemaker.Choice.Green:
                                state = State.RecruitingClubman;
                                break;
                            case Choicemaker.Choice.Red:
                                state = State.RecruitingSpearman;
                                break;
                        }
                    }
                }
                break;
            case State.RecruitingBlowgunner:
                {
                    // Recruit Blowgunner.
                    state = State.Idle;
                }
                break;
            case State.RecruitingClubman:
                {
                    // Recruit Clubman.
                    state = State.Idle;
                }
                break;
            case State.RecruitingSpearman:
                {
                    // Recruit Spearman.
                    state = State.Idle;
                }
                break;
        }
    }

    public void SetState(State state)
    {
        this.state = state;
    }
    
    /* Methods */
    public IButtonMap GetMap( ButtonAction action ) { return input.GetMap(action); }
    public float Getf( ButtonAction action ) { return input.Getf(action); }
    public bool Get( ButtonAction action ) { return input.Get(action); }
    public bool GetDown( ButtonAction action ) { return input.GetDown(action); }
    public bool GetUp( ButtonAction action ) { return input.GetUp(action); }

    public IAxisMap GetMap( AxisAction action ) { return input.GetMap(action); }
    public float Get( AxisAction action ) { return input.Get(action); }
}
