using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UiGenerics
{
    [RequireComponent(typeof(InputField))]
    public class InputFieldView : MonoBehaviour, ISelectHandler
    {
        [HideInInspector]
        public InputField InputField;
        
        void Awake()
        {
            InputField = GetComponent<InputField>();
            InputField.onEndEdit.AddListener(EndEdit);
            InputField.onValueChanged.AddListener(ValueChanged);
            SendMessage("Setup");
        }

        protected virtual void EndEdit(string text)
        {
        }

        protected virtual void ValueChanged(string text)
        {
        }

        public virtual void OnSelect(BaseEventData eventData)
        {
        }
    }
}