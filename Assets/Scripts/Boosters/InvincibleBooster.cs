using Abilities;
using Entities;
using Items;
using Modifiers;
using UnityEngine;

namespace Boosters
{
    public class InvincibleBooster : Item
    {
        public uint duration;
        public uint value;
        
        public override void PickUp(Player picker)
        {
            if (picker.TryGetComponent<GettingDamageAbility>(out var ability))
                ability.AddModifier(new Modifier(value, duration, "Damage", gameObject.GetComponent<SpriteRenderer>()));
            else
                Debug.LogError($"Cannot apply modifier to {picker} - entity hasn't appropriate ability");
        }
    }
}