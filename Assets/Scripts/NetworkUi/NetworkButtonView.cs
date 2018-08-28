using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace UiGenerics.NetworkUi
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(NetworkIdentity))]
    public class NetworkButtonView : NetworkBehaviour
    {
        protected Button Button;

        public NetworkInteractionType Interaction;
        
        void Awake()
        {
            Button = GetComponent<Button>();
            Button.onClick.AddListener(OnClick);
            
            SendMessage("Setup");
            
            CheckToDisable();
        }

        private void CheckToDisable()
        {
            switch (Interaction)
            {
                case NetworkInteractionType.OnlyServer:
                    if (!NetworkServer.active) gameObject.SetActive(false);
                    break;
                case NetworkInteractionType.OnlyPlayer:
                    if (!isLocalPlayer) gameObject.SetActive(false);
                    break;
                case NetworkInteractionType.All:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual void OnClick()
        {
            Invoke("Enable", 0.8f);
        }

        protected void Disable()
        {
            Button.interactable = false;
        }

        protected void Enable()
        {
            Button.interactable = true;
        }
    }
}