using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;  // イベント用

public class VideoAnimation : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RawImage rawImage;        
    [SerializeField] private Texture loadingTexture;   

    // 任意のトリガーから呼び出すためのイベント（インスペクターで設定可能）

    private void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        if (loadingTexture != null && rawImage != null)
        {
           
            rawImage.texture = loadingTexture;
        }

       
        videoPlayer.playOnAwake = false;
        videoPlayer.waitForFirstFrame = true;

        
        videoPlayer.prepareCompleted += OnVideoPrepared;

        
        videoPlayer.Prepare();
    }

    private void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.prepareCompleted -= OnVideoPrepared;
        }
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        
        if (rawImage != null)
        {
            rawImage.texture = vp.texture;  
        }

        vp.Play();  
    }

    // 動画の末尾にジャンプする公開メソッド
    public void JumpToVideoEnd()
    {
        if (videoPlayer == null || !videoPlayer.isPrepared)
            return;

        // フレームベースで最後のフレームへジャンプ
        videoPlayer.time = videoPlayer.length - 0.05f;
        videoPlayer.Play();
    }

}
