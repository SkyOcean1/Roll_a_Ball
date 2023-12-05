using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI totalScoreText;
    private int score;
    private int totalScore = 100;
    // 씬 관리 번호
    // 씬 전환

    private void Awake() //싱글톤 패턴 로직
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        {
            score = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotateObjectsWithTag("Item"); // 아이템 태그 회전 함수
    }


    public void IncreaseScore(int amount)
    {
        score += amount;
        totalScore += amount;
        UpdateScoreUI();
    }

    // UI 업데이트 함수
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
            totalScoreText.text = "Total Score: " + totalScore;
        }
    }

    void RotateObjectsWithTag(string tag) // 아이탬 태그 회전 함수 
    {
        GameObject[] rotateObjects = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject item in rotateObjects)
        {
            item.transform.Rotate(Vector3.up, 100.0f * Time.deltaTime);
        }
    }
}
