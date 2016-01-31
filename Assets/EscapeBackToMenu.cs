using UnityEngine;
using System.Collections;

public class EscapeBackToMenu : MonoBehaviour {

	ProperInput pIn;
	void Start () {
		pIn = new ProperInput(0);
	}
	
	// Update is called once per frame
	void Update () {

		if (pIn.GetDown(ButtonAction.Escape))
		{
			Application.LoadLevel(0);
		}
	
	}
}
