using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace Platformer
{
    public class PauseManager : ValidatedMonoBehaviour
    {
        [Header("References")]
        [SerializeField, Anywhere] InputReader input;
        [SerializeField] CameraManager cameraManager;
        [SerializeField] GameObject canvasPause;

        public bool isPaused;

        void OnEnable()
        {
            input.Pause += OnPause;
        }

        void OnDisable()
        {
            input.Pause -= OnPause;
        }

        void OnPause()
        {
            TogglePause();
        }

        void TogglePause()
        {
            if (!isPaused)
            {
                isPaused = true;
                canvasPause.SetActive(true);
                cameraManager.OnDisableMouseControlCamera();
            }
            else
            {
                isPaused = false;
                canvasPause.SetActive(false);
                cameraManager.OnEnableMouseControlCamera();
            }
        }
    }
}
