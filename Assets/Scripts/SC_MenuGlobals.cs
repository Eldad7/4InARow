using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class SC_MenuGlobals : MonoBehaviour {

	public Dictionary<string,GameObject> unityObjects;

	public static Listener listener;
	public static string userName = string.Empty;
	public static List<string> rooms;
	public static int index = 0;
	public Dictionary<string,object> data;



	static SC_MenuGlobals instance;
	public static SC_MenuGlobals Instance
	{
		get 
		{
			if (instance == null)
				instance = GameObject.Find("SC_MenuGlobals").GetComponent<SC_MenuGlobals> ();
			return instance;
		}
	}

	void Awake() 
	{
		listener = new Listener ();
		rooms = new List<string> ();

		unityObjects = new Dictionary<string, GameObject> ();
		GameObject[] _unityObjects = GameObject.FindGameObjectsWithTag ("UnityObject");
		foreach (GameObject g in _unityObjects) 
		{
			unityObjects.Add (g.name,g);
		}
	}

}
