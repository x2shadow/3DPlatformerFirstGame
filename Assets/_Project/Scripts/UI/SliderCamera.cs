using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class SliderCamera : MonoBehaviour
    {
        CameraManager cameraManager;

        private const string SensitivityKey = "CameraSensitivity";

        void Start()
        {
            cameraManager = FindObjectOfType<CameraManager>();

            Slider slider = GetComponent<Slider>();

            float savedSensitivity = PlayerPrefs.GetFloat(SensitivityKey, 3f);
            slider.value = savedSensitivity;

            slider.onValueChanged.AddListener(cameraManager.SetCameraSensitivity);
            
            cameraManager.SetCameraSensitivity(slider.value);
        }
    }
}
