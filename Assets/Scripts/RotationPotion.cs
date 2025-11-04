using UnityEngine;
using System.Collections;

public class RotationPotion : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject matchoman;
    [SerializeField] private GameObject backgroundClear;
    [SerializeField] private GameObject backgroundStart;
    [SerializeField] private AudioSource kira;
    [SerializeField] private AudioClip kiraA;
    [SerializeField] private AudioSource ban;
    [SerializeField] private AudioClip banA;
    private bool cleared = false;
    
    private static int numPotion;

    void Start()
    {
        numPotion = 0;
        targetObject.SetActive(true);
        matchoman.SetActive(false);
        backgroundClear.SetActive(false);
        backgroundStart.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突相手がターゲットの場合
        if (other.gameObject == targetObject)
        {
            numPotion++;
            Debug.Log(numPotion);
            StartCoroutine(RotateAndShrinkCoroutine());
            kira.PlayOneShot(kiraA);
        }
    }

    private IEnumerator RotateAndShrinkCoroutine()
    {
        float duration = 1f; // 回転時間
        float elapsed = 0f;

        // 回転の開始角度と終了角度（Z軸のみ）
        float startZ = transform.eulerAngles.z;
        float endZ = startZ + 360f;

        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            // Z軸回転
            float z = Mathf.Lerp(startZ, endZ, t);
            transform.eulerAngles = new Vector3(0, 0, z);

            // スケールを徐々に縮小
            transform.localScale = Vector3.Lerp(startScale, endScale, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 最終値を確実にセット
        transform.eulerAngles = new Vector3(0, 0, endZ);
        transform.localScale = endScale;
    }

    void Update()
    {
        if(!cleared && numPotion >= 5)
        {
            cleared = true;
            targetObject.SetActive(false);
            matchoman.SetActive(true);
            backgroundClear.SetActive(true);
            backgroundStart.SetActive(false);
            ban.PlayOneShot(banA);
        }
    }
}
