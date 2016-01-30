using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour {

	public GameObject[] MenuItems;
	

	void Update () {
		if (Input.GetButton("Action2_1"))
		{
				MenuItems[0].SetActive(true);
				MenuItems[1].SetActive(false);
		}
	
	}
}
