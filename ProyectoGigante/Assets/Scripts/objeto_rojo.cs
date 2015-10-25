﻿using UnityEngine;
using System.Collections;

public class objeto_rojo : MonoBehaviour
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
            personaje.GetComponent<Gigante>().salud = personaje.GetComponent<Gigante>().salud - 1;
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
