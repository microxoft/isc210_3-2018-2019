using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreProxy
{
    public int IdScore;
    public string PlayerName;
    public float Score;
}

public class GlobalScript : MonoBehaviour
{
    public int LeftScore;
    public int RightScore;
    public TextMesh LeftScoreText;
    public TextMesh RightScoreText;
    private const string urlWebService = "https://isc2103-2018-2019rpgwebapi.azurewebsites.net/api/scores";
    private UnityWebRequest www;
    

    // Start is called before the first frame update
    void Start()
    {
        LeftScore = 0;
        RightScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2))
        {
            StartCoroutine(GetPlayerNames());
        }
        else if(Input.GetKeyDown(KeyCode.F3))
        {
            StartCoroutine(SendCurrentScore());
        }
    }

    IEnumerator GetPlayerNames()
    {
        www = UnityWebRequest.Get(urlWebService);
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
    }

    IEnumerator SendCurrentScore()
    {
        ScoreProxy newScore = new ScoreProxy();
        newScore.PlayerName = "Miguel de Cervantes";
        newScore.Score = LeftScore;

        string bodyData = JsonUtility.ToJson(newScore);
        www = UnityWebRequest.Put(urlWebService, bodyData);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
    }

    public void IncrementScore(bool IsLeftPlayer)
    {
        if (IsLeftPlayer)
        {
            LeftScore++;
            LeftScoreText.text = LeftScore.ToString();
        }
        else
        {
            RightScore++;
            RightScoreText.text = RightScore.ToString();
        }
    }
}
