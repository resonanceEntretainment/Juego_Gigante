#pragma strict

var boss1: GameObject;
var timer: int = 20;
function Start () {
    boss1 = GameObject.Find("boss1"); 
}

function Update () {

}

function OnCollisionEnter2D(coll: Collision2D) {
	if (coll.gameObject.tag == "PJ"){
		boss1.GetComponent(Boss_1).castigar = true;
		Destroy(gameObject);
	}
	if (coll.gameObject.tag == "Piso"){
		Destroy(gameObject);
    }
    if (timer == 0){
        Destroy(gameObject);
    }
    timer -= Time.deltaTime;
}