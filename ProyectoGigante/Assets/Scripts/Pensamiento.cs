using UnityEngine;
using System.Collections;

public class Pensamiento : MonoBehaviour {
    public GameObject Gigante;
    private Transform pensamientoPosition;
    private Rigidbody2D pensamientoVelocity;
	// Use this for initialization
	void Start () {
	    Gigante = GameObject.Find("Gigante");
            pensamientoPosition = GetComponent<Transform>();
            pensamientoVelocity = GetComponent<Rigidbody2D>();

       //Ubicar encima del gigante
	    Vector2 tempPosition;
            tempPosition = Gigante.transform.position;
            tempPosition.y = tempPosition.y + 3;
            pensamientoPosition.position = tempPosition;
	}
	
	// Update is called once per frame
	void Update () {
            Vector2 tempVelocity; //Variable temporal para asignar la velocidad
            tempVelocity = Gigante.GetComponent<Rigidbody2D>().velocity;
            tempVelocity.y = 0;
            pensamientoVelocity.velocity = tempVelocity;
	}
}
