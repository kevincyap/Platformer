using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocoTrigger : MonoBehaviour
{
    public CocoController Coco;
    public string triggerType;
    bool reusable = false;
    bool used = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && (reusable || !used))
        {
            Coco.TriggerAction(triggerType);
            used = true;
        }
    }
}
