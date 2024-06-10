using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scaffold : MonoBehaviour
{
    private ObjectSensor sensor;
    // Start is called before the first frame update
    void Start()
    {
        sensor = transform.Find("Detected").GetComponent<ObjectSensor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sensor.dectected)
        {
            StartCoroutine(WaitSec());
        }
    }
    IEnumerator WaitSec()
    {
        this.GetComponent<BoxCollider2D>().isTrigger = true;
        yield return new WaitForSecondsRealtime(1);
        this.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
