using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Graphene.UiGenerics
{
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoPlayerView : MonoBehaviour
    {
        private VideoPlayer _player;

        public event Action OnPlay, OnStop, OnEnd;

        private System.Collections.Generic.Queue<Action> _queue = new System.Collections.Generic.Queue<Action>();

        private Text _infoText;
        private int _selectedResolution;

        public string Url = "https://s3-sa-east-1.amazonaws.com/sgy1lz.cre9l6g2m5ngs1zwg3.ntls-2fbs9r6.kexurgnuv4g3zj-ez.nt9/videos/";

        private string[] ResPaths = new string[]
        {
            "4k/",
            "FHD/",
            "HD/",
            "SD/"
        };

        private void Awake()
        {
            _player = GetComponent<VideoPlayer>();

            _infoText = GameObject.Find("VideoLoadingText").GetComponent<Text>();

            SendMessage("Setup", SendMessageOptions.DontRequireReceiver);

            _player.loopPointReached += LoopPointReached;
            _player.prepareCompleted += PrepareCompleted;

            if (Screen.currentResolution.width > 1920)
            {
                _selectedResolution = 0;
            }
            else if (Screen.currentResolution.width > 1280)
            {
                _selectedResolution = 2;
            }
            else if (Screen.currentResolution.width > 1080)
            {
                _selectedResolution = 3;
            }
            else
            {
                _selectedResolution = 3;
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
            if (_player.isPlaying) return;
            OnPlay?.Invoke();
            _player.Play();
        }

        public void Play(string clipName)
        {
            if (_player.isPlaying)
            {
                Stop();
            }

            _player.url = GetVideoURLPath() + clipName;
            _player.errorReceived += ErrorReceived;
            StartCoroutine(PrepareVideo());

            Play();
        }

        protected string GetVideoURLPath()
        {
            Url += ResPaths[_selectedResolution];
            return Url;
        }

        public void Stop()
        {
            if (!_player.isPlaying) return;

            OnStop?.Invoke();
            _player.Stop();
        }

        protected IEnumerator PrepareVideo()
        {
            _player.Prepare();
            _infoText.text = "Baixando o vídeo...";

            while (!_player.isPrepared)
            {
                yield return null;
            }

            _infoText.text = "";
        }

        protected void ErrorReceived (VideoPlayer source, string msg)
        {
            _infoText.text = "Não foi possível exibir o vídeo";
            _player.errorReceived -= ErrorReceived;
            StartCoroutine(CleanInfoText());
        }

        protected IEnumerator CleanInfoText()
        {
            yield return new WaitForSeconds(3);
            _infoText.text = "";
        }
    }
}