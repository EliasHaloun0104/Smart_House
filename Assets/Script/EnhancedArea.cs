using System;
using Script.Devices;
using TMPro;
using TMPro.Examples;
using UnityEngine;

namespace Script
{
    public class EnhancedArea: MonoBehaviour
    {
        
        private TextMeshProUGUI _text;
        private VertexJitter _vertexJitter;
        [SerializeField] private BlinksManager blinkManager;
        [SerializeField] private EegReader eegReader;
        [SerializeField] private DeviceIndex deviceIndex;
        
        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _vertexJitter = GetComponent<VertexJitter>();
            var tempTxt = DeviceManager.DeviceText(deviceIndex);
            if(tempTxt!=null)
                _text.SetText(tempTxt);
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
                var state = DeviceManager.Get(deviceIndex);
                _text.SetText(state);
                DeviceManager.Put(deviceIndex, int.Parse(state));
            }
        }
    }
}