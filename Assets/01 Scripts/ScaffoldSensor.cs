using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldSensor : MonoBehaviour
{
    public bool dectected;
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dectected = true;
        }
    }*/
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Scaffold is Detected");
            dectected = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Scaffold is Outted");
            dectected = false;
        }
    }
}
