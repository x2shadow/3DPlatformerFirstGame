using UnityEngine;

public class DisablePlatformRender : MonoBehaviour
{
    public Transform player; // Ссылка на персонажа
    public Transform playerCamera; // Ссылка на камеру

    private Renderer platformRenderer;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        player = GameObject.Find("Player").transform;
        playerCamera = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        // Отключаем рендеринг, если платформа между камерой и персонажем
        if (IsBetweenPlayerAndCamera())
        {
            platformRenderer.enabled = false;
        }
        else
        {
            platformRenderer.enabled = true;
        }
    }

    private bool IsBetweenPlayerAndCamera()
    {
        Vector3 playerPos = player.position;
        Vector3 cameraPos = playerCamera.position;
        Vector3 platformPos = transform.position;

        return platformPos.y > playerPos.y && platformPos.y < cameraPos.y;
    }
}