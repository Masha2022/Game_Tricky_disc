using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI _currentScoreLabel;
    [SerializeField]
    private TextMeshProUGUI _bestScoreLabel;
    [SerializeField]
    private float _newBestScoreAnimationDuration;
    
    private void Awake()
    {
        var currentScore = PlayerPrefs.GetInt(GlobalConstants.SCORE_PREFS_KEY);
        var bestScore = PlayerPrefs.GetInt(GlobalConstants.BEST_SCORE_PREFS_KEY);

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            SaveNewBestSore(bestScore);
            _bestScoreLabel.transform.DOPunchScale(Vector3.one, _newBestScoreAnimationDuration, 0);
        }

        _currentScoreLabel.text = currentScore.ToString();
        _bestScoreLabel.text = $"BEST {bestScore.ToString()}";
    }
    
    //вызывается при нажатии на кнопку рестарта игры
    [UsedImplicitly]
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(GlobalConstants.GAME_SCENE);
    }
    
    //вызывается при нажатии на кнопку выхода из игры
    [UsedImplicitly]
    public void ExitGame() 
    {
//данная конструкция выполняется только в редакторе Unity
#if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    private void SaveNewBestSore(int bestScore)
    {
        PlayerPrefs.SetInt(GlobalConstants.BEST_SCORE_PREFS_KEY, bestScore);
        PlayerPrefs.Save();
    }
}
