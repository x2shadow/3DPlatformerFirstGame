using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class SliderSound : MonoBehaviour
    {
        void Start()
        {
            Slider slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(AudioManager.Instance.SetSoundVolume);
        }
    }
}
