using UnityEngine;
using System.Collections;

public class Gigante : MonoBehaviour
{
    
    public KeyCode movimientoIzq;   //Tecla para que el gigante se mueva a la izquierda
    public KeyCode movimientoDer;   //Tecla para que el gigante se mueva a la derecha
    public KeyCode lanzar;		    //Tecla para lanzar la caja
    public float velocidad;         //Velocidad a la que se moverá el gigante
    public float fuerzaLanzamiento; //Fuerza con la que se lanzará la caja
    public bool agarrado;			//Determinar si está sosteniendo la caja
    public bool viendoDer;         //Determinar si el jugador está viendo a la derecha
    public bool empujado;
    public bool muerteDetectada;
    public int salud;              //Salud del gigante
    public float timer;
    private Rigidbody2D gigante;	//El gigante
    private GameObject caja;			//La caja
    private Vector2 velocidadCero;
    public Animator anim;
    

    void Start () {
        caja = GameObject.Find("caja");
        gigante = GetComponent<Rigidbody2D>();
        agarrado = false;
        viendoDer = true;
        empujado = false;
        salud = 3;
        anim = GetComponent<Animator>();
        velocidadCero.x = 0;
        velocidadCero.y = 0;
    }

    void Update() {

        if (salud>0){
            ComportamientoGiganteNormal();
        }
        else{
            Derrotado();
        }
    }
    void ComportamientoGiganteNormal(){

        if (!empujado){
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
                anim.SetBool("tieneCaja", false);
                caja.transform.parent = null;
            }

            //
            else {
                caja.GetComponent<Rigidbody2D>().velocity = new Vector2
                (gigante.velocity.x, gigante.velocity.y);
                caja.transform.localPosition = new Vector2(1.3f, 0f);
            }

            if (agarrado && Input.GetKey(lanzar)){
                Debug.Log("tercer if ");
                agarrado = false;
                anim.SetBool("tieneCaja", false);
                caja.transform.parent = null;
                if (viendoDer){
                    caja.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (fuerzaLanzamiento, 
		                                                                    fuerzaLanzamiento/2);
                }
                else{
                    caja.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (-fuerzaLanzamiento, 
		                                                                    fuerzaLanzamiento/2);
                }
            }
        }
        else {
            gigante.velocity = new Vector2(-10,0);
            if (timer <= 0){
                empujado = false;
            }
            else {
                timer -= Time.deltaTime;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D coll) {

        if (coll.gameObject.tag == "caja"){
            Debug.Log("agarrado " + agarrado);
            if (!agarrado){
                //agarrar caja
                agarrado = true;
                anim.SetBool("tieneCaja", true);
                Debug.Log("agarrado " + agarrado);
                caja.transform.parent = transform;
                caja.transform.localPosition = new Vector2(1.3f, 0f);
            }
        }
        if (coll.gameObject.tag == "Pared"){
            empujado = false;
        }
    }
    
    void Derrotado(){
        if (!muerteDetectada){
            muerteDetectada = true;
            timer = 3;
        }
        else{
            timer -= Time.deltaTime;
            gigante.velocity = velocidadCero;
            if (timer < 0){
                Destroy(gameObject);
            }
        }
    }
    void Reinicializar(){
        

    }
}
