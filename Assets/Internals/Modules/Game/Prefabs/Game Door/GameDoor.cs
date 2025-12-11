using BasicLegionInfected.Game;
using UnityEngine;
using UnityEngine.Events;

namespace BasicLegionInfected.Environment
{
	public class GameDoor : MonoBehaviour, IAccesser
	{
		[SerializeField] private Collider2D _collider;
		[field: SerializeField] public EnergyUser EnergyUser { get; private set; }

		public bool IsOpen { get => _collider.isTrigger; }

		public UnityEvent OnOpen = new();
		public UnityEvent OnClose = new();

		public float CloseEnergy = 20f;
		public float CloseDuration = 2f;

		public float CloseTimer { get; private set; }

		private string _initialTag;

		private void Awake()
		{
			_initialTag = gameObject.tag;
		}

		private void Update()
		{
			if (!IsOpen)
			{
				CloseTimer -= Time.deltaTime;

				if (CloseTimer < 0f) Open();
			}
		}

		public void Switch()
		{
			if (IsOpen)
			{
				Close();
			}
			else
			{
				Open();
			}
		}

		public void Open()
		{
			_collider.isTrigger = true;

			gameObject.tag = "Accesser";

			OnOpen.Invoke();
		}

		public void Close()
		{
			if (!EnergyUser.TryUseEnergy(CloseEnergy))
			{
				return;
			}

			_collider.isTrigger = false;

			gameObject.tag = _initialTag;

			CloseTimer = CloseDuration;

			OnClose.Invoke();
		}

		public void Receive(IAccesserUser user)
		{
			user.Move(transform.position);
		}
	}
}
