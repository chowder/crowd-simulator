using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class HttpManager : MonoBehaviour
{
    private const string BASE_URL = "localhost:5000/data?";

    private static HttpManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public static void Send(float time, Vector3 position)
    {
        // Build the URL
        string url = buildUrl(time, position);
        Instance.StartCoroutine(HttpGet(url));
    }

    private static string buildUrl(float time, Vector3 position)
    {
        return BASE_URL + "time=" + time + "&x=" + position.x + "&y=" + position.z;
    }

    static IEnumerator HttpGet(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        Debug.Log("HTTP Request Complete: " + www.downloadHandler.text);
    }
}
