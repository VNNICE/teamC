using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitileManager : MonoBehaviour
{
    
    [SerializeField] GameObject titleEffect;
    [SerializeField] GameObject titleUI;
    [SerializeField] GameObject howToControl;
    [SerializeField] GameObject credits;  

    Animator animator;
    bool onHowToControl;
    bool onCredits;
    public bool effectEnd = false;
    public bool canControl = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        titleUI.SetActive(false);
        onHowToControl = false;
        onCredits = false;
        howToControl.SetActive(false);
        credits.SetActive(false);
        
        animator.enabled = true;
    }
    private void Update()
    {
<<<<<<< HEAD
        animator.SetBool("EffectEnd", effectEnd);
        ControlSettings();
        ShowCredits();
=======
        if (isControl && !onCredits && Input.GetKeyDown(KeyCode.Space))
        {
            ShowHowToPlay();
        }
        else if (isControl && !onHowToControl && Input.GetKeyDown(KeyCode.C))
        {
            ShowCredit();
        }
        else if (isControl && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Stage01");
        }
>>>>>>> parent of 55ceca2 (0717)
    }

    void ControlSettings()
    {
        if (canControl && Input.GetKeyDown(KeyCode.Space) && !onCredits) 
        {
            onHowToControl = !onHowToControl;
            Debug.Log("onHowToControl: " + onHowToControl);
        }

        if (canControl && Input.GetKeyDown(KeyCode.C) && !onHowToControl)
        {
            onCredits = !onCredits;
            Debug.Log("onCredits: " + onCredits);
        }

        if (canControl && Input.GetKeyDown(KeyCode.Return) && !onCredits && !onHowToControl)
        {
            SceneManager.LoadScene("Stage01");
        }
        ShowHowToControl();
    }

    void ShowHowToControl() 
    {
        if (onHowToControl)
        {
            titleUI.SetActive(false);
            howToControl.SetActive(true);
        }
        else 
        {
            howToControl.SetActive(false);
            titleUI.SetActive(true);
        }
    }
    void ShowCredits()
    {
        if (onCredits)
        {
            titleUI.SetActive(false);
            credits.SetActive(true);
        }
        else
        {
            credits.SetActive(false);
            titleUI.SetActive(true);
        }
    }

    //アニメーションイベントで起動させています。
    void OnTitleEffectEnd()
    {
        effectEnd = true;
        titleUI.SetActive(true);
        Debug.Log(effectEnd);
    }

    void ActiveControl() 
    {
        canControl = true;
    }
}
