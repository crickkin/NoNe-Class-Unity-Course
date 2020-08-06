using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text timer;
    public Text rightScore;
    public Text leftScore;

    public void UpdateTimer(int seconds)
    {
        int minutes = seconds / 60;
        seconds = seconds - minutes * 60;

        string minutesText = (minutes < 10) ? "0" + minutes.ToString() : minutes.ToString();
        string secondsText = (seconds < 10) ? "0" + seconds.ToString() : seconds.ToString();

        timer.text = minutesText + ":" + secondsText;
    }

    public void UpdateScore(int player1pts, int player2pts)
    {
        leftScore.text = player1pts.ToString();
        rightScore.text = player2pts.ToString();
    }
}
