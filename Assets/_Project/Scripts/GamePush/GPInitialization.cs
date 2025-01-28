using UnityEngine;
using GamePush;

namespace Platformer
{
    public class GPInitialization : MonoBehaviour
    {
        private async void Start()
        {
            // Проверяем, готов ли плагин
            await GP_Init.Ready;
            OnPluginReady();
        }

        private void OnPluginReady()
        {
            Debug.Log("Plugin ready");
        }
    }
}
