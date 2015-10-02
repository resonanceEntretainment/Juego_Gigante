#pragma strict

var Personaje: GameObject;
var boss1: GameObject;

function Start () {
    boss1 = GameObject.Find("boss1");
}

function Update () {
   
}
function OnCollisionEnter2D(coll: Collision2D){
    if (coll.gameObject.tag == "PJ"){
        boss1.GetComponent(Boss_1).castigar = false;
    }
}