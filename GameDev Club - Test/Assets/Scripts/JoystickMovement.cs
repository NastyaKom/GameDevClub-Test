using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class JoystickMovement : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBig;
    public Vector2 joystickVector;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    public float movement;

    // Start is called before the first frame update
    void Start()
    {
        joystickOriginalPos = joystickBig.transform.position;
        joystickRadius = joystickBig.GetComponent<RectTransform>().sizeDelta.y / movement;
    }

    public void PointerDown()
    {
        joystick.transform.position = Input.mousePosition;
        joystickBig.transform.position = Input.mousePosition;
        joystickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVector = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if (joystickDist < joystickRadius)
        {
            joystick.transform.position = joystickTouchPos + joystickVector * joystickDist;
        }

        else
        {
            joystick.transform.position = joystickTouchPos + joystickVector * joystickRadius;
        }
    }

    public void PointerUp()
    {
        joystickVector = Vector2.zero;
        joystick.transform.position = joystickOriginalPos;
        joystickBig.transform.position = joystickOriginalPos;
    }

}
