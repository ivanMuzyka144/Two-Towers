using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
  public class SceneLoader
  {
    private readonly ICoroutineRunner _coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner) => 
      _coroutineRunner = coroutineRunner;

    public void LoadSameScene(Action onLoaded = null)
    {
      _coroutineRunner.StartCoroutine(CO_LoadSameScene(onLoaded));
    }

    public void Load(string name, Action onLoaded = null) =>
      _coroutineRunner.StartCoroutine(CO_LoadScene(name, onLoaded));
    
    public IEnumerator CO_LoadScene(string nextScene, Action onLoaded = null)
    {
      if (SceneManager.GetActiveScene().name == nextScene)
      {
        onLoaded?.Invoke();
        yield break;
      }
      
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

      while (!waitNextScene.isDone)
        yield return null;
      
      onLoaded?.Invoke();
    }
    
    public IEnumerator CO_LoadSameScene(Action onLoaded = null)
    {
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

      while (!waitNextScene.isDone)
        yield return null;
      
      onLoaded?.Invoke();
    }
  }
}