using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public Text stopwatch;
    public float time;
    float msec;
    float sec;
    float min;
    // Start is called before the first frame update
    private void Start(){
        StartCoroutine("StopWatch");
    }

        
    IEnumerator StopWatch()
    {
        while(true)
        {
            time += Time.deltaTime;
            msec = (int)((time - (int)time) * 100);
            sec = (int)(time % 60);
            min = (int)(time / 60 % 60);

            stopwatch.text = string.Format("{0:00}:{1:00}:{2:00}",min,sec,msec);
            yield return null;
        }
    }

    public void StopLootingTimer(){
        StopCoroutine("StopCoroutine");
        PlayerPrefs.SetString("lootingTime", string.Format("{0:00}:{1:00}:{2:00}",min,sec,msec));
    }
}

