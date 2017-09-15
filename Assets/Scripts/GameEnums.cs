using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnums : MonoBehaviour {

    public enum SlotState
    {
        Yellow=1, Red=-1, Empty=0
    };

    public enum GameState
    {
        NoWinner, Winner, Tie
    };

}
