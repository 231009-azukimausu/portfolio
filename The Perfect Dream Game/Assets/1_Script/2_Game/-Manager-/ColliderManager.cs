using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    [Header("部屋でのコライダー")]
    [SerializeField] private Collider UpDesk;
    [SerializeField] private Collider Bed;
    [SerializeField] private Collider Door;
    // 読み取り専用プロパティ
    public Collider upDesk => UpDesk;
    public Collider bed => Bed;
    public Collider door => Door;
    [Header("夢でのコライダー")]
    [SerializeField] private Collider ReturnDoor;
    [SerializeField] private Collider English;
    [SerializeField] private Collider Science;
    [SerializeField] private Collider Mathematics;
    [SerializeField] private Collider Japanese;
    [SerializeField] private Collider SocialStudies;
    [SerializeField] private Collider Ahead;
    // 読み取り専用プロパティ
    public Collider returnDoor => ReturnDoor;
    public Collider english => English;
    public Collider science => Science;
    public Collider mathematics => Mathematics;
    public Collider japanese => Japanese;
    public Collider socialStudies => SocialStudies;
    public Collider ahead => Ahead;
}