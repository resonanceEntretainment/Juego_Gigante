using UnityEngine;
using System.Collections;

public class ControlDelNivel : MonoBehaviour {
        public GameObject PrefabGigante;
        public GameObject PrefabBoss;
        public GameObject PrefabCaja;
        public GameObject PrefabBala;
        private GameObject Gigante;
        private GameObject Boss;
        private GameObject Caja;
        private GameObject Bala;
        public bool Derrota;
        public bool Destruido;
        private Vector2 posicion_spawn;
        private float timer;

	// Use this for initialization
	void Start () {
            
            Gigante = GameObject.Find("Gigante");
            Boss = GameObject.Find("boss1");
            Caja = GameObject.Find("caja");
	    Destruido = false;
            Derrota = false;
            timer = 1;
	}
	
	// Update is called once per frame
	void Update () {
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
                posicion_spawn = new Vector2(-1.82f,7.22f);
                Boss = (GameObject)Instantiate(PrefabBoss,posicion_spawn,Quaternion.identity);
                Boss.name = "boss1";
                posicion_spawn = new Vector2(-0.26f,1.76f);
                Caja = (GameObject)Instantiate(PrefabCaja,posicion_spawn,Quaternion.identity);
                Caja.name = "caja";
                Derrota = false;
                Destruido = false;
                timer = 1;

            }
            timer -= Time.deltaTime;
            
        }
}
