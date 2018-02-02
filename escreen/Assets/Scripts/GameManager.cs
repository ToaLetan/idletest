using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Timer gameTimer;

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
}
