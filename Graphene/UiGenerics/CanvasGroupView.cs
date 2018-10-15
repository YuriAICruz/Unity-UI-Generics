﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace Graphene.UiGenerics
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupView : MonoBehaviour
    {
        protected CanvasGroup CanvasGroup;

        void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
            SendMessage("Setup", SendMessageOptions.DontRequireReceiver);
        }

        public void Show()
        {
            CanvasGroup.alpha = 1;
            Unblock();
        }

        public void Hide()
        {
            CanvasGroup.alpha = 0;
            Block();
        }

        public void Block()
        {
            CanvasGroup.blocksRaycasts = false;
            CanvasGroup.interactable = false;
        }

        public void Unblock()
        {
            CanvasGroup.blocksRaycasts = true;
            CanvasGroup.interactable = true;
        }
    }
}