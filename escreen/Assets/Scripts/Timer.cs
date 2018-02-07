using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Timer //Timer class to track resource accumulation. Flagged as Serializable for file I/O
{
    public const string TIMER_FILE_NAME = "counter";

    private const float SECOND = 1000.0f;

    //Resources
    private double count = 0;
    private double resourcesAmount = 0;
    private double spiritAmount = 0;
    private double mindsetAmount = 0;
    private double talentAmount = 0;

    private float currentTime = 0.0f;
    private float incrementPerTick   = 0.0f;
    private float incrementPerSecond = 1.0f;
    private float ticksPerSecond     = 1.0f;

    private bool running = false;

    //Resource Properties
    public double Count
    {
        get { return count; }
        set { count = value; }
    }

    public double ResourcesAmount
    {
        get { return resourcesAmount; }
        set { resourcesAmount = value; }
    }

    public double SpiritAmount
    {
        get { return spiritAmount; }
        set { spiritAmount = value; }
    }

    public double MindsetAmount
    {
        get { return mindsetAmount; }
        set { mindsetAmount = value; }
    }

    public double TalentAmount
    {
        get { return talentAmount; }
        set { talentAmount = value; }
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
        //TODO: Figure out how to track resources accumulated outside the app (between pauses/exits)
        // refer to OnApplicationPause(bool) and OnApplicationFocus(bool)

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

            //Debug.Log(count);
        }
    }

    private void IncreaseIncrement()
    {
        IncrementPerSecond = incrementPerSecond * 1.07f;
    }
}
