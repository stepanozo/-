using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MyOwnClass;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using System;
using System.Globalization;


public  class MainScript : MonoBehaviour {

    //начало всячины для спауна
    int i;


    public GameObject UsedObject; //если что-то работать не будет, будешь его снизу создавать сразу, а не здесь
    public string nameOfFile;
    public GameObject SpawnObject;
    public Vector3 coordinatesForSpawn;
    public GameObject Person;
    //конец всячины для спауна


    public  InputField InputX;
    public  InputField InputY;
    public  InputField InputZ;
    public InputField InputSize;

    public Text Description;
    public InputField field;
    
    public float MouseWheel;
    string nameOfSavedFile;

    public GameObject ActiveObject;
    public Vector3 coordinates;



    //спаун любого объекта
    public void Spawn()
    {
        nameOfFile = EditorUtility.OpenFilePanel("Размещаемый объект", "Assets/Scenes/Resources", "PREFAB");


        i = nameOfFile.Length - 1;
        while (nameOfFile[i] != '/')
        {
            i--;
        }

        nameOfFile = nameOfFile.Substring(i + 1);

        i = 0;
        while (nameOfFile[i] != '.')
        {
            i++;
        }

        nameOfFile = nameOfFile.Substring(0, i);



        SpawnObject = Resources.Load<GameObject>(nameOfFile); //Ищешь объект в папочке


        coordinatesForSpawn = Person.transform.position;

        UsedObject = Instantiate(SpawnObject, coordinatesForSpawn, Quaternion.identity); //Спаунишь объект


        DifferentThings.allRealObjects.Add(UsedObject);
        DifferentThings.Objects.Add(new ObjectOfConstructor(UsedObject.transform.position.x, UsedObject.transform.position.y, UsedObject.transform.position.z, UsedObject.transform.rotation.x, UsedObject.transform.rotation.y, UsedObject.transform.rotation.z, nameOfFile, UsedObject.transform.localScale.x, ""));
        ClickReceiver CR = UsedObject.AddComponent<ClickReceiver>(); //Сразу даёшь созданному объекту клик рисивер








    }


    public void ChangeDescription()
    {
        if (Description.text==null)
                   GameObject.FindGameObjectWithTag("Panel").transform.localScale = new Vector3(1.53f, 4.82f, 0);

        DifferentThings.Objects[DifferentThings.numberOfActiveObject].description = field.text;
        Description.text = field.text;
        field.text = "Введите описание";
        if (Description.text == null)
            GameObject.Find("Panel").transform.localScale = new Vector3(0, 0, 0);
    }


