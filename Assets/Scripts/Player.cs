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

    /* Properties */
    public bool IsLeftPlayer { get { return transform.position.x < 0; } }

    /* Fields */
    public bool isHuman;
	public int playerIndex;
	public Choicemaker choicemaker;
	public GameObject myPriest;

	ProperInput input;
	State state;
	SacrificeThrow throwing;
	private QueueSystem queueSystem;
	private ArmySpawner armySpawner;
	private DivineBuffs divineBuffs;

    public void GameOver()
    {
        Debug.Log("GameOver!!!");
    }

    /* Lifetime Methods */
    void Start () {
        input = new ProperInput( playerIndex );
        state = State.Idle;
        throwing = GetComponentInChildren<SacrificeThrow>();
        armySpawner = GetComponentInChildren<ArmySpawner>();
        queueSystem = GetComponentInChildren<QueueSystem>();
        divineBuffs = GetComponent<DivineBuffs>();
        if(queueSystem == null)
        {
            Debug.Log("No Queuesystem is added to pyramid");
        }
        if (armySpawner == null)
        {
            Debug.Log("Armyspawner is null");
        }
	}

	void Update() {

		// Update queue.
		myPriest.GetComponent<Animator>().SetBool("Attack", false);

		// Update state.
		////Debug.Log( state );
		switch ( state )
		{
			case State.Idle:
				{
					if (queueSystem.CanSacrificeVillager())
					{
						state = State.ChoosingFate;
					}
					// Check queue for people.
					
				}
				break;
			case State.ChoosingFate:
				{
					// Choose fate
					if (!choicemaker.isUsing)
						choicemaker.StartUsing(ChoiceType.Fate);
					
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
					if(throwing.isUsing == false)
					{
						queueSystem.SacrificeVillagerDestroy();
						// Start throwing person.
						throwing.StartUsing();
					}
				}
				break;

			/* Divine fates */
			case State.ChoosingDivineFate:
				{
					// Update display of choice-thingy; show sacrifice paths.
					if (!choicemaker.isUsing)
					{ 
						choicemaker.StartUsing(ChoiceType.Sacrifice);
					}
					Choicemaker.Choice choice = choicemaker.Poll();
					if ( choice != Choicemaker.Choice.None )
					{
						queueSystem.SacrificeVillagerDestroy();
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
						myPriest.GetComponent<Animator>().SetBool("Attack", true); 
					}
				}
				break;
			case State.SacrificingForFertility:
				{
					// Increase City Fertility
					divineBuffs.BuffFertility();
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
					divineBuffs.BuffPower();
					state = State.Idle;
				}
				break;

            /* Soldier fates */
            case State.ChoosingSoldierFate:
                {
                    // Update display of choice-thingy; show soldier paths.
                    if ( !choicemaker.isUsing )
                        choicemaker.StartUsing( ChoiceType.Soldier );

                    Choicemaker.Choice choice = choicemaker.Poll();
                    if ( choice != Choicemaker.Choice.None )
                    {
                        switch ( choice )
                        {
                            case Choicemaker.Choice.Blue:
                                state = State.RecruitingSpearman;
                                break;
                            case Choicemaker.Choice.Green:
                                state = State.RecruitingBlowgunner;
                                break;
                            case Choicemaker.Choice.Red:
                                state = State.RecruitingClubman;
                                break;
                        }
                        queueSystem.SacrificeVillagerDestroy();
                    }
                }
                break;
            case State.RecruitingBlowgunner:
				{
					// Recruit Blowgunner.
					reqruitBlowgunner();
					state = State.Idle;
				}
				break;
			case State.RecruitingClubman:
				{
					// Recruit Clubman.
					reqruitClubman();
					state = State.Idle;
				}
				break;
			case State.RecruitingSpearman:
				{
					// Recruit Spearman.
					reqruitSpearman();
					state = State.Idle;
				}
				break;
		}
	}

             
	public void SetState(State state)
	{
		this.state = state;
	}

	private void reqruitSpearman()
	{
		if(armySpawner != null)
		{
			armySpawner.SpawnUnit(Warrior.WarriorType.SPEAR);
			//Debug.Log("Spawning a spearman");
		}
		else
		{

		}
	}

	private void reqruitClubman()
	{
		if (armySpawner != null)
		{
			armySpawner.SpawnUnit(Warrior.WarriorType.CLUB);
			//Debug.Log("Spawning a clubman");
		}
		else
		{

		}
	}

	private void reqruitBlowgunner()
	{
		if (armySpawner != null)
		{
			//Debug.Log("Spawning a blowgunner");
			armySpawner.SpawnUnit(Warrior.WarriorType.BLOWGUN);
		}
		else
		{

		}
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
