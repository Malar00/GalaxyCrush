using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    public GameObject[] dots;
    private BackgroundTile[,] backgroundTiles;
    public GameObject[,] allDots;
    // Start is called before the first frame update
    void Start()
    {
        backgroundTiles = new BackgroundTile[width, height];
        allDots = new GameObject[width,height];
        SetUp();
    }
    private void SetUp()
    {
        for(int i=0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPos = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPos, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( "+i+", "+j+" )";

                //dot
                int dotChoosen = Random.Range(0,dots.Length);
                GameObject dot = Instantiate(dots[dotChoosen], tempPos, Quaternion.identity);
                dot.transform.parent=this.transform;
                dot.name = "( "+i+", "+j+" )";
                allDots[i,j]=dot;
            }

        }
    }
}
