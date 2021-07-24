using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{

    public Transform target;
    private float offsetcamera = -12;

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y , offsetcamera);
    }
 
}
