using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class leaderboardmenu : MonoBehaviour
{
	public Text leaderboardui;
	public int leaderboardflag = 0;
	public string scenename;

	public static string Indent(int count)
	{
		return "".PadLeft(count);
	}

	IEnumerator getleaderboard()
	{
		if(SceneManager.GetActiveScene().name=="Leaderboard1Menu"){
			scenename="RacingScene";
		}
		if(SceneManager.GetActiveScene().name=="Leaderboard2Menu"){
			scenename="DesertRacing";
		}
		if(SceneManager.GetActiveScene().name=="Leaderboard3Menu"){
			scenename="Cota";
		}
		string leaderboard = "";
		UnityWebRequest uwr = UnityWebRequest.Get("https://formulacraft.herokuapp.com/fastestlaps?limit=10&track="+scenename);
		yield return uwr.SendWebRequest();

		if (uwr.result == UnityWebRequest.Result.ConnectionError ||
		    uwr.result == UnityWebRequest.Result.DataProcessingError ||
		    uwr.result == UnityWebRequest.Result.ProtocolError)
		{
			print("Error While Sending: " + uwr.error);
		}
		else
		{
			string[] datas;
			string datatime;
			string dataname;
			string data = uwr.downloadHandler.text;
			data = data.Substring(1, data.Length - 2);
			datas = data.Split(',');
			int i = 1;
			while (i < datas.Length + 1)
			{
				datatime = data.Split(',')[i].Split('"')[3].Trim('"');
				dataname = data.Split(',')[i + 1].Split('"')[3].Trim('"');
				leaderboard += Indent(0) + dataname + Indent(2) + ":" + Indent(2) + datatime + "\n";
				i += 3;
			}
		}

		leaderboardui.text = leaderboard;
	}

	void Start()
	{
		StartCoroutine(getleaderboard());
	}
}
