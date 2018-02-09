using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

[Serializable]
public class Token
{
    public string access_token;
}

public static class OAuthHandler
{
    public static IEnumerator GetAccessToken(Action<string> result)
    {
        Dictionary<string, string> content = new Dictionary<string, string>();
        //Fill key and value
        content.Add("client_id", "297e7ed51-e6e2-4b99-9fbf-4127e3912667");
        content.Add("client_secret", "RadovTu5OaGe");
        content.Add("user", "apiuser");
        content.Add("password", "Eobap9Ou]Ce~Uu");

        UnityWebRequest request = UnityWebRequest.Post("http://stage.escreen.radiantexp.com/oauth/token", content);
        //Send request
        yield return request.SendWebRequest();

        if (!request.isNetworkError)
        {
            string resultContent = request.downloadHandler.text;
            Token tokenJson = JsonUtility.FromJson<Token>(resultContent);

            //Return result
            result(tokenJson.access_token);
        }
        else
        {
            //Return null
            result("");
        }
    }

    public static IEnumerator GetTimer(string user, Action<string> result)
    {
        Dictionary<string, string> content = new Dictionary<string, string>();
        //Fill key and value
        content.Add("UserID", user);
        content.Add("Count", "0");
        content.Add("Resources", "0");
        content.Add("Spirit", "0");
        content.Add("Mindset", "0");
        content.Add("Talent", "0");

        UnityWebRequest www = UnityWebRequest.Post("http://stage.escreen.radiantexp.com/oauth/debug", content);

        string token = null;

        yield return OAuthHandler.GetAccessToken((tokenResult) => { token = tokenResult; });

        www.SetRequestHeader("Authorization", "Bearer " + token);
        www.SendWebRequest();

        if (!www.isNetworkError)
        {
            string resultContent = www.downloadHandler.text;
            Timer resultTimer = JsonUtility.FromJson<Timer>(resultContent);

            //Populate the Timer here
            Debug.Log("LOADED TIMER: " + resultTimer.ToString());
            Debug.Log("Count: " + resultTimer.Count);
            Debug.Log("Resources: " + resultTimer.ResourcesAmount);
            Debug.Log("Spirit: " + resultTimer.SpiritAmount);
            Debug.Log("Mindset: " + resultTimer.MindsetAmount);
            Debug.Log("Talent: " + resultTimer.TalentAmount);

            //Return result
            result(resultTimer.ToString());
        }
        else
        {
            //Return null
            result("");
        }
    }
}