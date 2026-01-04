using UnityEngine;
using UnityEngine.Video;

public class VideoEnd : TriggerBase
{
    [SerializeField] private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
         action.Invoke();
    }

    void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
    }
}