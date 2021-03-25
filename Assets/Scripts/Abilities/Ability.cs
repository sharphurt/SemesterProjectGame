using System;
using System.Collections;
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
            if (modifiers.Any(m => m.FieldName == modifier.FieldName))
            {
                modifiers.First(m => m.FieldName == modifier.FieldName).Reset();
            }
            else
            {
                modifiers.Add(modifier);
                modifier.OnExpiration += RemoveExpiredModifiers;
                modifier.StartTimer();
                OnModifierChanged?.Invoke();
            }
        }

        //private void Start() => StartCoroutine(UpdateModifiers());

        /*private IEnumerator UpdateModifiers()
        {
            while (true)
            {
                modifiers.ForEach(m => m.Update());
                yield return new WaitForSeconds(0.1f);
            }
        }*/

        private void Update()
        {
          //  modifiers.ForEach(m => m.Update());
        }

        private void RemoveExpiredModifiers()
        {
            Debug.Log("Expired");
            if (modifiers.All(m => !m.IsOver))
                return;

            modifiers = modifiers.Where(m => !m.IsOver).ToList();
            OnModifierChanged?.Invoke();
        }
    }
}