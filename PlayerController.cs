using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPK
{
    public class PlayerController : MonoBehaviour
    {
        // Player Movements
        public CharacterController characterController;  // Unity's Component that is responsible for many features on player movement.
        public KeyCode sprintButton;                    // Define it in Inspector Window.

        [SerializeField] protected float walkingSpeed = 4.0f;  // Suggested value, you can change it as you wish. 
        [SerializeField] protected float sprintSpeed = 6.0f;  // Suggested value, you can change it as you wish.


        // Applying gravity to the player
        [SerializeField] protected float gravityAcceleration = -20.0f;  // Suggested value, you can change it as you wish.
        private Vector3 gravityVector;


        // Reseting gravity when player is on ground
        [SerializeField] protected Transform groundCheck;
        [SerializeField] protected LayerMask groundLayerMask;
        [SerializeField] protected float groundCheckSphereRadius = 0.4f;  // Suggested value, you can change it as you wish.
        [SerializeField] protected bool isOnGround;


        // Jump Mechanics
        [SerializeField] protected float jumpHeight = 2.0f;  // Suggested value, you can change it as you wish. 
        public KeyCode jumpButton;  // Define it in Inspector Window.


        protected void Move()
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            float playerActualSpeed = this.walkingSpeed;  // By default, using walkingSpeed until sprinting.

            Vector3 movement = (transform.forward * verticalInput) + (transform.right * horizontalInput);


            if (this.groundCheck != null)
            {
                // It creates a sphere inside groundCheck GameObject to verify if player is on ground.
                this.isOnGround = Physics.CheckSphere(this.groundCheck.position, this.groundCheckSphereRadius, this.groundLayerMask);
            }
            else
            {
                Debug.LogErrorFormat("There's no groundCheck GameObject assigned. Please assign a groundCheck first.");
            }
            

            if (this.isOnGround && this.gravityVector.y < 0)  // this.gravityVector.y < 0 makes sure that the player can jump.
            {
                // By tests, setting the Y axis of gravity vector as -2 works pretty well in most cases.
                this.gravityVector.y = -2.0f;
            }
            else
            {
                this.gravityVector.y += this.gravityAcceleration * Time.deltaTime;
            }
              

            if (this.sprintButton != KeyCode.None)
            {
                if (Input.GetKey(this.sprintButton))
                {
                    // If player is pressing the sprint button...
                    playerActualSpeed = this.sprintSpeed;  
                }
            }
            else
            {
                Debug.LogErrorFormat("There's no sprintButton assigned. Please, assigned a sprintButton first.");
            }

            
            if (this.characterController != null)
            {
                this.characterController.Move(movement * playerActualSpeed * Time.deltaTime);
                this.characterController.Move(this.gravityVector * Time.deltaTime);
            }
            else
            {
                Debug.LogErrorFormat("There's no characterController assigned. Please assign a characterController.");
            }
        }
        

        protected void Jump()
        {
            if (this.jumpButton != KeyCode.None)
            {
                if (Input.GetKeyDown(this.jumpButton))
                {
                    this.gravityVector.y = Mathf.Sqrt(this.jumpHeight * -2 * this.gravityAcceleration);
                }
            }
            else
            {
                Debug.LogErrorFormat("There's no jumpButton assigned. Please assign a jumpButton first.");
            }
        }
    }
}
