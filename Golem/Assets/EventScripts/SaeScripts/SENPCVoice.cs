using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SENPCVoice : MonoBehaviour
{
    AudioSource audiosource;
    private Coroutine intervalPlayCoroutine;
    
    [SerializeField] private float interval = 1.0f;
    [SerializeField] private int loopCount = 5;

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    /// 一定間隔でSEを鳴らす
    public void Play()
    {
        if (intervalPlayCoroutine != null)
        {
            StopCoroutine(intervalPlayCoroutine);
        }
        intervalPlayCoroutine = StartCoroutine(PlayIntervalCoroutine(interval, loopCount));
    }

    /// 一定間隔でのSE再生を停止
    public void Stop()
    {
        if (intervalPlayCoroutine != null)
        {
            StopCoroutine(intervalPlayCoroutine);
            intervalPlayCoroutine = null;
        }
    }

    // 一定間隔でSEを鳴らすコルーチン（指定回数）
    private IEnumerator PlayIntervalCoroutine(float interval, int loopCount)
    {
        for (int i = 0; i < loopCount; i++)
        {
            audiosource.Play();
            if (i < loopCount - 1) // 最後の再生後は待機しない
            {
                yield return new WaitForSecondsRealtime(interval);
            }
        }
    }
}