    public void SaveEverything()
    {
        nameOfSavedFile = EditorUtility.SaveFilePanel("Сохранить улицу", "SavedStreets", "New street", "street");

        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream StreetStream = new FileStream(nameOfSavedFile, FileMode.OpenOrCreate))
        {
            formatter.Serialize(StreetStream, DifferentThings.Objects);

        }
    }

    public void LoadEverything()
    {

        DestroyEverything();


        nameOfSavedFile = EditorUtility.OpenFilePanel("Загрузить улицу", "SavedStreets", "street");

        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream StreetStream = new FileStream(nameOfSavedFile, FileMode.OpenOrCreate)) 
        {
            DifferentThings.Objects = (List<ObjectOfConstructor>)formatter.Deserialize(StreetStream);

        }

        //тут ещё не забудь все старые дома удалить

        foreach (ObjectOfConstructor loadedObject in DifferentThings.Objects)
        {
            coordinates = new Vector3(loadedObject.x, loadedObject.y, loadedObject.z);
            ActiveObject = Resources.Load<GameObject>(loadedObject.nameOfModel); //Ищешь объект в папочке
                                                                   //new Quaternion(loadedObject.rotationX, loadedObject.rotationY, loadedObject.rotationZ, 1)     //ВОЗМОЖНО ЕДИНИЧКУ ЗАМЕНИШЬ И СДЕЛАЕШЬ ПРИСВАИВАНИЕ КООРДИНАТ УЖЕ СОЗДАННОМУ ОБЪЕКТУ
            GameObject UsedObject = Instantiate(ActiveObject, coordinates, Quaternion.identity); //Спаунишь объект и сразу помещаешь его в массив реальных объектов юнити
            UsedObject.transform.localScale = new Vector3(loadedObject.size, loadedObject.size, loadedObject.size);
            UsedObject.transform.Rotate(loadedObject.rotationX, loadedObject.rotationY, loadedObject.rotationZ);


            DifferentThings.allRealObjects.Add(UsedObject);
            ClickReceiver CR = UsedObject.AddComponent<ClickReceiver>(); //Сразу даёшь созданному объекту клик рисивер
        }

    }

    public  void DestroyEverything()
    {
        GameObject.FindGameObjectWithTag("AllArrows").transform.localScale = new Vector3(0, 0, 0);
        GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.localScale = new Vector3(0, 0, 0);

        for (int i= DifferentThings.allRealObjects.Count-1; i>=0; i--)
        {

            Destroy(DifferentThings.allRealObjects[i]);
            DifferentThings.allRealObjects.RemoveAt(i);
            DifferentThings.Objects.RemoveAt(i);

        }

        Description.text = null;
       
            GameObject.Find("Panel").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("Оси").transform.localScale = new Vector3(0f, 0f, 0f); //прячем ручной ввод координат

    }

    public void ArrowsOfRotation ()
    {
        if (DifferentThings.IsThereAnyActiveObjects)
        {

            GameObject.FindGameObjectWithTag("AllArrows").transform.localScale = new Vector3(0, 0, 0);
            GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.localScale = new Vector3(1, 1, 1);

         

                InputX.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationX);
                InputY.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationY);
                InputZ.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationZ);
            

        }
        DifferentThings.MovementArrows = false;
        GameObject.Find("Стрелки движения").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("Стрелки вращения").transform.localScale = new Vector3(1, (float)5.1, 1);

    }

    public void ArrowsOfMovement ()
    {
        if (DifferentThings.IsThereAnyActiveObjects)
        {
           
            GameObject.FindGameObjectWithTag("AllArrows").transform.localScale = new Vector3(1, 1, 1);
            GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.localScale = new Vector3(0, 0, 0);

           
                InputX.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].x);
                InputY.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].y);
                InputZ.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].z);
      
        }
        DifferentThings.MovementArrows = true;
        GameObject.Find("Стрелки движения").transform.localScale = new Vector3(1, (float)5.1, 1);
        GameObject.Find("Стрелки вращения").transform.localScale = new Vector3(0, 0, 0);
    }

    public void OpenDescription()
    {

        DifferentThings.IsDescriptionActive = true;
        GameObject.Find("CloseDescription").transform.localScale = new Vector2(1, 5.1f);
        GameObject.Find("DescriptionButton").transform.localScale = new Vector2(0, 0);

        if (DifferentThings.IsThereAnyActiveObjects)
        {
            Description.text = DifferentThings.Objects[DifferentThings.numberOfActiveObject].description;
            GameObject.Find("Panel").transform.localScale = new Vector2(14, 2.87f);
           
        }
   

    }

    public void CloseDescription()
    {
        DifferentThings.IsDescriptionActive = false;

        GameObject.Find("Panel").transform.localScale = new Vector2(0, 0);
        GameObject.Find("CloseDescription").transform.localScale = new Vector2(0, 0);
        GameObject.Find("DescriptionButton").transform.localScale = new Vector2(1, 5.1f);
    }

    public void ChangeX()
    {

        if (DifferentThings.MovementArrows)
        {
            try
            {
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].x = (float)Convert.ToDouble(InputX.text);
                GameObject.FindGameObjectWithTag("ActiveObject").transform.position = new Vector3((float)Convert.ToDouble(InputX.text), GameObject.FindGameObjectWithTag("ActiveObject").transform.position.y, GameObject.FindGameObjectWithTag("ActiveObject").transform.position.z);
                GameObject.FindGameObjectWithTag("AllArrows").transform.position = new Vector3((float)Convert.ToDouble(InputX.text), GameObject.FindGameObjectWithTag("ActiveObject").transform.position.y, GameObject.FindGameObjectWithTag("ActiveObject").transform.position.z);
                GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.position = new Vector3((float)Convert.ToDouble(InputX.text), GameObject.FindGameObjectWithTag("ActiveObject").transform.position.y, GameObject.FindGameObjectWithTag("ActiveObject").transform.position.z);
            }
            catch
            {
                InputX.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].x);
            }
        }
        else
        {
            try
            {
                
                GameObject.FindGameObjectWithTag("ActiveObject").transform.Rotate((float)((float)Convert.ToDouble(InputX.text) - DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationX), 0, 0);//Вращаем
                GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.Rotate((float)Convert.ToDouble(InputX.text) - DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationX, 0, 0);
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationX = (float)Convert.ToDouble(InputX.text);
            }
            catch
            {
                InputX.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationX);
            }
        }
       
            
        
        
    }

    public void ChangeY()
    {


        if (DifferentThings.MovementArrows)
        {
            try
            {
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].y = (float)Convert.ToDouble(InputY.text);
                GameObject.FindGameObjectWithTag("ActiveObject").transform.position = new Vector3(GameObject.FindGameObjectWithTag("ActiveObject").transform.position.x, (float)Convert.ToDouble(InputY.text), GameObject.FindGameObjectWithTag("ActiveObject").transform.position.z);
                GameObject.FindGameObjectWithTag("AllArrows").transform.position = new Vector3(GameObject.FindGameObjectWithTag("ActiveObject").transform.position.x, (float)Convert.ToDouble(InputY.text), GameObject.FindGameObjectWithTag("ActiveObject").transform.position.z);
                GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.position = new Vector3(GameObject.FindGameObjectWithTag("ActiveObject").transform.position.x, (float)Convert.ToDouble(InputY.text), GameObject.FindGameObjectWithTag("ActiveObject").transform.position.z);
            }
            catch
            {
                InputY.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].y);
            }
        }
        else
        {
            try
            {
               
                GameObject.FindGameObjectWithTag("ActiveObject").transform.Rotate(0,(float)( (float)Convert.ToDouble(InputY.text) - DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationY), 0);//Вращаем
                GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.Rotate(0, (float)Convert.ToDouble(InputY.text) - DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationY, 0);
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationY = (float)Convert.ToDouble(InputY.text);
            }
            catch
            {
                InputY.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationY);
            }
        }
        
        
     
    }

    public void ChangeZ()
    {

        if (DifferentThings.MovementArrows)
        {
            try
            {
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].z = (float)Convert.ToDouble(InputZ.text);
                GameObject.FindGameObjectWithTag("ActiveObject").transform.position = new Vector3(GameObject.FindGameObjectWithTag("ActiveObject").transform.position.x, GameObject.FindGameObjectWithTag("ActiveObject").transform.position.y, (float)Convert.ToDouble(InputZ.text));
                GameObject.FindGameObjectWithTag("AllArrows").transform.position = new Vector3(GameObject.FindGameObjectWithTag("ActiveObject").transform.position.x, GameObject.FindGameObjectWithTag("ActiveObject").transform.position.y, (float)Convert.ToDouble(InputZ.text));
                GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.position = new Vector3(GameObject.FindGameObjectWithTag("ActiveObject").transform.position.x, GameObject.FindGameObjectWithTag("ActiveObject").transform.position.y, (float)Convert.ToDouble(InputZ.text));
            }   
            catch
            {
                InputZ.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].z);
            }
        }

        else
        {
            try
            {
                
                GameObject.FindGameObjectWithTag("ActiveObject").transform.Rotate(0, 0, (float)Convert.ToDouble(InputZ.text) - DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationZ);//Вращаем
                GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.Rotate(0, 0, (float)((float)Convert.ToDouble(InputZ.text) - DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationZ));
                DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationZ = (float)Convert.ToDouble(InputZ.text);

            }
            catch
            {
                InputZ.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotationZ);
            }
        }
        
        

       
       
    }

    public void ChangeSize()
    {
        try
        {
            DifferentThings.Objects[DifferentThings.numberOfActiveObject].size = (float)Convert.ToDouble(InputSize.text);
            GameObject.FindGameObjectWithTag("ActiveObject").transform.localScale = new Vector3((float)Convert.ToDouble(InputSize.text), (float)Convert.ToDouble(InputSize.text), (float)Convert.ToDouble(InputSize.text));

        }
        catch
        {
            InputZ.text = Convert.ToString(DifferentThings.Objects[DifferentThings.numberOfActiveObject].size);
        }
    }


    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DifferentThings.mouseSensitivity = 0;
        DifferentThings.CameraSpeed = 0;
        DifferentThings.InputX = InputX;
        DifferentThings.InputY = InputY;
        DifferentThings.InputZ = InputZ;
        DifferentThings.InputSize = InputSize;
        DifferentThings.description = Description;

        DifferentThings.MovementArrows = true;
        DifferentThings.IsDescriptionActive = false;
        MouseWheel = Input.GetAxis("Mouse ScrollWheel");


    }
	
	// Update is called once per frame
	void Update ()
    {
        //Ctrl для курсора
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            DifferentThings.mouseSensitivity = 3;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            DifferentThings.CameraSpeed = 15.0f;

        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            DifferentThings.mouseSensitivity = 0;
            Cursor.lockState = CursorLockMode.None;
            DifferentThings.CameraSpeed = 0;
            Cursor.visible = true;
        }

        //Пробел для перемещения к объекту
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DifferentThings.IsThereAnyActiveObjects)
            {
                GameObject.Find("Main Camera").transform.position = new Vector3(DifferentThings.allRealObjects[DifferentThings.numberOfActiveObject].transform.position.x, DifferentThings.allRealObjects[DifferentThings.numberOfActiveObject].transform.position.y + 30, DifferentThings.allRealObjects[DifferentThings.numberOfActiveObject].transform.position.z);
            }
        }

     

        //Удаление объекта
        if (Input.GetKeyDown(KeyCode.Delete))
            {
                for (int i = DifferentThings.allRealObjects.Count - 1; i >= 0; i--)
                {
                    if (DifferentThings.allRealObjects[i].tag == "ActiveObject")
                    {
                        Destroy(DifferentThings.allRealObjects[i]);
                        DifferentThings.allRealObjects.RemoveAt(i);
                        DifferentThings.Objects.RemoveAt(i);
                    }

                }
                GameObject.FindGameObjectWithTag("AllArrows").transform.localScale = new Vector3(0, 0, 0);
                GameObject.FindGameObjectWithTag("ArrowsOfRotation").transform.localScale = new Vector3(0, 0, 0);
            Description.text = null;
            GameObject.Find("Panel").transform.localScale = new Vector3(0, 0, 0);
            GameObject.Find("Оси").transform.localScale = new Vector3(0f, 0f, 0f); //прячем ручной ввод координат
        }

        if (Input.GetKeyUp(KeyCode.Delete))
        {
           
        }

        //Shift для ускорения
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DifferentThings.CameraSpeed = 75;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            DifferentThings.CameraSpeed = 15;
        }

        //Колёсико мыши


        MouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (MouseWheel > 0)
        {
            for (int i = DifferentThings.allRealObjects.Count - 1; i >= 0; i--)
            {
                if (DifferentThings.allRealObjects[i].tag == "ActiveObject")
                {
                    DifferentThings.allRealObjects[i].transform.localScale += new Vector3((float)30, (float)30, (float)30);
                    DifferentThings.Objects[i].size = DifferentThings.allRealObjects[i].transform.localScale.x;
                    InputSize.text = Convert.ToString(DifferentThings.allRealObjects[i].transform.localScale.x);
                }

            }
        }

        if (MouseWheel < 0)
        {
            for (int i = DifferentThings.allRealObjects.Count - 1; i >= 0; i--)
            {
                if (DifferentThings.allRealObjects[i].tag == "ActiveObject")
                {
                    DifferentThings.allRealObjects[i].transform.localScale += new Vector3((float)-30, (float)-30, (float)-30);
                    DifferentThings.Objects[i].size = DifferentThings.allRealObjects[i].transform.localScale.x;
                    InputSize.text = Convert.ToString(DifferentThings.allRealObjects[i].transform.localScale.x);
                }

            }
        }
        


    }
}
