using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorManager : MonoBehaviour
{
    [Header("カメラの定位置")]
    [SerializeField] private Vector3 CameraFixedPosition;
    [SerializeField] private Vector3 CameraFixedRotation;
    // 読み取り専用プロパティ
    public Vector3 cameraFixedPosition => CameraFixedPosition;
    public Vector3 cameraFixedRotation => CameraFixedRotation;
    [Header("キャラクターの定位置")]
    [SerializeField] private Vector3 CharacterFixedPosition;
    // 読み取り専用プロパティ
    public Vector3 characterFixedPosition => CharacterFixedPosition;
    [Header("カメラとキャラの移動先位置")]
    [Header("机のカメラ位置")]
    [SerializeField] private Vector3 DeskCameraDestinationPosition;
    [SerializeField] private Vector3 DeskCameraDestinationRotation;
    // 読み取り専用プロパティ
    public Vector3 deskCameraDestinationPosition => DeskCameraDestinationPosition;
    public Vector3 deskCameraDestinationRotation => DeskCameraDestinationRotation;
    [Header("机のキャラクター位置")]
    [SerializeField] private Vector3 DeskCharacterDestinationPosition;
    // 読み取り専用プロパティ
    public Vector3 deskCharacterDestinationPosition => DeskCharacterDestinationPosition;
    [Header("ドアの先のカメラ位置")]
    [SerializeField] private Vector3 DreamCameraDestinationPosition;
    [SerializeField] private Vector3 DreamCameraDestinationRotation;
    // 読み取り専用プロパティ
    public Vector3 dreamCameraDestinationPosition => DreamCameraDestinationPosition;
    public Vector3 dreamCameraDestinationRotation => DreamCameraDestinationRotation;
    [Header("ドアの先のキャラクター位置")]
    [SerializeField] private Vector3 DreamCharacterDestinationPosition;
    // 読み取り専用プロパティ
    public Vector3 dreamCharacterDestinationPosition => DreamCharacterDestinationPosition;
    [Header("国語のドアの先のカメラ位置")]
    [SerializeField] private Vector3 JapaneseCameraDestinationPosition;
    [SerializeField] private Vector3 JapaneseCameraDestinationRotation;
    // 読み取り専用プロパティ
    public Vector3 japaneseCameraDestinationPosition => JapaneseCameraDestinationPosition;
    public Vector3 japaneseCameraDestinationRotation => JapaneseCameraDestinationRotation;
    [Header("国語のドアの先のキャラクター位置")]
    [SerializeField] private Vector3 JapaneseCharacterDestinationPosition;
    // 読み取り専用プロパティ
    public Vector3 japaneseCharacterDestinationPosition => JapaneseCharacterDestinationPosition;
    [Header("社会のドアの先のカメラ位置")]
    [SerializeField] private Vector3 SocialStudiesCameraDestinationPosition;
    [SerializeField] private Vector3 SocialStudiesCameraDestinationRotation;
    // 読み取り専用プロパティ
    public Vector3 socialStudiesCameraDestinationPosition => SocialStudiesCameraDestinationPosition;
    public Vector3 socialStudiesCameraDestinationRotation => SocialStudiesCameraDestinationRotation;
    [Header("社会のドアの先のキャラクター位置")]
    [SerializeField] private Vector3 SocialStudiesCharacterDestinationPosition;
    // 読み取り専用プロパティ
    public Vector3 socialStudiesCharacterDestinationPosition => SocialStudiesCharacterDestinationPosition;
    [Header("数学のドアの先のカメラ位置")]
    [SerializeField] private Vector3 MathematicsCameraDestinationPosition;
    [SerializeField] private Vector3 MathematicsCameraDestinationRotation;
    // 読み取り専用プロパティ
    public Vector3 mathematicsCameraDestinationPosition => MathematicsCameraDestinationPosition;
    public Vector3 mathematicsCameraDestinationRotation => MathematicsCameraDestinationRotation;
    [Header("数学のドアの先のキャラクター位置")]
    [SerializeField] private Vector3 MathematicsCharacterDestinationPosition;
    // 読み取り専用プロパティ
    public Vector3 mathematicsCharacterDestinationPosition => MathematicsCharacterDestinationPosition;
    [Header("理科のドアの先のカメラ位置")]
    [SerializeField] private Vector3 ScienceCameraDestinationPosition;
    [SerializeField] private Vector3 ScienceCameraDestinationRotation;
    // 読み取り専用プロパティ
    public Vector3 scienceCameraDestinationPosition => ScienceCameraDestinationPosition;
    public Vector3 scienceCameraDestinationRotation => ScienceCameraDestinationRotation;
    [Header("理科のドアの先のキャラクター位置")]
    [SerializeField] private Vector3 ScienceCharacterDestinationPosition;
    // 読み取り専用プロパティ
    public Vector3 scienceCharacterDestinationPosition => ScienceCharacterDestinationPosition;
    [Header("英語のドアの先のカメラ位置")]
    [SerializeField] private Vector3 EnglishCameraDestinationPosition;
    [SerializeField] private Vector3 EnglishCameraDestinationRotation;
    // 読み取り専用プロパティ
    public Vector3 englishCameraDestinationPosition => EnglishCameraDestinationPosition;
    public Vector3 englishCameraDestinationRotation => EnglishCameraDestinationRotation;
    [Header("英語のドアの先のキャラクター位置")]
    [SerializeField] private Vector3 EnglishCharacterDestinationPosition;
    // 読み取り専用プロパティ
    public Vector3 englishCharacterDestinationPosition => EnglishCharacterDestinationPosition;
    [Header("テキスト移動先位置")]
    [SerializeField] private Vector2 DeskAnchoredPosition;
    [SerializeField] private Vector2 BedAnchoredPosition;
    [SerializeField] private Vector2 DoorAnchoredPosition;
    // 読み取り専用プロパティ
    public Vector2 deskAnchoredPosition => DeskAnchoredPosition;
    public Vector2 bedAnchoredPosition => BedAnchoredPosition;
    public Vector2 doorAnchoredPosition => DoorAnchoredPosition;
}
