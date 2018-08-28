using UnityEngine;
using UnityEngine.UI;

namespace UiGenerics
{
    [RequireComponent(typeof(Image))]
    public class ImageView : MonoBehaviour
    {
        protected Image Image;
        
        void Awake()
        {
            Image = GetComponent<Image>();
            SendMessage("Setup");
        }
    }
}