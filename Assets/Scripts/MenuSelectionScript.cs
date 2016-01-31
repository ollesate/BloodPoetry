using UnityEngine;
using System.Collections;

public class MenuSelectionScript : MonoBehaviour
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
    public AudioClip[] aClips = new AudioClip[3];
    private AudioSource myASource;
    public GameObject rightArrow;
    public GameObject[] OptionsItems;
    ProperInput pIn;

    void Start()
    {
        pIn = new ProperInput(0);
        myASource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        menuDirSel = GetStepping(pIn.Get(AxisAction.AimY));
        
        currSel += Mathf.RoundToInt(menuDirSel);
        if (currSel < 0)
        {
            currSel = elemAmount-1;
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
            if (currSel == 3) //Start
            {
                myASource.Stop();
                StartCoroutine(StartGame());
            }
            if (currSel == 2) //Options
            {
                MenuItems[1].SetActive(true);
                MenuItems[0].SetActive(false);
            }
            if (currSel == 1) //Credits
            {
                MenuItems[2].SetActive(true);
                MenuItems[0].SetActive(false);
            }
            if (currSel == 0) //Exit
            {
                StartCoroutine(Exit());
            }
        }
    }


    IEnumerator StartGame()
    {
        myASource.Stop();
        myASource.volume = PlayerPrefs.GetFloat("SEVolume");
        myASource.PlayOneShot(aClips[0]);
        yield return new WaitForSeconds(myASource.clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    IEnumerator Exit()
    {
        myASource.Stop();
        myASource.volume = PlayerPrefs.GetFloat("SEVolume");
        myASource.PlayOneShot(aClips[2]);
        yield return new WaitForSeconds(myASource.clip.length);
        Application.Quit();
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

            timeHeld += Time.deltaTime *1000;

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
