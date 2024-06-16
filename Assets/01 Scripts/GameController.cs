using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    GameObject player;
    GameObject enemy;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI gameClearText;
    [SerializeField] Button restartButton;

    TextMeshPro distanceText;

    float timer = 5f;
    bool blink = false;
    public bool onGame;
    public bool gameClear;

    int[] playerLocation = new int[2];
    int[] enemyLocation = new int[2];

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        enemy = FindObjectOfType<Enemy>().gameObject;
        onGame = true;
        gameClearText.enabled = false;
        restartButton.gameObject.SetActive(false);
    }
    private void Update()
    {
        CheckGameClearStatus();
        GetDistance();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (onGame)
        {
            timerText.text = timer.ToString("0.00").Replace(".", ":");
            timer -= Time.deltaTime;
            if (timer > 0 && timer <= 2 && !blink)
            {
                blink = true;
                StartCoroutine(Flashed());
            }
            else if (timer <= 0)
            {
                blink = false;
                if (!timerText.enabled)
                {
                    timerText.enabled = true;
                }
                timer = 0;
                gameClear = false;
                onGame = false;
            }
        }
        else 
        {
            restartButton.gameObject.SetActive(true);
        }
    }

    IEnumerator Flashed()
    {
        while (blink) 
        {
            timerText.enabled = !timerText.enabled;
            yield return new WaitForSeconds(0.1f);
        }

    }

    private void GetDistance()
    {
        distanceText = enemy.GetComponentInChildren<TextMeshPro>();
        playerLocation[0] = (int)player.transform.position.x;
        playerLocation[1] = (int)player.transform.position.y;

        enemyLocation[0] = (int)enemy.transform.position.x;
        enemyLocation[1] = (int)enemy.transform.position.y;

        if (gameClear)
        {
            distanceText.text = "0";
        }
        else 
        {
            distanceText.text = (Math.Abs(enemyLocation[0] - playerLocation[0]) + Math.Abs(enemyLocation[1] - playerLocation[1])).ToString();

        }
    }
    
    private void CheckGameClearStatus()
    {
        if (!onGame)
        {
            if (gameClear) 
            {
                gameClearText.enabled = true;
                gameClearText.text = "Clear";
            }
            else if (!gameClear)
            {
                onGame = false;
                gameClearText.enabled = true;
                gameClearText.text = "Game Over \n Restart: Space Key";
                if (Input.GetKey(KeyCode.Space)) 
                {
                    RestartStage();
                }
            }
        }

    }
    public void RestartStage() 
    {
       UnityEngine.SceneManagement.Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }
}
