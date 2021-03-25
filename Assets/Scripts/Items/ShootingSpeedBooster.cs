using Abilities;
using Entities;
using Modifiers;
using UnityEngine;

namespace Items
{
    public class ShootingSpeedBooster : Item
    {
        public override void PickUp(Player picker)
        {
            if (picker.TryGetComponent<ShootingAbility>(out var component))
                component.AddModifier(new Modifier(7, 10, "ShootingPeriod", gameObject.GetComponent<SpriteRenderer>()));
            else
                Debug.LogError($"Cannot apply modifier to {picker} - entity hasn't appropriate ability");
        }
    }
}