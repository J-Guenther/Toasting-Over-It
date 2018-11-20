using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick3D : MonoBehaviour {

    public bool sticks = false;

    private Rigidbody rb;

	// Use this for initialization
	void Awake () {
        rb = gameObject.GetComponentInParent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        SetConstraints(sticks);     
    }

    private void OnTriggerEnter(Collider collision)
    {
        sticks = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        sticks = false;
    }
    
    public void SetConstraints(bool onOff)
    {
        if (onOff)
            rb.constraints = RigidbodyConstraints.FreezeAll;
        else
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }
}
