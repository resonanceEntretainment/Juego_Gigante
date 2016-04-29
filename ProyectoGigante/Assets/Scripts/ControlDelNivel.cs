using UnityEngine;
using System.Collections;

public class ControlDelNivel : MonoBehaviour {
        public GameObject PrefabGigante;
        public GameObject PrefabBoss;
        public GameObject PrefabCaja;
        public GameObject PrefabBala;
        public GameObject PrefabPensamiento;
        private GameObject Gigante;
        private GameObject Boss;
        private GameObject Caja;
        private GameObject Bala;
        private GameObject Pensamiento;
        private GameObject BarraVidaJug;
        private GameObject BarraVidaBoss;
        private GameObject BarraVidaBossExterna;
        public int Tutorial;
        public bool CajaTocada;
        public bool Derrota;
        public bool Destruido;
        public bool PensamientoGenerado;
        private Vector2 posicion_spawn;
        private float timer;

	// Use this for initialization
	void Start () {
            
            Gigante = GameObject.Find("Gigante");
            BarraVidaBoss = GameObject.Find("RellenoBarraVida");
            BarraVidaBossExterna = GameObject.Find("BordeBarraVidaBoss");
            BarraVidaJug = GameObject.Find("RellenoBarraVidaJug");
            Boss = GameObject.Find("boss1");
            Caja = GameObject.Find("caja");
            CajaTocada = false;
	    Destruido = false;
            Derrota = false;
            Tutorial = 0;
            timer = 1;
	}
	
	// Update is called once per frame
	void Update () {
            ActualizarBarraDeVidaJug();
            ActualizarBarraDeVidaBoss();
            if (Tutorial == 1){
                if (!PensamientoGenerado){
                    PensamientoGenerado = true;
                    Pensamiento = (GameObject)Instantiate(PrefabPensamiento,posicion_spawn,Quaternion.identity);
                    Pensamiento.name = "pensamiento";
                }
            }
            if (Tutorial == 2){
                if (PensamientoGenerado){
                    PensamientoGenerado = false;
                    Destroy(Pensamiento);
                }
            }
            else if (Tutorial==3){
                Boss = (GameObject)Instantiate(PrefabBoss,posicion_spawn,Quaternion.identity);
                Boss.name = "boss1";
                Tutorial = 4;
            }
	    if (Derrota){
                Reinicializar();
            }
	}

        void Reinicializar(){
            if (!Destruido){
                Debug.Log("no ha sido destruido");
                Destroy(Gigante);
                Destroy(Boss);
                Destroy(Caja);
                Destruido = true;
            }
            else if ((Destruido)&&(timer < 0)){

                posicion_spawn = new Vector2(-8.67f,1.5f);
                Gigante = (GameObject)Instantiate(PrefabGigante,posicion_spawn,Quaternion.identity);
                Gigante.name = "Gigante";
                posicion_spawn = new Vector2(-0.26f,1.76f);
                Caja = (GameObject)Instantiate(PrefabCaja,posicion_spawn,Quaternion.identity);
                Caja.name = "caja";
                Derrota = false;
                Destruido = false;
                timer = 1;

            }
            timer -= Time.deltaTime;
            
        }
        void ActualizarBarraDeVidaBoss(){
            if (Boss != null){
                BarraVidaBossExterna.SetActive(true);
                Vector2 posicionNueva;
                //Debug.Log("Salud Boss" + Boss.GetComponent<Boss_1>().salud);
                posicionNueva.y = -(20-Boss.GetComponent<Boss_1>().salud) * 0.4f;
                posicionNueva.x = BarraVidaBoss.GetComponent<RectTransform>().localPosition.x;
                BarraVidaBoss.GetComponent<RectTransform>().localPosition = posicionNueva;
            }
            else{
                BarraVidaBossExterna.SetActive(false);
            }
        }
        void ActualizarBarraDeVidaJug(){
            if (Gigante != null){
                Vector2 posicionNueva;
                //Debug.Log("Salud Boss" + Boss.GetComponent<Boss_1>().salud);
                posicionNueva.x = -(3-Gigante.GetComponent<Gigante>().salud) * 1.04f;
                posicionNueva.y = BarraVidaJug.GetComponent<RectTransform>().localPosition.y;
                BarraVidaJug.GetComponent<RectTransform>().localPosition = posicionNueva;
            }
        }
}
