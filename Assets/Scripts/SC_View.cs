using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SC_View : MonoBehaviour {

	#region Singleton

	static SC_View instance;
	public static SC_View Instance
	{
		get 
		{
			if (instance == null)
				instance = GameObject.Find("SC_View").GetComponent<SC_View> ();
			return instance;
		}
	}

	#endregion

	void Start()
	{
		Init();
	}

	public void Init()
	{
		for (int i = 0; i < DefinedVariables.ColumnAmount; i++) 
			for (int j=0; j<DefinedVariables.RowAmount; j++)
				SetImage (i,j,GameEnums.SlotState.Empty);
	}

	public void SetImage(int row,int column, GameEnums.SlotState _NewState)
	{
		SetImage ("Image_Slot" + row.ToString() + column.ToString(),_NewState);
	}

	public void SetImage(string _ObjectName, GameEnums.SlotState _NewState)
	{
		Debug.Log (_ObjectName + " " + _NewState);
		if (_NewState == GameEnums.SlotState.Red) 
		{
			SC_Globals.Instance.unityObjects [_ObjectName].SetActive (true);
			SC_Globals.Instance.unityObjects [_ObjectName].GetComponent<Image> ().sprite = SC_Globals.Instance.red;
		}
		else if (_NewState == GameEnums.SlotState.Yellow) 
		{
			SC_Globals.Instance.unityObjects [_ObjectName].SetActive (true);
			SC_Globals.Instance.unityObjects [_ObjectName].GetComponent<Image> ().sprite = SC_Globals.Instance.yellow;
		}
	}

	public void SetText(string _ObjectName,string _Message)
	{
		SC_Globals.Instance.unityObjects [_ObjectName].GetComponent<Text>().text = _Message;
	}
}
