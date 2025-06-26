using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovingScript : MonoBehaviour
{
    [Header("読み取り先スクリプト")]
    [SerializeField] private GameSceneManagerScript gamescenemanagerscript;
    private Transform TargetPlayer;
    void Start()
    {
        //private型のもの達に、gamescenemanagerscriptのデータを入れておく
        TargetPlayer = gamescenemanagerscript.targetPlayer;
    }
    void LateUpdate()
    {
        if (TargetPlayer != null)
        {
            // カメラとターゲットの位置差を計算
            Vector3 direction = TargetPlayer.position - transform.position;

            // Y方向のみの回転用にXZ平面に投影（高さは無視）
            //direction.y = 0;

            // ゼロ方向は無視（近すぎるときなど）
            if (direction.sqrMagnitude > 0.001f)
            {
                // 回転を計算し、Y軸方向のみ回転させる
                Quaternion TargetPlayerRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, TargetPlayerRotation, Time.deltaTime * 5f);
            }
        }
    }
}