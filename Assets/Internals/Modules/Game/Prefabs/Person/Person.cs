using UnityEngine;

using BasicLegionInfected.Environment;

namespace BasicLegionInfected.Game
{
	public class Person : MonoBehaviour, IAccesserUser
	{
		[SerializeField] private Rigidbody2D _rigidbody;

		public float Speed = 5f;
		public float ChangeDirectionIntervalSecond = 2f;

		private Vector2 _randomDirection;
		private float _directionTimer = 0f;

		private float _detectionRadius = 2f;
		private float _detectionIntervalSecond = 5f;

		private bool _canDetect = false;
		private float _detectionTimer = 0f;
		private Collider2D _latestAccesser = null;

		private void Start()
		{
			ChangeDirection();
		}

		private void Update()
		{
			_directionTimer += Time.deltaTime;
			if (_directionTimer >= ChangeDirectionIntervalSecond)
			{
				ChangeDirection();
				_directionTimer = 0f;
			}

			_detectionTimer += Time.deltaTime;
			if (_detectionTimer >= _detectionIntervalSecond)
			{
				_canDetect = true;
				_detectionTimer = 0f;
			}
		}

		private void FixedUpdate()
		{
			_rigidbody.velocity = _randomDirection * Speed;

			if (!_canDetect) return;

			RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _detectionRadius, Vector2.down);

			foreach (RaycastHit2D hit in hits)
			{
				if (hit.collider.CompareTag("Accesser"))
				{
					IAccesser accesser = hit.collider.GetComponentInChildren<IAccesser>();

					if (accesser.IsOpen)
					{

						accesser.Receive(this);
						_latestAccesser = hit.collider;

						_canDetect = false;
					}

					break;
				}
			}
		}

		public void ChangeDirection()
		{
			float angle = Random.Range(0f, 360f);
			_randomDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
		}

		public void Move(Vector3 newPosition)
		{
			transform.position = newPosition;
		}
	}
}
