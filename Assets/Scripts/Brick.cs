using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] Renderer rend;
    [SerializeField] BoxCollider col;
    public ColorSet scatterColor;

    [SerializeField] Ease easeVertUp;
    [SerializeField] Ease easeVertDown;
    [SerializeField] float duration;

    [SerializeField] Transform parent;

    public Color color { get { return rend.material.color; } }

    public void ChangeColor(Color color)
    {
        rend.material.color = color;
    }

    public void Scatter()
    {
        parent = LevelManager.Ins.CurrentLevel.transform;
        transform.SetParent(parent);

        col.enabled = false;
        gameObject.transform.DOMoveZ(transform.position.z + Random.Range(-1.5f, 1.5f), duration * 2);
        gameObject.transform.DOMoveX(transform.position.x + Random.Range(-1.5f, 1.5f), duration * 2);
        gameObject.transform.DOMoveY(transform.position.y + 2, duration).SetEase(easeVertUp).OnComplete(() =>
        {
            gameObject.transform.DOMoveY(transform.position.y - 2.5f, duration).SetEase(easeVertDown).OnComplete(() =>
            {
                col.enabled = true;
            });
        });
    }
}
