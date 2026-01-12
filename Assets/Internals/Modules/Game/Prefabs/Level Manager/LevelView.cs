using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using BasicLegionInfected.Animation;
using System.Threading.Tasks;

namespace BasicLegionInfected.Game
{
	public class LevelView : MonoBehaviour
	{
		[Header("Component")]
		[SerializeField] private LevelManager _levelManager;
		[SerializeField] private AAnimation _environmentAnimation;

		private bool _isAnimationFinished;

		private void Awake()
		{
			SetEvent();
		}

		private void SetEvent()
		{
			_levelManager.OnLoadEnvironmentAsyncEvent.Add(OnLevelManagerLoadEnvironment);
			_environmentAnimation.OnAnimationFinished.AddListener(OnEnvironmentAnimationFinish);
		}

		private async Task OnLevelManagerLoadEnvironment()
		{
			_isAnimationFinished = false;
			_environmentAnimation.StartAnimation();

			int checkingIntervalSecond = 1;

			while (!_isAnimationFinished)
			{
				await Task.Delay(checkingIntervalSecond * 1000);
			}
		}

		private void OnEnvironmentAnimationFinish()
		{
			Debug.Log("_environmentAnimation finished");
			_isAnimationFinished = true;
		}
	}
}
