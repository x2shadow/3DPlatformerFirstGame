using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer
{
    public class CheckpointsManager : MonoBehaviour
    {
        [SerializeField] List<Checkpoint> checkpointList = new List<Checkpoint>();
        public int lastCheckpointIndex;
        
        void Start()
        {
            var children = GetComponentsInChildren<Checkpoint>();
            foreach (Checkpoint child in children)
            {
                checkpointList.Add(child);
            }
        }

        public void UpdateLastCheckpoint(Checkpoint checkpoint)
        {
            if (checkpoint == checkpointList.Last()) PauseManager.Instance.ShowWin();

            lastCheckpointIndex = checkpointList.IndexOf(checkpoint);
        }
    }
}
