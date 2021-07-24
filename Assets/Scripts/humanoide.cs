using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class humanoide : MonoBehaviour
{
    public Vector3 movement;
    public float speed = 4;
    protected Rigidbody characterRigidbody;
    public float life = 5;
    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract void move(Vector3 direction);

    public virtual void GetDamage()
    {
        life-=1;
    }


   
}
