using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scaffold : MonoBehaviour
{

    private ScaffoldSensor sensor;
    private BoxCollider2D col;
    private 
    // Start is called before the first frame update
    void Start()
    {
        sensor = transform.GetChild(0).GetComponent<ScaffoldSensor>();
        col = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        col.isTrigger = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        col.isTrigger = true;
    }
    /*
    private void FixedUpdate()
    {
        if (sensor.dectected)
        {
            col.isTrigger = true;
        }
        else if (!sensor.dectected)
        {
            StartCoroutine(WaitAndDisableCollider());
            //Invoke("WaitDisable", 1.0f);
        }
    }
    private IEnumerator WaitAndDisableCollider()
    {
        yield return new WaitForSeconds(1.0f);
        col.isTrigger = false;
    }
    private void WaitDisable() 
    {
        col.isTrigger = false;
    }*/
}