using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tele : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(col.transform.name);
        col.transform.Translate(Vector3.back * 48);
    }
}
