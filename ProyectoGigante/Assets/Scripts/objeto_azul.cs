using UnityEngine;
using System.Collections;

public class objeto_azul : MonoBehaviour
{
    public GameObject boss1;
    public GameObject personaje;
    public GameObject caja;
    public float timer;
    void Start()
    {
        timer = 20;
        boss1 = GameObject.Find("boss1");
        personaje = GameObject.Find("Gigante");
        caja = GameObject.Find("caja");
    }

    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if ((coll.gameObject.tag == "PJ") 
            &&(personaje.GetComponent<Gigante>().agarrado == true))
        {
            boss1.GetComponent<Boss_1>().castigar = true;
            personaje.GetComponent<Gigante>().agarrado = false;
            personaje.GetComponent<Gigante>().timer = 1;
            personaje.GetComponent<Gigante>().empujado = true;

	    if (personaje.GetComponent<Gigante>().viendoDer == true){
                personaje.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
                caja.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
            }
            else {
                personaje.GetComponent<Gigante>().viendoDer = true;
                Vector3 escala = personaje.GetComponent<Transform>().localScale;
                escala.x = -escala.x;
                personaje.GetComponent<Transform>().localScale = escala;
                personaje.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
                caja.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
            }

            Destroy(gameObject);
        }
        if (coll.gameObject.tag == "Piso")
        {
            Destroy(gameObject);
        }
        if (timer == 0)
        {
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
    }
}
