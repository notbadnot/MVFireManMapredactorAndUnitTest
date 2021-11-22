using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderForTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Vector3.forward / 10;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject);
    }



}
