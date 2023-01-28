using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour
{
    [SerializeField]
    private float _sceneChangeDelay;


    //вызывается по ивенту смерти игрока
    [UsedImplicitly]
    public void OnPlayerDied()
    {
        StartCoroutine(ShowGameOver());
    }

    private IEnumerator ShowGameOver()
    {
        //задержка, чтобы успели проиграться анимация и звук смерти игрока.
        yield return new WaitForSeconds(_sceneChangeDelay);
        SceneManager.LoadSceneAsync(GlobalConstants.GAME_OVER_SCENE);
    }
}