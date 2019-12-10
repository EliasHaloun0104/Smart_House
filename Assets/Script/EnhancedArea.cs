using System;
using TMPro;
using TMPro.Examples;
using UnityEngine;

namespace Script
{
    public class EnhancedArea: MonoBehaviour
    {
        private enum Device
        {
            Relax, LightOne, Door, LightTwo, Temperature, Music
        }
        private TextMeshProUGUI _text;
        private VertexJitter _vertexJitter;
        [SerializeField] private BlinksManager blinkManager;
        [SerializeField] private EegReader eegReader;
        [SerializeField] private Device device;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _vertexJitter = GetComponent<VertexJitter>();
            //_text.SetText(device.ToString());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _vertexJitter.AngleMultiplier = 2;
            _vertexJitter.CurveScale = 4;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _vertexJitter.AngleMultiplier = 0;
            _vertexJitter.CurveScale = 0;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (blinkManager.IsOneBlink())
            {
                //Change Device status
            }
        }
    }
}