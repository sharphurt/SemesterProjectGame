using Modifiers;

namespace Abilities
{
    public class GettingDamageAbility : Ability
    {
        private float damage;

        public float Damage
        {
            get => TryFindModifiersForField(nameof(Damage), out var modifier)
                    ? PrimitiveModifyingFunctions.DivisionModifier(damage, modifier.Value)
                    : damage;
            set => damage = value;
        }
    }
}