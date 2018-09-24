using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 15;
    private Rigidbody rig;


    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Use this for initialization
    void Start () {
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        yaw += speedH * Input.GetAxis("Mouse X");
        
        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);


        Vector3 movement = transform.forward * speed * Time.deltaTime;

        if (Input.GetKey("z"))
        {
            rig.MovePosition(transform.position + movement);
        }
        

    }
}
