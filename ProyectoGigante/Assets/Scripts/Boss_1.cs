﻿using UnityEngine;
using System.Collections;

public class Boss_1 : MonoBehaviour {
    public int vel_vertical;
    public float vel_horizontal;
    public int salud;
    public GameObject objetivo;
    public GameObject personaje;
    public int distancia_persecucion;
    public GameObject objeto_rojo;
    public GameObject objeto_azul;
    public GameObject caja;
    public Vector2 posicion_spawn;
    public float distancia_spawn_vert;
    public bool lanzar;
    public bool ataque_basico = true;
    public float radio_de_seguimiento;
    public bool derecha;
    public float timer = 100;
    public int max_cajas;
    public int cajas = 0;
    public int contador = 0;
    public int contador2 = 40;
    public float altura_de_vuelo;
    public bool arriba = false;
    public float frecuencia_cajas;
    public float frecuencia_castigo;
    public float frecuencia_especial;
    public float frecuencia_espasmos;
    public float duracion_especial;
    public int azules_por_combo;
    public int numero_random;
    public int azules;
    public int rojos;
    public bool castigar = false;
    public int version_castigo;
    public int repeticiones_castigo = 4;
    public bool separacion_castigo;
    public bool panic = false;
    public int contador_ataques;
    public int numero_de_balas;
    private Rigidbody2D RigidBody2DBoss;
    public int progreso_especial = 0;
    public Animator anim;
    public bool Intro = true;
    public GameObject ataqueDiagonal;
    public int SecuenciaIntro = 0;
    public bool EmpujonPanicoNecesario = false;
    public bool CastigoCancelado = false;


    void Start()
    {
        objetivo = GameObject.Find("BossOrbita");
        personaje = GameObject.Find("Gigante");
        caja = GameObject.Find("caja");
        separacion_castigo = false;
        RigidBody2DBoss = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        timer = 3;
    }

