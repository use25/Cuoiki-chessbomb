using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // States of games

    private GameObject[,] positions = new GameObject[GlobalVariable.END_BOARD_X - GlobalVariable.START_BOARD_X + 1, GlobalVariable.END_BOARD_Y - GlobalVariable.START_BOARD_Y + 1];
    public GameObject chessPiece;
    public GameObject rock;
    private int typePlayer = 0;
    private int testX = 3;
    private int testY = 3;
    private int test = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = GlobalVariable.START_BOARD_X; i<=GlobalVariable.END_BOARD_X; i++)
            for (int j = GlobalVariable.START_BOARD_Y; j <= GlobalVariable.END_BOARD_Y; j++)
                createRock(i, j);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            createPiece(testX, testY, typePlayer);
            if (typePlayer >= 4)
                typePlayer = 0;
            else
                typePlayer++;
            switch(test)
            {
                case 0:
                    testX = 3;
                    testY = 3;
                    test = 1;
                    break;
                case 1:
                    testX = 3;
                    testY = 4;
                    test = 2;
                    break;
                case 2:
                    testX = 4;
                    testY = 3;
                    test = 3;
                    break;
                case 3:
                    testX = 4;
                    testY = 4;
                    test = 0;
                    break;
            }
        }
    }
    public GameObject createPiece(int x, int y, int type)
    {
        GameObject obj = Instantiate(chessPiece, new Vector3(0, 0, -1), Quaternion.identity);
        ChessPiece cp = obj.GetComponent<ChessPiece>();
        cp.initiateChessPiece(x, y, type);
        obj.transform.position = setCoord(x, y);
        positions[x, y] = obj;
        return obj;
    }

    public GameObject createRock(int x, int y)
    {
        GameObject obj = Instantiate(rock, new Vector3(0, 0, -1), Quaternion.identity);
        Rock rockObj = obj.GetComponent<Rock>();
        rockObj.initiateRock(x, y);
        obj.transform.position = setCoord(x, y);
        positions[x, y] = obj;
        return obj;
    }

    public GameObject getPosition(int x, int y)
    {
        return positions[x, y];
    }

    public void deletePosition(int x, int y)
    {
        positions[x, y] = null;
        Destroy(positions[x, y]);
    }

    public Vector3 setCoord(int x, int y)
    {
        float finalX = -GlobalVariable.COORD_LENGTH + x;
        float finalY = GlobalVariable.COORD_LENGTH - y;
        Vector3 result = new Vector3(finalX, finalY, -1);
        return result;
    }
}
