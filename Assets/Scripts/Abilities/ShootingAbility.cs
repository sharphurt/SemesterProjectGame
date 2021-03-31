using Entities;
using Modifiers;
using UnityEngine;

namespace Abilities
{
    public class ShootingAbility : Ability
    {
        [SerializeField]
        private float shootingPeriod;

        public float ShootingPeriod
        {
            get => TryFindModifiersForField(nameof(ShootingPeriod), out var modifier)
                    ? PrimitiveModifyingFunctions.DivisionModifier(shootingPeriod, modifier.Value)
                    : shootingPeriod;
            set => shootingPeriod = value;
        }
        
        public Projectile bullet;
        public float speed;
        public float startDelay;
        public bool isInfinityShooting;
        public uint shootsCount;

    }
}