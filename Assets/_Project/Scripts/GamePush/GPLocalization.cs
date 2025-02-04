using UnityEngine;
using GamePush;
using TMPro;

namespace Platformer
{
    public class GPLocalization : MonoBehaviour
    {
        [Header("Canvas PAUSE")]
        [SerializeField] TextMeshProUGUI pauseText;
        [SerializeField] TextMeshProUGUI buttonContinueText;
        [SerializeField] TextMeshProUGUI buttonMainMenuText;
        [SerializeField] TextMeshProUGUI sliderMusicText;
        [SerializeField] TextMeshProUGUI sliderSoundText;
        [SerializeField] TextMeshProUGUI sliderCameraText;

        [Header("Canvas WIN")]
        [SerializeField] TextMeshProUGUI winText;
        [SerializeField] TextMeshProUGUI winButtonText;

        [Header("Canvas LOSE")]
        [SerializeField] TextMeshProUGUI loseText;
        [SerializeField] TextMeshProUGUI loseButtonText;

        [Header("Canvas GAMEPLAY")]
        [SerializeField] TextMeshProUGUI gameplayText;

        void Start()
        {
            Language language = GP_Language.Current();

            Debug.Log("LANGUAGE: " + language);

            if (language == Language.English) 
            {
                PauseMenuTranslateToEnglish();
                WinDialogTranslateToEnglish();
                LoseDialogTranslateToEnglish();
                GameplayTranslateToEnglish();
            }
        }

        void PauseMenuTranslateToEnglish()
        {
            pauseText.text = "Pause";
            buttonContinueText.text = "Continue";
            buttonMainMenuText.text = "Main menu";
            sliderMusicText.text = "Music";
            sliderSoundText.text = "Sounds";
            sliderCameraText.text = "Camera";
        }

        void WinDialogTranslateToEnglish()
        {
            winText.text = "Level completed!";
            winButtonText.text = "Main menu";
        }

        void LoseDialogTranslateToEnglish()
        {
            loseText.text = "Oops!";
            loseButtonText.text = "Continue";
        }

        void GameplayTranslateToEnglish()
        {
            gameplayText.text = "PAUSE";
        }
    }
}
