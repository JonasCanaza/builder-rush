using UnityEngine;

[CreateAssetMenu(fileName = "Tower Setup Data", menuName = "Scriptable Objects/Tower/Tower Setup Data")]
public class TowerSetupDataSO : ScriptableObject
{
    [field: Header("Physics Settings")]
    [field: SerializeField] public float GravityMultiplier { get; private set; }

    [field: Header("Tower Settings")]
    [field: SerializeField] public int ScoreValue { get; private set; }
    [field: SerializeField] public float PerfectOverlap { get; private set; }
    [field: SerializeField] public float GoodOverlap { get; private set; }
}