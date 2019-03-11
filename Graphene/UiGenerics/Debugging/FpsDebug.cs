using Graphene.UiGenerics;
using UnityEngine;

namespace Graphene.UiGenerics.Debugging
{
    public class FpsDebug : TextView
    {
        public string FormatedString = "{value} FPS";

        public float UpdateRateSeconds = 4.0F;

        private int _frameCount = 0;
        private float _dt = 0.0F;
        private float _fps = 0.0F;

        private void Update()
        {
            _frameCount++;
            _dt += Time.unscaledDeltaTime;
            if (_dt > 1.0 / UpdateRateSeconds)
            {
                _fps = _frameCount / _dt;
                _frameCount = 0;
                _dt -= 1.0F / UpdateRateSeconds;
            }
            Text.text = FormatedString.Replace("{value}", System.Math.Round(_fps, 1).ToString("0.0"));
        }
    }
}