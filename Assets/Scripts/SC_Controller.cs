using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Controller : MonoBehaviour {

    public void SlotClicked(int _Index)
    {
        SC_Logic.Instance.DoSlotLogic(_Index);
    }

	public void RestartMatch(){
        SC_Globals.Instance.audio["GameSound"].GetComponent<AudioSource>().Play();
		SC_Logic.Instance.RestartMatchLogic ();
	}
}
