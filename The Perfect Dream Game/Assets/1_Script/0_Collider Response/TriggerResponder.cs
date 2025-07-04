using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerResponder : MonoBehaviour
{
    public bool IsInsideTrigger { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsInsideTrigger = true;

            // ⬇ 入った瞬間に処理する
            OnEnteredTrigger();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsInsideTrigger = false;
        }
    }

    protected virtual void OnEnteredTrigger()
    {
        Debug.Log($"{gameObject.name} に入ったときの処理（共通）");
    }

    public virtual void DoSomething()
    {
        Debug.Log($"{gameObject.name} に入力されたときの処理（共通）");
    }
}