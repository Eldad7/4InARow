using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SC_Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}
    public void SlotClicked(int _Index)
    {
		SC_Logic.Instance.submitLogic(_Index);
    }

	public void RestartMatch(){
		if (!SC_MenuGlobals.Instance.multiplayer) {
			SC_Logic.Instance.RestartMatchLogic ();
			SC_Globals.Instance.audio ["GameSound"].GetComponent<AudioSource> ().Play ();
		} else {
			SC_Globals.Instance.unityObjects ["Btn_RestartMatch"].GetComponent<Button> ().interactable = false;
			SC_Logic.Instance.RequestRematch (true);
		}
	}
}
