using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaManager : MonoBehaviour
{

    
    [SerializeField]
    private float timeLive ;
    // Start is called before the first frame update
    void Start()
    {
        timeLive = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLive > 0) timeLive -= Time.deltaTime;
        else Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("zombie"))
        {
            
            collision.collider.gameObject.GetComponent<zombie>().GetDamage();
            collision.collider.gameObject.GetComponent<Animator>().Play("recibirdamage");
            Destroy(gameObject);
            
        }
    }
}
