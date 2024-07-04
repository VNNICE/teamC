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
    private bool onGround;
    private bool canTriangleJump;

    private ObjectSensor sensorB;
    private ObjectSensor sensorLT;
    private ObjectSensor sensorLB;
    private ObjectSensor sensorRT;
    private ObjectSensor sensorRB;

    [SerializeField] private Vector2 angleLT = new Vector2(-1, 3);
    [SerializeField] private Vector2 angleRT = new Vector2(1, 3);

    Rigidbody2D rb;

    void Start()
    {
        //Sensor Bottom, 足場を活性化させるため
        sensorB = transform.Find("Sensor_G").GetComponent<ObjectSensor>();
        //SensorLeftTop, 左上
        sensorLT = transform.Find("Sensor_LT").GetComponent<ObjectSensor>();
        //LeftBottom, 左下
        sensorLB = transform.Find("Sensor_LB").GetComponent<ObjectSensor>();
        //Right Top, 右上
        sensorRT = transform.Find("Sensor_RT").GetComponent<ObjectSensor>();
        //Right Bottom, 右下
        sensorRB = transform.Find("Sensor_RB").GetComponent<ObjectSensor>();

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
        /*
        if (rb.velocity.y > 0 && sensorG.dectected) 
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }*/
        if (gameController.onGame)
        {
            DetectedOnRight();
            DetectedOnLeft();
            //Move
            if (onMove && Input.GetKey(KeyCode.A) && !detectedLeft)
            {
                //移動を押した時加速を初期化(三角飛び中など)
                rb.velocity = new Vector2(0, rb.velocity.y);
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
            else if (onMove && Input.GetKey(KeyCode.D) && !detectedRight)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && onGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }//Triangle Jump
            else if (canTriangleJump && Input.GetKeyDown(KeyCode.Space) && !onGround && detectedLeft && !detectedRight)
            {
                rb.velocity = angleRT * triangleJumpForce;
                StartCoroutine(WaitTriangleJump());
            }
            else if (canTriangleJump && Input.GetKeyDown(KeyCode.Space) && !onGround && !detectedLeft && detectedRight)
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

    //三角飛び後すぐの方向転換を防ぐ
    IEnumerator WaitTriangleJump()
    {
        onMove = false;
        yield return new WaitForSecondsRealtime(0.3f);
        onMove = true;
    }

    //左上と左下を感知した時活性化、これにより左が完全に触れた時にだけ三角飛び可
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
    //右上と右下を感知した時活性化
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //足場か床に着いたとき止まらせるため（これがないと三角飛び後も滑ってしまう）
        //Scaffoldは足場、足場だけの何かを作る可能性があるため分けています
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Scaffold"))
        {
            Debug.Log("Ground!");
            rb.velocity = new Vector2(0, 0);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.CompareTag("Scaffold"))
        {
            onGround = true;
        }
        //左右センサー感知だけではくっついてないのに三脚飛びができてしまうためCollisionを確認
        if (collision.gameObject.tag == "Wall")
        {
            canTriangleJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.CompareTag("Scaffold"))
        {
            onGround = false;
        }
        if (collision.gameObject.tag == "Wall")
        {
            canTriangleJump = false;
        }
    }

}
