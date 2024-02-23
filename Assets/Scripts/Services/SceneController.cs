using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }
    private Animator anim;
    private bool fadeInFinished;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        anim = GetComponent<Animator>();
    }
    
    public void ChanceScene(string newScene)
    {
        StartCoroutine(ChangeSceneCoroutine(newScene));
    }

    private IEnumerator ChangeSceneCoroutine(string newScene)
    {
        string oldScene = SceneManager.GetActiveScene().name;
        anim.SetTrigger("FadeIn");
        fadeInFinished = false;
        while (!fadeInFinished)
            yield return null;
        yield return LoadAndUnloadAsync(newScene, oldScene);
        anim.SetTrigger("FadeOut");
    }

    private IEnumerator LoadAndUnloadAsync(string newScene, string oldScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
            yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(newScene));
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(oldScene);
        while (!asyncUnload.isDone)
            yield return null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {

    }
    private void OnSceneUnloaded(Scene scene)
    {

    }

    public void OnFadeInFinished()
    {
        fadeInFinished = true;
    }
}
