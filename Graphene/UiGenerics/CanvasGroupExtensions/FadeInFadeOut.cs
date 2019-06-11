using System;
using System.Collections;
using UnityEngine;

namespace Graphene.UiGenerics.CanvasGroupExtensions
{
    public class FadeInFadeOut : CanvasGroupView
    {
        private Coroutine _tween;
        public float Duration;
        private bool _disabled;

        protected void OnEnable()
        {
            _disabled = false;
        }

        protected void OnDisable()
        {
            _disabled = true;
            KillCoroutine();
        }

        public override void Show()
        {
            if (!gameObject.activeInHierarchy || _disabled)
            {
                base.Show();
                return;
            }
            CheckCanvasGroup();
            KillCoroutine();
            _tween = StartCoroutine(Tween(x => CanvasGroup.alpha = Mathf.Max(CanvasGroup.alpha, x), Duration));
            Unblock();
        }

        public override void Hide()
        {
            if (!gameObject.activeInHierarchy || _disabled)
            {
                base.Hide();
                return;
            }
            CheckCanvasGroup();
            KillCoroutine();
            _tween = StartCoroutine(Tween(x => CanvasGroup.alpha = Mathf.Min(CanvasGroup.alpha, 1-x), Duration));
            Block();
        }

        private void KillCoroutine()
        {
            if (_tween != null)
            {
                StopCoroutine(_tween);
                _tween = null;
            }
        }

        IEnumerator Tween(Action<float> update, float duration)
        {
            var t = 0f;
            update?.Invoke(t);

            yield return null;

            while (t < duration)
            {
                t += Time.deltaTime;
                update?.Invoke(t / duration);
                yield return null;
            }

            t = 1f;

            update?.Invoke(t);
        }
    }
}