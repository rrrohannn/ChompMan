using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementControl : MonoBehaviour
{
    PlayerInput playerInput;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;
    CharacterController characterController;
    // Animator animator;
    float rotationFactorPerFrame = 15.0f;
    float runMultiplier = 4.5f;
    // int isWalkingHash;
    // int isRunningHash;
    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        // animator = GetComponent<Animator>();

        // isWalkingHash = Animator.StringToHash("isWalking");
        // isRunningHash = Animator.StringToHash("isRunning");

        playerInput.CharControl.Move.started += onMovementInput;
        playerInput.CharControl.Move.canceled += onMovementInput; 
        playerInput.CharControl.Move.performed += onMovementInput; 
        playerInput.CharControl.Run.started += onRun;
        playerInput.CharControl.Run.canceled += onRun;
        playerInput.CharControl.Run.performed += onRun;
    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }   

    void handleRotation()
    {
        Vector3 postionToLookAt;
        postionToLookAt.x = currentMovement.x;
        postionToLookAt.y = 0.0f;
        postionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if(isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(postionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x * 3;
        currentMovement.z = currentMovementInput.y * 3;
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }
    
    // void handleAnimation()
    // {
    //     bool isWalking = animator.GetBool(isWalkingHash);
    //     bool isRunning = animator.GetBool(isWalkingHash);

    //     if(isMovementPressed && !isWalking)
    //     {
    //         animator.SetBool(isWalkingHash, true);
    //     }

    //     else if(!isMovementPressed && isWalking)
    //     {
    //         animator.SetBool(isWalkingHash, false);
    //     }

    //     if((isMovementPressed && isRunPressed) && !isRunning)
    //     {
    //         animator.SetBool(isRunningHash, false);
    //     }

    //     else if((!isMovementPressed || !isRunPressed) && isRunning)
    //     {
    //         animator.SetBool(isRunningHash, true);
    //     }
    // }
    void Update()
    {
        handleRotation();
        // handleAnimation();

        if(isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        } 
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }
    }

    void OnEnable()
    {
        playerInput.CharControl.Enable();
    }

    void OnDisable()
    {
        playerInput.CharControl.Disable();
    }
}
