using UnityEngine;

using Core;
public class Character : GameUnit, ICharacter
{
    [SerializeField]
    protected CharacterStats Stats;
    [SerializeField]
    CoreSystem core;

    public bool IsDie => Stats.HP.Value <= 0;

    public void OnInit(CharacterStats stats = null)
    {
        if (stats == null)
            Stats.Reset();
        else
            Stats = stats;
    }

    void OnEnable()
    {
        core.Initialize(Stats);
    }

    void Update()
    {
        core.UpdateData();
    }

    protected override void FixedUpdate()
    {
        core.FixedUpdate();
    }


}
