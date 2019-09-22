using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public static float CUTTING_TIME = 4;
}

public enum PLAYER { PLAYER_1, PLAYER_2};
public enum FOOD_STATE { FRESH, CUT};
public enum ORDER_TYPE { EASY = 1, MEDIUM = 2, HARD = 3};
