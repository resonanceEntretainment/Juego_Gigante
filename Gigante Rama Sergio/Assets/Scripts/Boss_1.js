#pragma strict
    
var vel_vertical: int; 
var vel_horizontal: int;
var salud: int;
var objetivo : GameObject;
var personaje : GameObject;
var distancia_persecucion: int;
var objeto_rojo: GameObject;
var objeto_azul: GameObject;
var caja: GameObject;
var posicion_spawn : Vector2;
var distancia_spawn_vert: float;
var lanzar: boolean;
var ataque_basico: boolean = true;
var radio_de_seguimiento: int;
var derecha: boolean;
var timer: int = 100;
var max_cajas: int;
var cajas: int = 0;
var contador : int = 0; 
var contador2 : int = 40;
var altura_de_vuelo : float;
var arriba: boolean = false;
var frecuencia_cajas: int;
var frecuencia_castigo: int;
var frecuencia_especial: int;
var frecuencia_espasmos: int;
var duracion_especial : int;
var azules_por_combo: int;
var numero_random: int;
var azules: int;
var rojos: int;
var castigar : boolean =false;
var version_castigo: int;
var repeticiones_castigo: int = 4;
var separacion_castigo: boolean;
var panic : boolean = false;
var contador_ataque_fin : int;


function Start () {
    separacion_castigo = false;
}

function Update () {
    timer -= Time.deltaTime;
    Bounce();
    if (salud <= 0){
        Destroy(gameObject);
    }
    else if (salud == 1){
        PanicShake();
        if(!panic){
            personaje.GetComponent.<Rigidbody2D>().velocity.x = -100;
            timer = frecuencia_espasmos;
            panic = true;
        }
        if (castigar){
            cajas = 0;
            ataque_basico = true;
            if (timer == 0){
                Castigo1();
            }
            if (timer <= 0){
                timer = frecuencia_castigo;
            }
        }
        else{
            separacion_castigo = false;
            PanicAttack();
        }
    }
    else {
        Movimiento();
        if (!castigar){
            separacion_castigo = false;
            if (ataque_basico){
                if (timer == 0){
                    if (cajas < max_cajas){
                        RegularAttack();
                    }
                    else if (cajas == max_cajas) {
                        ataque_basico = false;
                        timer = duracion_especial;
                    }
                }
            }
            else{
                SpecialAttack();
                if (timer == 0){
                    ataque_basico = true;
                }
            }
        
            if (timer <= 0){
                timer = frecuencia_cajas;
            }
        }
        else if (castigar){
            cajas = 0;
            ataque_basico = true;
            if (timer == 0){
                Castigo1();
            }
            if (timer <= 0){
                timer = frecuencia_castigo;
            }
        }
    }            
}