    void Update()
    {
        Bounce();
        timer -= Time.deltaTime;
        if (Intro){
            transform.position = new Vector2(0, 
            transform.position.y);
            //Inmovilización del personaje
            if(personaje.GetComponent<Gigante>().chocoConPared == false){
                objeto_azul.GetComponent<objeto_azul>().Empujar(personaje, caja,false);
            }
            if((personaje.GetComponent<Gigante>().chocoConPared == true)){
                objeto_azul.GetComponent<objeto_azul>().Detener(personaje);
            }
            //Codigo para la primera pelota negra del boss
            if ((SecuenciaIntro == 0)&&(timer<0)){
                posicion_spawn = new Vector2(0,
                8.2f);
                ataqueDiagonal = (GameObject)Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
                anim.SetBool("Atacando", true);
                ataqueDiagonal.GetComponent<objeto_rojo>().diagonal = true;
                SecuenciaIntro = 1;
                timer = 3;
            }
            else if ((SecuenciaIntro == 1)&&(timer<0)){
                Intro = false;
            }
        }
        else{
            if (salud <= 0)
            {
                ClearBullets();
                Destroy(gameObject);
            }
            else if (salud == 1)
            {
                EstadoDePanico();
            }
            else
            {
                ComportamientoRegular();
            }
        }
    }
    void ComportamientoRegular(){

        Movimiento();
        if (!castigar)
        {
            if(CastigoCancelado){
                ClearBullets();
                CastigoCancelado = false;
            }
            separacion_castigo = false;
            if (ataque_basico)
            {
                if (timer <= 0)
                {
                    if (cajas < max_cajas)
                    {
                        RegularAttack();
                    }
                    else if (cajas == max_cajas)
                    {
                        ataque_basico = false;
                        timer = duracion_especial;
                    }
                }
            }
            else
            {
                SpecialAttack();
                if (timer <= 0)
                {
                    ataque_basico = true;
                }
            }

            if (timer <= 0)
            {
                timer = frecuencia_cajas;
            }
        }
        else if (castigar)
        {
            cajas = 0;
            ataque_basico = true;
            if (timer <= 0)
            {
                Castigo1();
            }
            if (timer <= 0)
            {
                timer = frecuencia_castigo;
            }
        }
    }
    void Castigo1()
    {

        if (!separacion_castigo)
        {
            separacion_castigo = true;
        }
        if (repeticiones_castigo > 3)
        {
            numero_random = Random.Range(0, 2);
            repeticiones_castigo = 0;
        }
        if (numero_random == 0)
        {
            frecuencia_castigo = 1;
            if (version_castigo == 0)
            {
                anim.SetBool("Atacando", true);
                posicion_spawn = new Vector2(objetivo.transform.position.x - 6,
                transform.position.y - distancia_spawn_vert);
                Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
                posicion_spawn = new Vector2(objetivo.transform.position.x - 3, 
                transform.position.y - distancia_spawn_vert);
                Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
                posicion_spawn = new Vector2(objetivo.transform.position.x,
                transform.position.y - distancia_spawn_vert);
                Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
                posicion_spawn = new Vector2(objetivo.transform.position.x + 3, 
                transform.position.y - distancia_spawn_vert);
                Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
                posicion_spawn = new Vector2(objetivo.transform.position.x + 6, 
                transform.position.y - distancia_spawn_vert);
                Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
                version_castigo = 1;
            }
            else if (version_castigo == 1)
            {
                anim.SetBool("Atacando", true);
                posicion_spawn = new Vector2(objetivo.transform.position.x - 4.5f, 
                transform.position.y - distancia_spawn_vert);
                Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
                posicion_spawn = new Vector2(objetivo.transform.position.x - 1.5f, 
                transform.position.y - distancia_spawn_vert);
                Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
                posicion_spawn = new Vector2(objetivo.transform.position.x + 1.5f, 
                transform.position.y - distancia_spawn_vert);
                Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
                posicion_spawn = new Vector2(objetivo.transform.position.x + 4.5f, 
                transform.position.y - distancia_spawn_vert);
                Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
                version_castigo = 0;
            }
            repeticiones_castigo = repeticiones_castigo + 1;
        }
        else if (numero_random == 1)
        {
            frecuencia_castigo = 0.2f;
            if (version_castigo < 4){
                anim.SetBool("Atacando", true);
                posicion_spawn = new Vector2(objetivo.transform.position.x
                + 3*version_castigo - 6, transform.position.y - distancia_spawn_vert);
                Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
                version_castigo += 1;
            }
            else if ((version_castigo >= 4)&&(version_castigo < 7)){
                anim.SetBool("Atacando", true);
                posicion_spawn = new Vector2(objetivo.transform.position.x
                - (3*version_castigo - 3) + 12, transform.position.y - distancia_spawn_vert);
                Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
                version_castigo += 1;
            }
            else if (version_castigo == 7){
                repeticiones_castigo += 1;
                version_castigo = 0;
                
            }
        }
        
    }
    void Bounce()
    {
        if (transform.position.y != altura_de_vuelo)
        {
            if ((transform.position.y < altura_de_vuelo))
            {
                RigidBody2DBoss.velocity = new Vector2(RigidBody2DBoss.velocity.x,
                RigidBody2DBoss.velocity.y - (RigidBody2DBoss.velocity.y / contador2)
                + (Mathf.Abs(transform.position.y
                - altura_de_vuelo)));

            }

            else if (transform.position.y > altura_de_vuelo)
            {
                RigidBody2DBoss.velocity = new Vector2(RigidBody2DBoss.velocity.x,
                RigidBody2DBoss.velocity.y - (RigidBody2DBoss.velocity.y / contador2)
                - (Mathf.Abs(transform.position.y
                - altura_de_vuelo)));
            }
        }
    }
    void RegularAttack()
    {

        posicion_spawn = new Vector2(transform.position.x,
        transform.position.y - distancia_spawn_vert);
        if ((azules < azules_por_combo) && (rojos < max_cajas - azules_por_combo))
        {
            numero_random = Random.Range(0, 2);
            if (numero_random == 1)
            {
                azules = azules + 1;
                Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
                anim.SetBool("Atacando", true);
            }
            else
            {
                rojos = rojos + 1;
                Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
                anim.SetBool("Atacando", true);
            }
        }
        else if ((azules < azules_por_combo) && !(rojos < max_cajas - azules_por_combo))
        {
            azules = azules + 1;
            Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
            anim.SetBool("Atacando", true);
        }
        else if (!(azules < azules_por_combo) && (rojos < max_cajas - azules_por_combo))
        {
            rojos = rojos + 1;
            Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
            anim.SetBool("Atacando", true);
        }
        cajas = cajas + 1;
        lanzar = false;

    }
    void SpecialAttack()
    {

        if ((timer <= duracion_especial)&&
        (timer >= duracion_especial - frecuencia_especial) && (progreso_especial == 0))
        {
            posicion_spawn = new Vector2(personaje.transform.position.x,
            transform.position.y - distancia_spawn_vert);
            Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
            progreso_especial = 1;
        }
        else if ((timer <= duracion_especial - frecuencia_especial)&&
        ((timer >= duracion_especial - frecuencia_especial * 2)) && (progreso_especial == 1))
        {
            posicion_spawn = new Vector2(personaje.transform.position.x - 1, 
            transform.position.y - distancia_spawn_vert);
            Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
            posicion_spawn = new Vector2(personaje.transform.position.x + 1, 
            transform.position.y - distancia_spawn_vert);
            Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
            progreso_especial = 2;
        }
        else if ((timer <= duracion_especial - frecuencia_especial * 2)&&
        ((timer >= duracion_especial - frecuencia_especial * 3))&&
        (progreso_especial == 2))
        {
            posicion_spawn = new Vector2(personaje.transform.position.x - 2,
            transform.position.y - distancia_spawn_vert);
            Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
            posicion_spawn = new Vector2(personaje.transform.position.x + 2, 
            transform.position.y - distancia_spawn_vert);
            Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
            progreso_especial = 0;
        }

        cajas = 0;
        azules = 0;
        rojos = 0;
    }
    void Movimiento() { 

        if (derecha){
            RigidBody2DBoss.velocity = new Vector2(vel_horizontal, 
            RigidBody2DBoss.velocity.y);    
        }
        else {
            RigidBody2DBoss.velocity = new Vector2(-vel_horizontal, 
            RigidBody2DBoss.velocity.y);
        }
        if (((objetivo.transform.position.x + radio_de_seguimiento) 
            <= transform.position.x)){
            derecha = false;
        }
        else if (((objetivo.transform.position.x - radio_de_seguimiento) 
            >= transform.position.x)){
            derecha = true;
        }
    }

