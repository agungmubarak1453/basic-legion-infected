using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicLegionInfected.Animation
{
    public class ParticleCollidingAnimation : MonoBehaviour
    {
        [SerializeField] private string _particleCode;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                ParticleManager.Instance.SpawnParticle(_particleCode, contactPoint.point);
            }
        }
    }
}
