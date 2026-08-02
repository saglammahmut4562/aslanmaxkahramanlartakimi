using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class NerdFlurry : MonoBehaviour
{
	// Token: 0x06000009 RID: 9 RVA: 0x000024B0 File Offset: 0x000006B0
	public NerdFlurry()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			this.mCurrentActivity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			this.mFlurryClass = new AndroidJavaClass("com.flurry.android.FlurryAgent");
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000024FC File Offset: 0x000006FC
	public void StartSession(string API_KEY)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			this.mFlurryClass.CallStatic("setLogLevel", new object[] { 2 });
			this.mFlurryClass.CallStatic("setLogEnabled", new object[] { true });
			this.mFlurryClass.CallStatic("setLogEvents", new object[] { true });
			this.mFlurryClass.CallStatic("setCaptureUncaughtExceptions", new object[] { true });
			this.mFlurryClass.CallStatic("onStartSession", new object[] { this.mCurrentActivity, API_KEY });
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000025B4 File Offset: 0x000007B4
	public void EndSession()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			this.mFlurryClass.CallStatic("onEndSession", new object[] { this.mCurrentActivity });
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000025E4 File Offset: 0x000007E4
	public int GetAgentVersion()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			return this.mFlurryClass.CallStatic<int>("getAgentVersion", new object[0]);
		}
		return 0;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002618 File Offset: 0x00000818
	public void LogEvent(string eventId, bool timed = false)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (!timed)
			{
				this.mFlurryClass.CallStatic("logEvent", new object[] { eventId });
			}
			else
			{
				this.mFlurryClass.CallStatic("logEvent", new object[] { eventId, true });
			}
		}
	}

	// Token: 0x0600000E RID: 14 RVA: 0x0000267C File Offset: 0x0000087C
	public void LogEvent(string eventId, Dictionary<string, string> parameters, bool timed = false)
	{
		using (AndroidJavaObject androidJavaObject = new AndroidJavaObject("java.util.HashMap", new object[0]))
		{
			IntPtr methodID = AndroidJNIHelper.GetMethodID(androidJavaObject.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
			object[] array = new object[2];
			foreach (KeyValuePair<string, string> keyValuePair in parameters)
			{
				using (AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("java.lang.String", new object[] { keyValuePair.Key }))
				{
					using (AndroidJavaObject androidJavaObject3 = new AndroidJavaObject("java.lang.String", new object[] { keyValuePair.Value }))
					{
						array[0] = androidJavaObject2;
						array[1] = androidJavaObject3;
						AndroidJNI.CallObjectMethod(androidJavaObject.GetRawObject(), methodID, AndroidJNIHelper.CreateJNIArgArray(array));
					}
				}
			}
			if (!timed)
			{
				this.mFlurryClass.CallStatic("logEvent", new object[] { eventId, androidJavaObject });
			}
			else
			{
				this.mFlurryClass.CallStatic("logEvent", new object[] { eventId, androidJavaObject, true });
			}
		}
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002830 File Offset: 0x00000A30
	public void EndTimedEvent(string eventId)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			this.mFlurryClass.CallStatic("endTimedEvent", new object[] { eventId });
		}
	}

	// Token: 0x04000006 RID: 6
	private AndroidJavaObject mCurrentActivity;

	// Token: 0x04000007 RID: 7
	private AndroidJavaClass mFlurryClass;
}
