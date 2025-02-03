using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Platformer
{
    public class Checkpoint : MonoBehaviour
    {
        public List<ParticleSystem> _particles;

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
                this.gameObject.SetActive(false);

                foreach (ParticleSystem p in _particles)
                {
                    p.Play();
                }
            }
        }

    }
}
