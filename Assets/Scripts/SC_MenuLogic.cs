using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using com.shephertz.app42.gaming.multiplayer.client;
using AssemblyCSharp;
using com.shephertz.app42.gaming.multiplayer.client.events;

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

	public void Screen_Main_Btn_BackLogic()
	{
		Debug.Log ("Screen_Main_Btn_BackLogic");
		if (prevScreen != DefinedVariables.MenuScreens.Options)
			ChangeScreen (prevScreen);
		else {
			ChangeScreen (DefinedVariables.MenuScreens.Main);
		}
	}

	public void Connect_To_Room(){
		//ChangeScreen (DefinedVariables.MenuScreens.Loading);
		SC_MenuGlobals.Instance.data = new Dictionary<string, object> ();
		SC_MenuGlobals.Instance.data.Add ("Password", "12345");
		WarpClient.initialize (DefinedVariables.apiKey,DefinedVariables.secretKey);
		Debug.Log ("WarpClient.GetInstance ().AddConnectionRequestListener (SC_MenuGlobals.listener);");
		WarpClient.GetInstance ().AddConnectionRequestListener (SC_MenuGlobals.listener);
		Debug.Log ("WarpClient.GetInstance().AddChatRequestListener(SC_MenuGlobals.listener);");
		WarpClient.GetInstance().AddChatRequestListener(SC_MenuGlobals.listener);
		Debug.Log ("WarpClient.GetInstance().AddUpdateRequestListener(SC_MenuGlobals.listener);");
		WarpClient.GetInstance().AddUpdateRequestListener(SC_MenuGlobals.listener);
		Debug.Log ("WarpClient.GetInstance().AddLobbyRequestListener(SC_MenuGlobals.listener);");
		WarpClient.GetInstance().AddLobbyRequestListener(SC_MenuGlobals.listener);
		Debug.Log ("WarpClient.GetInstance().AddNotificationListener(SC_MenuGlobals.listener);");
		WarpClient.GetInstance().AddNotificationListener(SC_MenuGlobals.listener);
		Debug.Log ("WarpClient.GetInstance().AddRoomRequestListener(SC_MenuGlobals.listener);");
		WarpClient.GetInstance().AddRoomRequestListener(SC_MenuGlobals.listener);
		Debug.Log ("WarpClient.GetInstance().AddZoneRequestListener(SC_MenuGlobals.listener);");
		WarpClient.GetInstance().AddZoneRequestListener(SC_MenuGlobals.listener);
		Debug.Log ("WarpClient.GetInstance().AddTurnBasedRoomRequestListener (SC_MenuGlobals.listener);");
		WarpClient.GetInstance().AddTurnBasedRoomRequestListener (SC_MenuGlobals.listener);
		Debug.Log ("WarpClient.GetInstance ().AddConnectionRequestListener (SC_MenuGlobals.listener);");

		if (SC_MenuGlobals.Instance.unityObjects["UserNameText"].GetComponent<Text>().text!="")
			SC_MenuGlobals.userName = SC_MenuGlobals.Instance.unityObjects["UserNameText"].GetComponent<Text>().text;
		else
			SC_MenuGlobals.userName = System.DateTime.UtcNow.Ticks.ToString ();
		Debug.Log (SC_MenuGlobals.userName);
		WarpClient.GetInstance ().Connect (SC_MenuGlobals.userName);
		SC_MenuView.Instance.SetUserId(SC_MenuGlobals.userName);
		SC_MenuView.Instance.SetInfoText("Connecting..");
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

	void OnEnable()
	{
		Listener.OnConnect += OnConnect;
		Listener.OnRoomsInRange += OnRoomsInRange;
		Listener.OnCreateRoom += OnCreateRoom;
		Listener.OnGetLiveRoomInfo += OnGetLiveRoomInfo;
		Listener.OnUserJoinRoom += OnUserJoinRoom;
		Listener.OnGameStarted += OnGameStarted;
	}

	void OnDisable()
	{
		Listener.OnConnect -= OnConnect;
		Listener.OnRoomsInRange -= OnRoomsInRange;
		Listener.OnCreateRoom -= OnCreateRoom;
		Listener.OnGetLiveRoomInfo -= OnGetLiveRoomInfo;
		Listener.OnUserJoinRoom -= OnUserJoinRoom;
		Listener.OnGameStarted -= OnGameStarted;
	}

	public void OnConnect(bool _IsSuccess)
	{
		Debug.Log("OnConnect: " + _IsSuccess);
		SC_MenuView.Instance.SetInfoText("Connected!");
	}

	public void Btn_PlayLogic()
	{
		Debug.Log ("Btn_PlayLogic");
		WarpClient.GetInstance ().GetRoomsInRange (1, 2);
		GameObject.Find ("Btn_Play").GetComponent<Button> ().interactable = false; 
		SC_MenuView.Instance.SetInfoText("Get Rooms...");
	}


	public void OnRoomsInRange(bool _IsSuccess,MatchedRoomsEvent eventObj)
	{
		SC_MenuView.Instance.SetInfoText("On Rooms In Range: " + eventObj.getRoomsData());
		Debug.Log("OnRoomsInRange: " + _IsSuccess + " " + eventObj.getRoomsData());
		if (_IsSuccess) 
		{
			SC_MenuGlobals.rooms = new List<string> ();
			foreach (var roomData in eventObj.getRoomsData())
			{
				Debug.Log ("Getting Live info on room " + roomData.getId ());
				Debug.Log ("Room Owner " + roomData.getRoomOwner ());
				SC_MenuGlobals.rooms.Add (roomData.getId ());
			}

			SC_MenuGlobals.index = 0;
			if (SC_MenuGlobals.index < SC_MenuGlobals.rooms.Count) 
			{
				Debug.Log ("Getting Live Info on room: " +  SC_MenuGlobals.rooms[SC_MenuGlobals.index]);
				WarpClient.GetInstance ().GetLiveRoomInfo (SC_MenuGlobals.rooms[SC_MenuGlobals.index]);
			} 
			else 
			{
				Debug.Log ("No rooms were availible, create a room");
				WarpClient.GetInstance().CreateTurnRoom("Room Name",SC_MenuGlobals.userName,2,SC_MenuGlobals.Instance.data,60);
			}
		} 
		else GameObject.Find ("Btn_Play").GetComponent<Button> ().interactable = true;
	}

	public void OnCreateRoom(bool _IsSuccess,string _RoomId)
	{
		Debug.Log ("OnCreateRoom " + _IsSuccess + " " + _RoomId);
		if (_IsSuccess) 
		{
			SC_MenuView.Instance.SetInfoText("Created Room!");
			WarpClient.GetInstance ().JoinRoom (_RoomId);

			//so i can get events when other users join my room
			WarpClient.GetInstance ().SubscribeRoom (_RoomId);
		}
	}

	public void OnGetLiveRoomInfo(LiveRoomInfoEvent eventObj)
	{
		Debug.Log ("OnGetLiveRoomInfo " + eventObj.getData().getId() + " " +  eventObj.getResult() + " " + eventObj.getJoinedUsers().Length);
		SC_MenuView.Instance.SetInfoText("Room Information: " + eventObj.getData().getId() + " " +  eventObj.getJoinedUsers().Length);

		Dictionary<string,object> _temp = eventObj.getProperties ();
		Debug.Log (_temp.Count + " " + _temp["Password"] + " " + SC_MenuGlobals.Instance.data["Password"].ToString());

		if (eventObj.getResult () == 0 && eventObj.getJoinedUsers ().Length == 1 &&
			_temp["Password"].ToString() == SC_MenuGlobals.Instance.data["Password"].ToString())
		{
			WarpClient.GetInstance ().JoinRoom (eventObj.getData ().getId ());
			WarpClient.GetInstance ().SubscribeRoom (eventObj.getData ().getId ());
			SC_MenuView.Instance.SetInfoText("Joining Room...");
		}
		else 
		{
			SC_MenuGlobals.index++;
			if (SC_MenuGlobals.index < SC_MenuGlobals.rooms.Count) 
			{
				Debug.Log ("Getting Live Info on room: " +  SC_MenuGlobals.rooms[SC_MenuGlobals.index]);
				WarpClient.GetInstance ().GetLiveRoomInfo (SC_MenuGlobals.rooms[SC_MenuGlobals.index]);
			} 
			else 
			{
				Debug.Log ("No rooms were availible, create a room");
				WarpClient.GetInstance().CreateTurnRoom("Room Name",SC_MenuGlobals.userName,2,null,60);
			}
		}
	}

	public void OnUserJoinRoom(RoomData eventObj, string _UserName)
	{
		Debug.Log ("OnUserJoinRoom " + " " + _UserName);
		SC_MenuView.Instance.SetInfoText ("OnUserJoinRoom " + " " + _UserName);
		if(_UserName != eventObj.getRoomOwner())
		{
			WarpClient.GetInstance ().startGame ();
		}
	}

	public void OnGameStarted(string _Sender,string _RoomId,string _NextTurn)
	{
		Debug.Log ("SC_MenuLogic: " + _Sender + " " + _RoomId + " " + _NextTurn);
		SC_MenuView.Instance.SetInfoText ("OnGameStarted " + _Sender + " " + _RoomId + " " + _NextTurn);
		SC_MenuGlobals.Instance.unityObjects ["Screen_Menu"].SetActive (false);
		SC_MenuGlobals.Instance.unityObjects ["Screen_Game"].SetActive (true);
	}
}