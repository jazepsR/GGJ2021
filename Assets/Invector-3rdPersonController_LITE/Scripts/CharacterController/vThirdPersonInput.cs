﻿using System.Collections;
using UnityEngine;
using UniRx;
using GameSystem;

namespace Invector.vCharacterController
{
    public class vThirdPersonInput : MonoBehaviour
    {
        #region Variables       

        [Header("Controller Input")]
        public string horizontalInput = "Horizontal";
        public string verticallInput = "Vertical";
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;

        [Header("Camera Input")]
        public string rotateCameraXInput = "Mouse X";
        public string rotateCameraYInput = "Mouse Y";

        [HideInInspector] public vThirdPersonController cc;
        [HideInInspector] public vThirdPersonCamera tpCamera;
        [HideInInspector] public Camera cameraMain;

        #endregion
        [SerializeField] private Animator anim;
        [SerializeField] private float deathDelay = 1f;
        private bool couldJump = false;
        protected virtual void Start()
        {
            InitilizeController();
            InitializeTpCamera();
            GameEvents.PlayerDeath.Subscribe(dto => TriggerDeath()).AddTo(this);
        }

        private void TriggerDeath()
        {
            Debug.LogError("Player died!");
            anim.SetTrigger("death");
            StartCoroutine(DelayedDeath());
        }

        private IEnumerator DelayedDeath()
        {
            yield return new WaitForSeconds(deathDelay);
            Debug.LogError("Player died 2!");
            GameStateManager.PlayerDiedAnimationComplete();
        }

        protected virtual void FixedUpdate()
        {
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
        }

        protected virtual void Update()
        {
            InputHandle();                  // update the input methods
            cc.UpdateAnimator();            // updates the Animator Parameters
        }

        public virtual void OnAnimatorMove()
        {
            cc.ControlAnimatorRootMotion(); // handle root motion animations 
        }

        #region Basic Locomotion Inputs

        protected virtual void InitilizeController()
        {
            cc = GetComponent<vThirdPersonController>();

            if (cc != null)
                cc.Init();
        }

        protected virtual void InitializeTpCamera()
        {
            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<vThirdPersonCamera>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }
        }

        protected virtual void InputHandle()
        {
            MoveInput();
            CameraInput();
            SprintInput();
            StrafeInput();
            JumpInput();
            LandingCheck();
        }
        protected virtual void LandingCheck()
        {
            if(couldJump == false && JumpConditions())
            {
                anim.SetTrigger("landed");
            }
            couldJump = JumpConditions();
        }
        public virtual void MoveInput()
        {
            cc.input.x = Input.GetAxis(horizontalInput);
            cc.input.z = 0;// Input.GetAxis(verticallInput);
            anim.SetBool("walking", Mathf.Abs(cc.input.x) > 0.1f);
        }

        protected virtual void CameraInput()
        {
            if (!cameraMain)
            {
                if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
                else
                {
                    cameraMain = Camera.main;
                    cc.rotateTarget = cameraMain.transform;
                }
            }

            if (cameraMain)
            {
                cc.UpdateMoveDirection(cameraMain.transform);
            }

            if (tpCamera == null)
                return;

            var Y = Input.GetAxis(rotateCameraYInput);
            var X = Input.GetAxis(rotateCameraXInput);

            tpCamera.RotateCamera(X, Y);
        }

        protected virtual void StrafeInput()
        {
            if (Input.GetKeyDown(strafeInput))
                cc.Strafe();
        }

        protected virtual void SprintInput()
        {
            if (Input.GetKeyDown(sprintInput) || Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5) 
                || Input.GetKeyDown(KeyCode.Joystick2Button4) || Input.GetKeyDown(KeyCode.Joystick2Button5))
                cc.Sprint(true);
            else if (Input.GetKeyUp(sprintInput) || Input.GetKeyUp(KeyCode.Joystick1Button4) || Input.GetKeyUp(KeyCode.Joystick1Button5)
                || Input.GetKeyUp(KeyCode.Joystick2Button4) || Input.GetKeyUp(KeyCode.Joystick2Button5))
                cc.Sprint(false);

            anim.SetFloat("sprint", cc.inputMagnitude);
        }

        /// <summary>
        /// Conditions to trigger the Jump animation & behavior
        /// </summary>
        /// <returns></returns>
        protected virtual bool JumpConditions()
        {
            return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
        }

        /// <summary>
        /// Input to trigger the Jump 
        /// </summary>
        protected virtual void JumpInput()
        {


            if ((Input.GetKeyDown(jumpInput) || Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Joystick2Button0)) && JumpConditions())
            {
                cc.Jump();
               // StartCoroutine(JumpDelay(0.4f));
                anim.SetTrigger("jump");
                anim.ResetTrigger("landed");
            }
        }

        private IEnumerator JumpDelay(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            cc.Jump();
        }
        #endregion       
    }
}