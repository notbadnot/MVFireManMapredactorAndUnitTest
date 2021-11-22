using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using System;

[RequireComponent(typeof(BoxCollider))]
public class Vision : MonoBehaviour
{
    private BoxCollider boxCollider;
    [SerializeField] private int maxVisionDistance;
    [SerializeField] private bool lookForPlayers;
    [SerializeField] private bool lookForZombies;
    private float hypoOfMaxVisionDistance;
    private Coroutine canStillseeCourutine;
    public MonoBehaviour myTarget;

    public event Action<MonoBehaviour> noticedTargetEvent;
    public event Action<MonoBehaviour> stopSeeingTarget;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.center = new Vector3(maxVisionDistance / 2, 1, -maxVisionDistance /2);
        boxCollider.size = new Vector3(maxVisionDistance, 1, +maxVisionDistance);
        hypoOfMaxVisionDistance = Mathf.Sqrt(2) * maxVisionDistance;
        Debug.Log(gameObject);
        gameObject.transform.localRotation = new Quaternion(0, 90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnnoticedObject(MonoBehaviour target)
    {
        Debug.Log("I can see the " + target);
        if (myTarget == null)
        {
            myTarget = target;
            canStillseeCourutine = StartCoroutine(canSeeTimer());
            noticedTargetEvent?.Invoke(target);
        }
        
    }

    public void OnStopSeeingTarget( MonoBehaviour target)
    {
        Debug.Log("I stopped seeng target " + target);
        StopCoroutine(canStillseeCourutine);
        myTarget = null;
        stopSeeingTarget?.Invoke(target);
    }

    private IEnumerator canSeeTimer()
    {
        if (myTarget.gameObject != null)
        {
            while (CanSeeTarget(myTarget.gameObject))
            {
                yield return new WaitForSeconds(0.1f);
            }
            OnStopSeeingTarget(myTarget);
        }
    }

    private bool CanSeeTarget( GameObject target)
    {
        int castLayer = 128 + 32768;
        RaycastHit hit;
        Vector3 myShoulderPosition = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
        bool hittedsmthing = Physics.Raycast(myShoulderPosition, target.transform.position - /*myShoulderPosition*/gameObject.transform.position, out hit, castLayer);
        Debug.DrawRay(myShoulderPosition, target.transform.position - /*myShoulderPosition*/gameObject.transform.position, Color.red, 1f);
        if (hit.collider != null)
        {
            if (target == hit.collider.gameObject.transform.parent.gameObject)
            {
                return true;
            }
        }
        return false;
    }
    private void OnTriggerStay(Collider other)
    {
        GameObject another = other.gameObject.transform.parent.gameObject;

        if (lookForPlayers)
        {
            
            
            var player = another.GetComponent<PlayerController>();
            if (player != null)
            {
                if (CanSeeTarget(another))
                {
                    OnnoticedObject(player);
                }
            }
        }
        if (lookForZombies)
        {

            var zombie = another.GetComponent<ZombieComponent>();
            if (zombie != null)
            {
                if (CanSeeTarget(another))
                {
                    OnnoticedObject(zombie);
                }
            }
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (myTarget != null)
        {
            var gameObj = other.gameObject.transform.parent.gameObject;
            if (gameObj.GetComponent<ZombieComponent>() == myTarget)
            {
                OnStopSeeingTarget(myTarget);
            }
            if (gameObj.GetComponent<PlayerController>() == myTarget)
            {
                OnStopSeeingTarget(myTarget);
            }
        }
    }



    /*private void OnTriggerEnter(Collider other)
    {
        GameObject another = other.gameObject;
        Debug.DrawRay(gameObject.transform.position, another.transform.position - gameObject.transform.position, Color.red, 1f);
    }*/


}
