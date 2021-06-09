using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCharacterMoveController : MonoBehaviour
{
    [SerializeField] 
    private GameObject gameObj;
    private Rigidbody2D rb2d;
    private BoxCollider2D rb2dCollider;
    private MainCharacterController mainController;
    private float deltaHight = 0.03f;
    private float normalGravity = 1f;
    private float highGravity = 4f;
    private float maximumSpeed = 30f;


    // input buttons
    float moveDirection;
    bool jump;
    bool shift;

    private void Start()
    {
        mainController = gameObj.GetComponent<MainCharacterController>();
        rb2d = gameObj.GetComponent<Rigidbody2D>();
        rb2dCollider = gameObj.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        jump = Input.GetKeyDown(KeyCode.Space);
        shift = Input.GetKey(KeyCode.LeftShift);


        VerticalPositionCheck();
        if (moveDirection != 0f)
            Move();
        else if (moveDirection == 0f)
            rb2d.velocity = new Vector2(Vector3.zero.x, rb2d.velocity.y);
        if (jump) Jump();
    }

    private void FixedUpdate()
    {
        // normalize speed
        if (rb2d.velocity.magnitude > maximumSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * maximumSpeed;
        }



    }



    private void Move()
    { 
            if (!shift)
            {
                rb2d.velocity = new Vector2(moveDirection * mainController.HorizontalAcceleration, rb2d.velocity.y);
            }
            if (shift)
            {
                rb2d.velocity = new Vector2(moveDirection * mainController.HorizontalAcceleration * mainController.ShiftMagnitude, rb2d.velocity.y);
            }
   }


    private void Jump()
    {
        if (mainController.IsGrounded && mainController.AbilityDoubleJump)
        {
            mainController.canDoubleJump = true;
        }

        float reduce = 0.7f;
        if ((mainController.IsGrounded && !mainController.canDoubleJump)
            || (mainController.IsGrounded && mainController.canDoubleJump))
            rb2d.AddForce(new Vector2(rb2d.velocity.x, Vector2.up.y * mainController.VerticalAcceleration), ForceMode2D.Impulse);

        if (!mainController.IsGrounded && mainController.canDoubleJump)
        {
            rb2d.AddForce(new Vector2(rb2d.velocity.x, Vector2.up.y * mainController.VerticalAcceleration * reduce), ForceMode2D.Impulse);
            mainController.canDoubleJump = false;
        }
        if (!mainController.IsGrounded && !mainController.canDoubleJump)
        {
            // do nothing
        }
    }

    private void VerticalPositionCheck()
    {
        rb2dCollider.enabled = false;
        Vector2 leftRay = new Vector2(transform.position.x - rb2dCollider.size.x / 2, transform.position.y - rb2dCollider.size.y / 2);
        Vector2 midRay = new Vector2(transform.position.x, transform.position.y - rb2dCollider.size.y / 2);
        Vector2 rightRay = new Vector2(transform.position.x + rb2dCollider.size.x / 2, transform.position.y - rb2dCollider.size.y / 2);
        RaycastHit2D[] hits = {
            Physics2D.Raycast(leftRay, -Vector2.up),
            Physics2D.Raycast(midRay, -Vector2.up),
            Physics2D.Raycast(rightRay, -Vector2.up)
        };
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - rb2dCollider.size.y / 2), Vector2.down, Color.green);
        rb2dCollider.enabled = true;
        float distance = 0f;
        for (int i = 0; i < hits.Length; i++)
        {
            distance = hits[i].distance;
            if (distance <= deltaHight)
            {
                print($"hit {i} distance is {distance}");
                // double jump check
                mainController.IsGrounded = true;
                rb2d.gravityScale = normalGravity;
                return;
            }
            if (distance >= deltaHight)
            {
                print($"hit {i} distance is {distance}");
                mainController.IsGrounded = false;
                rb2d.gravityScale = highGravity;
                continue;
            }
        }
    }
}
