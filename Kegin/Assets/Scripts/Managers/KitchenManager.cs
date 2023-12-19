using System;
using Cinemachine;
using UnityEngine;

namespace Managers
{
    public enum KitchenStates
    {
        Kitchen,
        Counter
    }
    
    public class KitchenManager : MonoBehaviour
    {
        [Header("Real camera")]
        [SerializeField] private CinemachineBrain _cameraBrain;
        
        [Header("Virtual cameras")]
        [SerializeField] private CinemachineVirtualCamera _kitchenCamera;
        [SerializeField] private CinemachineVirtualCamera _counterCamera;

        [Header("Default state")]
        [SerializeField] KitchenStates _kitchenState = KitchenStates.Counter;
        
        [Header("UI")]
        [SerializeField] private GameObject _toKitchenUI;
        [SerializeField] private GameObject _toCounterUI;

        private void Start()
        {
            if(_kitchenState == KitchenStates.Counter) SetCounterState();
            else SetKitchenState();
        }

        public void SetCounterState()
        {
            _kitchenState = KitchenStates.Counter;
            
            _kitchenCamera.Priority = 0;
            _counterCamera.Priority = 10;
            
            _toKitchenUI.SetActive(true);
            _toCounterUI.SetActive(false);
        }

        public void SetKitchenState()
        {
            _kitchenState = KitchenStates.Kitchen;
            
            _counterCamera.Priority = 0;
            _kitchenCamera.Priority = 10;
            
            _toCounterUI.SetActive(true);
            _toKitchenUI.SetActive(false);
        }
    }
}
