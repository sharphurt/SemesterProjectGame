using System;
using System.Collections.Generic;
using System.Linq;
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
                    ? PrimitiveModifyingFunctions.DivisionModifier(shootingPeriod, modifier.value)
                    : shootingPeriod;
            set => shootingPeriod = value;
        }
        
        public Projectile bullet;
        public float speed;
        public float startDelay;
        public bool isInfinityShooting;
        public uint shootsCount;
        
        private bool TryFindModifiersForField(string fieldName, out Modifier modifier)
        {
            if (modifiers.All(m => m.fieldName != fieldName))
            {
                modifier = null;
                return false;
            }

            modifier = modifiers.First(m => m.fieldName == fieldName);
            return true;
        }
    }
}