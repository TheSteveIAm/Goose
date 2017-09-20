using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookFeeder : MonoBehaviour
{
    private Looker looker;

    void Start()
    {
        looker = GetComponentInChildren<Looker>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag != tag)
        {
            looker.lookTarget = col.transform;
        }
    }
}
