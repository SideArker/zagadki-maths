using System;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class EquationCreator : MonoBehaviour
{
    [Header("Main")]
    static List<string> equationChars = new List<string> { "+", "*", "-", "/", "=" };

    string firstNumber = "";
    string eqChar = "";
    string secondNumber = "";

    string equation = "";
    string rightSide = "";

    public char[] charArray;
    [SerializeField] Material[] numMaterials;
    [SerializeField] Material[] actionMaterials;
    [SerializeField] GameObject Sphere;

    [Header("Sphere creation")]

    public List<GameObject> Spheres;

    public float min_x = -8.9f;
    public float max_x = 8.9f;

    public void changeActionSphere()
    {

    }

    // Destroys previously made spheres
    void DestroySpheres()
    {
        foreach(GameObject Sphere in Spheres)
        {
            Destroy(Sphere);
        }
        Spheres.Clear();
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
            
        }
    }


    // Generates the equation
    public void GenerateEquation()
    {
        int randomCharIfMinus = UnityEngine.Random.Range(0, equationChars.Count - 2);
        int randomCharIfPlus = UnityEngine.Random.Range(0, equationChars.Count);
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

        equation += "=" + rightSide;

        charArray = equation.ToCharArray();

        CreateSpheres();
    }

    private void Start()
    {
        GenerateEquation();
    }
}
