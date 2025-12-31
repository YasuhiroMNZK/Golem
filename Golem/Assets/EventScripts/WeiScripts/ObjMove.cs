using UnityEngine;

public class ObjMove : MonoBehaviour
{
    /// <summary>
    /// 追従元となるオブジェクト（この Transform の移動に合わせて動く）
    /// </summary>
    public Transform target;

    /// <summary>
    /// Animator がアタッチされているオブジェクト
    /// （このスクリプトとは別オブジェクトの場合に指定）
    /// </summary>
    [SerializeField] private GameObject animatorObject;

    Animator animCtrl;

    /// <summary>
    /// 追従する軸を制御するフラグ
    /// </summary>
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = false;
    [SerializeField] private bool followZ = false;

    [SerializeField] private bool sameDirection = false;

    /// <summary>
    /// 追従の強さ（1 で等倍、0.5 で半分など）
    /// </summary>
    [Range(0f, 2f)]
    public float followFactor = 1f;

    [SerializeField] private float firstPushDistance = 0.1f; // 1回目の押し出し距離
    private bool hasPushedThisPunch = false;

    /// <summary>
    /// target の前フレーム位置
    /// </summary>
    private Vector3 _lastTargetPosition;

    /// <summary>
    /// 有効化されるたびに呼ばれ、追従情報をリセットする
    /// </summary>
    private void OnEnable()
    {
        if (target != null)
        {
            _lastTargetPosition = target.position;
        }
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        // Animator を取得（別オブジェクトから）
        if (animatorObject != null)
        {
            animCtrl = animatorObject.GetComponent<Animator>();
        }
        else
        {
            // フォールバック：同じオブジェクトから取得（従来挙動）
            animCtrl = GetComponent<Animator>();
        }

        // Start 時も同じリセット処理を行う
        OnEnable();
    }

    /// <summary>
    /// 毎フレーム、target の移動量に合わせてこのオブジェクトを移動させる
    /// </summary>
    private void LateUpdate()
    {
        if (target == null) return;
        if (animCtrl == null) return;

        bool animIsPunching = animCtrl.GetBool("isPunching");

        if (!animIsPunching)
        {
            // 攻撃が終了したらフラグをリセットし、基準位置を更新
            hasPushedThisPunch = false;
            _lastTargetPosition = target.position;
            return;
        }

        // 攻撃中

        Vector3 delta = target.position - _lastTargetPosition;

        if (!followX) delta.x = 0f;
        if (!followY) delta.y = 0f;
        if (!followZ) delta.z = 0f;

        // プレイヤーからこのオブジェクトへの方向を計算
        Vector3 toSelf = transform.position - target.position;

        // 1. この攻撃中にまだ一度も押し出しておらず & delta ≒ 0 の場合、1回目だけ押し出す
        if (!hasPushedThisPunch && delta.sqrMagnitude <= Mathf.Epsilon && toSelf.sqrMagnitude > Mathf.Epsilon)
        {
            Vector3 pushDir = (-toSelf).normalized;   // プレイヤーからオブジェクトへ押し出す方向
            Vector3 push = pushDir * firstPushDistance * followFactor;

            if (!followX) push.x = 0f;
            if (!followY) push.y = 0f;
            if (!followZ) push.z = 0f;

            transform.position -= push;

            hasPushedThisPunch = true;
            _lastTargetPosition = target.position;
            return;
        }

        // 2. 通常の delta による追従
        if (delta.sqrMagnitude <= Mathf.Epsilon)
        {
            _lastTargetPosition = target.position;
            return;
        }

        float dot = Vector3.Dot(delta.normalized, toSelf.normalized);

        if (sameDirection)
        {
            if (dot > 0f)
            {
                transform.position += delta * followFactor;
            }
        }
        else
        {
            transform.position += delta * followFactor;
        }

        _lastTargetPosition = target.position;
    }
}
