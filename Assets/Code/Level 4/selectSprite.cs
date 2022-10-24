using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] newSprite;
    int randomNumber;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        randomNumber = Random.Range(0, 7);
        spriteRenderer.sprite = newSprite[randomNumber];
        Destroy(gameObject, 10);
    }
    //void ChangeSprite()
    //{
      //  spriteRenderer.sprite = newSprite[randomNumber];
    //}

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back);
    }
}
