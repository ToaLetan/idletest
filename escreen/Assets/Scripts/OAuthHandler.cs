using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

[Serializable]
public class AccessToken
{
    public string access_token;
}

public static class OAuthHandler
{
    public static IEnumerator GetAccessToken(Action<string> result)
    {
        //DEBUG NOTES:
        //This is on the right track (general premise is to send the fields provided from Slack as the body for the POST, downloadHandler.text should return a JSON body if it's working correctly

        //Issue might not be a parsing issue, rather an issue with the POST not sending what's expected properly
        //Seems to be a Get and not after a POST

        //Using MultipartFormData (Unity suggested, recent)

        string CONTENT_TYPE = "application/x-www-form-urlencoded";

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("grant_type", "password", CONTENT_TYPE));
        formData.Add(new MultipartFormDataSection("client_id", "97e7ed51-e6e2-4b99-9fbf-4127e3912667", CONTENT_TYPE));
        formData.Add(new MultipartFormDataSection("client_secret", "RadovTu5OaGe", CONTENT_TYPE));
        formData.Add(new MultipartFormDataSection("username", "apiuser", CONTENT_TYPE));
        formData.Add(new MultipartFormDataSection("password", "Eobap9Ou]Ce~Uu", CONTENT_TYPE));

        //Using WWWForm (deprecated)
        /*
        WWWForm formData = new WWWForm();
        formData.AddField("grant_type", "password");
        formData.AddField("client_id", "97e7ed51-e6e2-4b99-9fbf-4127e3912667");
        formData.AddField("client_secret", "RadovTu5OaGe");
        formData.AddField("username", "apiuser");
        formData.AddField("password", "Eobap9Ou]Ce~Uu");
        */

        //Using Dictionary Key-value pairs provided from Slack
        /*
        Dictionary<string, string> formData = new Dictionary<string, string>();
        formData.Add("grant_type=", "password" + "&");
        formData.Add("client_id=", "97e7ed51-e6e2-4b99-9fbf-4127e3912667" + "&");
        formData.Add("client_secret=", "RadovTu5OaGe" + "&");
        formData.Add("username=", "apiuser" + "&");
        formData.Add("password=", "Eobap9Ou]Ce~Uu" + "&");
        */

        //String list
        /*
        String[] formData = new String[]
        {
            "grant_type=" + "password",
            "client_id=" + "97e7ed51-e6e2-4b99-9fbf-4127e3912667",
            "client_secret=" + "RadovTu5OaGe",
            "username=" + "apiuser",
            "password=" + "Eobap9Ou]Ce~Uu"
        };

        String postData = String.Join("&", formData);
        */


        //UnityWebRequest request = UnityWebRequest.Post("http://stage.escreen.radiantexp.com/oauth/token?_format=json", formData);

        UnityWebRequest request = UnityWebRequest.Post("http://stage.escreen.radiantexp.com/oauth/token", formData);
        request.uploadHandler.contentType = CONTENT_TYPE;

        //WWForm sending (deprecated)
        /*
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(formData.data);
        request.uploadHandler.contentType = "application/x-www-form-urlencoded";
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        */


        foreach (MultipartFormDataSection entry in formData)
            Debug.Log(entry.contentType);


        request.SetRequestHeader("Content-Type", CONTENT_TYPE);

        yield return request.SendWebRequest();

        //DEBUG NOTES:
        //This post is receiving a responseCode 405, meaning it isn't POSTing correctly.

        Debug.Log("UnityWebRequest::responseCode: " + request.responseCode);

        if (!request.isNetworkError) //If no errors returned, get the access token
        {
            Debug.Log("POST successful without nerwork errors");

            string resultContent = request.downloadHandler.text;

            if (resultContent.Contains("<!DOCTYPE html>"))
                Debug.Log("Returned an HTML page.");

            Debug.Log(resultContent);

            AccessToken tokenJson = JsonUtility.FromJson<AccessToken>(request.downloadHandler.text);

            Debug.Log(tokenJson);

            result(tokenJson.access_token);
        }
        
        else
        {
            //Return null
            Debug.Log("POST failed!");

            result("");
        }
    }
}