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
		
	}

	public void CleanBoard(){
		for (int i = 0; i < DefinedVariables.RowAmount; i++) 
			for (int j=0; j<DefinedVariables.ColumnAmount; j++)
				SetImage (i,j,GameEnums.SlotState.Empty);
	}

	public void SetImage(int row,int column, GameEnums.SlotState _NewState)
	{
		SetImage ("Image_Slot" + row.ToString() + column.ToString(),_NewState);
	}

	public void SetImage(string _ObjectName, GameEnums.SlotState _NewState)
	{
		if (_NewState == GameEnums.SlotState.Red) {
			SC_Globals.Instance.unityObjects [_ObjectName].SetActive (true);
			SC_Globals.Instance.unityObjects [_ObjectName].GetComponent<Image> ().sprite = SC_Globals.Instance.red;
		}
		if (_NewState == GameEnums.SlotState.Yellow) {
			SC_Globals.Instance.unityObjects [_ObjectName].SetActive (true);
			SC_Globals.Instance.unityObjects [_ObjectName].GetComponent<Image> ().sprite = SC_Globals.Instance.yellow;
		} 
		if (_NewState == GameEnums.SlotState.Empty){
			SC_Globals.Instance.unityObjects [_ObjectName].SetActive (true);
			SC_Globals.Instance.unityObjects [_ObjectName].GetComponent<Image> ().sprite = Resources.Load("barrier",typeof(Sprite)) as Sprite;
		}
	}

	public void SetText(string _ObjectName,string _Message)
	{
		SC_Globals.Instance.unityObjects [_ObjectName].GetComponent<Text>().text = _Message;
	}
}
