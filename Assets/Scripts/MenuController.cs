using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private string SceneName;

    [SerializeField]
    private GameObject BackCanvas;

    [SerializeField]
    private Animator Animator;

    [SerializeField]
    private ParticleSystem Particles;

    [SerializeField]
    private Button ContinueBtn;

    private AsyncOperation _loadingOp;

    private void Start()
    {
        ContinueBtn.interactable = Game.Instance.gameData != null;
    }

    public void StartNewGame(bool errase)
    {
        if (errase)
        {
            Game.Instance.gameData = new GameData();
            Game.Instance.Save();
        }
        else
        {
            Game.Instance.Load();
        }

        Animator.SetTrigger("Loading");
        Particles.Stop();
        _loadingOp = SceneManager.LoadSceneAsync(SceneName);
        _loadingOp.completed += OnComplete;
    }

    private void OnComplete(AsyncOperation v)
    {
        StartCoroutine(FinishLoading());
    }

    private IEnumerator FinishLoading()
    {
        yield return new WaitForSeconds(2);
        Animator.SetTrigger("Loaded");
        BackCanvas.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }


}
