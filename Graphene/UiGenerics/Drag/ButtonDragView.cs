using UnityEngine;
using UnityEngine.UI;

namespace Graphene.UiGenerics.Drag
{
    [RequireComponent(typeof(Button))]
    public class ButtonDragView : Draggable
    {
        protected Button Button;
        void Setup()
        {
            Button = GetComponent<Button>();
            Button.onClick.AddListener(OnClick);
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