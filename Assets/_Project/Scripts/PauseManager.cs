using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class PauseManager : ValidatedMonoBehaviour
    {
        public static PauseManager Instance;

        [Header("References")]
        [SerializeField, Anywhere] InputReader input;
        [SerializeField] CameraManager cameraManager;
        [SerializeField] GameObject canvasPause;
        [SerializeField] GameObject canvasWin;
        [SerializeField] GameObject canvasLose;

        public bool isPaused;

        void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

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

        public void TogglePause()
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

        public void LoadMainMenuScene()
        {
            isPaused = false;
            SceneManager.LoadScene("MainMenu");
        }

        public void ShowWin()
        {
            isPaused = true;
            canvasWin.SetActive(true);
            cameraManager.OnDisableMouseControlCamera();
        }

        public void ShowLose()
        {
            isPaused = true;
            canvasLose.SetActive(true);
            cameraManager.OnDisableMouseControlCamera();
        }

        public void CloseLose()
        {
            canvasLose.SetActive(false);
            isPaused = false;
            cameraManager.OnEnableMouseControlCamera();
        }
    }
}
