using UnityEngine;

public interface ICharacter
{
    public Transform Tf { get; }
    public Transform SkinTf { get; }
    public bool IsDie { get; }
    public void OnInit(CharacterStats stats);
}