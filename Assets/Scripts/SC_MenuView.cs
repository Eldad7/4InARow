using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SC_MenuView : MonoBehaviour
{
	static SC_MenuView instance;
	public static SC_MenuView Instance
	{
		get 
		{
			if (instance == null)
				instance = GameObject.Find("SC_MenuView").GetComponent<SC_MenuView> ();
			return instance;
		}
	}

	public void SetInfoText(string _TextToShow)
	{
		SC_MenuGlobals.Instance.unityObjects ["Screen_Loading_Title"].GetComponent<Text> ().text = _TextToShow;
		Debug.Log("Text_Info " + _TextToShow);
	}

	public void SetUserId(string _UserId)
	{
		SC_MenuGlobals.Instance.unityObjects ["Screen_Loading_Title"].GetComponent<Text> ().text = "User Id:" + _UserId;
		Debug.Log("User Id:" + _UserId);
	}
}
