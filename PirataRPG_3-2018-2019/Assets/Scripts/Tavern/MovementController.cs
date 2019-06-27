using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Vector3 maxWalkSpeed = new Vector3(2f, 2f), maxRunSpeed = new Vector3(5f, 5f), currentMovementSpeed;
    bool isAttacking, isRunning, lookingRight = true;
    Animator currentAnimator;

    private void Awake()
    {
        currentAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if(gameObject.name == "Player")
        {
            isRunning = Input.GetButton("Fire3");
            isAttacking = Input.GetButton("Fire1");

            currentMovementSpeed = new Vector3(Input.GetAxis("Horizontal") * (isRunning ? maxRunSpeed.x : maxWalkSpeed.x),
                Input.GetAxis("Vertical") * (isRunning ? maxRunSpeed.y : maxWalkSpeed.y));
        }
        currentAnimator.SetBool("IsAttacking", isAttacking);
        currentAnimator.SetFloat("Speed", currentMovementSpeed.magnitude);

        if (currentMovementSpeed.x < 0)
            lookingRight = false;
        else if (currentMovementSpeed.x > 0)
            lookingRight = true;

        gameObject.transform.rotation = new Quaternion(0, lookingRight ? 0 : 180, 0, 0);

        gameObject.GetComponent<Rigidbody>().velocity = currentMovementSpeed;
    }
}
