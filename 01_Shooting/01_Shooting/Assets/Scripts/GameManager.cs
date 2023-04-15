using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject player;

    int score = 0;

    public Text scoreText;

    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Invoke("StartGame", 3f);    // 3초 후 StartGame 실행
    }

    void StartGame()
    {
        player.GetComponent<Player>().canShoot = true;

        SpawnManager.instance.isSpawn = true;
    }

    public void AddScore(int enemyScore)    // 적을 격파할 때마다 점수가 올라가는 함수
    {
        score += enemyScore;
        scoreText.text = "Score:" + score;
    }
}
