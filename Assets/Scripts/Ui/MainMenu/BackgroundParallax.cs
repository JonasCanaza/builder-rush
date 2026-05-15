using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]

public class BackgroundParallax : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speedX = 0.02f;
    [SerializeField] private float speedY = -0.02f;

    private RawImage rawImage;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        float newPosX = rawImage.uvRect.x + speedX * Time.deltaTime;
        float newPosY = rawImage.uvRect.y + speedY * Time.deltaTime;

        rawImage.uvRect = new Rect(newPosX, newPosY, rawImage.uvRect.width, rawImage.uvRect.height);
    }
}