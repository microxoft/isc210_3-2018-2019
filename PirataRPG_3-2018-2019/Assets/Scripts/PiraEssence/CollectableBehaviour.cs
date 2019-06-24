using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehaviour : MonoBehaviour
{
    float accelX = -6f;
    float currentSpeedX = 0f;
    float deltaX;
    BoatPlayerBehaviour boatPlayerBehaviour;
    ScoreController scoreController;

    private void Awake()
    {
        boatPlayerBehaviour = GameObject.Find("Player").GetComponent<BoatPlayerBehaviour>();
        scoreController = GameObject.Find("Global Scripts").GetComponent<ScoreController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Vf = V0 + at
        currentSpeedX += accelX * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Xf = X0 + V0t + (at^2)/2
        deltaX = currentSpeedX * Time.deltaTime + accelX * Mathf.Pow(Time.deltaTime, 2) / 2;
        gameObject.transform.Translate(new Vector3(deltaX, 0f));

        // Vf = V0 + at
        currentSpeedX += accelX * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Player")
            return;

        if (gameObject.tag == "Enemy")
            boatPlayerBehaviour.OnHitted();
        else
            scoreController.AddEssence(gameObject.tag);
        Destroy(gameObject);
    }
}
