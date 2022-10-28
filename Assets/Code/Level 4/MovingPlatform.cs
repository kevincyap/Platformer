using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public int startPoint;
    public Transform[] points;
    private int i;
    bool waiting = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f && !waiting)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
            waiting = true;
            StartCoroutine(Wait());
        }
        if (!waiting) {
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
    }
    IEnumerator Wait() {
        yield return new WaitForSeconds(2f);
        waiting = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
