using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SC_MenuController : MonoBehaviour 
{

	static SC_MenuController instance;
	public static SC_MenuController Instance
	{
		get 
		{
			if (instance == null)
				instance = GameObject.Find("SC_MenuController").GetComponent<SC_MenuController> ();
			return instance;
		}
	}

	public void Screen_Main_Btn_SinglePlayer()
	{
		SC_MenuGlobals.Instance.multiplayer = false;
		//SC_Logic.Instance.InitTurn ();
		SC_MenuLogic.Instance.GameLogicSingle();
		Debug.Log ("Loaded Scene_4inArow");
	}

	public void Load_Multiplayer(){
		//SC_Logic.Instance.InitTurn ();
		SC_MenuLogic.Instance.GameLogicMultiplayer();
		Debug.Log ("Loaded Scene_4inArow");
	}

	public void Screen_Main_Btn_MultiPlayer()
	{
		SC_MenuLogic.Instance.Screen_Main_Btn_MultiPlayerLogic ();
	}

	public void Screen_Main_Btn_StudentInfo()
	{
		SC_MenuLogic.Instance.Screen_Main_Btn_StudentInfoLogic ();
	}

	public void Screen_Main_Btn_Options()
	{
		SC_MenuLogic.Instance.Screen_Main_Btn_OptionsLogic ();
	}

	public void Screen_Main_Btn_Back()
	{
		SC_MenuLogic.Instance.Screen_Main_Btn_BackLogic ();
	}

	public void Screen_Options_Slider_Music()
	{
		float _value = SC_MenuLogic.Instance.unityObjects ["Screen_Options_Slider_Music"].GetComponent<Slider> ().value;
		SC_MenuLogic.Instance.Screen_Options_Slider_MusicLogic (_value);
	}

	public void Screen_Options_Slider_fx()
	{
		float _value = SC_MenuLogic.Instance.unityObjects ["Screen_Options_Slider_Sfx"].GetComponent<Slider> ().value;
		SC_MenuLogic.Instance.Screen_Options_Slider_Sfx_Logic (_value);
	}

	public void Screen_Multiplayer_Slider()
	{
		float _value = SC_MenuLogic.Instance.unityObjects ["Screen_Multiplayer_Slider"].GetComponent<Slider> ().value;
		SC_MenuLogic.Instance.Screen_Multiplayer_Slider_Logic (_value);
	}

	public void LeaveGame(){
		SC_MenuLogic.Instance.LeaveClicked ();
	}
}
