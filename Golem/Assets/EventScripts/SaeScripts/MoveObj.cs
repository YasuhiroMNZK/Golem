using UnityEngine;

public class MoveObj : MonoBehaviour
{
    [SerializeField] private float pushForce = 5f;
    [SerializeField] private string playerTag = "Player";
    
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Rigidbodyが無い場合は追加
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        // Colliderが無い場合は追加
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }
    
    void OnCollisionStay(Collision collision)
    {
        // プレイヤーとの接触をチェック
        if (collision.gameObject.CompareTag(playerTag))
        {
            // プレイヤーからオブジェクトへの方向を計算
            Vector3 pushDirection = transform.position - collision.transform.position;
            pushDirection.y = 0; // Y軸方向の力を除去（水平方向のみ）
            pushDirection.Normalize();
            
            // 押す力を適用
            rb.AddForce(pushDirection * pushForce, ForceMode.Force);
        }
    }
}