using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Graphene.UiGenerics
{
    public class DropdownButtonView : ButtonView
    {
        public GameObject Dropdown;

        public LayoutElement LayoutElement;

        public Image Feedback;
        public Sprite Opened, Closed;

        public float Duration = 0.4f;
        
        private Coroutine _anim;
        
        public event Action OnShow, OnHide;
        
        private bool _show;
        private float _maxHeight;
        private float _startHeight;

        private void Start()
        {
            _startHeight = LayoutElement.minHeight;
            _maxHeight = _startHeight + Dropdown.GetComponent<RectTransform>().rect.height;
        }

        public void Show()
        {
            _show = true;
            if (_anim != null)
            {
                return;
            }

            _anim = StartCoroutine(Animate(LayoutElement.minHeight, _maxHeight, Duration, OnShow));
        }

        private void Update()
        {
			if (Input.GetKeyDown(KeyCode.Escape))
			{
                Hide();
			}
        }

        public void Hide()
        {
            _show = false;
            if (_anim != null)
            {
                return;
            }
            
            _anim = StartCoroutine(Animate(LayoutElement.minHeight, _startHeight, Duration, OnHide));
        }

        IEnumerator Animate(float from, float to, float duration, Action action)
        {
            var t = 0f;
            
            action?.Invoke();

            LayoutElement.minHeight = Mathf.Lerp(from, to, 0);
            while (t < duration)
            {
                t += Time.deltaTime;
                
                LayoutElement.minHeight = Mathf.Lerp(from, to, t/duration);
                
                yield return null;
            }
            LayoutElement.minHeight = Mathf.Lerp(from, to, 1);

            yield return null;


            Feedback.sprite = _show ? Closed : Opened;
            
            _anim = null;
        }

        protected override void OnClick()
        {
            base.OnClick();

            _show = !_show;

            if (_show)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
    }
}