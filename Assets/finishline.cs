using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finishline : MonoBehaviour
{
	public Text stopwatch;
	public Text lastlapui;
	public Text bestlapui;
	public float[] lastlap;
	public float[] bestlap;
	public float time;
	float msec;
	float sec;
	float min;
	public int checkpointA=0;
	public int checkpointB=0;
	public int checkpointC=0;

void Start(){
	lastlap = new float[3] {0.0f,0.0f,0.0f};
	bestlap = new float[3] {100.0f,100.0f,100.0f};
}

public void StopWatchStart()
{
	StartCoroutine("StopWatch");
}
public void StopWatchStop()
{
	StopCoroutine("StopWatch");
}
public void StopWatchReset()
{
	time = 0;
	stopwatch.text = "Lap Time: 00:00:00";
}

IEnumerator StopWatch()
{
	while(true)
	{
		time += Time.deltaTime;
		msec = (int)((time - (int)time) * 100);
		sec = (int)(time % 60);
		min = (int)(time / 60 % 60);

		stopwatch.text = string.Format("Lap Time: {0:00}:{1:00}:{2:00}",min,sec,msec);
		yield return null;
	}
}

void OnCollisionEnter(Collision collision){
	if (checkpointA== 0 && checkpointB==0 && checkpointC==0){
		StopWatchStart();
	}
	if (checkpointA== 1 && checkpointB==1 && checkpointC==1){
		soundManager.PlaySound(soundManager.Sound.KartFinishLap);
		lastlap[0] = min;
		lastlap[1] = sec;
		lastlap[2] = msec;
		lastlapui.text = "Last Lap: " + string.Format("{0:00}:{1:00}:{2:00}",min,sec,msec);
		
		if(lastlap[0] < bestlap[0]){
			bestlap[0]=lastlap[0];
			bestlap[1]=lastlap[1];
			bestlap[2]=lastlap[2];
			bestlapui.text = "Last Lap: " + string.Format("{0:00}:{1:00}:{2:00}",min,sec,msec);
		}else if(lastlap[1] < bestlap[1]){
			bestlap[0]=lastlap[0];
			bestlap[1]=lastlap[1];
			bestlap[2]=lastlap[2];
			bestlapui.text = "Last Lap: " + string.Format("{0:00}:{1:00}:{2:00}",min,sec,msec);
		}else if(lastlap[2] < bestlap[2]){
			bestlap[0]=lastlap[0];
			bestlap[1]=lastlap[1];
			bestlap[2]=lastlap[2];
			bestlapui.text = "Last Lap: " + string.Format("{0:00}:{1:00}:{2:00}",min,sec,msec);
		}
		StopWatchReset();
		checkpointA=0;
		checkpointB=0;
		checkpointC=0;
	}

}

}
