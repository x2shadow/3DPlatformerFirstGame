using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class ButtonManager : MonoBehaviour
    {
        public void LoadFirstLevel()
        {
            //SceneManager.LoadScene("SampleScene");
            SceneManager.LoadScene("Level1");
        }

        public void LoadSecondLevel()
        {
            //Debug.Log("SceneManager.LoadScene(Level2)");
            SceneManager.LoadScene("Level2");
        }

        public void LoadThirdLevel()
        {
            //Debug.Log("SceneManager.LoadScene(Level3)");
            SceneManager.LoadScene("Level3");
        }

        public void ShowSettings()
        {
            Debug.Log("Open Settings");
        }
    }
}
