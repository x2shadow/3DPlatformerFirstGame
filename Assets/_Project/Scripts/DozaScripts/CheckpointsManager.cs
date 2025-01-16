using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CheckpointsManager : MonoBehaviour
    {
        [SerializeField]
        List<Checkpoint> checkpointList = new List<Checkpoint>();
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
            //play interst. ads? with some probability
            lastCheckpointIndex = checkpointList.IndexOf(checkpoint);

        }
        //Death Mechanics
        //void ... player teleport to last checkpoint
    }
}
