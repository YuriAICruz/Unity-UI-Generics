using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Graphene.UiGenerics.Drag
{
    [RequireComponent(typeof(RectTransform))]
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, ICanvasRaycastFilter
    {
        protected Vector3 _iniPos, _lastPos;
        protected bool _isDragging;

        private void Awake()
        {
            _iniPos = transform.position;
            SendMessage("Setup", SendMessageOptions.DontRequireReceiver);
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(transform as RectTransform, eventData.position, eventData.pressEventCamera, out _lastPos);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            SetPosition(eventData);
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
        }

        protected void ResetPosition(Action onAnimationEnd)
        {
            StartCoroutine(TraslateBack(onAnimationEnd));
        }

        IEnumerator TraslateBack(Action onAnimationEnd)
        {
            var t = 0f;
            while (t < 0.4f)
            {
                transform.position = Vector3.Lerp(transform.position, _iniPos, t);
                t += Time.deltaTime;
                yield return null;
            }
            onAnimationEnd();
        }

        protected void SetPosition(PointerEventData eventData)
        {
            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform as RectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
            {
                transform.position = globalMousePos;
                //transform.rotation = m_DraggingPlanes[eventData.pointerId].rotation;
            }
        }

        public virtual bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            return !_isDragging;
        }
    }
}