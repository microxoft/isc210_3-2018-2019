using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OpenChest()
    {
        animator.SetBool("isOpen", true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player" || !Input.GetButtonDown("Submit"))
            return;

        OpenChest();
    }
}
