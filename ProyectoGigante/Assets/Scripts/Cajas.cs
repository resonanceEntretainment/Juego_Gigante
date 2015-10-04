using UnityEngine;
using System.Collections;

public class Cajas : MonoBehaviour{
    private GameObject boss1;
    void Start(){
        boss1 = GameObject.Find("boss1");
    }

    void Update(){
    }

    void OnCollisionEnter2D(Collision2D coll){

        if (coll.gameObject.tag == "PJ"){
            boss1.GetComponent<Boss_1>().castigar = false;
        }
    }
}
