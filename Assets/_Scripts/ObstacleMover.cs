using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;
    [SerializeField] float movementFactor;
    [SerializeField] float oscilationPeriod;
    [SerializeField] float oscilationPeriodOffset;

    Vector3 startPos;
        
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        oscilationPeriodOffset = Random.value;
    }

    // Update is called once per frame
    void Update()
    {
        if(oscilationPeriod <= Mathf.Epsilon) { return; }

        movementFactor = 0.5f+(0.5f*Mathf.Sin(((Time.time/(oscilationPeriod+oscilationPeriodOffset))* Mathf.PI*2) - Mathf.PI / 2));
        transform.position = startPos + movementVector * movementFactor;
    }
}
