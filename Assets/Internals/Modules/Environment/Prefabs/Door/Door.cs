using UnityEngine;
using UnityEngine.Events;

namespace BasicLegionInfected.Environment
{
	public class Door : MonoBehaviour, IAccesser
	{
		[SerializeField] private Collider2D _collider;

		public bool IsOpen { get => _collider.isTrigger; }

		public UnityEvent OnOpen = new();
		public UnityEvent OnClose = new();

		private string _initialTag;

		private void Awake()
		{
			_initialTag = gameObject.tag;
		}

		private void Start()
		{
			Close();
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
			_collider.isTrigger = false;

			gameObject.tag = _initialTag;

			OnClose.Invoke();
		}

		public void Receive(IAccesserUser user)
		{
			user.Move(transform.position);
		}
	}
}
