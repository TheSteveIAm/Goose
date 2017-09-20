using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looker : MonoBehaviour
{
    public float damping = 0.1f;
    public Transform lookTarget;
    private Quaternion startRot, currentRot;
    private bool looking = false;
    private float lookTimeout = 0.1f;
    private float lookTimer;
    private bool dazed;
    public float dazedTime = 0.5f;
    private float dazedTimer = 0f;
    public float sightDistance = 25f;

    public Transform baseTransform;

    void Start()
    {
        startRot = transform.rotation;
    }

    void Update()
    {
        Vector3 lookPos = Vector3.zero;

        if (lookTarget != null)
        {

            currentRot = Quaternion.LookRotation(lookTarget.position - transform.position);

            lookPos = (lookTarget.position - transform.position).normalized;
        }
        else
        {
            currentRot = Quaternion.LookRotation(baseTransform.forward * 5f + new Vector3(0, 2, 0));
        }


        if (Vector3.Dot(baseTransform.forward, lookPos) > 0f)
        {
            looking = true;

            if (!dazed)
            {
                if (lookTarget != null && Vector3.Distance(transform.position, lookTarget.position) < sightDistance)
                {
                    transform.LookAt(lookTarget, Vector3.up);
                }
            }
        }
        else
        {
            if (looking)
            {
                currentRot = startRot; // to reset where the goose looks
                transform.rotation = Quaternion.Slerp(transform.rotation, currentRot, Time.deltaTime * damping);

                lookTimer += Time.deltaTime;

                if (lookTimer >= lookTimeout)
                {
                    looking = false;
                    lookTimer = 0;
                }
            }
        }

        if (dazed)
        {
            dazedTimer += Time.deltaTime;

            if (dazedTimer >= dazedTime)
            {
                dazed = false;
                dazedTimer = 0;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag != tag)
        {
            dazed = true;
        }
    }

    void OnDrawGizmos()
    {
        if (lookTarget != null)
        {
            Gizmos.DrawLine(transform.position, lookTarget.position);
        }
    }
}