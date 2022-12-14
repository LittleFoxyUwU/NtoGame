using System;
using System.Collections;
using EditorScripts.Inspector;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;

namespace GameScripts.Logic.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public Action OnPlayerDeath;

        public Action<float> OnPlayerMaxHealthChange;
        [SerializeReadOnly, SerializeField] private float _maxHealth;
        public float MaxHealth
        {
            get => _maxHealth;
            set 
            {
                _maxHealth = value;
                if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
                OnPlayerMaxHealthChange?.Invoke(_maxHealth);
            }
        }
        
        public Action<float> OnPlayerHealthChange;
        [SerializeReadOnly, SerializeField] private float _currentHealth;
        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value >= _maxHealth ? _maxHealth : value;
                _currentHealth = value <= 0 ? 0 : value;
                OnPlayerHealthChange?.Invoke(_currentHealth);
            }
        }
        
        private float _healthRegenPerSecond;

        public void SetProperties(PlayerData playerData)
        {
            MaxHealth = playerData.health.maxHealth;
            CurrentHealth = MaxHealth;
            _healthRegenPerSecond = playerData.health.healthRegenPerSecond;
        }
        
        private void OnEnable()
        {
            StartCoroutine(RegenerationCoroutine());
        }

        private bool _isDead;
        public void GetDamage(float damage)
        {
            if (_isDead) return;
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                _isDead = true;
                OnPlayerDeath?.Invoke();
            }
        }

        private IEnumerator RegenerationCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                CurrentHealth += _healthRegenPerSecond;
            }
        }

        #if UNITY_EDITOR
        [InspectorButton("OnHealButton")]
        [SerializeField] private bool healButton;

        private void OnHealButton() => CurrentHealth = _maxHealth;
        #endif
    }
}