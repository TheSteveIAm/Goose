using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looker : MonoBehaviour
{
    public float damping = 0.1f;
    public Transform lookTarget;
    private Quaternion startRot, currentRot;
    private bool looking = false;
    private float lookTimeout = 0.5f;
    private float lookTimer;

    // Use this for initialization
    void Start()
    {
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentRot = Quaternion.LookRotation(lookTarget.position - transform.position);

        if (Quaternion.Dot(startRot, currentRot) > -0.5f)
        {
            if (looking)
            {
                currentRot = startRot;
                transform.rotation = Quaternion.Slerp(transform.rotation, currentRot, Time.deltaTime * damping);

                lookTimer += Time.deltaTime;

                if (lookTimer >= lookTimeout)
                {
                    looking = false;
                }
            }
        }
        else
        {
            looking = true;

            //if (Vector3.Distance(transform.position, lookTarget.position) > 1f)
            //{
                transform.LookAt(lookTarget, Vector3.up);

            //}
            //else
            //{
            //    transform.rotation = Quaternion.Slerp(transform.rotation, currentRot, Time.deltaTime * damping);
            //}
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, lookTarget.position);
    }
}
