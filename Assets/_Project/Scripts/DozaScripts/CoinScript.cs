using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Threading;


//spawn coins method
public class CoinScript : MonoBehaviour
{
    CancellationTokenSource _cts;

    async void Start()
    {
        _cts = new CancellationTokenSource();
        await IdleAnimation();
    }


    [Space]
    [Header("Idle Animation")]
    [SerializeField]
    float idleRotationSpeed = 3;
    [SerializeField]
    float frequency = 1;
    [SerializeField]
    float verticalAmplitude = 1;
    float vertOffset;

    async UniTask IdleAnimation()
    {
        using (var cts = new CancellationTokenSource())
        {
            float initialVertical = this.transform.position.y;
            _cts = cts;
            while (!cts.IsCancellationRequested)//
            {
                try
                {
                    this.transform.Rotate(Vector3.up, idleRotationSpeed);
                    vertOffset = verticalAmplitude * Mathf.Sin(Time.realtimeSinceStartup * frequency);
                    this.transform.position = new Vector3(this.transform.position.x, initialVertical + vertOffset, this.transform.position.z);
                }
                catch { }
                await UniTask.Yield();
            }
            this.gameObject.GetComponent<Collider>().enabled = false;
            PlayerData.AddCoins(1);
            int n = PlayerData.GetCoinsNumber();
            Debug.Log($"Добавлена 1 монета, Счёт:{n}");
            await PickUpAnimation();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        _cts.Cancel();
    }


    [Space]
    [Header("PickUpAnimation")]
    public AnimationCurve positionCurve;
    public AnimationCurve scaleCurve;
    public float animTime;
    public float magnitude;
    async UniTask PickUpAnimation()
    {
        float time = 0;

        using (var cts = new CancellationTokenSource())
        {
            _cts = cts;
            float initialVertical = this.transform.position.y;
            Vector3 initialScale = this.transform.localScale;
            while (time <= animTime)
            {
                time += Time.deltaTime;
                try
                {
                    this.transform.position = new Vector3(this.transform.position.x, initialVertical + magnitude * positionCurve.Evaluate(time), this.transform.position.z);
                    this.transform.localScale = initialScale * scaleCurve.Evaluate(time);
                }
                catch { }
                await UniTask.Yield();//give control back to main thread
            }
        }
        this.gameObject.SetActive(false);
    }
}
