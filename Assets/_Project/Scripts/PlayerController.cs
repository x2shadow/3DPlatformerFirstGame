using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Cinemachine;
using KBCore.Refs;
using UnityEngine;
using Utilities;

namespace Platformer
{
    public class PlayerController : ValidatedMonoBehaviour
    {
        [SerializeField] PlatformCollisionHandler platformCollisionHandler;
        [SerializeField] PlatformMover platformMoverDirect;
        [SerializeField] MovingPlatform movingPlatform;

        [Header("References")]
        [SerializeField, Self] Rigidbody rb;
        [SerializeField, Self] GroundChecker groundChecker;
        [SerializeField, Self] Animator animator;
        [SerializeField, Anywhere] CinemachineFreeLook freeLookVCam;
        [SerializeField, Anywhere] InputReader input;

        [Header("Settings")]
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float rotationSpeed = 12f;
        [SerializeField] float smoothTime = 0.2f;

        [Header("Jump Settings")]
        [SerializeField] float jumpForce = 10f;
        [SerializeField] float jumpDuration = 0.5f;
        [SerializeField] float jumpCooldown = 0f;
        [SerializeField] float jumpMaxHeight = 2f;
        [SerializeField] float gravityMultiplier = 3f;

        Transform mainCam;

        float currentSpeed;
        float velocity;
        float jumpVelocity;

        Vector3 movement;

        List<Timer> timers;
        CountdownTimer jumpTimer;
        CountdownTimer jumpCooldownTimer;

        // Animator parameters
        static readonly int Speed = Animator.StringToHash("Speed");

        void Awake()
        {
            mainCam = Camera.main.transform;
            freeLookVCam.Follow = transform;
            freeLookVCam.LookAt = transform;

            // Invoke event when observed transform is teleported, adjusting freeLookVCam's position accordingly
            freeLookVCam.OnTargetObjectWarped(
                transform,
                transform.position - freeLookVCam.transform.position - Vector3.forward
            );

            rb.freezeRotation = true;

            // Setup timers
            jumpTimer = new CountdownTimer(jumpDuration);
            jumpCooldownTimer = new CountdownTimer(jumpCooldown);
            timers = new List<Timer>(capacity:2) { jumpTimer, jumpCooldownTimer };

            jumpTimer.OnTimerStop += () => jumpCooldownTimer.Start();
        }

        void Start()
        {
            input.EnablePlayerActions();
        }

        void OnEnable()
        {
            input.Jump += OnJump;
        }

        void OnDisable()
        {
            input.Jump -= OnJump;
        }

        void OnJump(bool performed)
        {
            if (performed && !jumpTimer.IsRunning && !jumpCooldownTimer.IsRunning && groundChecker.IsGrounded)
            {
                jumpTimer.Start();
            }
            else if (!performed && jumpTimer.IsRunning)
            {
                jumpTimer.Stop();
            }
        }

        void Update()
        {
            movement = new Vector3(input.Direction.x, 0f, input.Direction.y);

            HandleTimers();
            UpdateAnimator();
        }

        void FixedUpdate()
        {
            HandleMovement();
            ApplyPlatformVelocity();
            HandleJump();
            ApplyGravity();
        }

        void ApplyPlatformVelocity()
        {
            if (platformCollisionHandler.IsOnPlatform)
            {
                //rb.velocity += platformCollisionHandler.movingPlatform.platformVelocity;
                
                rb.velocity = new Vector3(rb.velocity.x + platformCollisionHandler.movingPlatform.platformVelocity.x,
                                            platformCollisionHandler.movingPlatform.platformVelocity.y,
                                            rb.velocity.z + platformCollisionHandler.movingPlatform.platformVelocity.z);
                                            
            }
        }

        void HandleTimers()
        {
            foreach (var timer in timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }

        void HandleJump()
        {
            if (HandleGroundedState()) return;

            if (jumpTimer.IsRunning)
            {
                // Устанавливаем начальную скорость прыжка
                jumpVelocity = Mathf.Sqrt(2 * jumpMaxHeight * Mathf.Abs(Physics.gravity.y));
                
                // Прекращаем прыжковый таймер, так как импульс уже задан
                jumpTimer.Stop();
            }

            if (platformCollisionHandler.IsOnPlatform)
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + jumpVelocity, rb.velocity.z);
            else
                rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }

        void ApplyGravity()
        {
            if (!groundChecker.IsGrounded && !jumpTimer.IsRunning)
            {
                jumpVelocity += Physics.gravity.y * gravityMultiplier * Time.fixedDeltaTime;
            }
        }

        bool HandleGroundedState()
        {
            if (groundChecker.IsGrounded && !jumpTimer.IsRunning)
            {
                jumpVelocity = 0f;
                //jumpVelocity = platformCollisionHandler.movingPlatform ? platformCollisionHandler.movingPlatform.platformVelocity.y : 0f;
                return true;
            }
            return false;
        }

        private void UpdateAnimator()
        {
            animator.SetFloat(Speed, currentSpeed);
        }

        private void HandleMovement()
        {
            // Rotate movement direction to match camera rotation
            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movement;

            if (adjustedDirection.magnitude > 0f)
            {
                HandleRotation(adjustedDirection);
                HandleHorizontalMovement(adjustedDirection);
                SmoothSpeed(adjustedDirection.magnitude);
            }
            else
            {
                SmoothSpeed(0f);

                // Reset horizontal velocity for a snappy stop
                rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            }
        }

        void HandleHorizontalMovement(Vector3 adjustedDirection)
        {
            // Move the player
            Vector3 velocity = adjustedDirection * moveSpeed * Time.fixedDeltaTime;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }

        void HandleRotation(Vector3 adjustedDirection)
        {
            // Adjust rotation to match movement direction
            var targetRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        void SmoothSpeed(float value)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
        }
    }
}
