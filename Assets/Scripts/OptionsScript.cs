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

    ProperInput pIn;

    void Start()
    {
        pIn = new ProperInput(0);
    }

    void Update()
    {
        if (pIn.GetDown(ButtonAction.Green))
        {
            currGoreSet += 1;
            if (currGoreSet == GoreSettings.MAX)
            {
                currGoreSet = GoreSettings.Off;
            }
        }
        goreSetting.text = currGoreSet.ToString();
        if (pIn.GetDown(ButtonAction.Red))
        {
            MenuItems[0].SetActive(true);
            MenuItems[1].SetActive(false);
        }
    }
}
