using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ResourceDisplay resourceDisplay;

    private Timer gameTimer;

    private TimeSpan diffTimeBetweenPause = new TimeSpan(0, 0, 0); //The amount of time elapsed between pausing and resuming the app
    private DateTime lastPauseTime = DateTime.Now;

    //Testing OAuth
    private string userID = "apiuser";

    public Timer GameTimer
    {
        get { return gameTimer; }
        set { gameTimer = value; }
    }


	// Use this for initialization
	void Start ()
    {
        //OAuth
        StartCoroutine(OAuthHandler.GetAccessToken(userID =>
        {
            //string timerInfo = timerResult;

            //Populate the Timer
            //UnityEngine.Debug.Log(timerInfo);

        }));

        LoadTimer();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameTimer != null)
            gameTimer.Update();
	}

    void OnApplicationFocus(bool hasFocus) //Called every time the app loses or gains focus
    {
        if (!hasFocus) //If paused, store the date time and save the local timer file
        {
            lastPauseTime = DateTime.Now;
            SaveTimer();
        }
        else
        {
            //DEBUG NOTES: inactiveCount accumulated for inactive time becomes 0 eventually

            diffTimeBetweenPause = DateTime.Now - lastPauseTime;
            lastPauseTime = DateTime.Now;

            double inactiveCount = 0;

            if (gameTimer != null)
            {
                //Determine how many seconds passed, increment by +7% every second

                //Temporarily using the max size of float to cap idle time until a cap is determined for gameplay purposes
                float secondsElapsed = (float)diffTimeBetweenPause.TotalSeconds;

                if (diffTimeBetweenPause.TotalSeconds > float.MaxValue)
                    secondsElapsed = float.MaxValue;

                float wholeSeconds = Mathf.Floor(secondsElapsed);

                if (wholeSeconds > 0)
                    gameTimer.IncrementPerSecond += (1.07f * secondsElapsed);



                inactiveCount = Mathf.Floor(gameTimer.IncrementPerSecond * wholeSeconds);

                gameTimer.Count += inactiveCount;

                Debug.Log("Increment Per Second: " + gameTimer.IncrementPerSecond);
                Debug.Log("Elapsed time: " + secondsElapsed + " | Inactive resources earned: " + inactiveCount);
            }
            else
                Debug.Log("OnApplicationFocus::Error - gameTimer is null!");
            
        }
    }

    #region File I/O
    //LOCAL
    private void LoadTimer() //Load the Timer file and populate the display based on saved data
    {
        gameTimer = TimerIO.LoadCounter();

        if (gameTimer == null)
            gameTimer = new Timer(1, Time.deltaTime, true);

        resourceDisplay.InitDisplay();
    }

    private void SaveTimer()
    {
        TimerIO.SaveCounter(gameTimer);
    }



    #endregion
}
