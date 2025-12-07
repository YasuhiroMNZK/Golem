using UnityEngine;

public class NPCMisunder : MonoBehaviour
{
    [SerializeField] private Bag getMangaBag;
    [SerializeField] private GameObject character;

    void Start()
    {
        if (getMangaBag?.itemList?.Count > 0)
        {
            Item item = getMangaBag.itemList[0];

            // アニメーションクリップが設定されている場合はアニメーション実行
            if (item?.animationClips != null)
            {
                SetAnimation(item);
            }
            // アニメーションクリップがなく、スプライトがある場合はスプライト設定
            else if (item?.misunder != null)
            {
                SetSprite(item);
            }
        }
    }

    void SetSprite(Item item)
    {
        if (character == null || item?.misunder == null) return;

        // SpriteRendererを取得
        var spriteRenderer = character.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return;
        
        // Animatorがある場合は無効化
        var animator = character.GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = false;
        }

        // スプライトを設定
        spriteRenderer.sprite = item.misunder;
    }

    void SetAnimation(Item item)
    {
        var animator = character?.GetComponent<Animator>();
        if (animator?.runtimeAnimatorController != null)
        {
            // AnimatorOverrideControllerを作成
            var overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            // 既存のアニメーションクリップを取得
            var clips = animator.runtimeAnimatorController.animationClips;
            
            if (clips.Length > 0)
            {
                // 最初のクリップをアイテムのアニメーションクリップで上書き
                overrideController[clips[0]] = item.animationClips;
                // 上書きしたコントローラーをAnimatorに設定
                animator.runtimeAnimatorController = overrideController;
                // アニメーションを再生
                animator.Play(0);
            }
        }
    }
}