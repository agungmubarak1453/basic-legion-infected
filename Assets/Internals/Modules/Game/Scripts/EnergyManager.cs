using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace BasicLegionInfected.Game
{
    public class EnergyManager : MonoBehaviour
    {
		public static readonly List<EnergyManager> EnergyManagers = new();

		[field: SerializeField] public string EnergyId { get; private set; }

		private float _energy = 50;
		public float Energy
		{
			get => _energy;
			set
			{
				if (value < 0f)
				{
					_energy = 0f;
					return;
				}

				if (value > 100f)
				{
					_energy = 100f; return;
				}

				_energy = value;
			}
		}

		private void OnEnable()
		{
			EnergyManagers.Add(this);
		}

		private void OnDisable()
		{
			EnergyManagers.Remove(this);
		}

		public static EnergyManager GetEnergyManager(string energyId)
		{
			return EnergyManagers.Find(energyManager => energyManager.EnergyId.Equals(energyId));
		}
	}
}
