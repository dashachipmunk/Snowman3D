using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanController : MonoBehaviour
{
    [Header("Movement config")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed;

    [Header("References")]
    [SerializeField] private CharacterController characterController;

    [Header("Gravity")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityScale;
    private float gravity;
    private void Update()
    {
        Movement();
        Rotate();
    }

    private void Movement()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * inputV + transform.right * inputH;
        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }

               
        if (characterController.isGrounded)
        {
            gravity = 0;
            if (Input.GetButton("Jump"))
            {
                gravity = jumpHeight;
            }
        }
        else
        {
            gravity += gravityScale * Physics.gravity.y * Time.deltaTime;
        }
        
        moveDirection.y = gravity;
        characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        float mouseHorizontal = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseHorizontal * rotationSpeed * Time.deltaTime);
    }
}
