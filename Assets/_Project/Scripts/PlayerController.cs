using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using KBCore.Refs;
using UnityEngine;
using Utilities;

namespace Platformer
{
    public class PlayerController : ValidatedMonoBehaviour
    {
        [Header("References")]
        [SerializeField, Self] Rigidbody rb;
        [SerializeField, Self] GroundChecker groundChecker;
        [SerializeField, Self] Animator animator;
        [SerializeField, Self] PlatformCollisionHandler platformCollisionHandler;
        [SerializeField, Anywhere] InputReader input;

        [Header("Settings")]
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float rotationSpeed = 12f;
        [SerializeField] float smoothTime = 0.2f;

        [Header("Jump Settings")]
        //[SerializeField] float jumpForce = 10f;
        [SerializeField] float jumpDuration = 0.5f;
        [SerializeField] float jumpCooldown = 0f;
        [SerializeField] float jumpMaxHeight = 2f;
        [SerializeField] float gravityMultiplier = 3f;
        [SerializeField] float jumpingPlatformBoost = 40f;
        bool isBoosted;

        Transform mainCam;

        float currentSpeed;
        float velocity;
        float boostVelocity;


        Vector3 movement;

        List<Timer> timers;
        CountdownTimer boostTimer;
        CountdownTimer jumpTimer;
        CountdownTimer jumpCooldownTimer;

        // Animator parameters
        static readonly int Speed = Animator.StringToHash("Speed");

        void Awake()
        {
            mainCam = Camera.main.transform;

            rb.freezeRotation = true;

            // Setup timers
            jumpTimer = new CountdownTimer(jumpDuration);
            boostTimer = new CountdownTimer(jumpDuration);
            jumpCooldownTimer = new CountdownTimer(jumpCooldown);
            timers = new List<Timer>(capacity:2) { jumpTimer, jumpCooldownTimer, boostTimer };

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
            if (!PauseManager.Instance.isPaused) 
                movement = new Vector3(input.Direction.x, 0f, input.Direction.y);
            else 
                movement = Vector3.zero;

            HandleTimers();
            UpdateAnimator();
        }

        void FixedUpdate()
        {
            HandleMovement();
            ApplyPlatformVelocity();
            HandleJump();
            ApplyJumpingPlatformBoost();
            ApplyGravity();
        }

        void ApplyPlatformVelocity()
        {
            if (!platformCollisionHandler.IsOnPlatform) return;

            Vector3 platformVelocity = platformCollisionHandler.movingPlatform?.platformVelocity 
                                    ?? platformCollisionHandler.fallingPlatform?.platformVelocity 
                                    ?? platformCollisionHandler.jumpingPlatform?.platformVelocity
                                    ?? platformCollisionHandler.rotatingPlatform?.platformVelocity 
                                    ?? Vector3.zero;

            // Синхронизируем вертикальную скорость только если игрок стоит на платформе и не прыгает
            bool isGroundedOnPlatform = groundChecker.IsGrounded && !jumpTimer.IsRunning;

            float newYVelocity = isGroundedOnPlatform ? platformVelocity.y : rb.velocity.y;

            rb.velocity = new Vector3(
                rb.velocity.x + platformVelocity.x,
                newYVelocity,
                rb.velocity.z + platformVelocity.z
            );
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
            if (isBoosted) return; // Если работает буст, игнорируем HandleJump

            //if (HandleGroundedState()) return;

            if (jumpTimer.IsRunning)
            {
                // Высчитываем фиксированную скорость прыжка
                float initialJumpVelocity = Mathf.Sqrt(2 * jumpMaxHeight * Mathf.Abs(Physics.gravity.y));

                // Устанавливаем вертикальную скорость игрока относительно платформы
                //float platformVerticalVelocity = platformCollisionHandler.movingPlatform?.platformVelocity.y ?? 0f;
                //if (platformVerticalVelocity < 0f) platformVerticalVelocity = 0f;
                float platformVerticalVelocity = platformCollisionHandler.movingPlatform?.speed ?? 0f;

                rb.velocity = new Vector3(rb.velocity.x, initialJumpVelocity + platformVerticalVelocity, rb.velocity.z);

                jumpTimer.Stop(); // Останавливаем таймер прыжка
            }
        }


        void ApplyGravity()
        {
            if (!groundChecker.IsGrounded)
            {
                // Добавляем гравитацию к текущей вертикальной скорости
                float gravityForce = Physics.gravity.y * gravityMultiplier * Time.fixedDeltaTime;
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + gravityForce, rb.velocity.z);
            }   
        }

        public void StartJumpingPlatformBoost()
        {
            if (!boostTimer.IsRunning)
            {
                boostTimer.Start();
            }
        }

        public void ApplyJumpingPlatformBoost()
        {
            if (boostTimer.IsRunning)
            {
                isBoosted = true; // Устанавливаем состояние буста
                
                // Устанавливаем начальную скорость
                boostVelocity = Mathf.Sqrt(2 * jumpingPlatformBoost * Mathf.Abs(Physics.gravity.y));

                rb.velocity = new Vector3(rb.velocity.x, boostVelocity, rb.velocity.z);
                // Прекращаем таймер, так как импульс уже задан
                boostTimer.Stop();
                isBoosted = false; // Сброс бустового состояния
            }
        }

        bool HandleGroundedState()
        {
            if (groundChecker.IsGrounded && !jumpTimer.IsRunning)
            {
                //jumpVelocity = 0f; 
                return true;
            }
            return false;
        }

        void UpdateAnimator()
        {
            animator.SetFloat(Speed, currentSpeed);
        }

        void HandleMovement()
        {
            // Rotate movement direction to match camera rotation
            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movement;
            // var adjustedDirection = movement;

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
