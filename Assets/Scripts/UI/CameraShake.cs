using ClassroomIGI.Main;
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeMagnitude = 0.2f;
    private Transform cameraTransform;

    private Vector3 originalPosition;

    void Start() => cameraTransform = transform;

    private void OnEnable()
    {
        GameService.Instance.EventService.OnEnemyRoar.AddListener(OnEnemyRoar);
    }

    private void OnDisable()
    {
        GameService.Instance.EventService.OnEnemyRoar.RemoveListener(OnEnemyRoar);
    }

    public void ShakeCamera()
    {
        originalPosition = cameraTransform.localPosition;
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            Vector3 randomPos = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            cameraTransform.localPosition = randomPos;

            elapsed += Time.deltaTime;

            yield return null;
        }

        cameraTransform.localPosition = originalPosition;
    }

    private void OnEnemyRoar(int _)
    {
        ShakeCamera();
    }
}
