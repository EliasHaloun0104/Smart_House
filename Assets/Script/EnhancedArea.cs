using System;
using TMPro;
using TMPro.Examples;
using UnityEngine;

namespace Script
{
    public class EnhancedArea: MonoBehaviour
    {
        enum Device
        {
            LightOne, Curtain, LightTwo, Temperature, Music
        }
        private TextMeshProUGUI _text;
        private VertexJitter _vertexJitter;
        [SerializeField] private Device device;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _vertexJitter = GetComponent<VertexJitter>();
            _text.SetText(device.ToString());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Enter");
            _vertexJitter.AngleMultiplier = 2;
            _vertexJitter.CurveScale = 4;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("Exit");
            _vertexJitter.AngleMultiplier = 0;
            _vertexJitter.CurveScale = 0;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            
        }
    }
}