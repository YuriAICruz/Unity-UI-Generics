using UnityEngine;
using UnityEngine.UI;

namespace UiGenerics
{
    [RequireComponent(typeof(Button))]
    public class ButtonView : MonoBehaviour
    {
        protected Button Button;
        void Awake()
        {
            Button = GetComponent<Button>();
            Button.onClick.AddListener(OnClick);
            SendMessage("Setup");
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