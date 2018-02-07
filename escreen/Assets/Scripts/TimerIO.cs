using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class TimerIO
{
    public static bool SaveCounter(Timer timerToSave)
    {
        //saveFile.SaveAllOptions(); //Save the options lists to the file as strings

        string fileName = Timer.TIMER_FILE_NAME;

        BinaryFormatter fileFormatter = new BinaryFormatter();

        using (FileStream fileStream = new FileStream(GetOptionsPath(fileName), FileMode.Create))
        {
            try
            {
                fileFormatter.Serialize(fileStream, timerToSave);
                fileStream.Close();
            }
            catch (Exception)
            { return false; }

        }
        return true;
    }

    public static Timer LoadCounter()
    {
        string fileName = Timer.TIMER_FILE_NAME;

        if (CheckForOptionsFile(fileName) == false)
        {
            Debug.Log("TimerIO::LoadCounter() - File " + fileName + " could not be found!");
            return null;
        }


        BinaryFormatter fileFormatter = new BinaryFormatter();

        using (FileStream fileStream = new FileStream(GetOptionsPath(fileName), FileMode.Open))
        {
            try
            {
                Timer loadedTimer = fileFormatter.Deserialize(fileStream) as Timer;

                //loadedFile.LoadAllOptions(); //Load the options lists pulled from the file

                return loadedTimer;
            }
            catch (Exception)
            {
                Debug.Log("TimerIO::LoadCounter() failed to load file: " + GetOptionsPath(fileName));
                return null;
            }
        }
    }

    public static bool CheckForOptionsFile(string fileName)
    {
        return File.Exists(GetOptionsPath(fileName));
    }

    private static string GetOptionsPath(string fileName)
    {
        return fileName + ".json";
    }
}
