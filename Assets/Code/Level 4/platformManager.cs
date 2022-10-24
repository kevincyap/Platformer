using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformManager : MonoBehaviour
{
    public static platformManager Instance = null;
    [SerializeField] GameObject platform;
    [SerializeField] GameObject slantedPlatform;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance = null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Start()
    {
        Instantiate(platform, new Vector2(6.14f, -82.14f), platform.transform.rotation);
    }

    IEnumerator SpawnPlatform(Vector2 spawnPosition)
    {
        yield return new WaitForSeconds(3f);
        Instantiate(platform, spawnPosition, platform.transform.rotation);
    }
}
