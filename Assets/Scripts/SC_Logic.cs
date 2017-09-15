using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SC_Logic : MonoBehaviour {

    //private GameEnums.SlotState[,] gameStatus;
	private int[,] gameStatus;
	int turn,RowAmount,ColumnAmount;
	int flip;
	private Rigidbody2D currentRigitBody;
	private GameEnums.SlotState currentState;
	private GameObject currentPlayer;
	string Player;
    #region Singleton

    static SC_Logic instance;
    public static SC_Logic Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.Find("SC_Logic").GetComponent<SC_Logic>();
            return instance;
        }
    }

    #endregion
    // Use this for initialization
    void Start () {
		ColumnAmount = DefinedVariables.ColumnAmount;
		RowAmount = DefinedVariables.RowAmount;
		flip = Random.Range(1,42);
		gameStatus = new int[RowAmount, ColumnAmount];
		for (int i = 0; i < RowAmount; i++)
			for (int j = 0; j < ColumnAmount; j++) {
				gameStatus [i, j] = (int)GameEnums.SlotState.Empty;
			}
		InitTurn ();
	}
	
	// Update is called once per frame
	void Update () {
		Running ();
	}
	public GameEnums.GameState CheckWinner(int _Index,int _RowIndex)
    {
		int sum = 0;
		for (int i = 0; i < RowAmount; i++) { 	//Check for 4 in a column
			if ((i + 1) == RowAmount)
				sum += gameStatus [i,_Index];
			else if (gameStatus [i,_Index] != gameStatus [i + 1,_Index])
				sum = 0;
			else
				sum += gameStatus [i,_Index];
		}
		if (sum == 4 || sum == -4)
			return GameEnums.GameState.Winner;
		else {
			sum = 0;
			for (int i = 0; i < ColumnAmount; i++){ //Check for 4 in a row
				//We'll check for a 4 in a row already existed before finishing going all of the row
				sum += gameStatus [_RowIndex,i];
				if (i + 1 == ColumnAmount || sum == 4 || sum == -4)
					break;
				else if (gameStatus [_RowIndex, i] != gameStatus [_RowIndex, i + 1]) {
					sum = gameStatus [_RowIndex, i];
				}
			}
				
			if (sum >= 4 || sum <= -4)
				return GameEnums.GameState.Winner;
		}
		if (turn == 42)
			return GameEnums.GameState.Tie;
		return GameEnums.GameState.NoWinner;
    }
	public void InitTurn()
	{
		turn = 0;
		int _rand = Random.Range (0,2);
		if (_rand < 1)
			_rand = -1;
		currentState = (GameEnums.SlotState)_rand;
		if (currentState == GameEnums.SlotState.Red)
			Player = "Player 1";
		else
			Player = "PC";
		SC_Globals.Instance.unityObjects ["Image_MatchOver"].SetActive (false);
		SC_View.Instance.SetImage ("Image_CurrentTurn",currentState);
		SC_View.Instance.SetText ("Text_CurrentTurn", "Current Turn:" + Player);
		Debug.Log ("Current Turn is: " + currentState);
		/*if (string.Compare (Player, "PC") == 0)
			DoComputerTurn ();
		else*/
			Running ();
	}

	public void Running(){
		currentRigitBody = SC_Globals.Instance.unityObjects ["chip"].GetComponent < Rigidbody2D > ();
		float _direction = Input.GetAxis ("Horizontal");
		if (_direction != 0)
			currentRigitBody.velocity = new Vector2 (_direction * 10, currentRigitBody.velocity.y);
		if (_direction > 0)
			transform.localScale = new Vector3 (20, 1, 1);
		else
			transform.localScale = new Vector3 (-20, 1, 1);
		
			
		if (Input.GetKeyDown (KeyCode.Space)) {
			SC_Globals.Instance.unityObjects ["tile"].SetActive (false);
			DoSlotLogic ((float)(SC_Globals.Instance.unityObjects ["chip"].transform.position.x));
		}
	}


	public void DoSlotLogic(float _x){
		int _columnIndex = -1;
		if (-4.4 < _x && _x < -3.31 )
			_columnIndex = 0;
		if (-3.31< _x && _x < -1.97)
			_columnIndex = 1;
		if (-1.97 < _x && _x < -0.45)
			_columnIndex = 2;
		if (-0.45 < _x && _x < 0.45)
			_columnIndex = 3;
		if (0.45 < _x && _x < 1.85)
			_columnIndex = 4;
		if (1.85 < _x && _x < 3.02)
			_columnIndex = 5;
		if (3.02 < _x && _x < 4.22)
			_columnIndex = 6;

		DoSlotLogic (_columnIndex);
	}

	public void DoSlotLogic(int _Index)
    {
		Debug.Log (_Index);

		int i=0;
		while( i < RowAmount ) {	//Check in which row to place to  chip
			if (gameStatus[i,_Index]!=(int)GameEnums.SlotState.Empty)
				break;
			i++;
		}
		string slot = "Image_Slot" + _Index.ToString() + (i-1).ToString ();

		gameStatus [i-1,_Index] = (int)currentState;
		Debug.Log("Added " + (int)currentState + " to gameStatus[" +(i-1).ToString() + "," + _Index.ToString() +"]");  
		//SC_View.Instance.SetImage (slot, currentState);
		GameEnums.GameState _currentGameState = CheckWinner (_Index,i-1);

		if (_currentGameState == GameEnums.GameState.NoWinner)
			PassTurn ();
		else
			MatchOver (_currentGameState);
    }
		
    private void PassTurn()
    {
		//if (turn == flip)
		//	DoFlip ();
		if (currentState == GameEnums.SlotState.Red) {
			currentState = GameEnums.SlotState.Yellow;
			Player = "PC";
		} else {
			currentState = GameEnums.SlotState.Red;
			Player = "Player 1";
		}
		SC_View.Instance.SetImage ("Image_CurrentTurn",currentState);
		SC_View.Instance.SetText ("Text_CurrentTurn", "Current Turn:" + Player);
		if (string.Compare (Player, "PC") == 0)
			DoComputerTurn ();
    }

	public void MatchOver(GameEnums.GameState _CurrentGameState){
		for (int i = 0; i < ColumnAmount; i++) {
			SC_Globals.Instance.unityObjects ["Btn_Slot" + i].SetActive (false);
			SC_Globals.Instance.unityObjects ["Btn_Slot" + i].GetComponent<Button>().interactable = false;
		}
		SC_Globals.Instance.unityObjects ["Image_MatchOver"].SetActive (true);
		if (_CurrentGameState == GameEnums.GameState.Tie) {
			Debug.Log ("Tie");
			SC_View.Instance.SetText ("Text_MatchOverLabel", "Tie");
		} else {
			Debug.Log ("Winner is " + currentState);
			SC_View.Instance.SetText ("Text_MatchOverLabel", "Winner is " + currentState);
		}
	}

	public void RestartMatchLogic(){
		for (int i = 0; i < RowAmount; i++)
			for (int j = 0; j < ColumnAmount; j++) {
				gameStatus [i, j] = (int)GameEnums.SlotState.Empty;
				SC_Globals.Instance.unityObjects ["Btn_Slot" + i].SetActive (true);
				SC_Globals.Instance.unityObjects ["Btn_Slot" + i].GetComponent<Button>().interactable = true;
				Debug.Log (i.ToString() + " " + j);
				SC_Globals.Instance.unityObjects ["Image_Slot"+j.ToString()+i.ToString()].SetActive (false);
			}
		InitTurn ();
	}

	public void DoFlip(){
		int flips = Random.Range (1, 4);
		int[,] newGameStatus;
		int _ColumnAmount, _RowAmount;
		for (int _flips = 0; _flips < flips; _flips++){	//How many times to flip
			_ColumnAmount = RowAmount;
			_RowAmount = ColumnAmount;
			newGameStatus = new int[_RowAmount, _ColumnAmount];
			for (int i = 0; i < RowAmount; i++) {
				for (int j = 0; j < ColumnAmount; j++) {
					//If we flip from 6X7 to 7X6
					if ((_flips % 2) == 0) {	//
						//Debug.Log ("Old: [" + i + "," + j + "]");
						//Debug.Log ("new: [" + j + "," + (ColumnAmount - i - 1) + "]");
						newGameStatus [j, _ColumnAmount - i - 1] = gameStatus [i, j];
					} else {	//If we flip from 7X6 to 6X7
						//Debug.Log ("Old: [" + i + "," + j + "]");
						//Debug.Log ("new: [" + i + "," + (_RowAmount - j - 1) + "]");
						newGameStatus [_RowAmount - j - 1,i] = gameStatus [i, j];
					}
				}
			}
			ColumnAmount = _ColumnAmount;
			RowAmount = _RowAmount;
			gameStatus = null;
			gameStatus = new int[RowAmount, ColumnAmount];
			for (int i = 0; i < RowAmount; i++)
				for (int j = 0; j < ColumnAmount; j++) {
					gameStatus [i, j] = newGameStatus [i, j];
					Debug.Log("[" + i + "," + j + "] = " + gameStatus[i,j]);
				}
			newGameStatus = null;
		}
		SC_Globals.Instance.unityObjects ["Board"].transform.rotation = Quaternion.Euler (transform.localRotation.x, transform.localRotation.y, (transform.localRotation.z + 1) * (flips*90*-1)); //for it to rotate clockwise
		//We go over all of the columns to see if the bottom of that row has an space TODO - better logic
		for (int j = 0; j < ColumnAmount; j++) {
			if (gameStatus [RowAmount-1, j] == (int)GameEnums.SlotState.Empty)
				DoDrop (j);
		}
	}
	public void DoDrop(int _Index){
		for (int i = RowAmount-1; i>0; i--){
			gameStatus [i, _Index] = gameStatus [i-1,_Index];
			if (gameStatus[i,_Index]==(int)GameEnums.SlotState.Red)
				SC_View.Instance.SetImage ("Image_Slot"+i.ToString()+_Index.ToString(), GameEnums.SlotState.Red);
			else if (gameStatus[i, _Index]==(int)GameEnums.SlotState.Yellow)
				SC_View.Instance.SetImage ("Image_Slot"+i.ToString()+_Index.ToString(), GameEnums.SlotState.Yellow);
			else
				SC_View.Instance.SetImage ("Image_Slot"+i.ToString()+_Index.ToString(), GameEnums.SlotState.Empty);
			if (i==1)
				SC_View.Instance.SetImage ("Image_Slot"+(i-1).ToString()+(_Index).ToString(), GameEnums.SlotState.Empty);
		}
	}

	public void DoComputerTurn(){
		int column = Random.Range (0, 7);
		if (SC_Globals.Instance.unityObjects ["Btn_Slot" + column].GetComponent<Button> ().interactable == true)
			DoSlotLogic (column);
		else
			DoComputerTurn ();
	}
}
