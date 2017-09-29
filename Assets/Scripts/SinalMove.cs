using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinalMove : MonoBehaviour {

    private float speed;
    private float offset;
    private Vector3 startPos;
    private Rigidbody body;

    void Start()
    {
        speed = Random.Range(1.0f, 5.0f);
        offset = Random.Range(0.0f, 1.0f);
        body = GetComponent<Rigidbody>();
        startPos = body.position;
    }
	
	// Update is called once per frame
	void Update () {
        body.MovePosition(startPos + new Vector3(Mathf.Sin((Time.time + offset) * speed), 0, 0));
	}
}
