using UnityEngine;
using System.Collections;

public class Gigante : MonoBehaviour
{
	public KeyCode movimientoIzq;
	public KeyCode movimientoDer;
	public float velocidad;
	private Rigidbody2D gigante;

	void Start () {
		gigante = GetComponent<Rigidbody2D>();
	}
	
	void Update () {

		Vector2 tempVelocity;
		tempVelocity = gigante.velocity;

		if(Input.GetKey(movimientoIzq)){
			tempVelocity.x = velocidad;
			gigante.velocity = tempVelocity;
		}
		
		if(Input.GetKey(movimientoDer)){
			tempVelocity.x = -velocidad;
			gigante.velocity = tempVelocity;
		}
	}
}