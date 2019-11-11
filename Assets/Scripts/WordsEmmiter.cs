using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordsEmmiter : MonoBehaviour
{
    [SerializeField]
    private float forceMultiplyer;

    [SerializeField]
    private GameObject WordPrefab;

    [SerializeField]
    private float rate;

    [SerializeField]
    private MobVision Vision;

    public bool Active = false;

    // Start is called before the first frame update
    void Start()
    {
        Vision.OnInside += StartEmmition;
    }

    public void Activate(bool v)
    {
        StopAllCoroutines();
        Active = v;
        if (Active)
        {
            StartEmmition(null);
        }
    }

    private void StartEmmition(PlayerIdentity obj)
    {
        Active = true;
        StartCoroutine(Emmition());
    }

    private IEnumerator Emmition()
    {
        while (Active)
        {
            GameObject newWord = Instantiate(WordPrefab);
            newWord.GetComponentInChildren<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV(0.5f,0.8f);
            newWord.transform.localScale = Vector3.one * UnityEngine.Random.Range(1f, 3f);
            newWord.transform.SetParent(transform);
            newWord.transform.localPosition = Vector3.zero;
            newWord.transform.localRotation = Quaternion.identity * Quaternion.Euler(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));
            Vector3 pushDir = Vector3.Lerp(Vector3.up, (FindObjectOfType<PlayerIdentity>().transform.position).normalized- transform.position, 0.75f)*forceMultiplyer;
            newWord.GetComponent<Rigidbody>().AddForce(pushDir);
            Destroy(newWord, 15f);
            
            yield return new WaitForSeconds(rate);
        }
    }
}
