using System.Collections;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{

    [SerializeField] EquationCreator Equations;
    [SerializeField] TMP_Text Score;
    [SerializeField] TMP_Text ScorePopUp;


    public void updateText(int type, int score) // 0 - loss, 1 - gain
    {
        Score.text = $"{score}/600";

        TMP_Text scoreShow = Instantiate(ScorePopUp, ScorePopUp.transform.parent);

        if (type == 0)
        {
            scoreShow.text = "èle!";
            scoreShow.color = Color.red;

            TMP_Text childText = scoreShow.transform.GetChild(0).GetComponent<TMP_Text>();
            childText.text = "-50 PunktÛw";
            childText.color = Color.red;

        }

        scoreShow.gameObject.SetActive(true);
        scoreShow.GetComponent<Animator>().Play("ScorePopUp");

        Destroy(scoreShow.gameObject, 2f);
    }
    public void OnHoverEnter(GameObject self)
    {

        Animator anim = self.GetComponent<Animator>();
        if (!anim) return;

        anim.SetBool("Exit", false);

        anim.SetBool("Start", true);
    }

    public void OnHoverExit(GameObject self)
    {
        Animator anim = self.GetComponent<Animator>();
        if (!anim) return;

        anim.SetBool("Start", false);

        anim.SetBool("Exit", true);
    }
}
