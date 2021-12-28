using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class finishline : MonoBehaviour
{
	public Rigidbody kart;
	public GameObject checkA;
	public GameObject checkB;
	public GameObject checkC;
	public GameObject checkS;
	public GameObject checkD;
	public GameObject checkE;
	public GameObject checkF;
	public GameObject finishln;
	public Text stopwatch;
	public TMP_Text wrongdirection;
	public Text lastlapui;
	public Text bestlapui;
	public Text totaltimeui;
	public Text lapsui;
	public Text gamefinished;
	public CanvasGroup backgamefinished;
	public float lastlap;
	public float[] bestlap;
	public float calcbestlap;
	public DateTime totaltime;
	public float time;
	public string lootingtime;
	float msec;
	float sec;
	float min;
	public int checkpointA=0;
	public int checkpointB=0;
	public int checkpointC=0;
	public int checkpointD=0;
	public int checkpointE=0;
	public int checkpointF=0;
	public int checkpointS=0;
	public int lapcount;
	public int maxlaps;
	public string format = "mm:ss:ff";
	public string formatspam = @"mm\:ss\:ff";
	public DateTime loottimeparsed;
	public bool gamedone;
	private GameObject[] nitros;

	[SerializeField] private bool isTutorial;

	void Start(){
	bestlap = new float[3] {100.0f,100.0f,100.0f};
	lootingtime = PlayerPrefs.GetString("lootingTime");
	loottimeparsed = DateTime.ParseExact(lootingtime,format,null);
	totaltimeui.text = "Total Time: " + loottimeparsed.ToString("mm:ss:ff");
	lapcount= 1;
	if (isTutorial || SceneManager.GetActiveScene().name != "RacingScene")
		maxlaps = 3;
	else
		maxlaps = 5;
	lapsui.text = "Lap: " + "/" + maxlaps;
	gamedone = false;
	nitros = GameObject.FindGameObjectsWithTag("nitro");
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
	loottimeparsed = totaltime;
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
		totaltime = loottimeparsed + TimeSpan.ParseExact(string.Format("{0:00}:{1:00}:{2:00}",min,sec,msec),formatspam,null);
		totaltimeui.text = "Total Time: " + totaltime.ToString("mm:ss:ff");
		yield return null;
	}
}

