using UnityEngine;
using System.Collections;

public class OptionsScript : MonoBehaviour
{

    enum GoreSettings
    {
        Off,
        Minimal,
        High,
        EXTREME,
        MAX
    }

    GoreSettings currGoreSet = GoreSettings.EXTREME;

    public UnityEngine.UI.Text goreSetting;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            currGoreSet += 1;
            if (currGoreSet == GoreSettings.MAX)
            {
                currGoreSet = GoreSettings.Off;
            }
        }
        goreSetting.text = currGoreSet.ToString();
        
    }
}
