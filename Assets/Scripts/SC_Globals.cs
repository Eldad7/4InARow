﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Globals : MonoBehaviour {

	public Dictionary<string, GameObject> unityObjects;
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

    // Use this for initialization
    void Start () {
		
	}
    void Awake()
    {
        Init();
    }
    public void Init()
    {
        unityObjects = new Dictionary<string, GameObject>();
        GameObject[] _objects = GameObject.FindGameObjectsWithTag("UnityObject");
        foreach (GameObject g in _objects)
        {
            unityObjects.Add(g.name, g);
        }

    }
    // Update is called once per frame
    void Update () {
		
	}
}