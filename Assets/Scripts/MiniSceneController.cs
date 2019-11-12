using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSceneController : MonoBehaviour
{
    [SerializeField]
    private int SceneId;

    // Start is called before the first frame update
    void Start()
    {
        Game.Instance.gameData.ActiveMiniscene.AddListener(SceneChanged);
    }

    private void SceneChanged(int v)
    {
        transform.GetChild(0).gameObject.SetActive(SceneId == v || SceneId+1 == v);
    }

    public void Activate()
    {
        Game.Instance.gameData.ActiveMiniscene.SetState(SceneId);
        Game.Instance.Save();
    }
}
