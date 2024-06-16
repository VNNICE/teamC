using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSensor : MonoBehaviour
{
    GameController gameController;
    public bool dectected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dectected = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        dectected = false;
    }
}
