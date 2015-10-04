using UnityEngine;
using System.Collections;

public class Objeto_azul : MonoBehaviour{
    void Start(){
    }

    void Update(){
    }

    void OnCollisionEnter2D(Collision2D coll){

        if (coll.gameObject.tag == "PJ"){
            //boss1.GetComponent<Boss_1>.castigar = false;
        }
    }
}
