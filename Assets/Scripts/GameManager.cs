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
    // �� ���� ��ȣ
    // �� ��ȯ

    private void Awake() //�̱��� ���� ����
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
        RotateObjectsWithTag("Item"); // ������ �±� ȸ�� �Լ�
    }


    public void IncreaseScore(int amount)
    {
        score += amount;
        totalScore += amount;
        UpdateScoreUI();
    }

    // UI ������Ʈ �Լ�
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
            totalScoreText.text = "Total Score: " + totalScore;
        }
    }

    void RotateObjectsWithTag(string tag) // ������ �±� ȸ�� �Լ� 
    {
        GameObject[] rotateObjects = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject item in rotateObjects)
        {
            item.transform.Rotate(Vector3.up, 100.0f * Time.deltaTime);
        }
    }
}
