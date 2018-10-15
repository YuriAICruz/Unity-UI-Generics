﻿
using UnityEngine;
using UnityEngine.UI;

namespace Graphene.UiGenerics
{
    [RequireComponent(typeof(Text))]
    public class TextView : MonoBehaviour
    {
        protected Text Text;

        private void Awake()
        {
            Text = GetComponent<Text>();
            
            SendMessage("Setup", SendMessageOptions.DontRequireReceiver);
        }
    }
}