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
            Separar(personaje,caja);
            Destroy(gameObject);
        }
        else if ((coll.gameObject.tag == "PJ") 
            &&(personaje.GetComponent<Gigante>().agarrado == false))
        {
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

    public void Separar(GameObject Personaje,GameObject Caja){

        Personaje.GetComponent<Gigante>().agarrado = false;
        Personaje.GetComponent<Gigante>().timer = 1;
        Personaje.GetComponent<Gigante>().empujado = true;

	if (Personaje.GetComponent<Gigante>().viendoDer == true){

            Personaje.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
            Caja.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
        }
        else {

            Personaje.GetComponent<Gigante>().viendoDer = true;
            Vector3 escala = personaje.GetComponent<Transform>().localScale;
            escala.x = -escala.x;
            Personaje.GetComponent<Transform>().localScale = escala;
            Personaje.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
            Caja.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
        }
    }

    public void Empujar(GameObject Personaje,GameObject Caja){

        Personaje.GetComponent<Gigante>().timer = 1;
        Personaje.GetComponent<Gigante>().empujado = true;
        Personaje.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);

    }
}
