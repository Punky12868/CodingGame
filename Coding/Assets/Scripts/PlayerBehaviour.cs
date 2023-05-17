using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Cinemachine;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    PowerCounter counter;
    RestartLevel restart;
    TextInput commands;
    Rigidbody2D rb;
    Vector3 movingDir;
    public LayerMask IgnoreMe;
    public float movingSpeed;

    public float dashSpeed;
    public float dashTime;

    public float jumpHeight;
    public float waitTime;
    public float distance;
    public int dashCounter;
    int dashID;
    bool isGrounded;
    bool isMoving;
    bool canJump;
    bool canDash;
    private void Awake()
    {
        dashCounter = 0;
        counter = FindObjectOfType<PowerCounter>();
        counter.player = this.gameObject.GetComponent<PlayerBehaviour>();
        canDash = true;
        rb = GetComponent<Rigidbody2D>();
        restart = FindObjectOfType<RestartLevel>();
        restart.playerBehaviour = this.gameObject.GetComponent<PlayerBehaviour>();
        restart.playerInGame = this.gameObject;
        commands = FindObjectOfType<TextInput>();
        commands.playerScript = this.gameObject.GetComponent<PlayerBehaviour>();
    }
    private void Update()
    {
        Grounded();
        if (isMoving)
        {
            transform.position += movingDir * movingSpeed * Time.deltaTime;
        }
    }
    public void Test(string command)
    {
        switch (command)
        {
            case "move right":
                isMoving = true;
                movingDir = Vector2.right;
                waitTime = 0;
                break;

            case "move left":
                isMoving = true;
                movingDir = Vector2.left;
                waitTime = 0;
                break;

            case "stop":
                isMoving = false;
                movingDir = Vector2.zero;
                rb.velocity = Vector2.zero;
                waitTime = 0;
                break;

            case "jump":
                if (isGrounded && canJump)
                {
                    canJump = false;
                    float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
                    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                }
                waitTime = 0.05f;
                break;

            case string s when s.StartsWith("wait"):
                float time;
                if (float.TryParse(s.Substring(4), NumberStyles.Float | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out time))
                {
                    waitTime = time;
                }
                break;

            case "dash right":
                if (dashCounter > 0 && canDash)
                {
                    dashID = 1;
                    StartCoroutine(DashConfig());
                }
                waitTime = 0.05f;
                break;

            case "dash left":
                if (dashCounter > 0 && canDash)
                {
                    dashID = 2;
                    StartCoroutine(DashConfig());
                }
                waitTime = 0.05f;
                break;
            case "kys":
                Application.Quit();
                break;
            default:
                break;
        }
    }
    void Grounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, ~IgnoreMe);
        if (hit.collider != null)
        {
            isGrounded = true;
            canJump = true;
        }
        else
        {
            isGrounded = false;
        }
        Debug.DrawLine(transform.position, new Vector3(0, transform.position.y + -distance), Color.red);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Dash"))
        {
            dashCounter++;
            Destroy(other.gameObject);
        }
    }

    IEnumerator DashConfig()
    {
        dashCounter--;
        canDash = false;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (dashID == 1)
        {
            rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
        }
        else if (dashID == 2)
        {
            rb.velocity = new Vector2(transform.localScale.x * -dashSpeed, 0f);
        }
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        yield return new WaitForSeconds(0.05f);
        canDash = true;
    }
}
