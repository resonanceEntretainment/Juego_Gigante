using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlDelIntro : MonoBehaviour {
        private GameObject jugador;
        private GameObject nube1;
        private GameObject nube2;
        public GameObject nubes;
	// Use this for initialization
	void Start () {
	    jugador = GameObject.Find("Gigante");
            nube1 = GameObject.Find("nubes");
	}
	
	// Update is called once per frame
	void Update () {
            
            if (nube2 == null){

                nube2 = (GameObject)Instantiate(nubes, new Vector2(28, 4.5f), Quaternion.identity);
                  
            }
            if (nube1 == null){

                nube1 = (GameObject)Instantiate(nubes, new Vector2(28, 4.5f), Quaternion.identity);

            }            
            else {
                float posicionxVieja1 = nube1.transform.position.x;
                float posicionxVieja2 = nube2.transform.position.x;
                Vector2 posicionNube1 = new Vector2(posicionxVieja1 - 0.02f, nube1.transform.position.y);
                Vector2 posicionNube2 = new Vector2(posicionxVieja2 - 0.02f, nube2.transform.position.y);

                nube1.transform.position = posicionNube1;
                nube2.transform.position = posicionNube2;
                if (posicionNube1.x <= -28){
                    GameObject.Destroy(nube1);
                }
                if (posicionNube2.x <= -28){
                    GameObject.Destroy(nube2);
                }
            }
	    if (jugador.transform.position.x >= 11){
                 SceneManager.LoadScene("boss1-unity5");
                
            }
	}
}
