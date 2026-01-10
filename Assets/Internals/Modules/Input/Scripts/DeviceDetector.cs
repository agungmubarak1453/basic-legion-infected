using UnityEngine;

#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

namespace BasicLegionInfected.Input
{
	public class DeviceDetector : MonoBehaviour
	{
#if UNITY_WEBGL && !UNITY_EDITOR
		[DllImport("__Internal")]
		private static extern bool IsAndroid();
#endif

		public bool CheckRunningOnAndroid()
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			return IsAndroid();
#else
			return false;
#endif
		}
	}
}
