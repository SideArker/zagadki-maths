using System.Collections;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{

    [SerializeField] EquationCreator Equations;
    [SerializeField] TMP_Text Score;

    static float lerpDuration = 0.2f;
    static float waitBetweenLerps = 0.2f;
    IEnumerator colorLerp(Color goal)
    {
        float timeElapsed = 0f;
        Color startColor = Score.color;
        while(timeElapsed < lerpDuration)
        {
            Score.color = Color.Lerp(startColor, goal, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(waitBetweenLerps);

        if (Score.color != Color.white) StartCoroutine(colorLerp(Color.white));

    }

    public void updateText(int type, int score) // 0 - loss, 1 - gain
    {
        Score.text = $"{score}/600";

        if(type == 1) StartCoroutine(colorLerp(Color.green));
        else StartCoroutine(colorLerp(Color.red));

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