    void EstadoDePanico(){

            PanicShake();
            if (!panic)
            {
                ClearBullets();
                objeto_azul.GetComponent<objeto_azul>().Empujar(personaje, caja,true);
                timer = frecuencia_espasmos;
                anim.SetBool("Desesperado", true);
                panic = true;
            }
            if (castigar)
            {
                if(!EmpujonPanicoNecesario){
                    ClearBullets();
                    EmpujonPanicoNecesario = true; 
                }
                cajas = 0;
                ataque_basico = true;
                if (timer <= 0)
                {
                    Castigo1();
                }
                if (timer <= 0)
                {
                    timer = frecuencia_castigo;
                }
            }
            else
            {
                if(EmpujonPanicoNecesario){
                    objeto_azul.GetComponent<objeto_azul>().Empujar(personaje, caja,true);
                    ClearBullets();
                    EmpujonPanicoNecesario = false;
                }
                separacion_castigo = false;
                PanicAttack();
            }
    }

    void PanicAttack()
    {
        if (timer <= 0)
        {
            RigidBody2DBoss.velocity = new Vector2(RigidBody2DBoss.velocity.y, 10);
            posicion_spawn = new Vector2(10 - contador_ataques, transform.position.y - distancia_spawn_vert);
            if (contador_ataques == 0)
            {
                Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
                contador_ataques = contador_ataques + 1;
            }
            else if ((contador_ataques > 0)&&(contador_ataques < numero_de_balas))
            {
                Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
                contador_ataques = contador_ataques + 1;
            }
            else if (contador_ataques == numero_de_balas)
            {
                contador_ataques = 0;
            }
            timer = frecuencia_espasmos;

        }
    }
    void PanicShake()
    {
        if (transform.position.x < -0.3)
        {
            RigidBody2DBoss.velocity = new Vector2(vel_horizontal,
            RigidBody2DBoss.velocity.y);
        }
        else if (transform.position.x > 0.3)
        {
            RigidBody2DBoss.velocity = new Vector2(-vel_horizontal, 
            RigidBody2DBoss.velocity.y);
        }
    }
    void terminarAuch(){
        anim.SetBool("Herido", false);
    }
    void terminarAtaque(){
        anim.SetBool("Atacando", false);
    }
    void ClearBullets(){
        GameObject[] BalasRojas = GameObject.FindGameObjectsWithTag("Objeto Rojo");
        GameObject[] BalasAzules = GameObject.FindGameObjectsWithTag("Objeto Azul");
        foreach (GameObject target in BalasRojas) {
            GameObject.Destroy(target);
        }
        foreach (GameObject target in BalasAzules) {
            GameObject.Destroy(target);
        }
    }
}
