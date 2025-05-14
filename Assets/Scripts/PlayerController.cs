using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 8f;
    public float runSpeed = 14f;
    public float jumpPower = 8f;
    public float gravity = 10f;
    public bool crouching;


    public float lookSpeed = 1f;
    public float lookXLimit = 60f;


    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;


    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        lookSpeed = PlayerPrefs.GetFloat("sens");
    }

    void Update()
    {
        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        crouching = Input.GetKey(KeyCode.LeftControl);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        Vector3 moveDir = Vector3.zero;
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.z = Input.GetAxis("Vertical");
        float curSpeed = canMove ? (isRunning ? runSpeed : walkSpeed) : 0;

        if (crouching)
        {
            curSpeed = curSpeed / 2;
            characterController.height = 1f;
        } else
        {
            characterController.height = 2f;
        }
        
        moveDir = moveDir.normalized * curSpeed;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * moveDir.z) + (right * moveDir.x);

        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion
    }
}