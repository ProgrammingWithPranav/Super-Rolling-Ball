using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    private const float TIME_BEFORE_START = 3f;

    public float moveSpeed = 5f;
    public float drag = 0.5f;
    public float terminalRotationSpeed = 25f;
    public VirtualJoystick moveJoystick;

    public float boostSpeed = 5f;
    public float boostCooldown = 2;
    private float lastBoost;

    private Rigidbody controller;
    private Transform camTransform;

    private float startTime;

    void Start()
    {
        lastBoost = Time.time - boostCooldown;

        controller = GetComponent<Rigidbody>();
        controller.maxAngularVelocity = terminalRotationSpeed;
        controller.drag = drag;

        camTransform = Camera.main.transform;

        startTime = Time.time;
    }

    void Update()
    {
        if(Time.time - startTime < TIME_BEFORE_START)
        {
            return;
        }

        Vector3 dir = Vector3.zero;

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        if(dir.magnitude > 1)
        {
            dir.Normalize();
        }

        if(moveJoystick.InputDirection != Vector3.zero)
        {
            dir = moveJoystick.InputDirection;
        }

        Vector3 rotatedDir = camTransform.TransformDirection(dir);
        rotatedDir = new Vector3(rotatedDir.x, 0, rotatedDir.z);
        rotatedDir = rotatedDir.normalized * dir.magnitude;

        controller.AddForce(rotatedDir * moveSpeed);
    }

    public void Boost()
    {
        if(Time.time - startTime < TIME_BEFORE_START)
        {
            return;
        }
        if(Time.time - lastBoost > boostCooldown)
        {
        lastBoost = Time.time;
        controller.AddForce(controller.velocity.normalized * boostSpeed,ForceMode.VelocityChange);
        }
    }
}
