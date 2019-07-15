using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "EssenceItem" || !Input.GetButtonDown("Submit"))
            return;

        other.SendMessage("OpenChest");
    }
}
