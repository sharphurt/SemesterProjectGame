using System.Collections.Generic;
using System.Linq;
using Modifiers;
using UnityEngine;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        public List<Modifier> modifiers = new List<Modifier>();

        public delegate void ModifierAddHandler(Modifier modifier);

        public event ModifierAddHandler OnModifierAdded;

        public delegate void ModifierRemoveHandler(Modifier modifier);

        public event ModifierRemoveHandler OnModifierRemoved;

        public void AddModifier(Modifier modifier)
        {
            if (modifiers.Any(m => m.FieldName == modifier.FieldName))
                modifiers.First(m => m.FieldName == modifier.FieldName).Reset();
            else
            {
                modifiers.Add(modifier);
                modifier.OnExpiration += RemoveExpiredModifiers;
                modifier.StartTimer();
                OnModifierAdded?.Invoke(modifier);
            }
        }

        private void RemoveExpiredModifiers()
        {
            var expiredModifiers = modifiers.Where(m => m.IsOver).ToList();
            foreach (var modifier in expiredModifiers)
            {
                modifier.UnsubscribeFromUpdateEvent();
                modifiers.Remove(modifier);
                OnModifierRemoved?.Invoke(modifier);
            }
        }
    }
}