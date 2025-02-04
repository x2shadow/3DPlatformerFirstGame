using System;
using System.Collections;
using System.Collections.Generic;
using GamePush;
using TMPro;
using UnityEngine;

namespace Platformer
{
    public class GPLocalizationMainMenu : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI buttonFirstFloorText;
        [SerializeField] TextMeshProUGUI buttonSecondFloorText;
        [SerializeField] TextMeshProUGUI buttonThirdFloorText;
        [SerializeField] TextMeshProUGUI sliderMusicText;
        [SerializeField] TextMeshProUGUI sliderSoundText;

        void Start()
        {
            Language language = GP_Language.Current();

            Debug.Log("LANGUAGE: " + language);

            if (language == Language.English)
            {
                MainMenuTranslateToEnglish();
            }
        }

        void MainMenuTranslateToEnglish()
        {
            buttonFirstFloorText.text  = "First floor";
            buttonSecondFloorText.text = "Second floor";
            buttonThirdFloorText.text  = "Third floor";
            sliderMusicText.text       = "Music";
            sliderSoundText.text       = "Sounds";
        }

        public void ChangeToRu()
        {
            GP_Language.Change(Language.Russian);
            Language language = GP_Language.Current();

            Debug.Log("LANGUAGE: " + language);
        }
    }
}
