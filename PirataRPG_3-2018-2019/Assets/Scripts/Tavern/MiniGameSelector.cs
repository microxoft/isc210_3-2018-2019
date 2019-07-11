using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameSelector : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (!Input.GetButtonDown("Submit"))
            return;

        switch(gameObject.name)
        {
            case "PiraPongSelector":
                SceneManager.LoadScene("PiraPong");
                break;
            case "PiraEssenceSelector":
                SceneManager.LoadScene("PiraEssence");
                break;
        }
    }
}
