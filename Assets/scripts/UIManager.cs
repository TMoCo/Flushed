using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject youLosePanel;
    [SerializeField] private Player player;

    [SerializeField] private ObjectSpawner objectSpawner;
    [SerializeField] private PipeManager pipeManager;

    public void UpdateScore()
    {
        scoreText.text = "Score:\n" + player.score;
    }

    public void DisplayLoss()
    {
        youLosePanel.SetActive(true);
    }

    public void PlayAgain()
    {
        youLosePanel.SetActive(false);
        player.Reset();
        pipeManager.Reset();
        objectSpawner.StartCoroutines();
        UpdateScore();
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
