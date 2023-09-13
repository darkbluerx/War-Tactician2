using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Joysticks")]
    public float moveSpeed = 10f;
    //public VariableJoystick variableJoystick;
    //public VariableJoystick variableJoystickRight;
    [Space]

    const float Min_follow_y_offset = 2f;
    const float Max_follow_y_offset = 12f;

    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;

    CinemachineTransposer cinemachineTransposer;
    Vector3 targetFollowOffset;

    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

    private void FixedUpdate()
    {
        //HandleMovement();
        //HandleRotation();
        //HandleZoom();
    }

    private void HandleMovement()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);

        //inputMoveDir = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;

        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    //private void HandleMovement()
    //{
    //    Vector3 inputMoveDir = new Vector3(0, 0, 0);

    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        inputMoveDir.z = +1f;
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        inputMoveDir.z = -1f;
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        inputMoveDir.x = -1f;
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        inputMoveDir.x = +1f;
    //    }
    //    float moveSpeed = 10f;

    //    Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
    //    transform.position += moveVector * moveSpeed * Time.deltaTime;
    //}

    private void HandleRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);
        //rotationVector = Vector3.up * variableJoystickRight.Horizontal;

        float rotationSpeed = 100f;
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    //private void HandleRotation()
    //{
    //    Vector3 rotationVector = new Vector3(0, 0, 0);

    //    rotationVector = Vector3.forward * variableJoystickRight.Vertical + Vector3.up * variableJoystickRight.Horizontal;
    //    //rotationVector = Vector3.forward * variableJoystick.Direction;
    //    float rotationSpeed = 100f;
    //    transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    //}


    private void HandleZoom()
    {
        float zoomAmount = 1f;
        if (Input.mouseScrollDelta.y > 0f)
        {
            targetFollowOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0f)
        {
            targetFollowOffset.y += zoomAmount;
        }

        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, Min_follow_y_offset, Max_follow_y_offset);

        float ZoomSpeed = 5f;
        cinemachineTransposer.m_FollowOffset =
            Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * ZoomSpeed);
    }
}