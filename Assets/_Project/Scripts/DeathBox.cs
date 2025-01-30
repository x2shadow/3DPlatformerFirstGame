using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer
{
    public class DeathBox : MonoBehaviour
    {
        [SerializeField] AdManager adManager;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) 
            {
                adManager.ShowFullscreen();
                GameManager.Instance.ShowLose();
                GameManager.Instance.MovePlayerToCheckpoint();
                AudioManager.Instance.PlaySoundLose();
            }
        }
    }
}
