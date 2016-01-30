using UnityEngine;
using System.Collections;

public class OptionsScript : MonoBehaviour
{
    public GameObject[] MenuItems;
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
        if (Input.GetButton("Action1_1"))
        {
            currGoreSet += 1;
            if (currGoreSet == GoreSettings.MAX)
            {
                currGoreSet = GoreSettings.Off;
            }
        }
        goreSetting.text = currGoreSet.ToString();
        if (Input.GetButton("Action2_1"))
        {
            MenuItems[0].SetActive(true);
            MenuItems[1].SetActive(false);
        }
    }
}
