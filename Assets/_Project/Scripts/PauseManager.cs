using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace Platformer
{
    public class PauseManager : ValidatedMonoBehaviour
    {
        public static PauseManager Instance;

        [Header("References")]
        [SerializeField, Anywhere] InputReader input;
        [SerializeField] GameObject canvasPause;

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

        void TogglePause()
        {
            if (!isPaused)
            {
                isPaused = true;
                canvasPause.SetActive(true);
            }
            else
            {
                isPaused = false;
                canvasPause.SetActive(false);
            }
        }
    }
}
