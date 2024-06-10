using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSensor : MonoBehaviour
{
    public bool dectected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{this.name} is dectected!");
        dectected = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"{this.name} is outted!");
        dectected = false;
    }
}
