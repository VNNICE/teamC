using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitileManager : MonoBehaviour
{
    bool isControl;
    [SerializeField] GameObject titleObjects;
    [SerializeField] GameObject howToControl;
    bool onHowToControl;
    [SerializeField] GameObject credits;
    bool onCredits;
    // Start is called before the first frame update
    void Start()
    {
        onHowToControl = false;
        onCredits = false;
        howToControl.SetActive(false);
        credits.SetActive(false);
        StartCoroutine(WaitLoading());
    }
    private void Update()
    {
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
            SceneManager.LoadScene("Stage1");
        }
    }
    void ShowHowToPlay()
    {
        if (!onHowToControl)
        {
            onHowToControl = true;
            titleObjects.SetActive(false);
            howToControl.SetActive(true);
        }
        else 
        {
            onHowToControl = false;
            howToControl.SetActive(false);
            titleObjects.SetActive(true);
        }
    }
    void ShowCredit()
    {
        if (!onCredits)
        {
            onCredits = true;
            titleObjects.SetActive(false);
            credits.SetActive(true);
        }
        else
        {
            onCredits = false;
            credits.SetActive(false);
            titleObjects.SetActive(true);
        }
    }
    private IEnumerator WaitLoading() 
    {
        yield return new WaitForSeconds(3);
        isControl = true;
        StopCoroutine(WaitLoading());
    }

}
