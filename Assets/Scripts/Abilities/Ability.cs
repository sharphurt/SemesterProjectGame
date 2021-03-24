using System;
using System.Collections.Generic;
using System.Linq;
using Modifiers;
using UnityEngine;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        public List<Modifier> modifiers = new List<Modifier>();

        public delegate void ApplyModifierHandler();

        public event ApplyModifierHandler OnModifierChanged;

        public void AddModifier(Modifier modifier)
        {
            if (!modifiers.Contains(modifier))
                modifiers.Add(modifier);

            modifier.StartTimer();
            OnModifierChanged?.Invoke();
            modifier.OnExpiration += RemoveExpiredModifiers;
        }

        private void Update() => modifiers.ForEach(m => m.Update());

        private void RemoveExpiredModifiers()
        {
            if (modifiers.All(m => !m.IsOver))
                return;

            modifiers = modifiers.Where(m => !m.IsOver).ToList();
            OnModifierChanged?.Invoke();
        }
    }
}