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
    private bool dazed;
    public float dazedTime = 0.5f;
    private float dazedTimer = 0f;

    public Transform baseTransform;

    void Start()
    {
        startRot = transform.rotation;
    }

    void Update()
    {
        currentRot = Quaternion.LookRotation((lookTarget != null) ? lookTarget.position - transform.position : baseTransform.forward * 5f);

        Quaternion baseRot = (baseTransform.rotation.y % 360 < 0) ? baseTransform.rotation * Quaternion.Euler(0, 360, 0) : baseTransform.rotation;

        if (Quaternion.Dot(baseRot, currentRot) < 0.6f)
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
        else
        {
            looking = true;

            if (!dazed)
            {
                if (lookTarget != null)
                {
                    transform.LookAt(lookTarget, Vector3.up);
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