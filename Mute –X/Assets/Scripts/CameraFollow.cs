using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform transformToFollow;
    Vector3 tempPosition;
    private void Start()
    {
        transformToFollow = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        tempPosition.x = transformToFollow.position.x;//transformToFollow x
        tempPosition.y = transformToFollow.position.y;//transformToFollow y
        tempPosition.z = transform.position.z;//camera z

        transform.position = tempPosition;
    }
}
