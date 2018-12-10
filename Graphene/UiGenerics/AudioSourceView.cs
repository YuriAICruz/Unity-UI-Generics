using UnityEngine;

namespace Graphene.UiGenerics
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceView : MonoBehaviour
    {
        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            SendMessage("Setup", SendMessageOptions.DontRequireReceiver);
        }

        public void Play()
        {
            _source.Play();
        }
        public void Play(AudioClip clip)
        {
            _source.clip = clip;
            _source.Play();
        }
        

        public void Stop()
        {
            _source.Stop();
        }
    }
}