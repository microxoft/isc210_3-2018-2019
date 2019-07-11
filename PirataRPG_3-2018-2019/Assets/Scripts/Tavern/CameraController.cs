using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    const float MIN = -2.2f, MAX = 2.2f;

    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, Mathf.Clamp(Player.transform.position.y, MIN, MAX), -10);
    }
}
