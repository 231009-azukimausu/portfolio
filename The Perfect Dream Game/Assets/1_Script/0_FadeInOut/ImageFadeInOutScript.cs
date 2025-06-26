using System.Collections;//コルーチンを使う用
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UnityのUI関係を扱えるようにする

//FadeIn  = だんだん透明に
//FadeOut = だんだん黒く(不透明に)

public class ImageFadeInOutScript : MonoBehaviour
{
    [Header("フェードイン設定")]
    [SerializeField] private float fadein_time = 1.0f;// フェードインにかかる時間（秒）
    [SerializeField] private int fadein_loopcount = 1;// ループ回数
    [Header("フェードアウト設定")]
    [SerializeField] private float fadeout_time = 1.0f;// フェードインにかかる時間（秒）
    [SerializeField] private int fadeout_loopcount = 1;// ループ回数
    public IEnumerator IFadeIn()
    {
        Image fade = GetComponent<Image>();//色を変えるゲームオブジェクトからImageコンポーネントを取得

        fade.raycastTarget = true;

        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 255.0f), (255.0f / 255.0f));//フェード元の色を設定（ＲＧＢ ０なら黒 Ａ ０なら透明）

        float wait_time = fadein_time / fadein_loopcount;// ウェイト時間算出

        float alpha_interval = 255.0f / fadein_loopcount;// 色の間隔を算出

        for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)// 色を徐々に変えるループ
        {
            yield return new WaitForSeconds(wait_time);// 待ち時間

            // Alpha値を少しずつ下げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;

            if (Input.GetMouseButtonDown(0))
            {
                fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 255.0f));
                fade.raycastTarget = false;
                yield break;// ここでコルーチン終了
            }
        }
        fade.raycastTarget = false;
    }

    public IEnumerator IFadeOut()
    {
        Image fade = GetComponent<Image>();//色を変えるゲームオブジェクトからImageコンポーネントを取得

        fade.raycastTarget = true;

        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 255.0f));//フェード元の色を設定（ＲＧＢ ０なら黒 Ａ ０なら透明）

        float wait_time = fadeout_time / fadeout_loopcount;// ウェイト時間算出

        float alpha_interval = 255.0f / fadeout_loopcount;// 色の間隔を算出

        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)// 色を徐々に変えるループ
        {
            yield return new WaitForSeconds(wait_time);// 待ち時間

            // Alpha値を少しずつ下げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;

            if (Input.GetMouseButtonDown(0))
            {
                new_color.a = 255.0f;
                fade.color = new_color;
                fade.raycastTarget = false;
                yield break;// ここでコルーチン終了
            }
        }
        fade.raycastTarget = false;
    }
}