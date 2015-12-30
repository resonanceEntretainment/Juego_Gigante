using UnityEngine;
using System.Collections;

public class Cajas : MonoBehaviour{
    private AudioSource Source;
    public AudioClip cajaGolpeada;
    private GameObject boss1;
    private GameObject ControlDelJuego;
    private bool cajaDescubierta;
    void Start(){
        boss1 = GameObject.Find("boss1");
        ControlDelJuego = GameObject.Find("ControlDelJuego");
        Source = GetComponent<AudioSource>();
        cajaDescubierta = false;
    }

    void Update(){
    }

    void OnCollisionEnter2D(Collision2D coll){

        if (coll.gameObject.tag == "PJ"){
            if (!cajaDescubierta){
                ControlDelJuego.GetComponent<ControlDelNivel>().CajaTocada = true;
                boss1 = GameObject.Find("boss1");
                cajaDescubierta = true;
            }
            else if (cajaDescubierta){
                boss1 = GameObject.Find("boss1");
                boss1.GetComponent<Boss_1>().castigar = false;
            }
        }
        if (coll.gameObject.tag == "Objeto Rojo"){
            Source.PlayOneShot(cajaGolpeada,1f);
        }
    }
}
