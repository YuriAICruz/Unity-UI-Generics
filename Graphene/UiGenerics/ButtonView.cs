using UnityEngine;
using UnityEngine.UI;

namespace Graphene.UiGenerics
{
    [RequireComponent(typeof(Button))]
    public class ButtonView : MonoBehaviour
    {
        protected Button Button;
        void Awake()
        {
            Button = GetComponent<Button>();
            Button.onClick.AddListener(OnClick);
            SendMessage("Setup", SendMessageOptions.DontRequireReceiver);
        }

        protected virtual void OnClick()
        {
            Disable();
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