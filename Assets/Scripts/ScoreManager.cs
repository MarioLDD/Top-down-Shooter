using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text killCountUI;
    public static int score = 0;
    public int killCount;
    public int totalEnemy;
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
        totalEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
        killCountUI.text = "Kills: " + killCount.ToString() + " / " + totalEnemy.ToString();
    }

    public void RefreshScore(int addscore)
    {
        score = score + addscore;
        scoreText.text = "Score: " + score.ToString();
        killCount++;
        killCountUI.text = "Kills: " + killCount.ToString() + " / " + totalEnemy.ToString();
if(killCount == totalEnemy)
        {
            door.SetActive(false);
        }
    }
}