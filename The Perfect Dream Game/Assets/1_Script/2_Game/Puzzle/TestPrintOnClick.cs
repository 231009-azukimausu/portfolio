using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UnityのUI関係を扱えるようにする

public class TestPrintOnClick : MonoBehaviour
{
    //読み取り先スクリプト
    [SerializeField] private GameSceneManagerScript gamescenemanagerscript;
    private Image testPrint;
    public void OnClickObj()
    {
        gamescenemanagerscript.testPrint.SetActive(true);
    }
}
