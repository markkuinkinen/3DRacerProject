using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public float thrustSpeed;
    public float turnSpeed;
    public float hoverPower;
    public float hoverHeight;

    private float thrustInput;
    private float turnInput;
    private Rigidbody shipRigidbody;

    void Start()
    {
        shipRigidbody = GetComponent<Rigidbody>();

    }


    void Update()
    {
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        Debug.LogFormat("Thrust Input: {0}", thrustInput);
        Debug.LogFormat("Turn Input: {0}", turnInput);
    }


    void FixedUpdate()
    {
        //turning ship
        shipRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);

        //moving ship
        shipRigidbody.AddRelativeForce(0f, 0f, thrustInput * thrustSpeed);


        //OPEN EXERCISE - Editing a script
        if (Input.GetKey(KeyCode.LeftControl))
        {
            thrustSpeed = 12000;
        } else
        {
            thrustSpeed = 6000;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            hoverHeight = 6;
        } else
        {
            hoverHeight = 2;
        }

        //hovering
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverPower;
            shipRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }

    }
}