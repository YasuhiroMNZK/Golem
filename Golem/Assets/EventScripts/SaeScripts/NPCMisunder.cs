using UnityEngine;

public class NPCMisunder : MonoBehaviour
{
    [SerializeField] private Bag getMangaBag;
    [SerializeField] private GameObject character;

    void Start()
    {
    //     // バッグやアイテムリストが存在しない場合は何もしない
    //     if (getMangaBag?.itemList?.Count <= 0) return;
        
    //    Item item = getMangaBag.itemList[0];
        
    //     // アイテムが存在しない場合は何もしない
    //     if (item == null) return;

    //     // アニメーションクリップが設定されている場合はアニメーション実行
    //     if (item.animationClips != null)
    //     {
    //         SetAnimation(item);
    //     }
    //     // アニメーションクリップがなく、スプライトがある場合はスプライト設定
    //     else if (item.misunder != null)
    //     {
    //         SetSprite(item);
    //     }
    //     // どちらも設定されていない場合は何もしない
    }

    public void SetSprite()
    {
        Item item = getMangaBag.itemList[0];
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

    public void SetAnimation()
    {
        Item item = getMangaBag.itemList[0];
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