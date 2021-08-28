
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;

    void Start()
    {
        scoreText.SetText(score.ToString());
    }

    public void UpdateScore(int points)
    {
        score += points;

        scoreText.SetText(score.ToString());
    }

    public int CurrentScore()
    {
        return score;
    }
}
