using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMotor : MonoBehaviour
{
    private const float TIME_BEFORE_START = 2.5f;

    public Transform lookAt;
    public RectTransform virtualJoystickSpace;

    private Vector3 desiredPosition;
    private Vector3 offset;

    private Vector2 touchPosition;
    private float swipeResistance = 200f;

    private float smoothSpeed = 7.5f;
    public float distance = 15;
    public float yOffset = 3f;

    private float startTime = 0f;
    private bool isInsideVirtualJoystickSpace = false;

    private void Start()
    {
        offset = new Vector3(0, yOffset, -1f * distance);
        startTime = Time.time;
    }

    private void Update()
    {
        if(Time.time - startTime < TIME_BEFORE_START)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SlideCamera(true);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            SlideCamera(false);
        }

        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if(RectTransformUtility.RectangleContainsScreenPoint(virtualJoystickSpace, Input.mousePosition))
            {
                isInsideVirtualJoystickSpace = true;
            }
            else
            {
                touchPosition = Input.mousePosition;
            }
        }

        if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            if(isInsideVirtualJoystickSpace)
            {
                isInsideVirtualJoystickSpace = false;
                return;
            }

            float swipeForce = touchPosition.x - Input.mousePosition.x;
            if(Mathf.Abs(swipeForce) > swipeResistance)
            {
                if(swipeForce < 0)
                {
                    SlideCamera(true);
                }
                else
                {
                    SlideCamera(false);
                }
            }
        }        
        
    }

    private void FixedUpdate()
    {
        if(Time.time - startTime < TIME_BEFORE_START)
        {
            return;
        }
        desiredPosition = lookAt.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(lookAt.position + Vector3.up * yOffset);
    }

    public void SlideCamera(bool left)
    {
        if (left)
        {
            offset = Quaternion.Euler(0, 90, 0) * offset;
        }
        else
        {
            offset = Quaternion.Euler(0, -90, 0) * offset;
        }
    }
}
