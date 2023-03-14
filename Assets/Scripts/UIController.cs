using UnityEngine;

public class UIController : MonoBehaviour
{

    // Variables

    public void OnClick(string action)
    {

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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
