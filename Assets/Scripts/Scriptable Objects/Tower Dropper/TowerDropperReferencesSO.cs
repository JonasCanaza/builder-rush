using UnityEngine;

[CreateAssetMenu(fileName = "Tower Dropper References", menuName = "Scriptable Objects/Tower Dropper/Tower Dropper References")]
public class TowerDropperReferencesSO : ScriptableObject
{
    [field: Header("Prefabs")]
    [field: SerializeField] public TowerController TowerPrefab { get; private set; }

    [field: Header("Clips")]
    [field: SerializeField] public AudioClip LaunchSfx { get; private set; }
}