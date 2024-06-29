using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class DataController : MonoBehaviour
{
    public string serverURL = "https://nphy.cc:5580/score";

    public string GetJsonDataFile(int points)
    {
        PlayerData data = new PlayerData(points);
        return JsonUtility.ToJson(data);
    }

    public void SendJsonDataFile(string data)
    {
        StartCoroutine(SendDataToServer(data));
    }

    private IEnumerator SendDataToServer(string data)
    {
        using UnityWebRequest webRequest = new UnityWebRequest(serverURL, "POST");
        webRequest.SetRequestHeader("accept", "application/json");
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.SetRequestHeader("Authorization", "findTHeBuG123");
        var rawPlayerData = Encoding.UTF8.GetBytes(data);
        webRequest.uploadHandler = new UploadHandlerRaw(rawPlayerData);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
            Debug.Log("SEND SUCCESS");
        else if (webRequest.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log("PROTOCOLL ERROR");

        webRequest.Dispose();
    }
}

//CLASSE QUE CONTEM OS DADOS QUE SERÃO CONVERTIDOS EM JSON STRING E ENVIADOS PARA O SERVIDOR
[Serializable]
public class PlayerData
{
    public int score;

    public PlayerData(int points)
    {
        score = points;
    }
}