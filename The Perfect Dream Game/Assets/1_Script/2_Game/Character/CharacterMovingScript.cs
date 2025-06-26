using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovingScript : MonoBehaviour
{
    //読み取り先スクリプト
    [SerializeField] private GameSceneManagerScript gamescenemanagerscript;
    //キャラクターの移動方向元カメラ設定
    private Transform cameraTransform;
    //キャラクターの移動・回転
    private float moveSpeed;
    private float rotateSpeed;
    void Start()
    {
        cameraTransform = gamescenemanagerscript.cameraTransform;
        moveSpeed = gamescenemanagerscript.moveSpeed;
        rotateSpeed = gamescenemanagerscript.rotateSpeed;
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // カメラの向きを基に方向を計算
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = camForward * vertical + camRight * horizontal;

        // 移動
        if (moveDirection.magnitude > 0.1f)
        {
            // 向き変更（なめらかに）
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

            // 位置更新
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }
}