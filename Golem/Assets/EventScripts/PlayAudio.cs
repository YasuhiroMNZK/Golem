using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PlayAudio : MonoBehaviour, ActionBase
{
    AudioSource audiosource;
    private Coroutine intervalPlayCoroutine;
    
    [SerializeField] private float interval = 1.0f;

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }
    public void Action()
    {
        audiosource.Play();
    }

    //シーンが変わっても音を鳴らし続ける
    public void KeepAction()
    {
        DontDestroyOnLoad(gameObject);
        audiosource.Play();
        Destroy(gameObject, audiosource.clip.length);
    }

    /// 一定間隔でSEを鳴らす
    public void Play()
    {
        if (intervalPlayCoroutine != null)
        {
            StopCoroutine(intervalPlayCoroutine);
        }
        intervalPlayCoroutine = StartCoroutine(PlayIntervalCoroutine(interval));
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

    // 一定間隔でSEを鳴らすコルーチン
    private IEnumerator PlayIntervalCoroutine(float interval)
    {
        while (true)
        {
            audiosource.Play();
            yield return new WaitForSeconds(interval);
        }
    }
}
