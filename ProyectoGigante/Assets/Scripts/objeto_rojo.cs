using UnityEngine;
using System.Collections;

public class objeto_rojo : MonoBehaviour
{
    public GameObject boss1;
    public GameObject personaje;
    public GameObject caja;
    private Rigidbody2D ObjetoRojo;
    public float timer;
    public bool diagonal = false;
    void Start()
    {
        ObjetoRojo = GetComponent<Rigidbody2D>();
        timer = 20;
        boss1 = GameObject.Find("boss1");
        personaje = GameObject.Find("Gigante");
        caja = GameObject.Find("caja");
    }

    void Update()
    {
        Vector2 tempVelocity; //Variable temporal para asignar la velocidad
        tempVelocity = ObjetoRojo.velocity;
        if (diagonal){
            tempVelocity.x = -5.8f;
            tempVelocity.y = -3.5f;
            ObjetoRojo.velocity = tempVelocity;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.tag == "PJ")
        {
            personaje.GetComponent<Gigante>().salud = personaje.GetComponent<Gigante>().salud - 1;
            personaje.GetComponent<Gigante>().tomoDanno = true;
            Destroy(gameObject);
        }
        if (coll.gameObject.tag == "Piso")
        {
            Destroy(gameObject);
        }
        if (coll.gameObject.tag == "caja")
        {
            boss1.GetComponent<Boss_1>().anim.SetBool("Herido", true);
            boss1.GetComponent<Boss_1>().salud = boss1.GetComponent<Boss_1>().salud - 1;
            boss1.GetComponent<Rigidbody2D>().velocity = new Vector2(boss1.GetComponent<Rigidbody2D>().velocity.x,
                                                                     boss1.GetComponent<Rigidbody2D>().velocity.y + 7);
            Destroy(gameObject);
        }
        if (timer == 0)
        {
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
    }
}
