using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLinePool : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    Queue<GameObject> lines = new Queue<GameObject>();

    public Vector3 startPosition;


    public LineRenderer Get(Vector3 endPos)
    {
        if(lines.Count == 0)
        {
            Add();
        }

        LineRenderer line = lines.Dequeue().GetComponent<LineRenderer>();
        line.SetPosition(0, startPosition);
        line.SetPosition(1, endPos);
        line.gameObject.SetActive(true);

        return line;
    }

    public void ReturnToPool(LineRenderer line) 
    {
        line.gameObject.SetActive(false);
        lines.Enqueue(line.gameObject);
    }

    public void Add()
    {
        lines.Enqueue(Instantiate(prefab));
    }
}
