using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float minInt = 1.5f;
    public float maxInt = 3f;
    public float minRange = 0f;
    public float maxRange = 0f;
    public int duration = 5;
    private bool flag = false;

    void Update()
    {
        if (!flag)
        {
            flag = true;
            StartCoroutine(Flicker());
        }
        
    }
    protected IEnumerator Flicker()
    {
        float startTime = Time.time;
        float goalIntensity = Random.Range(minInt, maxInt);
        float goalRange = Random.Range(minRange, maxRange);
        for (int i = 0; i < duration; ++i)
        {
            yield return new WaitForSeconds(0.01f);
            float t = (Time.time - startTime) / (duration*100);
            float cur = GetComponent<Light>().intensity;
            if (cur > goalIntensity)
            {
                GetComponent<Light>().intensity = Mathf.SmoothStep(goalIntensity, cur, t);
            }
            else
            {
                GetComponent<Light>().intensity = Mathf.SmoothStep(cur, goalIntensity, t);
            }
        }
        flag = false;
    }
}

