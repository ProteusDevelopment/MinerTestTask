using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerAttack))]
    [RequireComponent(typeof(PlayerAnimation))]
    [RequireComponent(typeof(PlayerResources))]
    public class Player : MonoBehaviour
    {
        public PlayerMovement PlayerMovement
        {
            get
            {
                if (!_initialized)
                    Init();

                return _playerMovement;
            }
        }

        public PlayerAttack PlayerAttack
        {
            get
            {
                if (!_initialized)
                    Init();

                return _playerAttack;
            }
        }
        
        public PlayerAnimation PlayerAnimation
        {
            get
            {
                if (!_initialized)
                    Init();

                return _playerAnimation;
            }
        }
        
        public PlayerResources PlayerResources
        {
            get
            {
                if (!_initialized)
                    Init();

                return _playerResources;
            }
        }

        private PlayerMovement _playerMovement;
        private PlayerAttack _playerAttack;
        private PlayerAnimation _playerAnimation;
        private PlayerResources _playerResources;

        private bool _initialized = false;
        
        private void Init()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerAttack = GetComponent<PlayerAttack>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _playerResources = GetComponent<PlayerResources>();

            _initialized = true;
        }
    }
}
