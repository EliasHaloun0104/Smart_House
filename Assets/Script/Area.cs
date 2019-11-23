using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Script
{
    public class Area : MonoBehaviour
    {
        
        [SerializeField] private EegReader eegReader;
        [SerializeField] private LampManager lamp;
        [SerializeField] private bool active;

        private Stopwatch _stopwatch;
        
        private SpriteRenderer _spriteRenderer;
        
        private Color _originalColor;
        private Color _alternativeColor;

        private IEnumerator Start()
        {
            _stopwatch = new Stopwatch();
            active = false;
            _originalColor = new Color(0,72,18,255);
            _alternativeColor = new Color(0,72,207,255);
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _stopwatch.Start();
            yield return new WaitForSeconds(2);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            eegReader.EyeBlinking = 0;
            active = true;
            _spriteRenderer.color = _alternativeColor;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            eegReader.EyeBlinking = 0;
            active = false;
            _spriteRenderer.color = _originalColor;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (eegReader.EyeBlinking > 55)
            {
                if (_stopwatch.IsRunning)
                {
                    eegReader.EyeBlinking = 0;
                }
                else
                {
                     _stopwatch.Start();
                     lamp.UpdateLamp();
                     eegReader.EyeBlinking = 0;
                }
                
            }
            if(_stopwatch.IsRunning && _stopwatch.ElapsedMilliseconds> 1500l)
                _stopwatch.Reset();
        }
    }
}
