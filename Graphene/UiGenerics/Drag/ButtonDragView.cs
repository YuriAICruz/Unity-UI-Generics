using UnityEngine;
using UnityEngine.UI;

namespace Graphene.UiGenerics.Drag
{
    [RequireComponent(typeof(Button))]
    public class ButtonDragView : Draggable
    {
        protected Button Button;
        void SetupInner()
        {
            Button = GetComponent<Button>();
            Button.onClick.AddListener(OnClick);
            SendMessage("Setup", SendMessageOptions.DontRequireReceiver);
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