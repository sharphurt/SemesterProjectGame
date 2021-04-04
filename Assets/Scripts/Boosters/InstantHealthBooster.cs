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
            picker.Heal(picker.maxHealth * value);
        }
    }
}
