using System;
using UnityEngine;
using UnityEngine.Video;

namespace Graphene.UiGenerics
{
    [Serializable]
    public class VideoUrlSourceManage
    {
        private VideoPlayer _player;

        private int _selectedResolution, _currentResolution;

        public event Action<VideoPlayer, string> errorReceived;
        public event Action<VideoPlayer> loopPointReached, prepareCompleted;

        private string[] ResPaths = new string[]
        {
            "4k/",
            "FHD/",
            "HD/",
            "SD/"
        };

        private string _url;

        public void Setup(VideoPlayer player)
        {
            _player = player;

            _player.loopPointReached += LoopPointReached;
            _player.prepareCompleted += PrepareCompleted;
            _player.errorReceived += OnError;

            if (Screen.currentResolution.width > 1920)
            {
                _selectedResolution = 0;
            }
            else if (Screen.currentResolution.width > 1280)
            {
                _selectedResolution = 1;
            }
            else if (Screen.currentResolution.width > 1080)
            {
                _selectedResolution = 2;
            }
            else
            {
                _selectedResolution = 3;
            }

            _currentResolution = _selectedResolution;
        }

        private void PrepareCompleted(VideoPlayer source)
        {
            prepareCompleted?.Invoke(source);
        }

        private void LoopPointReached(VideoPlayer source)
        {
            loopPointReached?.Invoke(source);
        }

        private void OnError(VideoPlayer source, string message)
        {
            Debug.LogError(message);

            if (_currentResolution == _selectedResolution)
            {
                _currentResolution = _selectedResolution - 1;
            }
            
            errorReceived?.Invoke(source, message);
        }
        
        public void Play()
        {
            _player.Play();
        }
        
        public void Stop()
        {
            _player.Stop();
        }
        
        public void Prepare()
        {
            _player.Prepare();
        }
        
        
        public string GetResUrlPath()
        {
            return ResPaths[_currentResolution];
        }
        
        public bool IsPlaying()
        {
            return _player.isPlaying;
        }

        public void SetUrl(string url, string clipName)
        {
            _url = url;
            _player.url = _url + clipName;
        }

        public bool IsPrepared()
        {
            return _player.isPrepared;
        }
    }
}