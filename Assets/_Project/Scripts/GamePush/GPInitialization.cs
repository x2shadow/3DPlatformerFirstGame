using UnityEngine;
using GamePush;
using TMPro;

namespace Platformer
{
    public class GPInitialization : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI pauseText;

        private void Start()
        {
            if (GP_Language.Current() == Language.English) 
            {
                pauseText.text = "Pause";
                Debug.Log("Pause text changed to English");
            }
        }
    }
}
