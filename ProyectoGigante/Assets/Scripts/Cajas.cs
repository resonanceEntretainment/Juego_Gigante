using UnityEngine;
using System.Collections;

public class Cajas : MonoBehaviour{
    private AudioSource Source;
    public AudioClip cajaGolpeada;
    private GameObject boss1;
    private GameObject ControlDelJuego;
    private bool cajaDescubierta;
    private bool bossActivado;
    void Start(){
        boss1 = GameObject.Find("boss1");
        ControlDelJuego = GameObject.Find("ControlDelJuego");
        Source = GetComponent<AudioSource>();
        cajaDescubierta = false;
        bossActivado = false;
    }

    void Update(){
    }

    void OnCollisionEnter2D(Collision2D coll){
        //Cuando se consigue la caja por primera vez empieza el tutorial
        if (coll.gameObject.tag == "PJ"){
            if (!cajaDescubierta){
                ControlDelJuego.GetComponent<ControlDelNivel>().Tutorial = 1;
                boss1 = GameObject.Find("boss1");
                cajaDescubierta = true;
            }
            else if (cajaDescubierta){
                if (!bossActivado){
                    //Cuando se agarra la caja por segunda vez despues de haberla lanzado termina el tutorial
                    if (ControlDelJuego.GetComponent<ControlDelNivel>().Tutorial == 2){
                        ControlDelJuego.GetComponent<ControlDelNivel>().Tutorial = 3;
                        bossActivado = true;
                    }
                }
                else{
                    boss1 = GameObject.Find("boss1");
                    if (boss1 != null){
                        if (boss1.GetComponent<Boss_1>().castigar){
                            boss1.GetComponent<Boss_1>().castigar = false;
                            boss1.GetComponent<Boss_1>().CastigoCancelado = true;
                        }
                    }
                }
            }
        }
        if (coll.gameObject.tag == "Objeto Rojo"){
            Source.PlayOneShot(cajaGolpeada,1f);
        }
    }
}
