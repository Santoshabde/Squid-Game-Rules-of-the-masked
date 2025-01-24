using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SNGames.CommonModule
{
    /// <summary>
    /// Simple topdown locomotion script for Hyper casual games - character faces the movement direction
    /// </summary>
    
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class SN_TopDownCharacterLocomotion_Type1 : MonoBehaviour
    {
        [SerializeField] private bool activateLocomotion = false;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Animator animator;
        [SerializeField] private float characterMoveSpeed;
        [SerializeField] private float characterTurnSpeed;
        [SerializeField] private string animationFloat_Run;
        [SerializeField] private Transform mainCamera;
        [SerializeField] private ParticleSystem runSmokeVFX;

        private void Awake()
        {
            if (mainCamera == null)
                mainCamera = Camera.main.transform;
        }

        private void Update()
        {
            if(IsRequiredComponentMissing())
            {
                Debug.LogError("Script: SN_TopDownCharacterLocomotion_Type1 -- Failed To Execute -- Components Missing!!");
                return;
            }

            if(!activateLocomotion)
            {
                return;
            }

            float rawHorizontalInput = Input.GetAxisRaw("Horizontal");
            float rawVerticalInput = Input.GetAxisRaw("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            MoveCharacter(rawHorizontalInput, rawVerticalInput, horizontalInput, verticalInput);
        }

        private void MoveCharacter(float rawHorizontalInput, float rawVerticalInput, float horizontalInput, float verticalInput)
        {
            Vector3 moveDirectionRaw = new Vector3(rawHorizontalInput, 0, rawVerticalInput);

            Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
            animator.SetFloat(animationFloat_Run, moveDirection.magnitude);

            Transform mainCamera = Camera.main.transform;
            float angleToRotate = mainCamera.eulerAngles.y;
            moveDirectionRaw = Quaternion.Euler(0, angleToRotate, 0) * moveDirectionRaw;

            moveDirectionRaw = moveDirectionRaw.normalized;

            characterController.Move(moveDirectionRaw * Time.deltaTime * characterMoveSpeed);

            if (moveDirectionRaw.magnitude > 0.1f)
            {
                transform.forward = Vector3.Slerp(transform.forward, moveDirectionRaw, Time.deltaTime * characterTurnSpeed);
            }

            if (runSmokeVFX != null)
            {
                if (moveDirectionRaw.magnitude > 0.1)
                {
                    if (!runSmokeVFX.isPlaying)
                        runSmokeVFX.Play();
                }
                else
                {
                    if (runSmokeVFX.isPlaying)
                        runSmokeVFX.Stop();
                }
            }
        }

        private void MoveCharacter(float horizontalInput, float verticalInput)
        {
            Vector3 moveDirectionRaw = new Vector3(horizontalInput, 0, verticalInput);

            Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
            animator.SetFloat(animationFloat_Run, moveDirection.magnitude);

            moveDirectionRaw = moveDirectionRaw.normalized;

            characterController.Move(moveDirectionRaw * Time.deltaTime * characterMoveSpeed);

            if (moveDirectionRaw.magnitude > 0.1f)
            {
                transform.forward = Vector3.Slerp(transform.forward, moveDirectionRaw, Time.deltaTime * characterTurnSpeed);
            }

            if (runSmokeVFX != null)
            {
                if(moveDirectionRaw.magnitude > 0.1)
                {
                    if (!runSmokeVFX.isPlaying)
                        runSmokeVFX.Play();
                }
                else
                {
                    if (runSmokeVFX.isPlaying)
                        runSmokeVFX.Stop();
                }
            }
        }

        private bool IsRequiredComponentMissing()
        {
            return mainCamera == null;
        }

        public void SetLocomotion(bool value)
        {
            this.activateLocomotion = value;
        }
    }
}