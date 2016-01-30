using UnityEngine;  

public class Player : MonoBehaviour {

    /* Fields */
    public bool isHuman;
    public int playerIndex;
    ProperInput input;

    /* Lifetime Methods */
    void Start () {
        input = new ProperInput( playerIndex );
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
