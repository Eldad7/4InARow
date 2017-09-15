using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SC_MenuController : MonoBehaviour 
{
	public void Screen_Main_Btn_SinglePlayer()
	{
		SceneManager.LoadScene ("Scene_4inArow");
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
}
