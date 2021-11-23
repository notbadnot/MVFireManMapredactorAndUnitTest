using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using System.Linq;
using Search;

public class ZombieBotfromAstarBot : PlayerInput
{
    [SerializeField] public LevelMap _levelMap;
    [SerializeField] private Transform _zombie;

    private int[,] _map;
    private int _deltaX;
    private int _deltaZ;

    private int maxX;
    private int minX;
    private int minZ;
    private int maxZ;

    private Vector3 targetPosition;

    [SerializeField] Vision visionSensor;
    public Vector3 currenPointToGo;

    public override (Vector3 moveDirection, Quaternion viewDirection, bool shoot) CurrentInput()
    {



        if (visionSensor.myTarget != null)
        {
            currenPointToGo = visionSensor.myTarget.gameObject.transform.position;
        }else if((currenPointToGo - _zombie.position).magnitude < 0.5f)
        {
            RaycastHit hit;
            RaycastHit hitmirrored;
            currenPointToGo = new Vector3(Mathf.Round(UnityEngine.Random.Range(minX + 1, maxX - 1)), 0.0f, Mathf.Round(UnityEngine.Random.Range(minZ + 1, maxZ - 1)));
            Physics.Raycast(currenPointToGo, currenPointToGo + Vector3.up * 1, out hit, 100000f, 128);
            int counter = 0;
            Physics.Raycast(currenPointToGo + Vector3.up * 1, currenPointToGo , out hitmirrored, 100000f, 128);
            while ( hit.collider != null && hitmirrored.collider != null && counter<10000)
            {
                counter++;
                currenPointToGo = new Vector3(Mathf.Round(UnityEngine.Random.Range(minX + 1, maxX - 1)), 0.0f, Mathf.Round(UnityEngine.Random.Range(minZ + 1, maxZ - 1)));
                Physics.Raycast(currenPointToGo, currenPointToGo + Vector3.up * 1, out hit, 100000f, 128);
                Physics.Raycast(currenPointToGo + Vector3.up * 1, currenPointToGo, out hitmirrored, 100000f, 128);

            }
        }

        targetPosition = currenPointToGo;

        var zombiePosition = _zombie.position;
        var from = ToInt(zombiePosition);
        var to = ToInt(targetPosition);

        var path = AStarFromGoogle.FindPath(_map, @from, to);
        if (path == null)
        {
            currenPointToGo = gameObject.transform.position;
        }
        var nextPathPoint = path.Count >= 2 ? path[1] : to;
        nextPathPoint = new Vector2Int(nextPathPoint.x - _deltaX, nextPathPoint.y - _deltaZ);


        var moveDirection = new Vector3(nextPathPoint.x, zombiePosition.y, nextPathPoint.y) - zombiePosition;
        var directVector = targetPosition - zombiePosition;

        return (moveDirection, Quaternion.LookRotation(directVector), false);
    }

    private void Start()
    {
        maxX = _levelMap.Points.Max(p => Mathf.RoundToInt(p.x));
        minX = _levelMap.Points.Min(p => Mathf.RoundToInt(p.x));

        maxZ = _levelMap.Points.Max(p => Mathf.RoundToInt(p.z));
        minZ = _levelMap.Points.Min(p => Mathf.RoundToInt(p.z));

        _deltaX = minX < 0 ? -minX : 0;
        _deltaZ = minZ < 0 ? -minZ : 0;

        _map = new int[maxX + _deltaX + 1, maxZ + _deltaZ + 1];

        foreach (var point in _levelMap.Points)
        {
            _map[_deltaX + Mathf.RoundToInt(point.x), _deltaZ + Mathf.RoundToInt(point.z)] = -1;
        }

        if (currenPointToGo == null || currenPointToGo == Vector3.zero)
        {
            currenPointToGo = _zombie.position;
        }
    }

    private Vector2Int ToInt(Vector3 vector3) =>
        new Vector2Int(_deltaX + Mathf.RoundToInt(vector3.x), _deltaZ + Mathf.RoundToInt(vector3.z));
}

