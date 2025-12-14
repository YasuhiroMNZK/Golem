using UnityEngine;

public class RendererChange : MonoBehaviour
{
    [SerializeField] private Sprite change;
    [SerializeField] private GameObject NPC;
    private Sprite originalSprite; // 元のスプライトを保存
    
    public void Action()
    {
        // 元のスプライトを保存
        if (NPC != null)
        {
            SpriteRenderer spriteRenderer = NPC.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                originalSprite = spriteRenderer.sprite;
            }
            
            // NPCMisunderスクリプトを無効化
            NPCMisunder misunder = NPC.GetComponent<NPCMisunder>();
            if (misunder != null)
            {
                misunder.enabled = false;
            }
            
            // Animatorを無効化（アニメーションがスプライトを上書きするのを防ぐ）
            Animator animator = NPC.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
                Debug.Log("Animatorを無効化しました");
            }
        }

        // スプライトを変更
        if (NPC != null && change != null)
        {
            SpriteRenderer spriteRenderer = NPC.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = change;
            }
        }
        
        //5秒後に元のスプライトに戻す
        Invoke("ReturnAction", 5f);
    }

    // 元のスプライトに戻すメソッド
    public void ReturnAction()
    {
        if (NPC != null && originalSprite != null)
        {
            SpriteRenderer spriteRenderer = NPC.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = originalSprite;
            }
            
            // NPCMisunderスクリプトを再有効化
            NPCMisunder misunder = NPC.GetComponent<NPCMisunder>();
            if (misunder != null)
            {
                misunder.enabled = true;
            }
            
            // Animatorを再有効化
            Animator animator = NPC.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = true;
            }
        }
    }
}