function Castigo1(){
    
    if (!separacion_castigo){
        personaje.GetComponent.<Rigidbody2D>().velocity.x = -100;
        caja.GetComponent.<Rigidbody2D>().velocity.x = 100;
        separacion_castigo = true;
    }
    if (repeticiones_castigo>3){
        numero_random = Random.Range(0,2);
        repeticiones_castigo = 0;
    }
    if (numero_random == 0){

    	if (version_castigo == 0){
    		posicion_spawn = Vector2(objetivo.transform.position.x -5,transform.position.y -distancia_spawn_vert);
    		Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
    		posicion_spawn = Vector2(objetivo.transform.position.x -2,transform.position.y -distancia_spawn_vert);
    		Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
    		posicion_spawn = Vector2(objetivo.transform.position.x,transform.position.y -distancia_spawn_vert);
    		Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
    		posicion_spawn = Vector2(objetivo.transform.position.x +2,transform.position.y -distancia_spawn_vert);
    		Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
    		posicion_spawn = Vector2(objetivo.transform.position.x +5,transform.position.y -distancia_spawn_vert);        
    		Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
    		version_castigo = 1;    
    	}
    	else if (version_castigo == 1){
    		posicion_spawn = Vector2(objetivo.transform.position.x -3,transform.position.y -distancia_spawn_vert);
    		Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
    		posicion_spawn = Vector2(objetivo.transform.position.x -1,transform.position.y -distancia_spawn_vert);
    		Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
    		posicion_spawn = Vector2(objetivo.transform.position.x +1,transform.position.y -distancia_spawn_vert);
    		Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
	    	posicion_spawn = Vector2(objetivo.transform.position.x +3,transform.position.y -distancia_spawn_vert);        
        	Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
        	version_castigo = 0;
        }
    }
    else if (numero_random == 1){
            posicion_spawn = Vector2(objetivo.transform.position.x,transform.position.y -distancia_spawn_vert);
    		Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
    }
    repeticiones_castigo = repeticiones_castigo +1;
}
function Bounce(){
        if (transform.position.y != altura_de_vuelo){
            if (transform.position.y < altura_de_vuelo){
                GetComponent.<Rigidbody2D>().velocity.y = GetComponent.<Rigidbody2D>().velocity.y - GetComponent.<Rigidbody2D>().velocity.y/contador2 
                +(Mathf.Abs(transform.position.y
                - altura_de_vuelo));            
            }

            else if (transform.position.y > altura_de_vuelo){
                GetComponent.<Rigidbody2D>().velocity.y = GetComponent.<Rigidbody2D>().velocity.y - GetComponent.<Rigidbody2D>().velocity.y/contador2
                -(Mathf.Abs(transform.position.y 
                - altura_de_vuelo));
            }
        }
}
function RegularAttack(){

    posicion_spawn = Vector2(transform.position.x,transform.position.y -distancia_spawn_vert);            
    if ((azules<azules_por_combo)&&(rojos<max_cajas-azules_por_combo)){
        numero_random = Random.Range(0,2);
        if (numero_random == 1){
            azules = azules + 1;
            Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
        }
        else{
            rojos = rojos + 1;
            Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
        }
    }
    else if ((azules<azules_por_combo)&&!(rojos<max_cajas-azules_por_combo)){
        azules = azules + 1;
        Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
    }
    else if (!(azules<azules_por_combo)&&(rojos<max_cajas-azules_por_combo)){
         rojos = rojos + 1;
         Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
    }
    cajas = cajas + 1;
    lanzar = false;

}
function SpecialAttack(){

    if (timer == duracion_especial - frecuencia_especial){
        posicion_spawn = Vector2(personaje.transform.position.x,transform.position.y -distancia_spawn_vert);
  	    Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
   	}
  	else if (timer == duracion_especial - frecuencia_especial*2){
   	    posicion_spawn = Vector2(personaje.transform.position.x -1,transform.position.y -distancia_spawn_vert);
  	    Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
  	    posicion_spawn = Vector2(personaje.transform.position.x +1,transform.position.y -distancia_spawn_vert);
  	    Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
  	}
  	else if (timer == duracion_especial - frecuencia_especial*3){
   	    posicion_spawn = Vector2(personaje.transform.position.x -2,transform.position.y -distancia_spawn_vert);
  	    Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
  	    posicion_spawn = Vector2(personaje.transform.position.x +2,transform.position.y -distancia_spawn_vert);        
  	    Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
  	}
  	else if (timer == (duracion_especial - frecuencia_especial*2)){

  	}
  	else if (timer == (duracion_especial - frecuencia_especial*3)){

  	}
  	cajas = 0;
   	azules = 0;
   	rojos = 0;
}
function Movimiento(){
    if (derecha){
        GetComponent.<Rigidbody2D>().velocity.x = (vel_horizontal);    
    }
    else {
        GetComponent.<Rigidbody2D>().velocity.x = -(vel_horizontal); 
    }
    if (((objetivo.transform.position.x + radio_de_seguimiento) <= transform.position.x)){
        derecha = false;
    }
    else if (((objetivo.transform.position.x - radio_de_seguimiento) >= transform.position.x)){
        derecha = true;
    }
}
function PanicAttack(){

    if (timer == 0){
        GetComponent.<Rigidbody2D>().velocity.y = GetComponent.<Rigidbody2D>().velocity.y + 10;
        for(var i = 10;i>=0;i--){
            if (contador_ataque_fin == i){
                posicion_spawn = Vector2(5-i,transform.position.y -distancia_spawn_vert);
                if (i == 0){        
  	                Instantiate(objeto_rojo, posicion_spawn, Quaternion.identity);
  	                contador_ataque_fin = contador_ataque_fin +1;
  	            }
  	            else{
  	                Instantiate(objeto_azul, posicion_spawn, Quaternion.identity);
  	                contador_ataque_fin = contador_ataque_fin +1;
  	            }
  	            if (i == 10){
                    contador_ataque_fin = 0;
  	            } 
  	            break;
            }
        }
        timer = frecuencia_espasmos;
        
    }
}
function PanicShake(){
    if (transform.position.x < -0.3){
        GetComponent.<Rigidbody2D>().velocity.x = vel_horizontal;
    }
    else if (transform.position.x > 0.3){
        GetComponent.<Rigidbody2D>().velocity.x = -vel_horizontal;
    }
} 