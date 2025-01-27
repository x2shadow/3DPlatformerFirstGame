using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer
{
    public class CheckpointsManager : MonoBehaviour
    {
        [SerializeField] public List<Checkpoint> checkpointList = new List<Checkpoint>();
        [SerializeField] GameObject deathBox;
        public float deathBoxPositionY = 8f;
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
            if (checkpoint != checkpointList.First() && checkpoint != checkpointList.Last()) AudioManager.Instance.PlaySoundCheckpoint();
            if (checkpoint == checkpointList.Last()) { GameManager.Instance.ShowWin(); AudioManager.Instance.PlaySoundWin(); }

            lastCheckpointIndex = checkpointList.IndexOf(checkpoint);
            deathBox.transform.position = new Vector3(deathBox.transform.position.x,
                                                        checkpoint.gameObject.transform.position.y - deathBoxPositionY,
                                                        deathBox.transform.position.z);
        }
    }
}
