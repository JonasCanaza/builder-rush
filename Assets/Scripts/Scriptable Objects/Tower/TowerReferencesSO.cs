using UnityEngine;

[CreateAssetMenu(fileName = "Tower References", menuName = "Scriptable Objects/Tower/Tower References")]
public class TowerReferencesSO : ScriptableObject
{
    [field: Header("Clips")]
    [field: SerializeField] public AudioClip CollisionSfx { get; private set; }
    [field: SerializeField] public AudioClip GoodPlacedSfx { get; private set; }
    [field: SerializeField] public AudioClip PerfectPlacedSfx { get; private set; }
}