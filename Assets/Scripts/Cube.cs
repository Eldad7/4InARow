using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cube : MonoBehaviour {

    
    public GameObject lite;
    public int rang = 5;
    int i = 0;
    // Use this for initialization
	static SC_Globals instance;
	public static SC_Globals Instance
	{
		get
		{
			if (instance == null)
				instance = GameObject.Find("SC_Globals").GetComponent<SC_Globals>();
			return instance;
		}
	}
    void Start () {
        Debug.Log("start method\n");
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            CreateCube();
        }
    }

    void Awake()
    {
        Debug.Log("Awake method\n");
        Init();
    }
    public void Init()
    {
		
    }
    public void CreateCube()
    {
        
		if (Instance.unityObjects.ContainsKey("Cube") && Instance.unityObjects["Cube"]!=null)
        {
            Debug.Log("CreateCube method\n");
            GameObject _tempObject = (GameObject)Instantiate(Instance.unityObjects["Cube"]);
            _tempObject.name += i.ToString();
            int _x = Random.Range(0, rang);
            int _y = Random.Range(0, rang);
            int _z = Random.Range(0, rang);
            _tempObject.GetComponent<Transform>().position = new Vector3(_x, _y, _z);
            i++;
        }
    }
}
