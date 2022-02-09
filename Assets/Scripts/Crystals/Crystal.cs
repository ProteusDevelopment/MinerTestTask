using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Crystals
{
    public class Crystal : MonoBehaviour
    {
        [SerializeField] private GameObject _fullModel;
        [SerializeField] private GameObject _halfModel;
        [SerializeField] private GameObject _brokenModel;

        private int _health = 9;

        public void Damage(int amount)
        {
            if (_health == 1)
            {

                return;
            }
            
            _health -= amount;
            UpdateModel();

            if (_health < 7)
            {
                // Recovery
            }
        }

        private void UpdateModel()
        {
            switch (_health)
            {
                case 6:
                    _fullModel.SetActive(false);
                    _halfModel.SetActive(true);
                    break;
                case 3:
                    _halfModel.SetActive(true);
                    _brokenModel.SetActive(true);
                    break;
            }
        }
    }
}
