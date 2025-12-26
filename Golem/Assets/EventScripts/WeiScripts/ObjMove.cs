using UnityEngine;

public class ObjMove : MonoBehaviour
{
    /// <summary>
    /// 追従元となるオブジェクト（この Transform の移動に合わせて動く）
    /// </summary>
    public Transform target;

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
        // Start 時も同じリセット処理を行う
        OnEnable();
    }

    /// <summary>
    /// 毎フレーム、target の移動量に合わせてこのオブジェクトを移動させる
    /// </summary>
    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // target の今回フレームでの移動量
        Vector3 delta = target.position - _lastTargetPosition;

        // 軸ごとのフラグに応じて delta を制限する
        if (!followX) delta.x = 0f;
        if (!followY) delta.y = 0f;
        if (!followZ) delta.z = 0f;

        // target の移動がほぼゼロなら何もしない
        if (delta.sqrMagnitude <= Mathf.Epsilon)
        {
            _lastTargetPosition = target.position;
            return;
        }

        // target からこのオブジェクトへの方向
        Vector3 toSelf = transform.position - target.position;

        // 内積が正なら「delta と同じ側（= target の進行方向側）にこのオブジェクトがある」
        float dot = Vector3.Dot(delta.normalized, toSelf.normalized);

        if (sameDirection)
        {
            if (dot > 0f)
            {
                // 同じ方向に移動させる（followFactor で強さ調整）
                transform.position += delta * followFactor;
            }
        }
        else
        {
                transform.position += delta * followFactor;
        }


        // 次フレーム用に位置を保存
        _lastTargetPosition = target.position;
    }
}
