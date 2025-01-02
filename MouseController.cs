using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPK
{
    public class MouseController : MonoBehaviour
    {
        public GameObject playerContainer;  // The empty parent GameObject that stores the Player, the Main Camera and the Ground Check.

        [SerializeField] protected float mouseSensitivity = 100.0f;  // Suggested value, you can change it as you wish.
        [SerializeField] protected bool yAxisInverted = false;  // Suggested value, you can change it as you wish.
        [SerializeField] protected float minAngleClamp = -90.0f;  // Suggested value, you can change it as you wish.
        [SerializeField] protected float maxAngleClamp = 90.0f;  // Suggested value, you can change it as you wish.

        private float xRotation = 0;  // It controlls when you move up and down the mouse. 


        public static void LockAndHideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        public void Move()
        {
            float mouseX = Input.GetAxis("Mouse X") * this.mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * this.mouseSensitivity * Time.deltaTime;

            if (!this.yAxisInverted)
            {
                this.xRotation -= mouseY;  // Look up when you move the mouse up.
            }
            else
            {
                this.xRotation += mouseY;  // Look up when you move the mouse down.
            }

            // It prevents rotating the camera 360 degrees when looking up or down.
            this.xRotation = Mathf.Clamp(this.xRotation, this.minAngleClamp, this.maxAngleClamp);


            if (this.playerContainer != null)
            {
                this.playerContainer.transform.Rotate(Vector3.up * mouseX);  // It rotates the Player Container sidewards.
                this.transform.localRotation = Quaternion.Euler(this.xRotation, 0.0f, 0.0f);  // It rotates the Main Camera up and down.
            }
            else
            {
                Debug.LogErrorFormat("There's no playerContainer assingned. Please, assing a playerContainer.");
            }
        }

    }
}
