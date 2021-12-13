using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    //Some functions will need reference to the controller
    public GameObject controller;
    public GameObject explode;

    //Location on the board
    public int matrixX;
    public int matrixY;
    public Sprite sprite0, sprite1, sprite2, sprite3, sprite4;

    public float timeCountdown = 3f; // time countdown to explode then
    public bool timeIsRunning = false;
    public int type; // type of the bomb // 0: Horse
    public int damage = 1; // damage of the bomb inflicted

    // Constructor
    public void initiateChessPiece(int x, int y, int type)
    {
        this.matrixX = x;
        this.matrixY = y;
        this.type = type;
        switch(type)
        {
            case 0: // Knight
                this.timeCountdown = 1.2f;
                this.damage = 1;
                this.GetComponent<SpriteRenderer>().sprite = sprite0;
                break;
            case 1: // Bishop
                this.timeCountdown = 0.4f;
                this.damage = 1;
                this.GetComponent<SpriteRenderer>().sprite = sprite1;
                break;
            case 2: // Rook
                this.timeCountdown = 0.8f;
                this.damage = 1;
                this.GetComponent<SpriteRenderer>().sprite = sprite2;
                break;
            case 3: // Queen
                this.timeCountdown = 1.5f;
                this.damage = 1;
                this.GetComponent<SpriteRenderer>().sprite = sprite3;
                break;
            case 4: // King
                this.timeCountdown = 1f;
                this.damage = 1;
                this.GetComponent<SpriteRenderer>().sprite = sprite4;
                break;
            case 5:
                break;
            default:
                break;
        }
        this.timeIsRunning = true;
        controller = GameObject.FindGameObjectWithTag("GameController");
}

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(this, setPosition(this.matrixX, this.matrixY), Quaternion.identity);
        timeIsRunning = true; // Set countdown
        //setPosition(matrixX, matrixY); // Set position in board
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIsRunning)
        {
            if (timeCountdown <= 0)
            {
                createExplode(type);
                controller.GetComponent<Game>().deletePosition(matrixX, matrixY);
                Destroy(gameObject);
                timeIsRunning = false;
                Debug.Log("Test2");
            }
            else
            {
                timeCountdown -= Time.deltaTime;
                Debug.Log("Test");
            }
        }
    }

    public GameObject createExplodeArea(int x, int y)
    {
        if (
            x <= GlobalVariable.END_BOARD_X &&
            x >= GlobalVariable.START_BOARD_X &&
            y <= GlobalVariable.END_BOARD_Y &&
            y >= GlobalVariable.START_BOARD_Y
            )
        {
            GameObject obj = Instantiate(explode, new Vector3(0, 0, -1), Quaternion.identity);
            ExplodeArea ea = obj.GetComponent<ExplodeArea>();
            ea.animationIsRunning = true;
            ea.matrixX = x;
            ea.matrixY = y;
            obj.transform.position = controller.GetComponent<Game>().setCoord(x, y);
            return obj;
        }
        return null;
    }

    void createExplode(int type)
    {
        createExplodeArea(matrixX, matrixY);
        switch (type)
        {
            case 0: // Knight
                createExplodeArea(matrixX + 1, matrixY - 2);
                createExplodeArea(matrixX - 1, matrixY - 2);
                createExplodeArea(matrixX - 1, matrixY + 2);
                createExplodeArea(matrixX + 1, matrixY + 2);
                createExplodeArea(matrixX + 2, matrixY - 1);
                createExplodeArea(matrixX - 2, matrixY - 1);
                createExplodeArea(matrixX - 2, matrixY + 1);
                createExplodeArea(matrixX + 2, matrixY + 1);
                break;
            case 1: // Bishop
                createExplodeAreaLoop(matrixX, matrixY, -1, -1); // Tu phai sang trai, tu duoi len tren
                createExplodeAreaLoop(matrixX, matrixY, 1, -1); // Tu trai sang phai, tu duoi len tren
                createExplodeAreaLoop(matrixX, matrixY, 1, 1); // Tu trai sang phai, tu tren xuong duoi
                createExplodeAreaLoop(matrixX, matrixY, -1, 1); // Tu trai sang phai, tu tren xuong duoi
                break;
            case 2: // Rook
                createExplodeAreaLoop(matrixX, matrixY, -1, 0); // Tu phai sang trai
                createExplodeAreaLoop(matrixX, matrixY, 1, 0); // Tu trai sang phai
                createExplodeAreaLoop(matrixX, matrixY, 0, -1); // Tu duoi len tren
                createExplodeAreaLoop(matrixX, matrixY, 0, 1); // Tu tren xuong duoi
                break;
            case 3: // Queen
                createExplodeAreaLoop(matrixX, matrixY, -1, -1); // Tu phai sang trai, tu duoi len tren
                createExplodeAreaLoop(matrixX, matrixY, 1, -1); // Tu trai sang phai, tu duoi len tren
                createExplodeAreaLoop(matrixX, matrixY, 1, 1); // Tu trai sang phai, tu tren xuong duoi
                createExplodeAreaLoop(matrixX, matrixY, -1, 1); // Tu trai sang phai, tu tren xuong duoi
                createExplodeAreaLoop(matrixX, matrixY, -1, 0); // Tu phai sang trai
                createExplodeAreaLoop(matrixX, matrixY, 1, 0); // Tu trai sang phai
                createExplodeAreaLoop(matrixX, matrixY, 0, -1); // Tu duoi len tren
                createExplodeAreaLoop(matrixX, matrixY, 0, 1); // Tu tren xuong duoi
                break;
            case 4: // King
                createExplodeArea(matrixX, matrixY - 1);
                createExplodeArea(matrixX + 1, matrixY - 1);
                createExplodeArea(matrixX + 1, matrixY);
                createExplodeArea(matrixX + 1, matrixY + 1);
                createExplodeArea(matrixX, matrixY + 1);
                createExplodeArea(matrixX - 1, matrixY + 1);
                createExplodeArea(matrixX - 1, matrixY);
                createExplodeArea(matrixX - 1, matrixY - 1);
                break;
        }
    }

    private void createExplodeAreaLoop(int x, int y, int directionX, int directionY)
    {
        createExplodeArea(x + directionX, y + directionY);
        if (!checkIfRockOnWay(x + directionX, y + directionY) && !(directionX == 0 && directionY == 0))
            createExplodeAreaLoop(x + directionX, y + directionY, directionX, directionY);
    }

    private bool checkIfRockOnWay(int x, int y)
    {
        if (x < GlobalVariable.START_BOARD_X || x > GlobalVariable.END_BOARD_X)
            return true;
        if (y < GlobalVariable.START_BOARD_Y || y > GlobalVariable.END_BOARD_Y)
            return true;
        if (controller.GetComponent<Game>().getPosition(x, y) == null)
            return false;
        if (controller.GetComponent<Game>().getPosition(x, y).tag == "Object")
            return true;
        return false;
    }
}
