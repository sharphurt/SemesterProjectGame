using Entities;
using Items;
using UnityEngine;

namespace Boosters
{
    public class InstantHealthBooster : Item
    {
        public float value;
        
        public override void PickUp(Player picker)
        {
            picker.health = Mathf.Clamp(picker.health + picker.maxHealth * value, 0, picker.maxHealth);
        }
    }
}
