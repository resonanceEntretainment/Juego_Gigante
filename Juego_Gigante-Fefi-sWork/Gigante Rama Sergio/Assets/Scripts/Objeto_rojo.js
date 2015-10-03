#pragma strict

var boss1: GameObject;
var Personaje: GameObject;

function Start () {
    boss1 = GameObject.Find("boss1");
}

function Update () {

}

function OnCollisionEnter2D(coll: Collision2D) {
	if (coll.gameObject.tag == "Piso"){
		Destroy(gameObject);
    }
	if (coll.gameObject.tag == "caja"){
	    boss1.GetComponent(Boss_1).salud = boss1.GetComponent(Boss_1).salud - 1;
	    boss1.GetComponent.<Rigidbody2D>().velocity.y = boss1.GetComponent.<Rigidbody2D>().velocity.y + 7;
	    Destroy(gameObject);
	}
	if (coll.gameObject.tag == "PJ"){
//	    boss1.GetComponent(Personaje).salud = boss1.GetComponent(Personaje).salud - 1;
	    Destroy(gameObject);
	}
		
}