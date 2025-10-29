using UnityEngine;
using System.Collections;

public class RotationPotion : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        //衝突相手がターゲットの場合
        if (!hasTriggered && other.gameObject == targetObject)
        {
            hasTriggered = true;
            StartCoroutine(RotateAndShrinkCoroutine());
        }
    }
    
    private IEnumerator RotateAndShrinkCoroutine()
    {
        //回転アニメーション
        float duration = 1f;
        float elapsed = 0f;
        Quaternion startRot = transform.rotation;
        Quaternion endRot = transform.rotation * Quaternion.Euler(0, 360, 0);

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endRot;

        //スケール0,0,0
        transform.localScale = Vector3.zero;
    }
}
