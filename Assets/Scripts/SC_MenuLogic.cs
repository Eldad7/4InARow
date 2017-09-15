using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SC_MenuLogic : MonoBehaviour 
{
	public Dictionary<string,GameObject> unityObjects;
	private DefinedVariables.MenuScreens currentScreen;
	private DefinedVariables.MenuScreens prevScreen;


	static SC_MenuLogic instance;
	public static SC_MenuLogic Instance
	{
		get 
		{
			if (instance == null)
				instance = GameObject.Find ("SC_MenuLogic").GetComponent<SC_MenuLogic>();
			return instance;
		}
	}

	void Awake ()
	{
		Init ();
	}

	void Start ()
	{
	
	}

	private void Init()
	{
		prevScreen = DefinedVariables.MenuScreens.Default;
		currentScreen = DefinedVariables.MenuScreens.Default;

		unityObjects = new Dictionary<string, GameObject> ();
		GameObject[] _unityObjects = GameObject.FindGameObjectsWithTag ("UnityObject");
		foreach (GameObject g in _unityObjects) 
		{
			unityObjects.Add (g.name, g);
			Debug.Log (g.name);
		}

		DeactivateScreens ();
		ChangeScreen (DefinedVariables.MenuScreens.Main);
	}

	public void Screen_Main_Btn_SinglePlayerLogic()
	{
		Debug.Log ("Screen_Main_Btn_SinglePlayerLogic");
		ChangeScreen (DefinedVariables.MenuScreens.Loading);
	}

	public void Screen_Main_Btn_StudentInfoLogic()
	{
		Debug.Log ("Screen_Main_Btn_StudentInfo_Logic");
		ChangeScreen (DefinedVariables.MenuScreens.StudentInfo);
	}

	public void Screen_Main_Btn_MultiPlayerLogic()
	{
		Debug.Log ("Screen_Main_Btn_MultiPlayerLogic");
		ChangeScreen (DefinedVariables.MenuScreens.Multiplayer);
	}

	public void Screen_Main_Btn_OptionsLogic()
	{
		Debug.Log ("Screen_Main_Btn_OptionsLogic");
		ChangeScreen (DefinedVariables.MenuScreens.Options);
	}

	public void 	Screen_Main_Btn_BackLogic()
	{
		Debug.Log ("Screen_Main_Btn_BackLogic");
		if (prevScreen != DefinedVariables.MenuScreens.Options)
			ChangeScreen (prevScreen);
		else {
			ChangeScreen (DefinedVariables.MenuScreens.Main);
		}
	}

	public void Screen_Options_Slider_MusicLogic(float _Value)
	{
		unityObjects ["Music_Handle_Text"].GetComponent<Text> ().text = _Value.ToString();
	}

	public void Screen_Options_Slider_Sfx_Logic(float _Value)
	{
		unityObjects ["Sfx_Handle_Text"].GetComponent<Text> ().text = _Value.ToString();
	}

	public void Screen_Multiplayer_Slider_Logic(float _Value)
	{
		unityObjects ["Multiplayer_Slider_Text"].GetComponent<Text> ().text = _Value.ToString();
	}

	private void DeactivateScreens()
	{
		unityObjects ["Screen_Loading"].SetActive (false);
		unityObjects ["Screen_Main"].SetActive (false);
		unityObjects ["Screen_Options"].SetActive (false);
		unityObjects ["Screen_Multiplayer"].SetActive (false);
		unityObjects ["Screen_StudentInfo"].SetActive (false);
	}

	private void ChangeScreen(DefinedVariables.MenuScreens _ToScreen)	
	{
		prevScreen = currentScreen;

		switch(prevScreen)
		{
			case DefinedVariables.MenuScreens.Loading:unityObjects ["Screen_Loading"].SetActive (false); break;
			case DefinedVariables.MenuScreens.Main:unityObjects ["Screen_Main"].SetActive (false); break;
		case DefinedVariables.MenuScreens.Multiplayer:
			unityObjects ["Screen_Multiplayer"].SetActive (false); break;
			case DefinedVariables.MenuScreens.Options:unityObjects ["Screen_Options"].SetActive (false);break;
			case DefinedVariables.MenuScreens.SinglePlayer:break;
		case DefinedVariables.MenuScreens.StudentInfo:unityObjects ["Screen_StudentInfo"].SetActive (false); break;
		}

		currentScreen = _ToScreen;

		switch(currentScreen)
		{
			case DefinedVariables.MenuScreens.Loading:unityObjects ["Screen_Loading"].SetActive (true); break;
			case DefinedVariables.MenuScreens.Main:
				unityObjects ["Screen_Main"].SetActive (true); 
				unityObjects ["Screen_Title"].GetComponent<Text> ().text = "";
				break;
			case DefinedVariables.MenuScreens.Multiplayer:
				unityObjects ["Screen_Multiplayer"].SetActive (true);
				unityObjects ["Screen_Title"].GetComponent<Text> ().text = "Multiplayer";
				break;
			case DefinedVariables.MenuScreens.Options:
				unityObjects ["Screen_Options"].SetActive (true);
				unityObjects ["Screen_Title"].GetComponent<Text> ().text = "Options";
				break;
			case DefinedVariables.MenuScreens.SinglePlayer:break;
			case DefinedVariables.MenuScreens.StudentInfo:
				unityObjects ["Screen_StudentInfo"].SetActive (true);
				unityObjects ["Screen_Title"].GetComponent<Text> ().text = "Student Info";
				break;
		}
	}
}
