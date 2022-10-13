using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public string enterAnim;
    public string exitAnim = "Default";
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && enterAnim != "")
        {
            CamController.instance.Play(enterAnim);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && exitAnim != "")
        {
            CamController.instance.Play(exitAnim);
        }
    }
}
