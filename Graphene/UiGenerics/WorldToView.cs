using UnityEngine;

namespace Graphene.UiGenerics
{
    [RequireComponent(typeof(RectTransform))]
    public class WorldToView : MonoBehaviour
    {
        private RectTransform _rect;

        public Transform Target;
        private Camera _cam;

        private void Awake()
        {
            _rect = transform as RectTransform;
            _cam = Camera.main;
        }

        private void Update()
        {
            if(Target == null) return;

            var pos = _cam.WorldToScreenPoint(Target.position);

            _rect.position = pos;
        }
    }
}