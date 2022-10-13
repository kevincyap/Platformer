using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTrigger : MonoBehaviour
{
    public TrainController train;
    public int delay = 0;
    bool used = false;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !used)
        {
            train.ReleaseHold();
            train.StartMoving(delay);
            used = true;
        }
    }
}
