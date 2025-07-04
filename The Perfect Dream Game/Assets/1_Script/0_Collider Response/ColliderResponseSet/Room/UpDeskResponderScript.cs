using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDeskResponderScript : TriggerResponder
{
    protected override void OnEnteredTrigger()
    {
        Debug.Log("ベッドに入った瞬間：UIを表示します");
        // 例：ベッドに近づいたら「寝ますか？」ボタンを表示など
    }

    public override void DoSomething()
    {
        Debug.Log("Xキー or クリックで寝ます！");
    }
}
