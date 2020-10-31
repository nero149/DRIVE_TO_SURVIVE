using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrWheel : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Suspension")]
    public float restLength;
    public float springTravel;
    public float springStiffness;
    public float damperStiffness;

    private float minLength;
    private float maxLength;
    private float lastLength;
    private float springLength;
    private float springVelocity;
    private float springForce;
    private float damperForce;

    [Header("Wheel")]
    public float steerAngle;
    public float steerTime;

    private Vector3 suspensionForce;
    private Vector3 wheelVelocityLS; // Local Space
    private float Fx;
    private float Fy;
    private float wheelAngle;

    [Header("Wheel")]
    public float wheelRadius;

    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();

        minLength = restLength - springTravel;
        maxLength = restLength + springTravel;
    }

    void Update()
    {
        wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, steerTime * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);

        Debug.DrawRay(transform.position, -transform.up * (springLength + wheelRadius), Color.cyan);
    }
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength + wheelRadius))
        {
            // Calculate Spring Behaviour And Resulting Forces
            lastLength = springLength;
            springLength = hit.distance - wheelRadius;
            springLength = Mathf.Clamp(springLength, minLength, maxLength);
            springVelocity = (lastLength - springLength) / Time.fixedDeltaTime;
            springForce = (restLength - springLength) * springStiffness;
            damperForce = damperStiffness * (lastLength - springLength) / Time.fixedDeltaTime;
        }
        else
        {
            //Reset Values
            springLength = restLength;
            lastLength = restLength;
        }

        suspensionForce = (springForce + damperForce) * transform.up;

        wheelVelocityLS = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));
        Fx = Input.GetAxis("Vertical") * 0.5f * springForce;
        Fy = wheelVelocityLS.x * springForce;

        rb.AddForceAtPosition(suspensionForce + (Fx * transform.forward) + (Fy * transform.right), hit.point);
    }

}
