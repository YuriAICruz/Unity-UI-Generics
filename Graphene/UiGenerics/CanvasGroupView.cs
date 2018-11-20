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
            CheckCanvasGroup();
            SendMessage("Setup", SendMessageOptions.DontRequireReceiver);
        }

        protected void CheckCanvasGroup()
        {
            if(CanvasGroup == null)
                CanvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Show()
        {
            CheckCanvasGroup();
            CanvasGroup.alpha = 1;
            Unblock();
        }

        public virtual void Hide()
        {
            CheckCanvasGroup();
            CanvasGroup.alpha = 0;
            Block();
        }

        public void Block()
        {
            CheckCanvasGroup();
            CanvasGroup.blocksRaycasts = false;
            CanvasGroup.interactable = false;
        }

        public void Unblock()
        {
            CheckCanvasGroup();
            CanvasGroup.blocksRaycasts = true;
            CanvasGroup.interactable = true;
        }
    }
}