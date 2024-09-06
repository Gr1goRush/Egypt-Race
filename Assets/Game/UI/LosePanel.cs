
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreOutput;
    [SerializeField] private TextMeshProUGUI _bestScoreOutput;

    private Wallet _scoreWallet;

    public void Init(Wallet scoreWallet)
    {
        _scoreWallet = scoreWallet;
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void Open()
    {
        gameObject.SetActive(true);
        ShowScore();
    }
    private void ShowScore()
    {
        int score = _scoreWallet.Coins;
        int bestScore = Saver.GetInt("BestScore", 0);

        string scoreText = $"SCORE: {score}M";
        string bestScoreText = $"BESTSCORE: {bestScore}M";

        _scoreOutput.text = scoreText;
        _bestScoreOutput.text = bestScoreText;
    }
    public void BackToHome()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
