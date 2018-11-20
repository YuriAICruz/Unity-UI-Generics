using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

namespace Graphene.UiGenerics
{
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoPlayerView : MonoBehaviour
    {
        private VideoPlayer _player;

        public event Action OnPlay, OnStop, OnEnd;

        private Queue<Action> _queue = new Queue<Action>();

        private void Awake()
        {
            _player = GetComponent<VideoPlayer>();

            SendMessage("Setup", SendMessageOptions.DontRequireReceiver);

            _player.loopPointReached += LoopPointReached;
            _player.prepareCompleted += PrepareCompleted;
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

        public void Play(VideoClip clip)
        {
            if (_player.isPlaying)
            {
                Stop();
            }

            _player.clip = clip;

            Play();
        }

        public void Stop()
        {
            if (!_player.isPlaying) return;

            OnStop?.Invoke();
            _player.Stop();
        }
    }
}