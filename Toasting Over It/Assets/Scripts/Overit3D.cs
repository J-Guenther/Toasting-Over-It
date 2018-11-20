using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overit3D : MonoBehaviour {


    [SerializeField] private float force;                 

    bool isGrounded = false;            // Is character grounded?
    bool jumpReady = false;             // Is character ready to jump?
    public bool jamRecharges = false;   // Is the jam recharging?
    bool jump = false;                  // Was jump pressed?
    Vector3 direction = new Vector3(0, 0, 0);

    float jumpTimer = 1f;               // Jump Timer
    [SerializeField] float jumpCooldown = 0.5f;          // Jump Cooldown

    private float stickTimer = 1f;       // How long is the character allowed to sticks?    
    [SerializeField] private float stickCooldown = .5f;   // Startvalue to reset timer

    private float rechargeJamTimer = 1f;
    [SerializeField] private float rechargeCooldown = 2f;   

    Stick3D jam;

    [SerializeField] private Vector3 torque = new Vector3(0f,0f,10f);

    public Rigidbody rb;

    public Transform startPosition = null;

    private bool controlEnabled = false;

    public bool firstClick = false;


    // Use this for initialization
    void Awake () {
        jam = gameObject.GetComponentInChildren<Stick3D>();
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        if(transform.position.y <= -10f)
        {
            ResetToastPosition();
        }

        // Allowed to jump?
        if (jumpTimer > 0 && jumpReady == false)
        {
            jumpTimer -= Time.deltaTime;
        } else if (jumpReady == false)
        {
            jumpReady = true;
        }

        // Is sticking possible?
        if (jam.sticks && stickTimer > 0 && jamRecharges == false)
        {
            stickTimer -= Time.deltaTime;
        } else if (jam.sticks)
        {
            jam.sticks = false;
            jamRecharges = true;
            //print("Jam needs to recharge!");
        }

        // Is recharging?
        if (jamRecharges && rechargeJamTimer > 0)
        {
            //print("RECHARGE!");

            jam.sticks = false;
            rechargeJamTimer -= Time.deltaTime;

        } else if (jamRecharges)
        {
            jamRecharges = false;
            rechargeJamTimer = rechargeCooldown;
            stickTimer = stickCooldown;
            //print("Jam recharged!");
        }

        bool lmb = Input.GetMouseButtonDown(0);
        float horizontalInput = Input.GetAxis("Horizontal");

        if (lmb && (isGrounded || jam.sticks) && jumpReady && controlEnabled)
        {
            firstClick = true;
            if (jam.sticks)
            {
                jam.sticks = false;
            }

            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);

            direction = objPos - transform.position;

            jump = true;

            jumpReady = false;
            jumpTimer = jumpCooldown;
            stickTimer = stickCooldown;
        }

        if (!isGrounded && !jam.sticks)
        {
            if (horizontalInput > 0)
            {
                rb.AddTorque(torque * (-1));
            } else if (horizontalInput < 0)
            {
                rb.AddTorque(torque * (1));
            }
        } 
	}

    private void FixedUpdate()
    {
        Jump(jump, direction);
        jump = false;
    }

    private void Jump(bool jump, Vector3 dir)
    {
        if (jump)
        {
            rb.AddForce(dir * force, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    public void ResetToastPosition()
    {
        transform.position = startPosition.position;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        rb.velocity = new Vector3(0, 0, 0);
    }

    public void ControlOnOff(bool onOff)
    {
        controlEnabled = onOff;
    }
}
