using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeArea : MonoBehaviour
{
    // Start is called before the first frame update
    public float animationTime = 2f;
    public bool animationIsRunning = false;
    public int damage; // Damage will be inherited from bomb type

    //Location on the board
    public int matrixX;
    public int matrixY;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (animationIsRunning)
        {
            if (animationTime <= 0)
                Destroy(gameObject);
            else
                animationTime -= Time.deltaTime;
        }
    }
}
