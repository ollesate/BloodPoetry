using UnityEngine;
using System.Collections;

public class OptionsScript : MonoBehaviour
{
    const float DEAD_ZONE = 0.01f;
    const int MS_DELAY = 300;
    const int MS_REPEAT = 120;

    float timeHeld;
    float timeSinceRepeat;

    public float menuDirSel = 0;
    public int elemAmount = 4;
    public int currSel = 0;
    private float spacing = 40;
    public GameObject[] MenuItems;
    public GameObject rightArrow;

    ProperInput pIn;
    public GameObject[] OptionsItems;
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
    public UnityEngine.UI.Text seSetting;
    public UnityEngine.UI.Text mSetting;

    private float SEVolume = 0.75f;
    private float MVolume = 0.75f;

    void Start()
    {
        pIn = new ProperInput(0);
        PlayerPrefs.SetFloat("SEVolume", SEVolume);
        PlayerPrefs.SetFloat("MVolume", MVolume);
    }

    void Update()
    {
        menuDirSel = GetStepping(pIn.Get(AxisAction.AimY));

        currSel += Mathf.RoundToInt(menuDirSel);
        if (currSel < 0)
        {
            currSel = elemAmount - 1;
        }
        if (currSel >= elemAmount)
        {
            currSel = 0;
        }

        Vector3 pos = OptionsItems[currSel].transform.position;
        pos.z = transform.position.z;
        transform.position = pos;

        float distance = OptionsItems[currSel].GetComponent<RectTransform>().sizeDelta.x / 30;
        Vector3 rArrowPos = transform.InverseTransformPoint(new Vector3(pos.x + distance, pos.y, rightArrow.transform.position.z));
        rightArrow.transform.localPosition = rArrowPos;

        if (pIn.GetDown(ButtonAction.Green))
        {
            if (currSel == 3) //SE Volume
            {
                SEVolume += 0.05f;
                SEVolume = Mathf.Clamp01(RoundFloat(SEVolume));
            }
            if (currSel == 2) //Music Volume
            {
                MVolume += 0.05f;
                MVolume = Mathf.Clamp01(RoundFloat(MVolume));
            }
            if (currSel == 1) //Gore
            {
                //Debug.Log("ALL GORE");
            }
            if (currSel == 0) //Back
            {
                MenuItems[0].SetActive(true);
                MenuItems[1].SetActive(false);
            }
        }
        if (pIn.GetDown(ButtonAction.Red))
        {
            if (currSel == 3) //SE Volume
            {
                SEVolume -= 0.05f;
                SEVolume = Mathf.Clamp01(RoundFloat(SEVolume));
            }
            if (currSel == 2) //Music Volume
            {
                MVolume -= 0.05f;
                MVolume = Mathf.Clamp01(RoundFloat(MVolume));
            }
            if (currSel == 1) //Gore
            {
                //Debug.Log("ALL GORE");
            }
            if (currSel == 0) //Back
            {
                MenuItems[0].SetActive(true);
                MenuItems[1].SetActive(false);
            }
        }
        PlayerPrefs.SetFloat("SEVolume", SEVolume);
        PlayerPrefs.SetFloat("MVolume", MVolume);
        seSetting.text = SEVolume.ToString();
        mSetting.text = MVolume.ToString();
    }

    private float RoundFloat(float f)
    {
        return (Mathf.Ceil(f * 100)) / 100;
    }

    public float GetStepping(float direction)
    {
        bool stepThisFrame = false;

        // If moving along any axis, normalize along that axis.
        if (direction != 0)
        {
            if (direction < -DEAD_ZONE)
            {
                direction = -1;
            }
            else if (direction > DEAD_ZONE)
            {
                direction = 1;
            }
        }

        // If we have input outside the dead zone
        if (direction != 0)
        {
            if (timeHeld == 0)
            {
                // If this is new input, we go
                stepThisFrame = true;
            }

            timeHeld += Time.deltaTime * 1000;

            if (timeHeld > MS_DELAY - MS_REPEAT)
            {
                // start counting up to the first repetetion
                timeSinceRepeat += Time.deltaTime * 1000;

                if (timeSinceRepeat > MS_REPEAT)
                {
                    timeSinceRepeat -= MS_REPEAT;
                    stepThisFrame = true;
                }
            }
        }
        else
        {
            // If there is completely new input, we reset the time held.
            timeHeld = 0;
            timeSinceRepeat = 0;
        }

        if (stepThisFrame)
        {
            return direction;
        }

        return 0;
    }
}
