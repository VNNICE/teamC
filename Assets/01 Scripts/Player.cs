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
    [SerializeField] private ObjectSensor sensorG;
    [SerializeField] private ObjectSensor sensorLT;
    [SerializeField] private ObjectSensor sensorLB;
    [SerializeField] private ObjectSensor sensorRT;
    [SerializeField] private ObjectSensor sensorRB;

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

        float inputX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
        if (sensorG.dectected && Input.GetKeyDown(KeyCode.Space) && !detectedLeft && !detectedRight) 
        {
            Jump();
        }
    }

    

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void DetectedOnLeft() 
    {
        if (sensorLT.dectected && sensorLB.dectected)
        {
            detectedLeft = true;
        }
        else
        {
            detectedLeft = false;
        }
    }
    private void DetectedOnRight()
    {
        if (sensorRT.dectected && sensorRB.dectected)
        {
            detectedRight = true;
        }
        else
        {
            detectedRight = false;
        }
    }

}
