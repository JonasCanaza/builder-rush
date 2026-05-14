using UnityEngine;

[CreateAssetMenu(fileName = "Tower Dropper Setup Data", menuName = "Scriptable Objects/Tower Dropper Setup Data")]
public class TowerDropperSetupDataSO : ScriptableObject
{
    [field: Header("Movement Settings")]
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float MovementLimitX { get; private set; }
}