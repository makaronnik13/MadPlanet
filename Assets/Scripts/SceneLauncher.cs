using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLauncher : MonoBehaviour
{
    public GameObject End;
   
    public void Load()
    {
        End.SetActive(true);
    }
}
