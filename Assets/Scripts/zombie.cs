using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class zombie : humanoide
{
    private SpriteRenderer spriteZombie;
    private float tiempoCuerpo = 5;
    private bool caminandoPuerta;
    public bool persiguiendoPersonaje;
    private bool atacando;
    private bool persiguiendoDoctor;
    private NavMeshAgent agent;
    public Transform target;

    

    public Vector3 destinacion;

    private ParedMadera paredVictima;

    private CharacterController victima;

    public Transform puertaDestino;
    private ParedMadera eventoDeDestruccionDePared;
    private bool fueAcataco;

    protected override void move(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        spriteZombie = gameObject.GetComponent<SpriteRenderer>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Debug.Log("este es el agente "+agent);
        Debug.Log("Esta es la posicion de la puerta " + puertaDestino.position);

        if (persiguiendoPersonaje)
        {
            CambiarTrayectoria();
        }
        else
        {
            CambiarTrayectoria(puertaDestino.position);
        }
        
        eventoDeDestruccionDePared = puertaDestino.gameObject.GetComponent<ParedMadera>();
        eventoDeDestruccionDePared.destruyoPared += CambiarTrayectoria;
        //CambiarTrayectoria(puertaDestino.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (persiguiendoPersonaje)
        {
            CambiarTrayectoria();
        }
        transform.up = -(agent.destination - transform.position); //=>>>>>> VOLTEAR ZOMBIAR HACIA EL TARGET
        if (life <= 0 & gameObject.GetComponent<Collider>().enabled)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<Animator>().Play("muertezombie");
            StartCoroutine("DestruirCuerpo");
            //spriteZombie.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 0.3f));

        }     
    }
    IEnumerator DestruirCuerpo()
    {
        
        persiguiendoPersonaje = false;
        agent.isStopped = true;
        Debug.Log("Entre a destruir el cuerpoi");
        yield return new WaitForSeconds(1f);
        
        gameObject.GetComponent<Animator>().Play("desaparecercuerpo");
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("constructor"))
        {
            paredVictima = collision.collider.gameObject.GetComponent<ParedMadera>();
            Debug.Log("Zombie llego a la pared " + paredVictima );
            agent.isStopped = true;
            gameObject.GetComponent<Animator>().Play("atacar");
        
        }
        if (collision.collider.CompareTag("Player"))
        {
            agent.isStopped = true;
            gameObject.GetComponent<Animator>().Play("atacar");

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            agent.isStopped = false;
            gameObject.GetComponent<Animator>().StopPlayback();

        }
    }



    private void hacerDano()
    {
        if (paredVictima != null) { paredVictima.DestruirMuro(); }
        
    }

    public void CambiarTrayectoria(Vector3 trayecto)
    {
        agent.SetDestination(trayecto);
        destinacion = agent.destination;
        Debug.Log("LLame al cambio de trayecto");
        if (agent.isStopped)
        {
            agent.isStopped = false;
        }
    }

    public void CambiarTrayectoria()
    {
        Debug.Log("Se cambio trayectoria hacia el personaje");
        persiguiendoPersonaje = true;
        if (agent != null)
        {
            agent.SetDestination(target.position);
            agent.isStopped = false;
        }
   
    }
}
