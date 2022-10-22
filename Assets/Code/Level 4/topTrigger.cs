using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class topTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private bool tagged = false;
    GameObject cam;
    public Vector3 coords;
    void Start()
    {
        cam = GameObject.Find("Main Camera");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && tagged == false)
        {
            other.transform.position = coords;
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + 11.5f, cam.transform.position.z);
            tagged = true;
        }
        else if (other.CompareTag("Player") && tagged == true)
        {
            SceneManager.LoadScene("Level 4");
        }
    }
}
