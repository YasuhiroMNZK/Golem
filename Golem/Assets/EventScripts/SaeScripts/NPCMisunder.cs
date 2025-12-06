using UnityEngine;

public class NPCMisunder : MonoBehaviour
{
    [SerializeField] private Bag getMangaBag;
    [SerializeField] private GameObject character;

    void Start()
    {
        if (getMangaBag?.itemList?.Count > 0)
        {
            Item item = getMangaBag.itemList[0]; // 常に最初のアイテム
            if (item?.misunder != null && item.animationClips == null)
                SetSprite(item);
            else if (item?.animationClips != null)
                SetAnimation(item);
        }
    }

    void SetSprite(Item item)
    {
        var spriteRenderer = character?.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
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