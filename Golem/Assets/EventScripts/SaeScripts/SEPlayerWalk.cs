using UnityEngine;

public class SEPlayerWalk : MonoBehaviour
{
    AudioSource audiosource;
    [SerializeField] private PlayerCtrl playerCtrl; // PlayerCtrlの参照
    [SerializeField] private float walkSoundInterval = 0.6f; // 歩行音の間隔（秒）
    [SerializeField] private float runSoundInterval = 0.4f; // 走行音の間隔（秒）
    [SerializeField] private AudioClip walkSound; // 歩くSE
    [SerializeField] private AudioClip runSound; // 走るSE
    [SerializeField] private AudioClip jumpSound; // ジャンプSE
    [SerializeField] private AudioClip BigSound; // 巨大SE
    private float lastWalkSoundTime = 0f;
    private bool wasWalking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        bool canRun = playerCtrl != null ? playerCtrl.CanRun : true; // PlayerCtrlのcanRunを参照
        bool canJump = playerCtrl != null ? playerCtrl.CanJump : true; // PlayerCtrlのcanJumpを参照
        bool canScale = playerCtrl != null ? playerCtrl.CanScale : true; // PlayerCtrlのcanScaleを参照
        bool isRunning = Input.GetButton("Fire3") && isWalking && canRun; // Shiftキー + 移動 + canRun条件

        // ジャンプSE
        if (canJump && Input.GetButtonDown("Jump"))
        {
            if (jumpSound != null)
            {
                audiosource.PlayOneShot(jumpSound);
            }
        }

        if(isWalking)
        {
            // 移動開始時にタイマーをリセット（音は鳴らさない）
            if (!wasWalking)
            {

                lastWalkSoundTime = Time.time;
            }
            //　移動中は一定間隔で歩行音を鳴らす
            else
            {
                float soundInterval = isRunning ? runSoundInterval : walkSoundInterval;
                if (Time.time - lastWalkSoundTime >= soundInterval)
                {
                    AudioClip clipToPlay;
                    
                    // canScaleがTrueの時はBigSoundを使用
                    if (canScale && BigSound != null)
                    {
                        clipToPlay = BigSound;
                    }
                    // それ以外は走行音か歩行音を使用
                    else
                    {
                        clipToPlay = isRunning ? runSound : walkSound;
                    }
                    
                    if (clipToPlay != null)
                    {
                        audiosource.clip = clipToPlay;
                        audiosource.Play();
                    }
                    lastWalkSoundTime = Time.time;
                }
            }
        }

        wasWalking = isWalking;

    }
}
