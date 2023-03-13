using System;
using System.Data;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EquationCreator : MonoBehaviour
{
    static string[] equationChars = new string[]{"+", "*", "-", "/"};

    string firstNumber = "";
    string eqChar = "";
    string secondNumber = "";

    public string equation = "";
    public string rightSide = "";

    

    private void Start()
    {
      
    }



    [Button] void GenerateEquation()
    {

        
        
            int randomCharIfMinus = UnityEngine.Random.Range(0, equationChars.Length-2);
            int randomCharIfPlus = UnityEngine.Random.Range(0, equationChars.Length);
            int randomFirstNumber = UnityEngine.Random.Range(1, 10);
            int randomSecondNumber = UnityEngine.Random.Range(1, 10);

            firstNumber = randomFirstNumber.ToString();
            secondNumber = randomSecondNumber.ToString();
        
        if(randomFirstNumber < randomSecondNumber)
        {
            eqChar = equationChars[randomCharIfMinus];
        }
        else
        {
            eqChar = equationChars[randomCharIfPlus];

            if(eqChar == "/" && randomFirstNumber%randomSecondNumber != 0)
            {
                eqChar = "*";
            }


        }


       
        equation = firstNumber + eqChar + secondNumber;

        

        DataTable dt = new DataTable();
        var v = dt.Compute(equation, "");
        var w = v.ToSafeString();


        rightSide = w;

       

    }


}
