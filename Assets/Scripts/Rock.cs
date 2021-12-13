using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public int health = 1;

    //Some functions will need reference to the controller
    public GameObject controller;

    //Location on the board
    int matrixX = 0;
    int matrixY = 0;

    // Constructor
    public void initiateRock(int x, int y)
    {
        this.matrixX = x;
        this.matrixY = y;
        this.health = 1;
        controller = GameObject.FindGameObjectWithTag("GameController");
    }

    // Start is called before the first frame update
    // Set position and health
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            controller.GetComponent<Game>().deletePosition(matrixX, matrixY);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collided with " + col.gameObject.tag + " detected");
        if (col.gameObject.tag == "Explode")
        {
            this.health -= 1;
        }
    }
}
