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

        if (coll.gameObject.tag == "PJ")
        {
            boss1.GetComponent<Boss_1>().castigar = true;
            personaje.GetComponent<Gigante>().agarrado = false;
            personaje.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
            caja.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
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