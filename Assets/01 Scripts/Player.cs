using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    GameController gameController;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float triangleJumpForce = 5f;

    private bool detectedLeft;
    private bool detectedRight;
    private bool onMove;
    private bool canTriangleJump;

    [SerializeField] private ObjectSensor sensorG;
    [SerializeField] private ObjectSensor sensorLT;
    [SerializeField] private ObjectSensor sensorLB;
    [SerializeField] private ObjectSensor sensorRT;
    [SerializeField] private ObjectSensor sensorRB;

    public Vector2 angleLT = new Vector2(-1, 1);
    public Vector2 angleRT = new Vector2(1, 1);

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>().GetComponent<GameController>();
        if (gameController == null)
        {
            Debug.Log("Player: Cannot Find GameContorller");
        }
        else
        {
            Debug.Log("Player: Successfully find GameContorller");
        }
        rb = GetComponent<Rigidbody2D>();
        onMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.onGame)
        {
            DetectedOnRight();
            DetectedOnLeft();
            //Move
            if (onMove && Input.GetKey(KeyCode.A) && !detectedLeft)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
            else if (onMove && Input.GetKey(KeyCode.D) && !detectedRight)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && sensorG.dectected)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }//Triangle Jump
            else if (canTriangleJump && Input.GetKeyDown(KeyCode.Space) && !sensorG.dectected && detectedLeft && !detectedRight)
            {
                rb.velocity = angleRT * triangleJumpForce;
                StartCoroutine(WaitTriangleJump());
            }
            else if (canTriangleJump && Input.GetKeyDown(KeyCode.Space) && !sensorG.dectected && !detectedLeft && detectedRight)
            {
                rb.velocity = angleLT * triangleJumpForce;
                StartCoroutine(WaitTriangleJump());
            }
        }
        else if (!gameController.gameClear)
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (gameController.gameClear)
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            this.GetComponent<SpriteRenderer>().color = Color.blue;
        }

    }

    IEnumerator WaitTriangleJump()
    {
        onMove = false;
        yield return new WaitForSecondsRealtime(0.3f);
        onMove = true;
    }

    private void DetectedOnLeft()
    {
        if (sensorLT.dectected && sensorLB.dectected)
        {
            Debug.Log("Left is dectected!");
            detectedLeft = true;
        }
        else
        {
            Debug.Log("Left is outed!");
            detectedLeft = false;
        }
    }
    private void DetectedOnRight()
    {
        if (sensorRT.dectected && sensorRB.dectected)
        {
            Debug.Log("Right is dectected!");
            detectedRight = true;
        }
        else
        {
            Debug.Log("Right is outed!");
            detectedRight = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            canTriangleJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Wall")
        {
            canTriangleJump = false;
        }
    }

}
