using System;
using System.Collections;
using System.Timers;
using UnityEngine;
using Utils;

namespace Modifiers
{
    public class Modifier
    {
        public readonly float Duration;
        public float Remained;
        public readonly float Value;
        public readonly string FieldName;
        public readonly Sprite Icon;

        public delegate void ModifierExpirationHandler();

        public event ModifierExpirationHandler OnExpiration;

        public delegate void ModifierUpdateHandler(Modifier modifier);

        public event ModifierUpdateHandler OnUpdate;

        private float timer;

        public Modifier(float value, float duration, string fieldName, SpriteRenderer spriteRenderer)
        {
            Duration = duration;
            Value = value;
            FieldName = fieldName;
            Icon = spriteRenderer.sprite;

            UpdateCaller.OnUpdate += Update;
        }

        public void StartTimer()
        {
            Remained = Duration;
            timer = 0;
        }

        public bool IsOver => Remained <= 0;

        private void Update()
        {
            if (IsOver)
                return;

            timer += Time.deltaTime;
            if (timer >= 1)
            {
                Remained--;
                timer -= 1;
                OnUpdate?.Invoke(this);
            }


            if (IsOver)
                OnExpiration?.Invoke();
        }

        public void Reset() => StartTimer();

        public void UnsubscribeFromUpdateEvent() => UpdateCaller.OnUpdate -= Update;
    }
}