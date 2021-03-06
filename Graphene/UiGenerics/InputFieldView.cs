﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Graphene.UiGenerics
{
    [RequireComponent(typeof(InputField))]
    public class InputFieldView : MonoBehaviour, ISelectHandler
    {
        public event Action<string> OnValueChanged;
        
        [HideInInspector]
        public InputField InputField;
        
        void Awake()
        {
            InputField = GetComponent<InputField>();
            InputField.onEndEdit.AddListener(EndEdit);
            InputField.onValueChanged.AddListener(ValueChanged);
            SendMessage("Setup", SendMessageOptions.DontRequireReceiver);
        }

        protected virtual void EndEdit(string text)
        {
        }

        protected virtual void ValueChanged(string text)
        {
            OnValueChanged?.Invoke(text);
        }

        public virtual void OnSelect(BaseEventData eventData)
        {
        }
    }
}