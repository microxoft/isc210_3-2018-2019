using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Scores
{
    public int IdScore;
    public string PlayerName;
    public double Score;
}

public class PuntajeWebServiceClient : MonoBehaviour
{
    UnityWebRequest www;
    GlobalScript globalScript;

    // Start is called before the first frame update
    void Awake()
    {
        globalScript = Camera.main.GetComponent<GlobalScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire3"))
        {
            StartCoroutine(SendPostRequest("https://isc2103-2018-2019rpgwebapi.azurewebsites.net/api/Scores"));
        }
    }

    IEnumerator SendGetRequest(string url)
    {
        www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isHttpError || www.isNetworkError)
            Debug.Log("No se logró obtener el listado de los jugadores.");
        else
            Debug.Log(www.downloadHandler.text);
    }

    IEnumerator SendPostRequest(string url)
    {
        Scores newScore = new Scores { PlayerName = "Fulano de Tal", Score = globalScript.LeftScore };

        www = UnityWebRequest.Put(url, JsonUtility.ToJson(newScore));
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);
    }
}