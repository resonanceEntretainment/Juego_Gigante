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
    public bool ArribaDeLaCabeza;
    public bool tomoDanno = false;
    public bool seRecupero = false;
    public bool detenido = false;
    public bool chocoConPared = false;
    private Rigidbody2D gigante;	//El gigante
    private GameObject caja;			//La caja
    private GameObject ControlDelNivel;
    private Vector2 velocidadCero;
    private bool señalDeDerrota;
    public Animator anim;
    

    void Start () {
        caja = GameObject.Find("caja");
        ControlDelNivel = GameObject.Find("ControlDelJuego");
        gigante = GetComponent<Rigidbody2D>();
        agarrado = false;
        viendoDer = true;
        empujado = false;
        señalDeDerrota = false;
        salud = 3;
        anim = GetComponent<Animator>();
        velocidadCero.x = 0;
        velocidadCero.y = 0;
        ArribaDeLaCabeza = false;
        detenido = false;
    }

    void Update() {
        if (salud>0){
            if (tomoDanno){
                anim.SetBool("tomoDanno", true);
                tomoDanno = false;
                seRecupero = true;
            }
            else{
                if (seRecupero){
                    Debug.Log("Si entro a la cosa");
                    anim.SetBool("tomoDanno", false);
                    seRecupero = false;
                }
            }
            ComportamientoGiganteNormal();
        }
        else{
            Derrotado();
        }
    }
    void ComportamientoGiganteNormal(){

        if (!empujado){
            if (detenido){
                //*****************************//
                //Espera para la primera pelota//
                //*****************************//
                MirarALaDer();
                ControlDeLaCaja();
                LevantarLaCaja();
                detenido = false;
            }
            else{
                //******************//
                //Movimiento regular//
                //******************//
                //Codigo para moverse
                MovimientoRegular();
                if (!(caja == null)){
                    //Codigo para agarrar la caja
                    ControlDeLaCaja();
                    //Codigo para levantar caja
                    LevantarLaCaja();
                    //Codigo para lanzar la caja
                    LanzamientoDeLaCaja();
                }
            }
        }
        else {
            //******************//
            //     Empujado     //
            //******************//
            anim.SetBool("esEmpujado", true);
            MirarALaDer();
            ControlDeLaCaja();
            LevantarLaCaja();
            gigante.velocity = new Vector2(-10,0);

        }
    }
    void OnCollisionEnter2D(Collision2D coll) {

        if (coll.gameObject.tag == "caja"){
            if (!agarrado){
                //agarrar caja
                agarrado = true;
                anim.SetBool("tieneCaja", true);
                caja.transform.parent = transform;
                caja.transform.localPosition = new Vector2(1.3f, 0.3f);
            }
        }
        if (coll.gameObject.tag == "Pared"){
            Debug.Log("ChocoConPared");
            chocoConPared = true;
            anim.SetBool("esEmpujado", false);
            empujado = false;
            detenido = true;
        }
    }
    
    void MovimientoRegular(){
        Vector2 tempVelocity; //Variable temporal para asignar la velocidad
        tempVelocity = gigante.velocity;
        if (Input.GetKey(movimientoIzq)) {

                tempVelocity.x = -velocidad;
                gigante.velocity = tempVelocity;
                MirarALaIzq();
                anim.SetBool("estacaminando", true);
        }
        else if (Input.GetKey(movimientoDer)) {

                tempVelocity.x = velocidad;
                gigante.velocity = tempVelocity;
                MirarALaDer();
                anim.SetBool("estacaminando", true);
        }
        else
        {
            if (!(tempVelocity.x == 0)){
                anim.SetBool("estacaminando", false);
                tempVelocity = new Vector2(0, 0);
                gigante.velocity = tempVelocity;
            }
        }
    }
    void MirarALaIzq(){
        if (viendoDer){
            viendoDer = false;
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }
    void MirarALaDer(){
        if (!viendoDer){
            viendoDer = true;
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }
    void ControlDeLaCaja(){
        if(ArribaDeLaCabeza){
            if (!agarrado){
                anim.SetBool("tieneCaja", false);
                caja.transform.parent = null;
            }
            else {
                caja.GetComponent<Rigidbody2D>().velocity = new Vector2
                (gigante.velocity.x, gigante.velocity.y);
                caja.transform.localPosition = new Vector2(0.5f, 2f);
            }
        }
        else{
            if (!agarrado){
                anim.SetBool("tieneCaja", false);
                caja.transform.parent = null;
            }
            else {
                caja.GetComponent<Rigidbody2D>().velocity = new Vector2
                (gigante.velocity.x, gigante.velocity.y);
                caja.transform.localPosition = new Vector2(1.3f, 0.3f);
            }
        }
    }
    void LevantarLaCaja(){
        if (Input.GetButtonDown("LevantarCaja")){
            ArribaDeLaCabeza=true;
        }
        else if(Input.GetButtonDown("BajarCaja")){
            ArribaDeLaCabeza=false;
        }
    } 
    void LanzamientoDeLaCaja(){
        if (agarrado && Input.GetKey(lanzar)){
            agarrado = false;
            anim.SetBool("tieneCaja", false);
            caja.transform.parent = null;
            if (viendoDer){
                caja.gameObject.GetComponent<Rigidbody2D>().velocity = 
                new Vector2 (fuerzaLanzamiento, fuerzaLanzamiento/2);
            }
            else{
                caja.gameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector2 (-fuerzaLanzamiento,fuerzaLanzamiento/2);
            }
            if (ControlDelNivel.GetComponent<ControlDelNivel>().Tutorial == 1){
                ControlDelNivel.GetComponent<ControlDelNivel>().Tutorial = 2;
            }
        }
    }
    void Derrotado(){
        if (!muerteDetectada){
            muerteDetectada = true;
            anim.SetBool("estacaminando", false);
            anim.SetBool("tieneCaja", false);
            timer = 3;
        }
        else{
            timer -= Time.deltaTime;
            gigante.velocity = velocidadCero;
            if ((timer < 0)&&(!señalDeDerrota)){
                ControlDelNivel.GetComponent<ControlDelNivel>().Derrota = true;
                señalDeDerrota = true;
            }
        }
    }
    void Reinicializar(){
        

    }
}
