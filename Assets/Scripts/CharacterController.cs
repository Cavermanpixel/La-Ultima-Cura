
using System.Collections;
using System.Collections.Generic;
//using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
//using UnityEngine.UIElements;
//using UnityEngine.WSA;

public class CharacterController : humanoide
{

    [SerializeField] Vector3 puntomouse;
    public Transform sight;
    [SerializeField]
    Transform target;
    private ParedMadera muroActual;

    [SerializeField]
    Vector3 directionRot;
    public GameObject balaPrefab;
    public Transform lugarBala;
    private GameObject indicadorConstruccion;
    private GameObject colision;
    [SerializeField] private bool puedeDisparar;


    private void Start()
    {
        Cursor.visible = false;
        indicadorConstruccion = transform.GetChild(0).gameObject;
        target = transform;
        characterRigidbody = GetComponent<Rigidbody>();
        puedeDisparar = true;
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) & puedeDisparar){
            shoot();
        }
        movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"), 0).normalized;
        

        puntomouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, 10));

        sight.transform.position = puntomouse;

        directionRot = puntomouse - transform.position;

        target.up =-directionRot ;
        
        if (colision != null)
        {
            if (colision.CompareTag("constructor"))
            {
                if (Input.GetKey("e") )
                {
                    Debug.Log("Active el muro");
                    muroActual.ConstruirMuro();
                }
                else if (Input.GetKey("r") & colision.transform.GetChild(0).gameObject.activeInHierarchy)
                {
                    Debug.Log("Destrui Muro");
                    muroActual.DestruirMuro();
                }
            }
        }    
    }

    private void FixedUpdate()
    {
        move(movement);

    }

    protected override void move(Vector3 direction)
    {
        characterRigidbody.velocity = direction * speed * Time.deltaTime ;
    }

    private void shoot()
    {
        for (int i = 0; i < 6; i++)
        {
            Quaternion rotacionAnterior = lugarBala.rotation;
            //Debug.Log(lugarBala.rotation);
            //float ramdonnumero = Random.Range(-7f, 7f);

            lugarBala.Rotate(Vector3.forward, Random.Range(-30f, 30f));
            //lugarBala.rotation = Quaternion.Euler(lugarBala.localEulerAngles.x, lugarBala.localEulerAngles.y,ramdonnumero);
            //Debug.Log(lugarBala.rotation);
            GameObject bala = Instantiate(balaPrefab, lugarBala.position, lugarBala.rotation);

            Vector3 directionRot = puntomouse - transform.position;
            bala.GetComponent<Rigidbody>().AddForce(-lugarBala.up * 7, ForceMode.VelocityChange);
            lugarBala.rotation = rotacionAnterior;
            puedeDisparar = false;
           // EmpujeBala();
            StartCoroutine("ActivarDisparo");
            
        }
        CameraShake.Instance.ShakeCamera(2f, .1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("constructor"))
        {
            indicadorConstruccion.SetActive(true);
            colision = other.gameObject;
            muroActual = other.GetComponent<ParedMadera>();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("constructor"))
        {
            indicadorConstruccion.SetActive(false);
            //colision = null;
            muroActual = null;
        }
    }

    //Activar la habilidad de construir los muros
    IEnumerator ActivarDisparo()
    {
        yield return new WaitForSeconds(0.5f);
        puedeDisparar = true;
    }

    private void EmpujeBala()
    {
        Vector3 direccionContraria = transform.position - puntomouse;
        characterRigidbody.AddExplosionForce(500, new Vector3( lugarBala.position.x, lugarBala.position.y, lugarBala.position.z ), 1000f);
    }
   
}
