using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persecucion : MonoBehaviour
{
    private ControladorNavMesh ControladorDeNavegacion;
    // Start is called before the first frame update
    void Awake()
    {
        ControladorDeNavegacion = gameObject.GetComponent<ControladorNavMesh>();
    }

    // Update is called once per frame
    private void OnEnable()
    {
        ControladorDeNavegacion.CambiarTrayectoria();
    }
}
