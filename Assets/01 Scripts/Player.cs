using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float jumpForce = 10f;
    public float moveSpeed = 10f;
    private bool detectedLeft;
    private bool detectedRight;

    private bool onMove;

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
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectedOnRight();
        DetectedOnLeft();
        //Move
        if (Input.GetKey(KeyCode.A) && !detectedLeft)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) && !detectedRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && sensorG.dectected)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !sensorG.dectected && detectedLeft && !detectedRight)
        {
            rb.velocity = new Vector2(angleRT.x, angleRT.y) * jumpForce;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !sensorG.dectected && !detectedLeft && detectedRight)
        {
            rb.velocity = new Vector2(angleLT.x, angleLT.y) * jumpForce;
        }
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

}
