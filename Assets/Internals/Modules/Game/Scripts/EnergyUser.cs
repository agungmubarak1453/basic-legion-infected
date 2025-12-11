using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicLegionInfected.Game
{
    public class EnergyUser : MonoBehaviour
    {
		[field: SerializeField] public string EnergyId { get; private set; }

		public bool TryUseEnergy(float neededEnergy)
		{
			if (IsEnergyEnough(neededEnergy))
			{
				UseEnergy(neededEnergy);

				return true;
			}
			else
			{
				return false;
			}
		}

		public void UseEnergy(float neededEnergy)
		{
			EnergyManager.GetEnergyManager(EnergyId).Energy -= neededEnergy;
		}

		public bool IsEnergyEnough(float neededEnergy)
		{
			return EnergyManager.GetEnergyManager(EnergyId).Energy >= neededEnergy;
		}
	}
}
