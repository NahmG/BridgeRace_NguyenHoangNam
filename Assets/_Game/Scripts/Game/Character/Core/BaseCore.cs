using UnityEngine;

public abstract class BaseCore : MonoBehaviour
{
    public virtual void Initialize() { }
    public virtual void UpdateData() { }

    public virtual void FixedUpdateData() { }
}

public abstract class BaseCore<D> : BaseCore where D : CoreData, new()
{
    public D Data { get; protected set; }

    public override void Initialize()
    {
        base.Initialize();
        Data = new();
    }
}