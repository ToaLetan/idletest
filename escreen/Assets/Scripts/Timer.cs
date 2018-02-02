using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private const float SECOND = 1000.0f;

    private double count = 0;

    private float currentTime = 0.0f;
    private float incrementPerTick   = 0.0f;
    private float incrementPerSecond = 1.0f;
    private float ticksPerSecond     = 1.0f;

    private bool running = false;

    public double Count
    {
        get { return count; }
    }

    public float IncrementPerSecond
    {
        get
        {
            return incrementPerSecond;
        }
        set //Calculate the incrementPerTick when setting incrementPerSecond
        {
            incrementPerTick = value / ticksPerSecond;
            incrementPerSecond = value;
        }
    }

    public bool Running { get; set; }

    public Timer(float ips, float tps, bool startUponCreation = false)
    {
        incrementPerSecond = ips;
        ticksPerSecond = 1000 / tps;

        running = startUponCreation;
    }

    public void Update()
    {
        if (running)
        {
            currentTime++;

            //Increment the rate of counting every second
            if (currentTime >= SECOND)
            {
                IncreaseIncrement();
                currentTime = 0;
            }

            if (incrementPerTick < 1.0f)
                count++;
            else
                count += incrementPerTick;

            Debug.Log(count);
        }
    }

    private void IncreaseIncrement()
    {
        IncrementPerSecond = incrementPerSecond * 1.07f;
    }
}
