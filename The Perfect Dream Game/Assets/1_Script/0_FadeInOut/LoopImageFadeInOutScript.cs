using System.Collections;//コルーチンを使う用
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UnityのUI関係を扱えるようにする

//FadeIn  = だんだん透明に
//FadeOut = だんだん黒く(不透明に)

public class LoopImageFadeInOutScript : MonoBehaviour
{
    [SerializeField] private float fade_time = 1.0f;// フェードインにかかる時間（秒）
    [SerializeField] private int fade_loopcount = 1;// ループ回数
    public IEnumerator LIFade()
    {
        Image fade = GetComponent<Image>();//色を変えるゲームオブジェクトからImageコンポーネントを取得

        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 255.0f), (255.0f / 255.0f));//フェード元の色を設定（ＲＧＢ ０なら黒 Ａ ０なら透明）

        float wait_time = fade_time / fade_loopcount;// ウェイト時間算出

        float alpha_interval = 255.0f / fade_loopcount;// 色の間隔を算出

        while (true) // このGameObjectが有効な間実行し続ける
        {
            for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)// 色を徐々に変えるループ
            {
                yield return new WaitForSeconds(wait_time);// 待ち時間

                // Alpha値を少しずつ下げる
                Color new_color = fade.color;
                new_color.a = alpha / 255.0f;
                fade.color = new_color;
            }
            for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)// 色を徐々に変えるループ
            {
                yield return new WaitForSeconds(wait_time);// 待ち時間

                // Alpha値を少しずつ上げる
                Color new_color = fade.color;
                new_color.a = alpha / 255.0f;
                fade.color = new_color;
            }
        }
    }
}