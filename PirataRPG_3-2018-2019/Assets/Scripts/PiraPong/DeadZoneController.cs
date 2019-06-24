using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    public GameObject Ball;
    public GameObject LeftPlayer;
    public GameObject RightPlayer;
    public GlobalScript GlobalScript;
    bool isLeftDeadZone;

    // Start is called before the first frame update
    void Start()
    {
        isLeftDeadZone = gameObject.name == "LeftDeadZone";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Ball")
            return;

        GlobalScript.IncrementScore(isLeftDeadZone);
        Ball.transform.SetParent(isLeftDeadZone ? RightPlayer.transform : LeftPlayer.transform);
        Ball.transform.localPosition = new Vector3(isLeftDeadZone ? -2.5f : 2.5f, 0);
        Ball.SendMessage("UpdateBallState");
    }
}
