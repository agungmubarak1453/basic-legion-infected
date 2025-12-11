using UnityEngine;

using BasicLegionInfected.Input;
using BasicLegionInfected.Environment;
using BasicLegionInfected.Utility;

namespace BasicLegionInfected.Game
{
    public class GameDoorView : MonoBehaviour
    {
		[SerializeField] private CrossPlatformInputEventHelper _crossPlatformInputEventHelper;

		[SerializeField] private SpriteRenderer _doorSprite;
		private Color _initialDoorColor;
		[SerializeField] private Color _beforeClosedColor;
		[SerializeField] private float _notificationBeforeCloseColorTimeSecond = 2f;
		[SerializeField] private GameObject _enoughEnergyVisualizer;

		private EnergyManager _doorEnergyManager;

		[field: SerializeField] public GameDoor GameDoor { get; private set; }

		private void Awake()
		{
			_initialDoorColor = _doorSprite.color;

			_crossPlatformInputEventHelper.OnClicked.AddListener(GameDoor.Close);

			GameDoor.OnOpen.AddListener(OnDoorOpen);
			GameDoor.OnClose.AddListener(OnDoorClose);
		}

		private void Start()
		{
			_doorEnergyManager = EnergyManager.GetEnergyManager(GameDoor.EnergyUser.EnergyId);
		}

		private void Update()
		{
			_enoughEnergyVisualizer.SetActive(GameDoor.IsOpen && _doorEnergyManager.Energy >= GameDoor.CloseEnergy);

			if (!GameDoor.IsOpen && Rounder.IsNearFloat(GameDoor.CloseTimer, _notificationBeforeCloseColorTimeSecond, 0.5f))
			{
				_doorSprite.color = _beforeClosedColor;
			}
		}

		public void OnDoorOpen()
		{
			_doorSprite.color = _initialDoorColor;
			_doorSprite.gameObject.SetActive(false);
		}

		public void OnDoorClose()
		{
			_doorSprite.gameObject.SetActive(true);
		}
	}
}
