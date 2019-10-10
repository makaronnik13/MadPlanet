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

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Game.Instance.Paused.SetState(!Game.Instance.Paused.State);  
            }
        }
    }

    private void Start()
    {
        ContinueBtn.interactable = Game.Instance.gameData != null;
        Game.Instance.Paused.AddListener(OnPause, true);
    }

    private void OnPause(bool v)
    {
        Animator.SetBool("Paused", v);
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
            if (Game.Instance.Paused.State)
            {
                Debug.Log("P");
                Game.Instance.Paused.SetState(false);
                return;
                
            }
            else
            {
                Game.Instance.Load();
            }
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
      //  BackCanvas.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }


}
