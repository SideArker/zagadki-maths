using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

   [SerializeField] EquationCreator equationCreator;

    public int score;

    public void regenLevel()
    {
        equationCreator.GenerateEquation();
    }

    void Start()
    {
        regenLevel();  
    }
}
