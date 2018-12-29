using UnityEngine;
using UnityEngine.UI;

namespace Graphene.UiGenerics
{
    [RequireComponent(typeof(Image))]
    public class ImageView : MonoBehaviour
    {
        [HideInInspector]
        public Image Image;
        
        void Awake()
        {
            Image = GetComponent<Image>();
            SendMessage("Setup", SendMessageOptions.DontRequireReceiver);
        }
    }
}