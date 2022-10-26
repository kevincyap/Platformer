using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocoControllerCutscene : MonoBehaviour
{
    Animator anim;
    Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TriggerAction(string action) {
        if (action == "Stand") {
            anim.SetInteger("State", 1);
        } else if (action == "Lay") {
            anim.SetInteger("State", 0);
        }
    }
}
