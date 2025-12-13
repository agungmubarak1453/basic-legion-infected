using UnityEngine;
using UnityEngine.Events;

using BasicLegionInfected.Input;

namespace BasicLegionInfected.Game
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject _curerPrefab;

        [field: SerializeField] public EnergyManager EnergyManager { get; private set; }
        [SerializeField] private EnergyUser _energyUser;

        public float EnergyRecoverySpeed = 2f;

		public float CureRadius = 3f;
        public float CureEnergy = 20f;

        [field: SerializeField] public UnityEvent OnClear { get; private set; } = new();

		private void Update()
		{
            EnergyManager.Energy += EnergyRecoverySpeed * Time.deltaTime;
		}

		public void Cure(Vector3 position)
        {
            if (!_energyUser.TryUseEnergy(CureEnergy))
            {
                return;
            }

            GameObject gameObject = GameObject.Instantiate(
                _curerPrefab, position, Quaternion.identity, transform
            );

            Curer curer = gameObject.GetComponentInChildren<Curer>();
            curer.CureRadius = CureRadius;
        }

        public void Clear()
        {
            Curer[] curers = GetComponentsInChildren<Curer>();

			foreach (Curer curer in curers)
            {
                DestroyImmediate(curer.gameObject);
            }

            EnergyManager.Energy = 100f;

            InputManager.Instance.IsInputBlocked = false;

            OnClear.Invoke();
        }
    }
}
