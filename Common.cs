using UnityEngine;
using System.Collections;
using System;

public class Common : MonoBehaviour {
	public static Color s_normalGoldColor = new Color (241f / 255f, 182f / 255f, 36f / 255f); 
	public static GameObject FindChild(string pRoot, string pName)
	{
		Transform pTransform = GameObject.Find(pRoot).GetComponent<Transform>();
		foreach (Transform trs in pTransform) {
			if (trs.gameObject.name == pName)
				return trs.gameObject;
		}
		return null;
	}
	public static GameObject FindChild(GameObject go ,string pName)
	{
		Transform pTransform = go.transform;
		foreach (Transform trs in pTransform) {
			if (trs.gameObject.name == pName)
				return trs.gameObject;
		}
		return null;
	}
	public static T ProduceComponent<T>(string produceName) where T : MonoBehaviour
	{
		GameObject temp = GameObject.Find (produceName);
		T component;
		if (temp == null) {
			component = (new GameObject (produceName)).AddComponent<T> ();
		} else {
			component = temp.GetComponent<T> ();
		}
		return component;
	}
	public static GameObject LoadPrefab(string path)
	{
		UnityEngine.Object o = Resources.Load(path);
		if(o == null)
		{
			Debug.LogError("LoadPrefab fail, path: "+path);
			return null;
		}

		GameObject go =  (GameObject)MonoBehaviour.Instantiate(o);
		return go;
	}
	public static GameObject LoadPrefab(string path,GameObject baseObj)
	{
		UnityEngine.Object o = Resources.Load(path);
		if(o == null)
		{
			Debug.LogError("LoadPrefab fail, path: "+path);
			return null;
		}

		GameObject go = (GameObject)o;
		GameObject targetGO = LoadPrefab (go,baseObj);
		return targetGO;
	}
	public static GameObject LoadPrefab(GameObject obj,GameObject baseObj)
	{

		GameObject go =  (GameObject)MonoBehaviour.Instantiate(obj);
		if(baseObj != null)
			go.transform.SetParent (baseObj.transform);
		go.transform.localScale = Vector3.one;
		go.transform.localPosition = Vector3.zero;
		return go;
	}
	public static GameObject LoadPrefab(string path, Vector3 position, Quaternion rotation)
	{
		UnityEngine.Object o = Resources.Load(path);
		GameObject go = (GameObject)MonoBehaviour.Instantiate(o, position, rotation);
		return go;
	}
	public static T IntToEnum<T>(int num)  where T : struct, IConvertible
	{
		T enumType = (T)Enum.ToObject(typeof(T) , num);
		return enumType;
	}
	/// <summary>
	/// Convert a date time object to Unix time representation.
	/// </summary>
	/// <param name="datetime">The datetime object to convert to Unix time stamp.</param>
	/// <returns>Returns a numerical representation (Unix time) of the DateTime object.</returns>
	public static long ConvertToUnixTime(DateTime datetime)
	{
		DateTime sTime = new DateTime(1970, 1, 1,0,0,0,DateTimeKind.Utc);

		return (long)(datetime - sTime).TotalSeconds;
	}

	/// <summary>
	/// Convert Unix time value to a DateTime object.
	/// </summary>
	/// <param name="unixtime">The Unix time stamp you want to convert to DateTime.</param>
	/// <returns>Returns a DateTime object that represents value of the Unix time.</returns>
	public static DateTime UnixTimeToDateTime(long unixtime)
	{
		DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return sTime.AddSeconds(unixtime);
	}
	public static long NowUnixTime()
	{
		return Common.ConvertToUnixTime (DateTime.UtcNow);
	}
	public static string MinuteTime (long unixtime)
	{
		long minute = unixtime / 60;
		long second = unixtime % 60;
		return minute + " m " + second + " s ";
	}
	public static string HourMinTime(long unixtime)
	{
		long hour = unixtime / 3600;
		long minute = (unixtime % 3600) / 60;
		long second = (unixtime % 3600) % 60;
		return hour.ToString("00") +" :"+ minute.ToString("00") + " : " + second.ToString("00");
	}
	public static string MinuteTime (float sconds)
	{
		int minute = (int)sconds / 60;
		int second = (int)sconds % 60;
		return minute.ToString("00") + ":" + second.ToString("00");
	}
	public static System.DateTime GetLocalTime()
	{
		return System.DateTime.Now;
	}
	public static bool CheckOpenTime(long start,long end)
	{
		bool isOpen = false;
		long curTime = Common.NowUnixTime ();
		if (start < curTime && end > curTime) {
			isOpen = true;
		}
		return isOpen;

	}

	public static long NowTotalSeconds()
	{
		int hourSeconds = (DateTime.Now.Hour * 60 * 60);
		int minSeconds = (DateTime.Now.Minute * 60 * 60);
		int seconds = DateTime.Now.Second + minSeconds + hourSeconds;
		return (long)seconds;
	}
	public static int SecondToHourTime(long seconds)
	{
		int hour = (int)(seconds / 3600);
		return hour;
	}
}


