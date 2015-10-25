﻿using UnityEngine;
using System.Collections;

public class Gigante : MonoBehaviour
{
    
    public KeyCode movimientoIzq;   //Tecla para que el gigante se mueva a la izquierda
    public KeyCode movimientoDer;   //Tecla para que el gigante se mueva a la derecha
    public KeyCode lanzar;		    //Tecla para lanzar la caja
    public float velocidad;         //Velocidad a la que se moverá la caja
    public float fuerzaLanzamiento; //Fuerza con la que se lanzará la caja
    public bool agarrado;			//Determinar si está sosteniendo la caja
    public bool viendoDer;         //Determinar si el jugador está viendo a la derecha
    public int salud;              //Salud del gigante
    private Rigidbody2D gigante;	//El gigante
    private GameObject caja;			//La caja
    public Animator anim;
    

    void Start () {
        caja = GameObject.Find("caja");
        gigante = GetComponent<Rigidbody2D>();
        agarrado = false;
        viendoDer = true;
        salud = 3;
        anim = GetComponent<Animator>();
    }

    void Update() {

        if (salud>0){
            ComportamientoGiganteNormal();
        }
    }
    void ComportamientoGiganteNormal(){

        Vector2 tempVelocity; //Variable temporal para asignar la velocidad
        tempVelocity = gigante.velocity;

        if (Input.GetKey(movimientoIzq)) {

            tempVelocity.x = -velocidad;
            gigante.velocity = tempVelocity;
            if (viendoDer)
            {
                viendoDer = false;
                Vector3 escala = transform.localScale;
                escala.x *= -1;
                transform.localScale = escala;
            }
            anim.SetBool("estacaminando", true);
        }

        else if (Input.GetKey(movimientoDer)) {

            tempVelocity.x = velocidad;
            gigante.velocity = tempVelocity;
            if (!viendoDer)
            {
                viendoDer = true;
                Vector3 escala = transform.localScale;
                escala.x *= -1;
                transform.localScale = escala;
            }
            anim.SetBool("estacaminando", true);
        }

        else
        {
            anim.SetBool("estacaminando", false);
            tempVelocity = new Vector2(0, 0);
            gigante.velocity = tempVelocity;
        }

        //fix para las cajas azules
        if (!agarrado)
        {
            anim.SetBool("tieneCaja", false);
            caja.transform.parent = null;
        }

        //
        else {
            caja.GetComponent<Rigidbody2D>().velocity = new Vector2(gigante.velocity.x, gigante.velocity.y);
            caja.transform.localPosition = new Vector2(1.3f, 0f);
        }

        if (agarrado && Input.GetKey(lanzar)){
            Debug.Log("tercer if ");
            agarrado = false;
            anim.SetBool("tieneCaja", false);
            caja.transform.parent = null;
            if (viendoDer){
                caja.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (fuerzaLanzamiento, 
		                                                                    fuerzaLanzamiento/2);
            }
            else{
                caja.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (-fuerzaLanzamiento, 
		                                                                    fuerzaLanzamiento/2);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D coll) {

        if (coll.gameObject.tag == "caja"){
            Debug.Log("agarrado " + agarrado);
            if (!agarrado){
                //agarrar caja
                agarrado = true;
                anim.SetBool("tieneCaja", true);
                Debug.Log("agarrado " + agarrado);
                caja.transform.parent = transform;
                caja.transform.localPosition = new Vector2(1.3f, 0f);
            }
        }
    }
    
    void Derrotado(){

    }
}
