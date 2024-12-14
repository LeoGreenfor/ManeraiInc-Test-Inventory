using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public static class ServerRequestSender
{
    public static string ServerUrl = "https://wadahub.manerai.com/api/inventory/status";
    public static string AutorizationKey = "kPERnYcWAY46xaSy8CEzanosAgsWM84Nx7SKM4QBSqPq6c7StWfGxzhxPfDh8MaP";

    public static IEnumerator SendRequest(InventoryItem data)
    {
        string jsonData = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest(ServerUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {AutorizationKey}");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Request successful: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Request failed: " + request.error);
        }
    }
}
