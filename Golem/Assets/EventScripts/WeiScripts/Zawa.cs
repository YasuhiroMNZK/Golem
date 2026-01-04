using UnityEngine;
using UnityEngine.UI;

public class Zawa : MonoBehaviour
{
    // フェードさせたい Image
    [SerializeField] private Image targetImage;

    // 各フェーズ時間の最小/最大(秒)
    [Header("Fade In (時間1)")]
    [SerializeField] private float fadeInMin = 0.5f;
    [SerializeField] private float fadeInMax = 1.5f;

    [Header("Hold (時間2)")]
    [SerializeField] private float holdMin = 1f;
    [SerializeField] private float holdMax = 3f;

    [Header("Fade Out (時間3)")]
    [SerializeField] private float fadeOutMin = 0.5f;
    [SerializeField] private float fadeOutMax = 1.5f;

    [Header("After Fade Out (時間4)")]
    [SerializeField] private float afterFadeOutMin = 0.5f;
    [SerializeField] private float afterFadeOutMax = 2f;

    [Header("初期完全透明維持時間（秒）")]
    [SerializeField] private float initialInvisibleMin = 0f;
    [SerializeField] private float initialInvisibleMax = 0f;
    private float initialInvisibleDuration;

    [Header("出現位置のランダム範囲 (Canvas 上の anchoredPosition)")]
    [SerializeField] private Vector2 positionMin = new Vector2(-200f, -200f);
    [SerializeField] private Vector2 positionMax = new Vector2(200f, 200f);

    [Header("Scale のランダム範囲")]
    [SerializeField] private float scaleMin = 0.8f;
    [SerializeField] private float scaleMax = 1.2f;

    // このサイクルで実際に使用する時間
    private float fadeInDuration;
    private float holdDuration;
    private float fadeOutDuration;
    private float afterFadeOutDuration;

    private float _timer;
    private bool _isPlaying;

    private RectTransform _rectTransform;

    // 初期完全透明フェーズ管理用
    private float _initialTimer;
    private bool _isInitialPhase;

    void Start()
    {
        if (targetImage != null)
        {
            _rectTransform = targetImage.GetComponent<RectTransform>();

            // 最初は完全に透明にしておく
            var c = targetImage.color;
            c.a = 0f;
            targetImage.color = c;

            // 最初に位置とスケールもランダムにしておく（必要なければ削除可）
            SetRandomPositionAndScale();

            SetupNewCycle();
            _timer = 0f;

            // 初期完全透明時間をランダム決定
            initialInvisibleDuration = Random.Range(initialInvisibleMin, initialInvisibleMax);
            if (initialInvisibleDuration < 0f) initialInvisibleDuration = 0f;

            // 初期完全透明フェーズ開始
            _initialTimer = 0f;
            _isInitialPhase = initialInvisibleDuration > 0f;
            _isPlaying = !_isInitialPhase;
        }
    }

    // 1サイクル分のランダム時間を決定
    private void SetupNewCycle()
    {
        fadeInDuration       = Random.Range(fadeInMin,       fadeInMax);
        holdDuration         = Random.Range(holdMin,         holdMax);
        fadeOutDuration      = Random.Range(fadeOutMin,      fadeOutMax);
        afterFadeOutDuration = Random.Range(afterFadeOutMin, afterFadeOutMax);

        // 万一 min == max == 0 で割り算エラーを避けるため、極端な値を防ぐ
        if (fadeInDuration <= 0f) fadeInDuration = 0.0001f;
        if (holdDuration < 0f) holdDuration = 0f;
        if (fadeOutDuration <= 0f) fadeOutDuration = 0.0001f;
        if (afterFadeOutDuration < 0f) afterFadeOutDuration = 0f;
    }

    private void SetRandomPositionAndScale()
    {
        if (_rectTransform == null) return;

        // 位置
        float x = Random.Range(positionMin.x, positionMax.x);
        float y = Random.Range(positionMin.y, positionMax.y);
        _rectTransform.anchoredPosition = new Vector2(x, y);

        // Scale
        float s = Random.Range(scaleMin, scaleMax);
        _rectTransform.localScale = new Vector3(s, s, 1f);
    }

    void Update()
    {
        if (targetImage == null)
            return;

        // 初期完全透明フェーズ
        if (_isInitialPhase)
        {
            _initialTimer += Time.deltaTime;

            // 念のため毎フレーム透明を維持
            var initCol = targetImage.color;
            initCol.a = 0f;
            targetImage.color = initCol;

            if (_initialTimer >= initialInvisibleDuration)
            {
                // 初期フェーズ終了 → 通常サイクル開始
                _isInitialPhase = false;
                _isPlaying = true;
                _timer = 0f;
            }
            return;
        }

        if (!_isPlaying)
            return;

        _timer += Time.deltaTime;

        // 時間4 + 時間1 + 時間2 + 時間3 の順で 1 ループ
        float totalDuration = afterFadeOutDuration + fadeInDuration + holdDuration + fadeOutDuration;
        if (_timer >= totalDuration)
        {
            // 1サイクル終了 → ランダムな時間・位置・スケールで新しいサイクルを開始
            _timer = 0f;
            SetupNewCycle();
            SetRandomPositionAndScale();
        }

        float alpha = 0f;

        if (_timer <= afterFadeOutDuration)
        {
            // 完全透明維持区間: 0 (時間4)
            alpha = 0f;
        }
        else if (_timer <= afterFadeOutDuration + fadeInDuration)
        {
            // フェードイン区間: 0 → 1 (時間1)
            float t = (_timer - afterFadeOutDuration) / fadeInDuration;
            alpha = Mathf.Clamp01(t);
        }
        else if (_timer <= afterFadeOutDuration + fadeInDuration + holdDuration)
        {
            // 表示維持区間: 1 (時間2)
            alpha = 1f;
        }
        else
        {
            // フェードアウト区間: 1 → 0 (時間3)
            float t = (_timer - afterFadeOutDuration - fadeInDuration - holdDuration) / fadeOutDuration;
            alpha = 1f - Mathf.Clamp01(t);
        }

        var col = targetImage.color;
        col.a = alpha;
        targetImage.color = col;
    }
}
