using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/QuestiInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    public string id { get; private set; }

    [Header("General")]
    public string displayName;

    [Header("Requierements")]
    public int levelRequired;
    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Rewards")]
    public int gold;

    public void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif

    }
}
