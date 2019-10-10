using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpikesSpawner : MonoBehaviour
{
    public float spikeDamageDelay = 4f;
    public float rate = 2f;
    private bool spiking = false;
    public GameObject SpikePrefab;
    private List<Vector3> _positions = new List<Vector3>();

    [ContextMenu("Activate")]
    public void Activate()
    {
        StartCoroutine(SpikeTime());
    }

    public void Deactivate()
    {
        StopCoroutine(SpikeTime());
        spiking = false;
        _positions.Clear();
    }

    private IEnumerator SpikeTime()
    {
        spiking = true;
        while (spiking)
        {
            if (_positions.Count == 0)
            {
                RefillPositions();
            }
            Vector3 p = _positions.FirstOrDefault();
            Spawn(p);
            _positions.Remove(p);
            yield return new WaitForSeconds(rate);
        }
    }

    private void RefillPositions()
    {
        foreach (Transform t in transform)
        {
            _positions.Add(t.position);
        }
    }

    private void Spawn(Vector3 pos)
    {
        GameObject newSpike = Instantiate(SpikePrefab);
        newSpike.transform.position = pos;
        Destroy(newSpike, spikeDamageDelay * 10f);
        newSpike.GetComponent<Spikes>().Shake(spikeDamageDelay);
    }
}
