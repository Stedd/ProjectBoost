using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float thrustBase; 
    [SerializeField] float thrustModifier;

    [SerializeField] float rotationSpeedBase;
    [SerializeField] float rotationSpeedModifier;


    float thrustFinal;
    float rotationSpeedFinal;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
        thrustFinal = thrustBase * thrustModifier * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(0, thrustFinal, 0);
        }
    }
    void ProcessRotation()
    {
        rotationSpeedFinal = rotationSpeedBase * rotationSpeedModifier * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeTorque(0, 0, rotationSpeedFinal);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeTorque(0, 0, -rotationSpeedFinal);
        }
    }
}
