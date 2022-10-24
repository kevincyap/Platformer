using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PidgeonFollow : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D coll;
    public bool active;
    public float speed;
    public float attackSpeed;
    private float distance;
    private float distanceY;
    public bool attacking = false;
    public float attackCD;
    public Vector2 lastX;
    GameObject player;
    Vector2 goal;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        coll = gameObject.GetComponent<BoxCollider2D>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("bird hit player");
        } else
        {
            Physics2D.IgnoreCollision(other.collider, coll);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, goal, attackSpeed * Time.deltaTime);
            if (transform.position.x == goal.x)
            {
                attacking = false;
            }
        }
        distance = Vector2.Distance(transform.position, player.transform.position);
        distanceY = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, player.transform.position.y));
        if (distance > 50)
        {
            transform.position = new Vector2(player.transform.position.x-15, player.transform.position.y - 15);
        }
        if (attacking == false && distance > 5) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        if (attacking == false && distance < 5 && distanceY > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, player.transform.position.y), speed * Time.deltaTime);
        }
        if (attacking == false && distance < 5 && distanceY < 1)
        {
            StartCoroutine(Wait());
            attacking = true;
            if (transform.position.x < player.transform.position.x)
            {
                goal = new Vector2(transform.position.x + 15, transform.position.y);
            } else
            {
                goal = new Vector2(transform.position.x - 15, transform.position.y);
            }
        }
        if (transform.position.x > lastX.x)
        {
            spriteRenderer.flipX = false;
        } else if (transform.position.x < lastX.x)
        {
            spriteRenderer.flipX = true;
        }
        lastX = new Vector2(transform.position.x, transform.position.y);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(attackCD);
    }
}
