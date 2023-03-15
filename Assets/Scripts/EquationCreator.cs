using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EquationCreator : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] GameController gc;
    [SerializeField] UIController ui;
    static List<string> equationChars = new List<string> { "+", "*", "-", "/", "=" };

    string firstNumber = "";
    string eqChar = "";
    string secondNumber = "";

    public string equation = "";
    string rightSide = "";
    char[] charArray;

    [Header("Lists")]
    [SerializeField] Material[] numMaterials;
    [SerializeField] Material[] actionMaterials;

    [Header("Sphere creation")]
    [SerializeField] GameObject Sphere;

    List<GameObject> Spheres = new List<GameObject>();
    List<GameObject> actionSpheres = new List<GameObject>();
    GameObject sphereToReplace;
    int actionToFind;
    int currentAction;
    [SerializeField] Material defaultMaterial;

    public float min_x = -8.9f;
    public float max_x = 8.9f;

    void checkAction()
    {
        if (actionToFind == currentAction)
        {
            gc.score += 50;
            Debug.Log("Correct action selected si");
            GenerateEquation();
            ui.updateText(1, gc.score);
        }
        else
        {
            if(gc.score > 50) gc.score -= 50;
            Debug.Log("Wrong action");
            GenerateEquation();
            ui.updateText(0, gc.score);
        }

        if (gc.score == 600)
        {
            Debug.Log("Win Game");
            gc.regenLevel();
        }
    }
    
    public void changeActionSphere(int action) // goes by indexes of equationChars
    {
        currentAction = action;
        sphereToReplace.GetComponent<MeshRenderer>().material = actionMaterials[action];
        checkAction();
    }

    // Destroys previously made spheres
    void DestroySpheres()
    {
        foreach(GameObject Sphere in Spheres)
        {
            Destroy(Sphere);
        }
        Spheres.Clear();
        actionSpheres.Clear();

    }

    // Create and assign materials to spheres
     void CreateSpheres()
    {
        // Destroy already made spheres
        if(Spheres.Count != 0) DestroySpheres();


        for (int i = 0; i < charArray.Length; i++)
        {
            // Create spheres
            GameObject sphereClone = Instantiate(Sphere);

            float x = min_x + ((max_x - min_x) / (charArray.Length - 1) * i);
            sphereClone.transform.position = new Vector3(x, 0, 0);

            MeshRenderer sphereMesh = sphereClone.GetComponent<MeshRenderer>();

            // If sphere is a digit, assign material
            if (char.IsDigit(charArray[i]))
            {
                int currentNum = Convert.ToInt32(charArray[i].ToString());

                sphereMesh.material = numMaterials[currentNum];

            }
            else
            {
                actionSpheres.Add(sphereClone);
                sphereMesh.material = actionMaterials[equationChars.IndexOf(charArray[i].ToString())];
            }

            Spheres.Add(sphereClone);
            
        }

        sphereToReplace = actionSpheres[UnityEngine.Random.Range(0, actionSpheres.Count)];

        sphereToReplace.GetComponent<MeshRenderer>().material = defaultMaterial;

        string action = charArray[Spheres.IndexOf(sphereToReplace)].ToString();
        
        actionToFind = equationChars.IndexOf(action);
    }


    // Generates the equation
    [Button] public void GenerateEquation()
    {
        int randomCharIfMinus = UnityEngine.Random.Range(0, equationChars.Count - 3);
        int randomCharIfPlus = UnityEngine.Random.Range(0, equationChars.Count - 1);
        int randomFirstNumber = UnityEngine.Random.Range(1, 10);
        int randomSecondNumber = UnityEngine.Random.Range(1, 10);

        firstNumber = randomFirstNumber.ToString();
        secondNumber = randomSecondNumber.ToString();

        if (randomFirstNumber < randomSecondNumber)
        {
            eqChar = equationChars[randomCharIfMinus];
        }
        else
        {
            eqChar = equationChars[randomCharIfPlus];

            if (eqChar == "/" && randomFirstNumber % randomSecondNumber != 0)
            {
                eqChar = "*";
            }


        }

        equation = firstNumber + eqChar + secondNumber;

        DataTable dt = new DataTable();
        var v = dt.Compute(equation, "");
        var w = v.ToSafeString();

        rightSide = w;

        if(UnityEngine.Random.Range(1, 3) == 1)
        {
            equation = equation.Insert(0, rightSide + "=");

            charArray = equation.ToCharArray();
        }
        else
        {

            equation += "=" + rightSide;

            charArray = equation.ToCharArray();
        }
        CreateSpheres();
    }
}
