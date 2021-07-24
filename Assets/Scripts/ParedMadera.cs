using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParedMadera : MonoBehaviour
{

    private Transform madera;
    public Image muro;
    public MaquinaEstados EstadosZombie1;
    public BoxCollider colisionMuro;
    [SerializeField]
    private bool muroActivado;
    [SerializeField] private float lifeMuro;
    // Start is called before the first frame update

    public event Action destruyoPared;  

    private void Awake()
    {
        
    }
    void Start()
    {
        //muroActivado = false;
        //colisionMuro.enabled = muroActivado;

        madera = transform.GetChild(0).transform;
        //lifeMuro = 0;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConstruirMuro()
    {
        
        muro.fillAmount += 0.0005f;
        lifeMuro = muro.fillAmount;
        if (muro.fillAmount == 1 && !muroActivado)
        {
            muroActivado = true;
            madera.gameObject.SetActive(true);
            colisionMuro.enabled = muroActivado;
        }
       
    }

    public void DestruirMuro()
    {
        if (lifeMuro <=0 )
        {
            madera.gameObject.SetActive(false);
            colisionMuro.enabled = false;
            if (destruyoPared != null) destruyoPared();
        }
        else
        {
            lifeMuro -= 3f;
        }
    }
}
