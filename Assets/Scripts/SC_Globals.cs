using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Globals : MonoBehaviour {

	public Dictionary<string, GameObject> unityObjects;
	public Dictionary<string, GameObject> buttons;
    public Dictionary<string, GameObject> audio;
    public Sprite yellow;
	public Sprite red;

	#region Singleton
	static SC_Globals instance;
	public static SC_Globals Instance
	{
		get
		{
			if (instance == null)
				instance = GameObject.Find ("SC_Globals").GetComponent<SC_Globals> ();
			return instance;
		}
	}

	#endregion

    void Awake()
    {
        Init();
    }
    public void Init()
    {
        unityObjects = new Dictionary<string, GameObject>();
		buttons = new Dictionary<string, GameObject>();
        audio = new Dictionary<string, GameObject>();
        GameObject[] _objects = GameObject.FindGameObjectsWithTag("UnityObject");
        foreach (GameObject g in _objects)
        {
            unityObjects.Add(g.name, g);
        }
		GameObject[] _buttons = GameObject.FindGameObjectsWithTag("Button");
		foreach (GameObject g in _buttons)
		{
			buttons.Add(g.name, g);
			Debug.Log (g.name);
		}
        GameObject[] _audio = GameObject.FindGameObjectsWithTag("Audio");
        foreach (GameObject g in _audio)
        {
            audio.Add(g.name, g);
            Debug.Log(g.name);
        }
    }

	public void updateUnityObjects(){
		GameObject[] _objects = GameObject.FindGameObjectsWithTag("UnityObject");
		foreach (GameObject g in _objects)
		{
			if(unityObjects.ContainsKey(g.name))
				unityObjects.Remove(g.name);
			unityObjects.Add(g.name, g);
		}
	}
}
