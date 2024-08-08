using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] GameObject onBridgeBrick;
    [SerializeField] Transform stair;
    [SerializeField] float distance;
    [SerializeField] float length;

    public Transform start;
    public Transform end;

    Stack<GameObject> bricks = new Stack<GameObject>();

    private void Awake()
    {
        for (float i = -length / 2; i < length / 2; i += distance)
        {
            GameObject b = Instantiate(onBridgeBrick, stair);
            b.transform.position += new Vector3(0, 0, i);
            bricks.Push(b);
        }

        stair.rotation = Quaternion.Euler(new Vector3(-60, 0, 0));
    }

    public int GetCount(Color color)
    {
        int count = 0;  
        foreach(GameObject br in bricks)
        {
            OnBridgeBrick b = br.GetComponent<OnBridgeBrick>();

            if(b.Active && b.Color == color)
            {
                count++;
            }
        }
        return count;
    }

}
