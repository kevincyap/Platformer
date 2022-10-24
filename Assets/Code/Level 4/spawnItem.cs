using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnItem : MonoBehaviour
{
    public float time = 30f;
    public float otime;
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        otime = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0f) { 
            Instantiate(obj);
            obj.transform.position = transform.position;
            time = otime;
    }
        time -= 0.1f;
    }
}
