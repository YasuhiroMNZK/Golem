using UnityEngine;

public class SEPlayerWalk : MonoBehaviour
{
    AudioSource audiosource;
    [SerializeField] private float walkSoundInterval = 0.6f; // 歩行音の間隔（秒）
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

        if(isWalking)
        {
            // 移動開始時にタイマーをリセット（音は鳴らさない）
            if (!wasWalking)
            {
                lastWalkSoundTime = Time.time;
            }
            // 歩き始めてから2秒間隔で音を再生
            else if (Time.time - lastWalkSoundTime >= walkSoundInterval)
            {
                audiosource.Play();
                lastWalkSoundTime = Time.time;
            }
        }

        wasWalking = isWalking;

    }
}
