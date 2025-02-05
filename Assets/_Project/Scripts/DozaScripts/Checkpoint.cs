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

        async void OnTriggerEnter(Collider other)//async with animation
        {
            if (!marked)
            {
                marked = true;
                try
                {
                    cm.UpdateLastCheckpoint(this);
                }
                catch { }
                foreach (ParticleSystem p in _particles)
                {
                    p.Play();
                }
            }
            //show rewarded?
            await SizeAnimation();
        }

        CancellationTokenSource cts;
        public float animTime;
        public AnimationCurve scaleCurve;
        async UniTask SizeAnimation()
        {
            float time = 0;

            using (var _cts = new CancellationTokenSource())
            {
                cts = _cts;
                Vector3 initialScale = this.transform.localScale;
                while (time <= animTime)
                {
                    time += Time.deltaTime;
                    try
                    {
                        this.transform.localScale = initialScale * scaleCurve.Evaluate(time);
                    }
                    catch { }
                    await UniTask.Yield();//give control back to main thread
                }
            }
            this.gameObject.SetActive(false);
        }

    }
}
