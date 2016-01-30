using UnityEngine;

public enum ButtonAction
{
    Blue,
    Green,
    Red,
    Pause,
    Click,
    ClickBack,
    NUM
}

public enum AxisAction
{
    AimX,
    AimY,
    CamX,
    NUM
}

public enum JoystickButton
{
    A,
    B,
    X,
    Y,
    LB,
    RB,
    Back,
    Start,
    LS,
    RS,
    NUM
}

public enum JoystickAxis
{
    LX,
    LY,
    RX,
    RY,
}

public interface IButtonMap
{
    float Getf();
    bool Get();
    bool GetDown();
    bool GetUp();
}

public interface IAxisMap
{
    float Get();
}

public struct KeyboardButtonMap : IButtonMap
{
    KeyCode code;

    public KeyboardButtonMap(KeyCode code)
    {
        this.code = code;
    }

    public float Getf()
    {
        return ( Get() ? 1 : 0 );
    }

    public bool Get()
    {
        return Input.GetKey( code );
    }

    public bool GetDown()
    {
        return Input.GetKeyDown( code );
    }

    public bool GetUp()
    {
        return Input.GetKeyUp( code );
    }
}

public struct JoystickButtonMap : IButtonMap
{
    JoystickButton code;
    int playerIndex_add1;

    public JoystickButtonMap( JoystickButton code, int playerIndex )
    {
        this.code = code;
        this.playerIndex_add1 = playerIndex + 1;
    }

    public float Getf()
    {
        return ( Get() ? 1 : 0 );
    }

    public bool Get()
    {
        return Input.GetKey( "joystick " + playerIndex_add1.ToString() + " button " + ((int)code).ToString() );
    }

    public bool GetDown()
    {
        return Input.GetKeyDown( "joystick " + playerIndex_add1.ToString() + " button " + ( (int)code ).ToString() );
    }

    public bool GetUp()
    {
        return Input.GetKeyUp( "joystick " + playerIndex_add1.ToString() + " button " + ( (int)code ).ToString() );
    }
}

public struct AlternativeButtonMap : IButtonMap
{
    IButtonMap[] maps;

    public AlternativeButtonMap( params IButtonMap[] maps )
    {
        this.maps = maps;
    }

    public float Getf()
    {
        float s = 0;
        foreach (var map in maps ) {
            s += map.Getf();
        }
        return s;
    }

    public bool Get()
    {
        foreach ( var map in maps ) {
            if ( map.Get() )
                return true;
        }
        return false;
    }

    public bool GetDown()
    {
        foreach ( var map in maps ) {
            if ( map.GetDown() )
                return true;
        }
        return false;
    }

    public bool GetUp()
    {
        foreach ( var map in maps ) {
            if ( map.GetUp() )
                return true;
        }
        return false;
    }
}

public struct DualButtonAxisMap : IAxisMap
{
    IButtonMap up;
    IButtonMap down;

    public DualButtonAxisMap( IButtonMap up, IButtonMap down)
    {
        this.up = up;
        this.down = down;
    }

    public float Get()
    {
        return up.Getf() - down.Getf();
    }
}

public struct JoystickAxisMap : IAxisMap
{
    const float DEAD_ZONE = 0.1f;

    JoystickAxis axis;
    int playerIndex;

    public JoystickAxisMap(JoystickAxis axis, int playerIndex)
    {
        this.axis = axis;
        this.playerIndex = playerIndex;
    }

    public float Get()
    {
        float x = Input.GetAxisRaw("p" + playerIndex.ToString() + "_" + axis.ToString());
        if (Mathf.Abs( x ) < DEAD_ZONE)
            return 0;
        return x;
    }
}

public struct AlternativeAxisMap : IAxisMap
{
    IAxisMap[] maps;

    public AlternativeAxisMap( params IAxisMap[] maps )
    {
        this.maps = maps;
    }

    public float Get()
    {
        float s = 0;
        foreach ( var map in maps )
        {
            s += map.Get();
        }
        return s;
    }
}

public class ProperInput
{
    /* Fields */
    int playerIndex;
    IButtonMap[] btns;
    IAxisMap[] axes;

    /* Constructors */
    public ProperInput(int playerIndex)
    {
        btns = new IButtonMap[]
        {
            new JoystickButtonMap(JoystickButton.X, playerIndex), // Blue,
            new JoystickButtonMap(JoystickButton.A, playerIndex), // Green,
            new JoystickButtonMap(JoystickButton.B, playerIndex), // Red,
            new JoystickButtonMap(JoystickButton.Start, playerIndex), // Pause
            new AlternativeButtonMap(new JoystickButtonMap(JoystickButton.A, playerIndex), new KeyboardButtonMap(KeyCode.Return)), //MenuClick
            new AlternativeButtonMap(new JoystickButtonMap(JoystickButton.B, playerIndex), new KeyboardButtonMap(KeyCode.Backspace)) //MenuClickBack
        };

        axes = new IAxisMap[]
        {
            new JoystickAxisMap(JoystickAxis.LX, playerIndex), // AimX,
            new JoystickAxisMap(JoystickAxis.LY, playerIndex), // AimY,
            new JoystickAxisMap(JoystickAxis.RX, playerIndex) // CamX
        };
    }

    /* Methods */
    public IButtonMap GetMap( ButtonAction action ) { return btns[(int)action]; }
    public float Getf( ButtonAction action ) { return btns[(int)action].Getf(); }
    public bool Get( ButtonAction action ) { return btns[(int)action].Get(); }
    public bool GetDown( ButtonAction action ) { return btns[(int)action].GetDown(); }
    public bool GetUp( ButtonAction action ) { return btns[(int)action].GetUp(); }

    public IAxisMap GetMap( AxisAction action ) { return axes[(int)action]; }
    public float Get( AxisAction action ) { return axes[(int)action].Get(); }
}
