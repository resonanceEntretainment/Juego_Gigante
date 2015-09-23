#pragma strict

var movimientoIzq: KeyCode;
var movimientoDer: KeyCode;
var velocidad: int;
 
function Start () {
		
}

function Update () {

	if(Input.GetKey(movimientoIzq)){
		GetComponent.<Rigidbody2D>().velocity.x = -velocidad;
	}
	
	if(Input.GetKey(movimientoDer)){
		GetComponent.<Rigidbody2D>().velocity.x = velocidad;
	}
}