using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private ICoroutineRunner _coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutine) => 
        _coroutineRunner = coroutine;

    public void Load(string name, Action onLoaded = null) => 
        _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

    public IEnumerator LoadScene(string nextScene, Action onLoaded = null)
    {
        /*if(SceneManager.GetActiveScene().name == nextScene)
        {
            onLoaded?.Invoke();
            yield break;
        }*/

        AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

        while (!waitNextScene.isDone)
        {
            yield return null;
        }

        onLoaded?.Invoke();
    }
}
