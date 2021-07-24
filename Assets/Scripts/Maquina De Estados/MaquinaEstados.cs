using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaquinaEstados : MonoBehaviour
{
    [SerializeField] private MonoBehaviour CaminarApuerta;
    [SerializeField] private MonoBehaviour EntrarAEdificio;
    [SerializeField] private MonoBehaviour Persecucion;
    [SerializeField] private MonoBehaviour EstadoInicial;
    public ParedMadera pared;

    [SerializeField]
    private MonoBehaviour estadoActual;

    private void Awake()
    {
        
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        CaminarApuerta = GetComponent<CaminarAPuerta>();
        EntrarAEdificio = GetComponent<EntrarAEdificio>();
        EstadoInicial = CaminarApuerta;
        pared.destruyoPared += ActivarPersecusion;
        ActivarEstado(EstadoInicial);
    }

    public void ActivarEstado(MonoBehaviour estado)
    {
        if(estadoActual !=null) estadoActual.enabled = false;
        estadoActual = estado;
        estadoActual.enabled = true;
    }

    public void ActivarPersecusion()
    {
        Debug.Log("Se activo persecusion");
        ActivarEstado(Persecucion);
    }

}
