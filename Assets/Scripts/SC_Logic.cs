using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SC_Logic : MonoBehaviour {

	private int[,] gameStatus;
	int turn,RowAmount,ColumnAmount;
	int flip;
	private GameEnums.SlotState currentState;
	private GameObject currentPlayer;
	string Player;
	bool vertical = false;
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
		//flip = Random.Range(1,42);
		flip=3;
		gameStatus = new int[RowAmount, ColumnAmount];
		for (int i = 0; i < RowAmount; i++)
			for (int j = 0; j < ColumnAmount; j++) {
				gameStatus [i, j] = (int)GameEnums.SlotState.Empty;
			}
		InitTurn ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public GameEnums.GameState CheckWinner(int _Index,int _RowIndex)
    {
		int sum = 0;
		for (int i = 0; i < RowAmount; i++) { 	//Check for 4 in a column
			//Debug.Log ("Current = " + gameStatus [i,_Index]);
			if (sum>=4 || sum<=-4)
				return GameEnums.GameState.Winner;
			sum += gameStatus [i,_Index];
			if (i!=RowAmount-1)
				if (gameStatus [i,_Index] != gameStatus [i + 1,_Index] && (sum!=4 && sum!=-4))
					sum = 0;
			//Debug.Log ("Sum = " + sum);
		}
		if (sum >= 4 || sum <= -4)
			return GameEnums.GameState.Winner;
		else {
			sum = 0;
			for (int i = 0; i < ColumnAmount; i++){ //Check for 4 in a row
				//We'll check for a 4 in a row already existed before finishing going all of the row
				//Debug.Log("Current - " + gameStatus [_RowIndex,i]);
				if (sum>=4 || sum<=-4)
					return GameEnums.GameState.Winner;
				sum += gameStatus [_RowIndex,i];
				if (i != ColumnAmount-1)
					if (gameStatus [_RowIndex,i]!=gameStatus [_RowIndex,i+1] && (sum!=4 && sum!=-4))
						sum = 0;
				//Debug.Log ("Sum = " + sum);
			}
				
			if (sum >= 4 || sum <= -4)
				return GameEnums.GameState.Winner;
		}
		bool diagonal = CheckDiagonal(_RowIndex,_Index);
		if (diagonal)
			return GameEnums.GameState.Winner;
		if (turn == 42)
			return GameEnums.GameState.Tie;
		return GameEnums.GameState.NoWinner;
    }
	public void InitTurn()
	{
		for (int i = 0; i < RowAmount; i++) {
			SC_Globals.Instance.buttons ["fButton" + (i + 1).ToString ()].SetActive (false);
			SC_Globals.Instance.buttons ["Button" + (i + 1).ToString ()].SetActive (true);
		}
		SC_Globals.Instance.buttons ["Button7"].SetActive (true);
		SC_View.Instance.CleanBoard ();
		turn = 0;
		int _rand = Random.Range (0,2);
		if (_rand < 1)
			_rand = -1;
		currentState = (GameEnums.SlotState)_rand;
		if (currentState == GameEnums.SlotState.Red)
			Player = "Player 1";
		else
			Player = "PC";
		//SC_Globals.Instance.unityObjects ["Image_MatchOver"].SetActive (false);
		SC_View.Instance.SetImage ("Image_CurrentTurn",currentState);
		SC_View.Instance.SetText ("Text_CurrentTurn", "Current Turn:" + Player);
		Debug.Log ("Current Turn is: " + currentState);
		if (string.Compare (Player, "PC") == 0)
			DoComputerTurn ();
	}

    public void DoSlotLogic(int _Index)
    {
		turn++;
		string slot = "";
		int i=0;
		while( i < RowAmount ) {
			if (gameStatus[i,_Index]!=(int)GameEnums.SlotState.Empty)
				break;
			i++;
		}
		slot = "Image_Slot" + (i-1).ToString ()+_Index.ToString();
		if (i==1)
			SC_Globals.Instance.buttons ["Button"+(_Index+1).ToString()].GetComponent<Button> ().interactable = false;
		Debug.Log("Added " + (int)currentState + " to gameStatus[" +(i-1).ToString() + "," + _Index.ToString() +"]");  
		gameStatus [i-1,_Index] = (int)currentState;

		SC_View.Instance.SetImage (slot, currentState);
		GameEnums.GameState _currentGameState = CheckWinner (_Index,i-1);
		Debug.Log ("Current Game state: "+_currentGameState);
		if (_currentGameState == GameEnums.GameState.NoWinner) {
			PassTurn ();
		}
		else
			MatchOver (_currentGameState);
    }
		
    private void PassTurn()
    {
		if (turn == flip)
			DoFlip ();
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
		string button = "Bx`utton";
		int numOfButtons=8;
		if (vertical) {
			button = "fButton";
			numOfButtons = 7;
		}
		for (int i = 1; i < numOfButtons; i++) {
			SC_Globals.Instance.buttons [button + i.ToString()].GetComponent<Button>().interactable = false;
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
		
		for (int i = 0; i < 7; i++)
			for (int j = 0; j < ColumnAmount; j++) {
				gameStatus [i, j] = (int)GameEnums.SlotState.Empty;
				SC_Globals.Instance.buttons ["Button" + (i+1).ToString()].SetActive (true);
				SC_Globals.Instance.buttons ["Button" + (i+1).ToString()].GetComponent<Button>().interactable = true;
				if (i != 6) {
					SC_Globals.Instance.buttons ["fButton" + (i + 1).ToString ()].SetActive (false);
					SC_Globals.Instance.buttons ["fButton" + (i + 1).ToString ()].GetComponent<Button> ().interactable = true;
				}
				SC_Globals.Instance.unityObjects ["Image_Slot"+j.ToString()+i.ToString()].SetActive (false);
			}
		InitTurn ();
	}

	public void DoFlip(){
		int flips = Random.Range (1, 4);
		//Debug.Log ("New Angle: " + (flips * 90).ToString () + " Degrees");
		int[,] newGameStatus;
		int _ColumnAmount, _RowAmount;
		for (int _flips = 0; _flips < flips; _flips++){	//How many times to flip
			StartCoroutine (RotateRight(flips));
			_ColumnAmount = RowAmount;
			_RowAmount = ColumnAmount;
			newGameStatus = new int[_RowAmount, _ColumnAmount];
			for (int i = 0; i < RowAmount; i++) {
				for (int j = 0; j < ColumnAmount; j++) {
					//Debug.Log ("Old: [" + i + "," + j + "]");
					//Debug.Log ("new: [" + j + "," + (_ColumnAmount - i - 1) + "]");
					newGameStatus [j,(_ColumnAmount - i - 1)] = gameStatus [i, j];
					SC_Globals.Instance.unityObjects ["Image_Slot" + i.ToString () + j.ToString()].name = "Image_Slot" + j.ToString () + (_ColumnAmount - i - 1).ToString ();
				}
			}
			SC_View.Instance.CleanBoard ();
			ColumnAmount = _ColumnAmount;
			RowAmount = _RowAmount;
			gameStatus = null;
			gameStatus = new int[RowAmount, ColumnAmount];
			for (int i = 0; i < RowAmount; i++)
				for (int j = 0; j < ColumnAmount; j++) 
					gameStatus [i, j] = newGameStatus [i, j];
				
			newGameStatus = null;
			SC_Globals.Instance.updateUnityObjects ();
		}
		SC_Globals.Instance.unityObjects ["Board"].transform.rotation = Quaternion.Euler (transform.localRotation.x, transform.localRotation.y, (transform.localRotation.z + 1) * (flips*90*-1)); //for it to rotate clockwise
		//We go over all of the columns to see if the bottom of that row has an space TODO - better logic
		for (int j = 0; j < ColumnAmount; j++) 
			DoDrop (j);
		if (flips!=2){
			vertical = true;
			for (int i = 0; i < 7; i++)
				SC_Globals.Instance.buttons ["Button" + (i + 1).ToString ()].SetActive (false);
			for (int i = 0; i < 6; i++) 
				SC_Globals.Instance.buttons ["fButton" + (i + 1).ToString ()].SetActive (true);
		}

		Debug.Log ("Current Board:");
		for (int i = 0; i < RowAmount; i++)
			for (int j = 0; j < ColumnAmount; j++) {
				//Debug.Log ("[" + i + "," + j + "]=" + gameStatus [i, j]);
				SC_View.Instance.SetImage ("Image_Slot" + (i).ToString () + j.ToString (), (GameEnums.SlotState)gameStatus [i, j]);
			}
	}
	public void DoDrop(int _Index){
		bool _dropped = false;
		for (int i = 0; i < RowAmount; i++) {
			if ((gameStatus [i, _Index] != (int)GameEnums.SlotState.Empty) && !_dropped) {
				_dropped = true;
			}
			if ((gameStatus [i, _Index] == (int)GameEnums.SlotState.Empty) && _dropped) {
				for (int j = i; j > 0; j--) {
					gameStatus [j, _Index] = gameStatus [j-1, _Index];
					if (j == 1)
						gameStatus [j-1, _Index] = (int)GameEnums.SlotState.Empty;
				}
			}
		}
	}

	public void DoComputerTurn(){
		string button = "Button";
		int numOfButtons=7;
		if (vertical) {
			button = "fButton";
			numOfButtons = 6;
		}
		int column = Random.Range (1, numOfButtons+1);
		if (SC_Globals.Instance.buttons [button + column].GetComponent<Button> ().interactable == true)
			DoSlotLogic (column-1);
		else
			DoComputerTurn ();
	}

	private bool CheckRightBottomDiagonal(int i,int j){
		int count = 0;
		while (j > 0 && i>0 && count<4) {
			i--;
			j--;
			count++;
		}
		if ((i + 3) >= RowAmount || (j + 3) >= ColumnAmount)
			return false;
		Debug.Log ("[" + i + "," + j + "] + " + "[" + (i+1) + "," + (j+1) + "] + " + "[" + (i+2) + "," + (j+2) + "] + " + "[" + (i+3) + "," + (j+3) + "]");
		int sum = gameStatus [i, j] + gameStatus [i+1, j+1] + gameStatus [i+2, j+2] + gameStatus [i+3, j+3];
		if (sum == 4 || sum == -4)
			return true;
		return false;
	}

	private bool CheckRightTopDiagonal(int i,int j){
		int count = 0;
		while (j > 0 && i<RowAmount-1 && count<4) {
			i++;
			j--;
			count++;
		}
		if ((i - 3) < 0 || (j + 3) >= ColumnAmount)
			return false;
		Debug.Log ("[" + i + "," + j + "] + " + "[" + (i-1) + "," + (j+1) + "] + " + "[" + (i-2) + "," + (j+2) + "] + " + "[" + (i-3) + "," + (j+3) + "]");
		int sum = gameStatus [i, j] + gameStatus [i-1, j+1] + gameStatus [i-2, j+2] + gameStatus [i-3, j+3];
		if (sum == 4 || sum == -4)
			return true;
		return false;
	}

	private bool CheckLeftBottomDiagonal(int i,int j){
		int count = 0;
		while (j < ColumnAmount-1 && i>0 && count<4) {
			i--;
			j++;
			count++;
		}
		if ((i + 3) >= RowAmount || (j - 3) < 0 )
			return false;
		Debug.Log ("[" + i + "," + j + "] + " + "[" + (i+1) + "," + (j-1) + "] + " + "[" + (i+2) + "," + (j-2) + "] + " + "[" + (i+3) + "," + (j-3) + "]");
		int sum = gameStatus [i, j] + gameStatus [i+1, j-1] + gameStatus [i+2, j-2] + gameStatus [i+3, j-3];
		if (sum == 4 || sum == -4)
			return true;
		return false;
	}

	private bool CheckLeftTopDiagonal(int i,int j){
		int count = 0;
		while (j < ColumnAmount-1 && i<RowAmount-1 && count<4) {
			i++;
			j++;
			count++;
		}
		if ((i - 3) < 0 || (j - 3) < 0)
			return false;
		Debug.Log ("[" + i + "," + j + "] + " + "[" + (i-1) + "," + (j-1) + "] + " + "[" + (i-2) + "," + (j-2) + "] + " + "[" + (i-3) + "," + (j-3) + "]");
		int sum = gameStatus [i, j] + gameStatus [i-1, j-1] + gameStatus [i-2, j-2] + gameStatus [i-3, j-3];
		if (sum == 4 || sum == -4)
			return true;
		return false;
	}

	private bool CheckDiagonal(int i, int j){
		bool sumLb, sumLt, sumRb, sumRt;
		sumLb = sumLt = sumRb = sumRt=false;
		sumLb = CheckLeftBottomDiagonal (i, j);
		sumLt = CheckLeftTopDiagonal (i, j);
		sumRb = CheckRightBottomDiagonal (i, j);
		sumRt = CheckRightTopDiagonal (i, j);
		if (sumLb || sumLt || sumRb || sumRt)
			return true;
		return false;
	}

	IEnumerator RotateRight(int flips)
	{
		Debug.Log ("Rotating");
		GameObject Board = SC_Globals.Instance.unityObjects ["Board"];
		// add rotation step to current rotation.
		int degrees = -90 * flips;
		Quaternion rot = transform.localRotation;
		while ((int)Board.transform.localRotation.z < degrees) {
			rot.eulerAngles = new Vector3 (0.0f, 0.0f, Board.transform.localRotation.z - 10f);
			Board.transform.localRotation = rot;
			yield return new WaitForSeconds (1);
		}

		if ((int)Board.transform.localRotation.z < degrees) // for anti-clockwise
		{
			StartCoroutine (RotateRight(flips));
		}
	}
	/*IEnumerator rotateObjectAgain()
	{
		yield return new WaitForSeconds (0.2f);
		rotateObject();
	}*/
}
