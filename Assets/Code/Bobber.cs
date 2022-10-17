using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    public float height;
    public float speed;
    Vector3 initialPos;
    void Start() {
        initialPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = initialPos + new Vector3(0, Mathf.Sin(Time.time * speed), 0) * height;
    }
}
