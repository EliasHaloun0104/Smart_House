using System;
using System.Collections;
using UnityEngine;

namespace Script
{
    public class Area : MonoBehaviour
    {
        
        [SerializeField] private EegReader eegReader;
        [SerializeField] private LampManager lamp;
        [SerializeField] private bool active;
        private SpriteRenderer _spriteRenderer;
        
        private Color _originalColor;
        private Color _alternativeColor;

        private void Start()
        {
            active = false;
            _originalColor = new Color(0,72,18,255);
            _alternativeColor = new Color(0,72,207,255);
            _spriteRenderer = GetComponent<SpriteRenderer>();

        }

        
        private void OnTriggerEnter2D(Collider2D other)
        {
            active = true;
            _spriteRenderer.color = _alternativeColor;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            active = false;
            _spriteRenderer.color = _originalColor;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (eegReader.EyeBlinking > 55)
            {
                lamp.ReverseStatus();
                eegReader.EyeBlinking = 0;
            }
        }

        //TODO if blink turn on light
        public IEnumerator BlinkDetector()
        {
            if (active)
            {
                lamp.ReverseStatus();
                yield return new WaitForSeconds(2);
                
            }
        }
        
    }
}
