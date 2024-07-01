using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    GameController gameController;
    [SerializeField] private float jumpForce = 45f;
    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private float triangleJumpForce = 15f;

    private bool detectedLeft;
    private bool detectedRight;
    private bool onMove;
    private bool canTriangleJump;
    private bool onAir;

    private ObjectSensor sensorG;
    private ObjectSensor sensorLT;
    private ObjectSensor sensorLB;
    private ObjectSensor sensorRT;
    private ObjectSensor sensorRB;

    [SerializeField] private Vector2 angleLT = new Vector2(-1, 3);
    [SerializeField] private Vector2 angleRT = new Vector2(1, 3);

    Rigidbody2D rb;
    private void Awake()
    {
        
    }

    void Start()
    {
        sensorG = transform.FindChild("Sensor_G").GetComponent<ObjectSensor>();
        sensorLT = transform.FindChild("Sensor_LT").GetComponent<ObjectSensor>();
        sensorLB = transform.FindChild("Sensor_LB").GetComponent<ObjectSensor>();
        sensorRT = transform.FindChild("Sensor_RT").GetComponent<ObjectSensor>();
        sensorRB = transform.FindChild("Sensor_RB").GetComponent<ObjectSensor>();

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
        //éOäpîÚÇ—å„íÖínÇµÇΩèÍçávelocityÇèâä˙âª
        if (rb.velocity.y > 0 && sensorG.dectected) 
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (gameController.onGame)
        {
            DetectedOnRight();
            DetectedOnLeft();
            //Move
            if (onMove && Input.GetKey(KeyCode.A) && !detectedLeft)
            {
                //à⁄ìÆÇâüÇµÇΩéûvelocityÇèâä˙âª
                rb.velocity = new Vector2(0, rb.velocity.y);
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
            else if (onMove && Input.GetKey(KeyCode.D) && !detectedRight)
            {
                //à⁄ìÆÇâüÇµÇΩéûvelocityÇèâä˙âª
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
