using UnityEngine;
using System.Collections;

public class Cajas : MonoBehaviour{
    private AudioSource Source;
    public AudioClip cajaGolpeada;
    private GameObject boss1;
    void Start(){
        boss1 = GameObject.Find("boss1");
        Source = GetComponent<AudioSource>();
    }

    void Update(){
    }

    void OnCollisionEnter2D(Collision2D coll){

        if (coll.gameObject.tag == "PJ"){
            boss1.GetComponent<Boss_1>().castigar = false;
        }
        if (coll.gameObject.tag == "Objeto Rojo"){
            Source.PlayOneShot(cajaGolpeada,1f);
        }
    }
}
