using UnityEngine;

using BasicLegionInfected.Core;
using BasicLegionInfected.Data.Catalogues;
using System;

namespace BasicLegionInfected.Animation
{
    public class ParticleManager : ASingleton<ParticleManager>
    {
        [SerializeField] private GameObjectCatalogue _particleCatalogue;

        public GameObject SpawnParticle(string particleCode, Vector3 position)
        {
            GameObject particlePrefab = _particleCatalogue.GetItem(particleCode);

            if (particlePrefab == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                GameObject particle = Instantiate(particlePrefab, position, Quaternion.identity, transform);

                return particle;
            }
        }

        public void Clear()
        {
            DetroyAllParticles();
        }

        public void DetroyAllParticles()
        {
            ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem particle in particles)
            {
                Destroy(particle.gameObject);
            }
        }
    }
}
