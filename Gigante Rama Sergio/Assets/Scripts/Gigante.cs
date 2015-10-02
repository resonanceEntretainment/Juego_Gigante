using UnityEngine;
using System.Collections;

public class Gigante : MonoBehaviour
{
	public KeyCode movimientoIzq;   //Tecla para que el gigante se mueva a la izquierda
	public KeyCode movimientoDer;   //Tecla para que el gigante se mueva a la derecha
	public KeyCode lanzar;		    //Tecla para lanzar la caja
	public float velocidad;         //Velocidad a la que se moverá la caja
	public float fuerzaLanzamiento; //Fuerza con la que se lanzará la caja
	public bool agarrado;			//Determinar si está sosteniendo la caja
	private bool viendoDer;         //Determinar si el jugador está viendo a la derecha
	private Rigidbody2D gigante;	//El gigante
	private Transform caja;			//La caja

	void Start () {
		gigante = GetComponent<Rigidbody2D>();
		agarrado = false;
		viendoDer = true;
	}
	
	void Update () {
		
		Vector2 tempVelocity; //Variable temporal para asignar la velocidad
		tempVelocity = gigante.velocity;
		
		if (Input.GetKey(movimientoIzq)){
			tempVelocity.x = -velocidad;
			gigante.velocity = tempVelocity;
		}
		
		if (Input.GetKey(movimientoDer)){
			tempVelocity.x = velocidad;
			gigante.velocity = tempVelocity;
		}

		if (agarrado && Input.GetKey(lanzar)){
			Debug.Log("tercer if ");
			agarrado = false;
			caja.parent = null;
			caja.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
			if (viendoDer){
				caja.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (fuerzaLanzamiento, 
				                                                                    2*fuerzaLanzamiento);
			}
			else{
				caja.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (-fuerzaLanzamiento, 
				                                                                    2*fuerzaLanzamiento);
			}
			caja = null;
		}

		if ((gigante.velocity.x > 0 && !viendoDer) || (gigante.velocity.x < 0 && viendoDer)){
			viendoDer = !viendoDer;
			Vector3 escala = transform.localScale;
			escala.x *= -1;
			transform.localScale = escala;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "caja"){
			Debug.Log("agarrado " + agarrado);
			if (!agarrado){
				//agarrar caja
				agarrado = true;
				Debug.Log("agarrado " + agarrado);
				caja = coll.transform;
				caja.parent = gigante.transform;
				caja.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
				caja.localPosition = new Vector2(1.3f, 0f);
			}
		}
	}
}