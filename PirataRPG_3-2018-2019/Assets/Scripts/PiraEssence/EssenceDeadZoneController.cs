using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceDeadZoneController : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag.Contains("Essence"))
        {
            Destroy(other.gameObject);
        }
    }
}
