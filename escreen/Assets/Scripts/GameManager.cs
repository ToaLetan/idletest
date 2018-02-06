using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private Timer gameTimer;

    TimeSpan diffTimeBetweenPause = new TimeSpan(0, 0, 0); //The amount of time elapsed between pausing and resuming the app
    DateTime lastPauseTime = DateTime.Now;

    public Timer GameTimer
    {
        get { return gameTimer; }
        set { gameTimer = value; }
    }


	// Use this for initialization
	void Start ()
    {
        gameTimer = new Timer(1, Time.deltaTime, true);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameTimer != null)
            gameTimer.Update();
	}

    void OnApplicationFocus(bool hasFocus) //Called every time the app loses or gains focus
    {

        if (!hasFocus) //If paused, store the date time
        {
            lastPauseTime = DateTime.Now;
        }
        else
        {
            diffTimeBetweenPause = DateTime.Now - lastPauseTime;
            lastPauseTime = DateTime.Now;

            if (gameTimer != null)
            {
                //Determine how many seconds passed, increment by +7% every second

                //Temporarily using the max size of float to cap idle time until a cap is determined for gameplay purposes
                float secondsElapsed = (float)diffTimeBetweenPause.TotalSeconds;

                if (diffTimeBetweenPause.TotalSeconds > float.MaxValue)
                    secondsElapsed = float.MaxValue;

                float wholeSeconds = Mathf.Floor(secondsElapsed);

                gameTimer.IncrementPerSecond = (gameTimer.IncrementPerSecond * 1.07f * wholeSeconds);

                gameTimer.Count += Mathf.Floor(gameTimer.IncrementPerSecond * wholeSeconds);
            }
            Debug.Log("Elapsed time: " + diffTimeBetweenPause.TotalSeconds);
        }
    }
}
