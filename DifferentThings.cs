using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

namespace MyOwnClass
{ 

    public abstract class DifferentThings
    {
        public static float mouseSensitivity;
        public static float CameraSpeed;

        public static InputField InputX;
        public static InputField InputY;
        public static InputField InputZ;
        public static InputField InputSize;

        public static Text description;
        public static bool MovementArrows;
        public static bool IsDescriptionActive;

        public static List<GameObject> allRealObjects = new List<GameObject>(); //Список реальных объектов GameObjectb
        public static List<ObjectOfConstructor> Objects = new List<ObjectOfConstructor>();

        public static int numberOfActiveObject;


        public static bool IsThereAnyActiveObjects
        {

            get
            {
                foreach (GameObject obj in DifferentThings.allRealObjects)
                {
                    if (obj.tag == "ActiveObject")
                        return true;
                }
                return false;
            }
        }

 
    }

    [Serializable]
    public class ObjectOfConstructor
    {
        public float x;
        public float y;
        public float z;
        public float rotationX;
        public float rotationY;
        public float rotationZ;
        public string nameOfModel;
        public float size;
        public string description;

        public ObjectOfConstructor(float x, float y, float z, float rotationX, float rotationY, float rotationZ, string nameOfModel, float size, string description)
        {

            this.x = x;
            this.y = y;
            this.z = z;
            this.rotationX = rotationX;
            this.rotationY = rotationY;
            this.rotationZ = rotationZ;
            this.nameOfModel = nameOfModel;
            this.size = size;
            this.description = description;
        }
    }
}
