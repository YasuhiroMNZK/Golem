using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoAnimation : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RawImage rawImage;        
    [SerializeField] private Texture loadingTexture;   

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
}
