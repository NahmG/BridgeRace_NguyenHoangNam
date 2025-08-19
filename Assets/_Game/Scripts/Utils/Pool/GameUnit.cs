using UnityEngine;


public abstract class GameUnit : MonoBehaviour
{
    [SerializeField]
    private Transform tf;
    [SerializeField]
    protected Transform skinTf;
    [SerializeField]
    private PoolType poolType;
    public PoolType PoolType => poolType;
    public Transform Tf => tf;
    public Transform SkinTf => skinTf;
    public RectTransform RectTf => (RectTransform)tf;

    protected virtual void Awake()
    {
    }

    protected virtual void FixedUpdate()
    {
    }
}

