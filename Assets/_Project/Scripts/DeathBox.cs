using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer
{
    public class DeathBox : MonoBehaviour
    {
        [SerializeField] CheckpointsManager checkpointsManager;

        void OnTriggerEnter(Collider other)
        {
            PauseManager.Instance.ShowLose();

            Vector3 spawnPosition = checkpointsManager.checkpointList[checkpointsManager.lastCheckpointIndex].gameObject.transform.position;
            if (other.CompareTag("Player")) other.transform.position = spawnPosition;
        }
    }
}
