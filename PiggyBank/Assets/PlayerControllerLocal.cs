using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLocal : MonoBehaviour
{
    public float speed = 10;
    public float rotationSpeed = 60;
    public float dashTimer = 3;
    public float dashColdown = 0.5f;
    private Vector2 input;

    private Rigidbody rb;
    private bool dashing;
    private float origDashTimer;
    private float origDashColdown;
    private bool dashed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        origDashTimer = dashTimer;
        origDashColdown = dashColdown;
    }


    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
    }
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1") && !dashing && !dashed)
        {
            rb.velocity = new Vector3(input.y, 0, -input.x).normalized * (speed * 2.5f) * Time.deltaTime;
            dashing = true;
        }

        Dash();
        if (!dashing)
            rb.velocity = new Vector3(input.y, 0, -input.x).normalized * speed * Time.deltaTime;

        if (rb.velocity.magnitude != 0)
            transform.forward = rb.velocity;

    }

    void Dash()
    {
        if (dashed)
        {
            if (dashColdown > 0)
                dashColdown -= Time.deltaTime;
            else
            {
                dashColdown = origDashColdown;
                dashed = false;
            }
        }


        if (dashing)
        {
            dashed = true;
            if (dashTimer > 0)
                dashTimer -= Time.deltaTime;
            else
            {
                dashing = false;
                dashTimer = origDashTimer;
            }
        }
    }
}
