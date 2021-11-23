using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallConfig : ScriptableObject
{

    [System.Serializable]
    public struct Position
    {
        //trow error without it CS0592
    }
    public List<Vector3> VecPositions;
}
