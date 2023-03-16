using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : StateMachineBehaviour
{
    [SerializeField] bool MoveBack;

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!MoveBack) SceneManager.LoadScene(1);
        else
        {
            GameController gc = FindObjectOfType<GameController>();
            gc.regenLevel();
        }
    }
}
