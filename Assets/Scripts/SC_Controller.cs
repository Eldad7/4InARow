using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SlotClicked(int _Index)
    {
        SC_Logic.Instance.DoSlotLogic(_Index);
    }

	public void RestartMatch(){
		SC_Logic.Instance.RestartMatchLogic ();
	}
}
