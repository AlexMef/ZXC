using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObj;
    private Rigidbody2D rb2d;
    private BoxCollider2D col2D;
    private Vector2 initPosition;
    // use to calculate movement limits
    private float range;
    private float direction;
    private Vector2 left, right;
    private float maxVelocity = 64f;
    
    void Start()
    {
        col2D = gameObj.GetComponent<BoxCollider2D>();
        rb2d = gameObj.GetComponent<Rigidbody2D>();
        initPosition = transform.position;
        range = col2D.size.x * 2;
        direction = 1f;
        left = new Vector2(transform.position.x - range, transform.position.y);
        right = new Vector2(transform.position.x + range, transform.position.y);
        rb2d.velocity = new Vector2(8f, rb2d.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < left.x)
        {
            rb2d.velocity = new Vector2(8f * direction, rb2d.velocity.y);
        }

        if (transform.position.x > right.x)
        {
            rb2d.velocity = new Vector2(8f * -direction, rb2d.velocity.y);
        }

        if (rb2d.velocity.magnitude > maxVelocity)
        {
            rb2d.velocity = new Vector2(maxVelocity * Time.deltaTime, rb2d.velocity.y);
        }

    }
}
