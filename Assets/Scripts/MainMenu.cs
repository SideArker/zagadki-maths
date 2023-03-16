using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] Animator sceneChangeAnim;

    // Start is called before the first frame update

    public void changeScene()
    {
        sceneChangeAnim.Play("SceneMove");

    }

    public void exit()
    {
        Application.Quit();
    }
}
