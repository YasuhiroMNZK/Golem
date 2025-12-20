using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScene : MonoBehaviour
{
    [SerializeField] private Image fadePanel; // フェード用のUIパネル
    [SerializeField] private float fadeDuration = 1.0f; // フェードの時間
    [SerializeField] private string targetSceneName; // フェード後に遷移するシーン名

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void FadeOutAction()
    {
        StartCoroutine(FadeOut()); // FadeOut()コルーチンを開始
    }

    public void FadeInAction()
    {
        StartCoroutine(FadeIn()); // FadeIn()コルーチンを開始
    }

    private IEnumerator FadeOut()
    {
        fadePanel.enabled = true; // フェードパネルを有効にする
        float elapsedTime = 0.0f; // 経過時間
        Color startColor = fadePanel.color; // パネルの初期色
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f); // パネルの終了色（不透明）

        //フェードアウトアニメーションを実行
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime; //経過時間を増やす
            float t = Mathf.Clamp01(elapsedTime / fadeDuration); //フェードの進行度を計算
            fadePanel.color = Color.Lerp(startColor, endColor, t); // パネルの色を更新
            yield return null; // 次のフレームまで待機
        }

        fadePanel.color = endColor; // フェードが完了したら最終色に設定
        SceneManager.LoadScene(targetSceneName); // 指定されたシーンに遷移
    }

    private IEnumerator FadeIn()
    {
        fadePanel.enabled = true; // フェードパネルを有効にする
        float elapsedTime = 0.0f; // 経過時間
        Color startColor = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 1.0f); // パネルの初期色（不透明）
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f); // パネルの終了色（透明）

        fadePanel.color = startColor; // 初期状態を不透明に設定

        //フェードインアニメーションを実行
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime; //経過時間を増やす
            float t = Mathf.Clamp01(elapsedTime / fadeDuration); //フェードの進行度を計算
            fadePanel.color = Color.Lerp(startColor, endColor, t); // パネルの色を更新
            yield return null; // 次のフレームまで待機
        }

        fadePanel.color = endColor; // フェードが完了したら最終色に設定
        fadePanel.enabled = false; // フェードパネルを無効にする
    }
}
