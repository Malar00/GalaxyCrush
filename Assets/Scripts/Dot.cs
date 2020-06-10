﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public int column;
    public int row;
    public int targetX;
    public int targetY;
    private Board board;
    private GameObject otherDot;
    private Vector2 firstTouchPos;
    private Vector2 finalTouchPos;
    public float swipeAngle=0;
    private Vector2 tempPos;

    // Start is called before the first frame update
    void Start()
    {
     board=FindObjectOfType<Board>();   
     targetX = (int)transform.position.x;
     targetY = (int)transform.position.y;
     row=targetY;
     column=targetX;
    }

    // Update is called once per frame
    void Update()
    {
        targetX=column;
        targetY=row;
        if(Mathf.Abs(targetX-transform.position.x)>.1){
            tempPos = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPos,.4f);
        }else{
            tempPos = new Vector2(targetX,transform.position.y);
            transform.position = tempPos;
            board.allDots[column,row] = this.gameObject;
        }

        if(Mathf.Abs(targetY-transform.position.y)>.1){
            tempPos = new Vector2( transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPos,.4f);
        }else{
            tempPos = new Vector2( transform.position.x, targetY);            
            transform.position = tempPos;
            board.allDots[column,row] = this.gameObject;
        }
    }

    private void OnMouseDown(){
        firstTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnMouseUp(){
        finalTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    private void CalculateAngle(){
        swipeAngle = Mathf.Atan2(finalTouchPos.y-firstTouchPos.y, finalTouchPos.x-firstTouchPos.x) * 180/Mathf.PI;
        MoveDots();
    }

    private void MoveDots(){
        
        if(swipeAngle>-45 && swipeAngle<=45 && column<board.width){
            //Right
            otherDot = board.allDots[column+1,row];
            otherDot.GetComponent<Dot>().column-=1;
            column+=1;
        }else if(swipeAngle>45 && swipeAngle<=135 && row<board.height){
            //Up
            otherDot = board.allDots[column,row+1];
            otherDot.GetComponent<Dot>().row-=1;
            row+=1;
        }else if((swipeAngle>135 || swipeAngle<=-135) && column>0){
            //Left
            otherDot = board.allDots[column-1,row];
            otherDot.GetComponent<Dot>().column+=1;
            column-=1;
        }else if(swipeAngle<-45 && swipeAngle>=-135 && row>0){
            //Down
            otherDot = board.allDots[column,row-1];
            otherDot.GetComponent<Dot>().row+=1;
            row-=1;
        }
    }
}
