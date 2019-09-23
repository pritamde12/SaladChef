using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static UnityAction<PLAYER, float> UpdateScore;

    private void Awake()
    {
        instance = this;
    }
    [Header("MIN/MAX")]
    public Vector2 heightBounds;
    public Vector2 sideBounds;

    public TextMeshProUGUI scoreP1T;
    public TextMeshProUGUI scoreP2T;
    public TextMeshProUGUI timeP2T;
    public TextMeshProUGUI timeP1T;

    private float scorePlayer1;
    private float scorePlayer2;

    private float timerPlayer1;
    private float timerPlayer2;

    public const float totalTime = 120;

    public float ScorePlayer1 { get => scorePlayer1;
        private set
        {
            scorePlayer1 = value;
            scoreP1T.text = "" + scorePlayer1;
        }
    }
    public float ScorePlayer2 { get => scorePlayer2;
        private set
        {
            scorePlayer2 = value;
            scoreP2T.text = "" + scorePlayer2;
        }
    }

    public float TimerPlayer1 { get => timerPlayer1;
        private set
        {
            timerPlayer1 = value;
            timeP1T.text = "" + timerPlayer1;
        }
    }
    public float TimerPlayer2 { get => timerPlayer2;
        private set
        {
            timerPlayer2 = value;
            timeP2T.text = "" + timerPlayer2;
        }
    }

    private void Start()
    {
        TimerPlayer1 = TimerPlayer2 = totalTime;
        UpdateScore += ScoreChange;
        ScorePlayer1 = 0;
        ScorePlayer2 = 0;
        InvokeRepeating("TickTock",3, 1);
    }

    private void OnDisable()
    {
        UpdateScore -= ScoreChange;
    }


    void ScoreChange(PLAYER who, float addScore)
    {
        if(who == PLAYER.PLAYER_1)
        {
            ScorePlayer1 += addScore;
        }
        else
        {
            ScorePlayer2 += addScore;
        }
    }
    //update time
    void TickTock()
    {
        TimerPlayer1--;
        TimerPlayer2--;
    }
    private void Update()
    {
        
    }

    private void GameOver(PLAYER lostPlayer)
    {
        if(lostPlayer == PLAYER.PLAYER_1)
        {

        }
        else
        {

        }
    }
}
