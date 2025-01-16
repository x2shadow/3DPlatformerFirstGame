using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Checkpoint : MonoBehaviour
    {
        CheckpointsManager cm;
        bool marked;
        void Awake()
        {
            cm = this.GetComponentInParent<CheckpointsManager>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!marked)
            {
                marked = true;
                cm.UpdateLastCheckpoint(this);
            }
            //show rewarded go to next checkpoint(cm)
        }
        
    }
}
