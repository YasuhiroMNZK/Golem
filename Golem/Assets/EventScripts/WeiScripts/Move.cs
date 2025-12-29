using UnityEngine;

public class Move : MonoBehaviour
{
    // インスペクタで設定可能な目標地点(アンカー座標)と移動時間
    [SerializeField] private Vector2 targetAnchoredPosition = new Vector2(0f, 100f);
    [SerializeField] private float duration = 2f; // 移動にかかる時間（秒）

    // UI 用 RectTransform
    private RectTransform rectTransform;

    private Vector2 startAnchoredPosition;
    private float startTime;

    // ObjMove 実行時にだけ移動させるためのフラグ
    private bool isMoving = false;
    // 一度だけ完全移動させるためのフラグ
    private bool hasMovedOnce = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        // 初期位置を記録（UI のアンカー座標）
        startAnchoredPosition = rectTransform.anchoredPosition;
        startTime = 0f;
        isMoving = false;
        hasMovedOnce = false;
    }

    void Update()
    {
        // ObjMove で移動が開始されていなければ何もしない
        if (!isMoving)
        {
            return;
        }

        // 線形補間で移動
        float elapsedTime = Time.time - startTime;
        float fractionOfJourney = elapsedTime / duration;

        if (fractionOfJourney >= 1f)
        {
            // 移動完了：最終位置に固定して終了
            rectTransform.anchoredPosition = targetAnchoredPosition;
            isMoving = false;
            hasMovedOnce = true;
            return;
        }

        rectTransform.anchoredPosition = Vector2.Lerp(
            startAnchoredPosition,
            targetAnchoredPosition,
            fractionOfJourney
        );
    }

    public void ObjMove()
    {
        // すでに一度移動し終わっていたら、何もしない
        if (hasMovedOnce)
        {
            return;
        }

        // 移動開始位置と開始時間を設定し、移動を開始
        startAnchoredPosition = rectTransform.anchoredPosition;
        startTime = Time.time;
        isMoving = true;
    }
}

