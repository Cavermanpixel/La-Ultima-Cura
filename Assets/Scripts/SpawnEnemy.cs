using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private ParedMadera ParedDestino;
    [SerializeField] private Transform Personaje;
    [SerializeField] private float spawnTime = 1f;
    public GameObject enemyPrefab;
    [SerializeField] private bool puedeInstanciar;
    [SerializeField] Transform puertaDestino;
    

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (puedeInstanciar)
        {
            puedeInstanciar = false;
            StartCoroutine("IspawnEnemy");
            
        }
    }

    IEnumerator IspawnEnemy()
    {
        int i = 8;
        while (i >0)
        {
            i--;
            yield return new WaitForSeconds(spawnTime);
            GameObject zombieEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
            zombieEnemy.GetComponent<zombie>().puertaDestino = puertaDestino;
            zombieEnemy.GetComponent<zombie>().target = Personaje;
            zombieEnemy.GetComponent<zombie>().persiguiendoPersonaje = !puertaDestino.gameObject.GetComponent<ParedMadera>().colisionMuro.enabled;

        }
        
        
    }
}
