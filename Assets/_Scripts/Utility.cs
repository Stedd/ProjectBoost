using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    Mover rocketMover;
    CollisionHandler rocketCollisionHandler;
    void Start()
    {
        rocketMover             = GetComponent<Mover>();
        rocketCollisionHandler  = GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            rocketCollisionHandler.LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            rocketCollisionHandler.isInvincible = !rocketCollisionHandler.isInvincible;
        }
    }
}
