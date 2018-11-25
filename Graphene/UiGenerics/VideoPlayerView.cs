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
        public string Url = "https://s3-sa-east-1.amazonaws.com/sgy1lz.cre9l6g2m5ngs1zwg3.ntls-2fbs9r6.kexurgnuv4g3zj-ez.nt9/videos/";
        
        public event Action OnPlay, OnStop, OnEnd;

        private System.Collections.Generic.Queue<Action> _queue = new System.Collections.Generic.Queue<Action>();

        private VideoLoadingText _infoText;

        public VideoUrlSourceManage _player;

        private void Awake()
        {
            _infoText = FindObjectOfType<VideoLoadingText>();
            
            _player.loopPointReached += LoopPointReached;
            _player.prepareCompleted += PrepareCompleted;
            _player.errorReceived += OnError;
            
            _player.Setup(GetComponent<VideoPlayer>());

            SendMessage("Setup", SendMessageOptions.DontRequireReceiver);
        }

        private void OnError(VideoPlayer arg1, string arg2)
        {
            _infoText.SetText("Não foi possível exibir o vídeo");
            StartCoroutine(CleanInfoText());
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

        public void Play(string clipName)
        {
            if (_player.IsPlaying())
            {
                Stop();
            }

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
            _infoText.SetText("Baixando o vídeo...");

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