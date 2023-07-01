﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MyOwnClass;
using UnityEngine.UI;
using System;
using System.Globalization;

public class ClickReceiver : MonoBehaviour, IPointerClickHandler {


    public InputField InputX;
    public InputField InputY;
    public  InputField InputZ;
    public InputField InputSize;

    public Text Description;
    public float X;
    public float Y;
    public float Z;

    public void OnPointerClick(PointerEventData eventData)
    {
        InputX = DifferentThings.InputX;
        InputY = DifferentThings.InputY;
        InputZ = DifferentThings.InputZ;
        InputSize = DifferentThings.InputSize;

        if ( this.tag == "ArrowXback" || this.tag == "ArrowYback" || this.tag == "ArrowZback" || this.tag == "ArrowXforward" || this.tag == "ArrowYforward" || this.tag == "ArrowZforward" || this.tag == "ArrowXback" || this.tag == "RotationXBack" || this.tag == "RotationYBack" || this.tag == "RotationZBack" || this.tag == "RotationXForward" || this.tag == "RotationYForward" || this.tag == "RotationZForward")
        { //когда кликаем на стрелки

            //когда кликаем на стрелки вращения
            if(this.tag == "RotationXForward")
            {
                GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.Rotate(3f, 0, 0);
                GameObject.FindGameObjectWithTag("ActiveObject").transform.Rotate(3f, 0, 0);
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationX += 3;
                InputX.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationX);
            }
            else if (this.tag == "RotationXBack")
            {
                GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.Rotate(-3f, 0, 0);
                GameObject.FindGameObjectWithTag("ActiveObject").transform.Rotate(-3f, 0, 0);
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationX += -3;
                InputX.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationX);
            }
            else if (this.tag == "RotationYForward")
            {
                GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.Rotate(0, 3f, 0);
                GameObject.FindGameObjectWithTag("ActiveObject").transform.Rotate(0, 3f, 0);
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationY += 3;
                InputY.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationY);
            }
            else if (this.tag == "RotationYBack")
            {
                GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.Rotate(0, -3f, 0);
                GameObject.FindGameObjectWithTag("ActiveObject").transform.Rotate(0, -3f, 0);
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationY += -3;
                InputY.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationY);
            }
            else if (this.tag == "RotationZForward")
            {
                GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.Rotate(0, 0, 3f);
                GameObject.FindGameObjectWithTag("ActiveObject").transform.Rotate(0, 0, 3f);
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationZ += 3;
                InputZ.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationZ);
            }
            else if (this.tag == "RotationZBack")
            {
                GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.Rotate(0, 0, -3f);
                GameObject.FindGameObjectWithTag("ActiveObject").transform.Rotate(0, 0, -3f);
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationZ -= 3;
                InputZ.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationZ);
            }




            //а теперь когда просто двигаем
            if (this.tag == "ArrowXforward")
            {
                
                GameObject.FindGameObjectWithTag("ActiveObject").transform.position += new Vector3(1, 0, 0);
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].x += 1;
                InputX.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].x);
            }

            else if (this.tag == "ArrowYforward")
            {
                GameObject.FindGameObjectWithTag("ActiveObject").transform.position += new Vector3(0, 1, 0);
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].y += 1;
                InputY.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].y);
            }
            else if (this.tag == "ArrowZforward")
            {
                GameObject.FindGameObjectWithTag("ActiveObject").transform.position += new Vector3(0, 0, 1);
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].z += 1;
                InputZ.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].z);
            }
            else if (this.tag == "ArrowXback")
            {
                GameObject.FindGameObjectWithTag("ActiveObject").transform.position += new Vector3(-1, 0, 0);
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].x += -1;
                InputX.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].x);
            }
            else if (this.tag == "ArrowYback")
            {
                GameObject.FindGameObjectWithTag("ActiveObject").transform.position += new Vector3(0, -1, 0);
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].y += -1;
                InputY.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].y);

            }
            else if (this.tag == "ArrowZback")
            {
                GameObject.FindGameObjectWithTag("ActiveObject").transform.position += new Vector3(0, 0, -1);
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].z += -1;
                InputZ.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].z);
            }

            X = DifferentThings.Objects[DifferentThings.numberOfActiveObject].x;
            Y = DifferentThings.Objects[DifferentThings.numberOfActiveObject].y;
            Z = DifferentThings.Objects[DifferentThings.numberOfActiveObject].z;

            GameObject.FindGameObjectWithTag("AllArrows").transform.position = GameObject.FindGameObjectWithTag("ActiveObject").transform.position; //перемещаем стрелки к объекту
            GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.position = GameObject.FindGameObjectWithTag("ActiveObject").transform.position; //перемещаем стрелки к объекту
        }
        else
        { //когда кликаем на сам объект


            

            if (DifferentThings.MovementArrows)
            {
                GameObject.FindGameObjectWithTag("AllArrows").transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.localScale = new Vector3(1, 1, 1);
            }

            if (GameObject.FindGameObjectWithTag("ActiveObject") != null)
            {
                GameObject.FindGameObjectWithTag("ActiveObject").tag = "Untagged"; //прошлый активный объект деактивируем
            }


            this.tag = "ActiveObject";


           //Ищем активный объект в массиве
            

                for (int i = 0; i < DifferentThings.allRealObjects.Count; i++)
                {
                    if (DifferentThings.allRealObjects[i].tag == "ActiveObject")
                    {
                         DifferentThings.numberOfActiveObject = i;
                    }
                }


            GameObject.Find("Оси").transform.localScale = new Vector3(1.70062f, 1.70062f, 1.70062f); //ручной ввод координат выводится на экран
            InputSize.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].size);
            if (DifferentThings.MovementArrows)
            {
                InputX.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].x);
                InputY.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].y);
                InputZ.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].z);
               
            }
            else
            {
               
                InputX.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationX);
                InputY.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationY);
                InputZ.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationZ);
            }
            GameObject.FindGameObjectWithTag("AllArrows").transform.position = this.transform.position; //перемещаем стрелки к объекту
            GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.position = this.transform.position;

           
                GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.rotation = GameObject.FindGameObjectWithTag("ActiveObject").transform.rotation;
            Description = DifferentThings.description;//удалить если чё
            if (!DifferentThings.IsDescriptionActive)
            {
                GameObject.Find("Panel").transform.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                GameObject.Find("Panel").transform.localScale = new Vector2(14, 2.87f);
                Description.text = DifferentThings.Objects[DifferentThings.numberOfActiveObject].description;
            }
           
        }
      


    }

   


    // Use this for initialization
    void Start ()
    {

        GameObject.Find("Panel").transform.localScale = new Vector3(0, 0, 0);
        GameObject.FindGameObjectWithTag("AllArrows").transform.localScale = new Vector3(0, 0, 0);
        GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.localScale = new Vector3(0, 0, 0);
        Description = DifferentThings.description;


     

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
