using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class DeathBox : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            PauseManager.Instance.ShowLose();
        }
    }
}
