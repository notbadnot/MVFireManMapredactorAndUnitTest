using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class checknavmesh : MonoBehaviour
{
    private NavMeshPath path;
    [SerializeField] private GameObject obj1;
    [SerializeField] private GameObject obj2;
    private float elapsed = 0.0f;
    // Start is called before the first frame update

    void Start()
    {
        path = new NavMeshPath();
        elapsed = 0.0f;
    }

    void Update()
    {
        // Update the way to the goal every second.
        elapsed += Time.deltaTime;
        if (elapsed > 1.0f)
        {
            elapsed -= 1.0f;
            NavMesh.CalculatePath(obj1.transform.position, obj2.transform.position, NavMesh.AllAreas, path);
        }
        for (int i = 0; i < path.corners.Length - 1; i++)
        { 
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red); 
        }

        Debug.Log(path.corners[0]);
    }

}
