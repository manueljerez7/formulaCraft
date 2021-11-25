using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class finishline : MonoBehaviour
{
	public Text stopwatch;
	public Text lastlapui;
	public Text bestlapui;
	public float lastlap;
	public float[] bestlap;
	public float calcbestlap;
	public float time;
	float msec;
	float sec;
	float min;
	public int checkpointA=0;
	public int checkpointB=0;
	public int checkpointC=0;

void Start(){
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

IEnumerator addlap(string laptime,string playername)
{
	UnityWebRequest uwr = UnityWebRequest.Get("https://formulacraft.herokuapp.com/addlap?laptime="+laptime+"&playername="+playername);
	yield return uwr.SendWebRequest();

	if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.DataProcessingError || uwr.result == UnityWebRequest.Result.ProtocolError)
	{
		print("Error While Sending: " + uwr.error);
	}
	else
	{
		print("Received: " + uwr.downloadHandler.text);
	}
}

void OnCollisionEnter(Collision collision){
	if (checkpointA== 0 && checkpointB==0 && checkpointC==0){
		StopWatchStart();
	}
	if (checkpointA== 1 && checkpointB==1 && checkpointC==1)
	{
		lastlap = min * 60000 + sec * 1000 + msec;
		calcbestlap = bestlap[0] * 60000 + bestlap[1] * 1000 + bestlap[2];
		lastlapui.text = "Last Lap: " + string.Format("{0:00}:{1:00}:{2:00}",min,sec,msec);
		if(lastlap < calcbestlap){
			bestlap[0]=min;
			bestlap[1]=sec;
			bestlap[2]=msec;
			StartCoroutine(
				addlap(string.Format("{0:00}:{1:00}:{2:00}", bestlap[0], bestlap[1], bestlap[2]),
					PlayerPrefs.GetString("username")));
		}
		bestlapui.text = "Best Lap: " + string.Format("{0:00}:{1:00}:{2:00}",bestlap[0],bestlap[1],bestlap[2]);
		StopWatchReset();
		checkpointA=0;
		checkpointB=0;
		checkpointC=0;
	}

}

}