IEnumerator addlap(string laptime,string playername)
{

	if (!isTutorial) //we don't send the result to the leaderboards if we're on the tutorial
	{
		print("preparing to send result to the leaderboards");
	  Scene scene = SceneManager.GetActiveScene();
	  UnityWebRequest uwr = UnityWebRequest.Get("https://formulacraft.herokuapp.com/addlap?laptime="+laptime+"&playername="+playername+"&track="+scene.name);
	  yield return uwr.SendWebRequest();

		if (uwr.result == UnityWebRequest.Result.ConnectionError ||
		    uwr.result == UnityWebRequest.Result.DataProcessingError ||
		    uwr.result == UnityWebRequest.Result.ProtocolError)
		{
			print("Error While Sending: " + uwr.error);
		}
		else
		{
			print("Received: " + uwr.downloadHandler.text);
		}
	}
	else
	{
		print("won't send result to the leaderboards, because tutorial");
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
		}
		bestlapui.text = "Best Lap: " + string.Format("{0:00}:{1:00}:{2:00}",bestlap[0],bestlap[1],bestlap[2]);
		StopWatchReset();
		checkpointA=0;
		checkpointB=0;
		checkpointC=0;
		checkpointS=0;
		checkpointD=0;
		checkpointE=0;
		checkpointF=0;
		lapcount++ ;
		foreach (var obj in nitros) {
			obj.SetActive(true);
		}
	}
}
	void Update()
    	{
	        if (checkpointS == 0 && checkpointA== 0 && checkpointB==0 && checkpointC==0){
		        var targetdir = (checkS.transform.position - kart.transform.position).normalized;
		        var currentdir = kart.transform.forward.normalized;
		        if (Vector3.Dot(targetdir, currentdir) < 0)
		        {
			        wrongdirection.text = "Wrong Direction!";
		        }else{wrongdirection.text="";}

	        }else if (checkpointA== 0 && checkpointB==0 && checkpointC==0){
		        var targetdir = (checkA.transform.position - kart.transform.position).normalized;
		        var currentdir = kart.transform.forward.normalized;
		        
		        if (Vector3.Dot(targetdir, currentdir) < 0)
		        {
			        wrongdirection.text = "Wrong Direction!";
		        }else{wrongdirection.text="";}
		        
	        }else if (checkpointA == 1 && checkpointB == 0 && checkpointC == 0)
	        {
		        var targetdir = (checkB.transform.position - kart.transform.position).normalized;
		        var currentdir = kart.transform.forward.normalized;
		        if (Vector3.Dot(targetdir, currentdir) <0)
		        {
			        wrongdirection.text = "Wrong Direction!";
		        }else{wrongdirection.text="";}
	        }else if (checkpointA == 1 && checkpointB == 1 && checkpointC == 0)
	        {
		        var targetdir = (checkC.transform.position - kart.transform.position).normalized;
		        var currentdir = kart.transform.forward.normalized;
		        if (Vector3.Dot(targetdir, currentdir) < 0)
		        {
			        wrongdirection.text = "Wrong Direction!";
		        }else{wrongdirection.text="";}
	        }else if (checkpointA == 1 && checkpointB == 1 && checkpointC == 1 && checkpointD == 0)
	        {
		        var targetdir = (checkD.transform.position - kart.transform.position).normalized;
		        var currentdir = kart.transform.forward.normalized;
		        if (Vector3.Dot(targetdir, currentdir) < 0)
		        {
			        wrongdirection.text = "Wrong Direction!";
		        }else{wrongdirection.text="";}
	        }else if (checkpointA == 1 && checkpointB == 1 && checkpointC == 1 && checkpointD == 1 && checkpointE == 0)
	        {
		        var targetdir = (checkE.transform.position - kart.transform.position).normalized;
		        var currentdir = kart.transform.forward.normalized;
		        if (Vector3.Dot(targetdir, currentdir) < 0)
		        {
			        wrongdirection.text = "Wrong Direction!";
		        }else{wrongdirection.text="";}
	        }else if (checkpointA == 1 && checkpointB == 1 && checkpointC == 1 && checkpointD == 1 && checkpointE == 1 && checkpointF == 0)
	        {
		        var targetdir = (checkF.transform.position - kart.transform.position).normalized;
		        var currentdir = kart.transform.forward.normalized;
		        if (Vector3.Dot(targetdir, currentdir) < 0)
		        {
			        wrongdirection.text = "Wrong Direction!";
		        }else{wrongdirection.text="";}
	        }else if (checkpointA == 1 && checkpointB == 1 && checkpointC == 1)
	        {
		        var targetdir = (finishln.transform.position - kart.transform.position).normalized;
		        var currentdir = kart.transform.forward.normalized;
		        if (Vector3.Dot(targetdir, currentdir) <0)
		        {
			        wrongdirection.text = "Wrong Direction!";
		        }else{wrongdirection.text="";}
	        }

	        if(lapcount >= maxlaps +1){
			StopWatchStop();
			string toshow = "Game Ended\nTotal Time: " + totaltime.ToString("mm:ss:ff") +"\nBestlap: "
			+ string.Format("{0:00}:{1:00}:{2:00}",bestlap[0],bestlap[1],bestlap[2]) 
			+ "\nPress Enter To Return to Main Menu";
			backgamefinished.alpha = 0.46f;
			gamefinished.text=toshow;
			if(!gamedone){
				StartCoroutine(addlap(totaltime.ToString("mm:ss:ff"),PlayerPrefs.GetString("username")));
			}
			if(Input.GetKeyUp(KeyCode.Return)){
				SceneManager.LoadScene("MainMenu");
			}
			if(Input.GetKeyUp("r")){
				SceneManager.LoadScene("RacingScene");
			}
			gamedone = true;
		}else{lapsui.text = "Lap: " + lapcount + "/" + maxlaps;}
	}
}
