using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] private float _minLoadingTime = 2;

    private float _currentTime;

    public void LoadGame()
    {
        gameObject.SetActive(true);
        StartCoroutine(Load());
    }
    private IEnumerator Load()
    {
        yield return null;
        AsyncOperation loading = SceneManager.LoadSceneAsync("Game");
        while(loading.isDone && _currentTime >= _minLoadingTime)
        {
            _currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
