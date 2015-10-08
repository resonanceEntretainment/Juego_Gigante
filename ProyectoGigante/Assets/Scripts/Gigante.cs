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
	private GameObject caja;			//La caja
    public Animator anim;
    

        void Start () {
        caja = GameObject.Find("caja");
        gigante = GetComponent<Rigidbody2D>();
		agarrado = false;
		viendoDer = true;
        anim = GetComponent<Animator>();
    }

    void Update() {

        Vector2 tempVelocity; //Variable temporal para asignar la velocidad
        tempVelocity = gigante.velocity;

        if (Input.GetKey(movimientoIzq)) {

            tempVelocity.x = -velocidad;
            gigante.velocity = tempVelocity;
            if (viendoDer)
            {
                viendoDer = false;
                Vector3 escala = transform.localScale;
                escala.x *= -1;
                transform.localScale = escala;
            }
            anim.SetBool("estacaminando", true);
        }

        else if (Input.GetKey(movimientoDer)) {

            tempVelocity.x = velocidad;
            gigante.velocity = tempVelocity;
            if (!viendoDer)
            {
                viendoDer = true;
                Vector3 escala = transform.localScale;
                escala.x *= -1;
                transform.localScale = escala;
            }
            anim.SetBool("estacaminando", true);

        }

        else
        {
            anim.SetBool("estacaminando", false);
            tempVelocity = new Vector2(0, 0);
            gigante.velocity = tempVelocity;
        }

        //fix para las cajas azules
        if (!agarrado)
        {
            caja.transform.SetParent(null);
            caja.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        //

		if (agarrado && Input.GetKey(lanzar)){
			Debug.Log("tercer if ");
			agarrado = false;
            caja.transform.SetParent(null);
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
	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "caja"){
			Debug.Log("agarrado " + agarrado);
			if (!agarrado){
				//agarrar caja
				agarrado = true;
				Debug.Log("agarrado " + agarrado);
                caja.transform.SetParent(gigante.transform);
				caja.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
				caja.transform.localPosition = new Vector2(1.3f, 0f);
			}
		}
	}
}