using System;
using System.Collections;
using System.Linq;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Graphene.UiGenerics
{
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoPlayerView : MonoBehaviour
    {
        public static bool fromURL = true;
        public bool useURL = true;
    
        public string Url = "https://s3-sa-east-1.amazonaws.com/sgy1lz.cre9l6g2m5ngs1zwg3.ntls-2fbs9r6.kexurgnuv4g3zj-ez.nt9/videos/";
        
        public event Action OnPlay, OnStop, OnEnd;

        private System.Collections.Generic.Queue<Action> _queue = new System.Collections.Generic.Queue<Action>();

        private VideoLoadingText _infoText;

        public VideoUrlSourceManage _player;
        private string _clipName;
        private VideoClip _clip;

        private bool isPT;

        private void Awake()
        {
            _infoText = FindObjectOfType<VideoLoadingText>();
            
            _player.loopPointReached += LoopPointReached;
            _player.prepareCompleted += PrepareCompleted;
            _player.errorReceived += OnError;
            
            _player.Setup(GetComponent<VideoPlayer>());

            fromURL = useURL;
            Debug.Log("Getting videos from URL: " + fromURL);

            SendMessage("Setup", SendMessageOptions.DontRequireReceiver);

            isPT = PlayerPrefs.GetString("language", "PT") == "PT";
            Debug.Log("isPT: " + isPT);
        }

        private void OnError(VideoPlayer arg1, string arg2)
        {
            string msg = isPT ? "Não foi possível exibir o vídeo" : "No es posible ver el video";

            _infoText.SetText(msg);

            StartCoroutine(CleanInfoText());
            
            if (fromURL)
            {
                Play(_clipName);
            }
            else
            {
                Play(_clip);
            }
        }

        private void PrepareCompleted(VideoPlayer source)
        {
            for (int i = 0, n = _queue.Count; i < n; i++)
            {
                _queue.Dequeue()();
            }
        }

        private void LoopPointReached(VideoPlayer source)
        {
            OnEnd?.Invoke();
        }

        public void Play()
        {
            if (_player.IsPlaying()) return;
            OnPlay?.Invoke();
            _player.Play();
        }

        public void Play(VideoClip clip)
        {
            if (fromURL)
            {
                Debug.Log("Playing from embedded VideoClip is disabled, check bool useURL in VideoPlayerView");
                return;
            }

            if (_player.IsPlaying())
            {
                Stop();
            }
             _player.SetClip(clip);
             Play();
        }

        public void Play(string clipName)
        {
            if (!fromURL)
            {
                Debug.Log("Playing from URL is disabled");
                return;
            }

            if (_player.IsPlaying())
            {
                Stop();
            }

            _clipName = clipName;
            _player.SetUrl(Url + _player.GetResUrlPath(), clipName);
            StartCoroutine(PrepareVideo());

            Play();
        }

        public void Stop()
        {
            if (!_player.IsPlaying()) return;

            OnStop?.Invoke();
            _player.Stop();
        }

        protected IEnumerator PrepareVideo()
        {
            _player.Prepare();

            if (fromURL)
            {
                string msg = isPT ? "Baixando o vídeo..." : "Descargando el vídeo...";
               _infoText.SetText(msg);
            }

            while (!_player.IsPrepared())
            {
                yield return null;
            }

            _infoText.SetText("");
        }

        protected IEnumerator CleanInfoText()
        {
            yield return new WaitForSeconds(3);
            _infoText.SetText("");
        }
    }
}