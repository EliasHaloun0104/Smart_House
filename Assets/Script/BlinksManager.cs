using System;
using System.Collections;
using System.Diagnostics;
using Script.Devices;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Script
{
    public class BlinksManager: MonoBehaviour
    {
        
        [SerializeField] private int _blinkCounter;
        private Stopwatch _stopwatch;
        [SerializeField] private bool oneBlink;
        [SerializeField] private bool twoBlink;
        [SerializeField] private TextMeshProUGUI blinkInfo;
        private long _waitTime;

        private EnhancedArea _enhancedArea;

        public void EnterArea(EnhancedArea enhancedArea)
        {
            _enhancedArea = enhancedArea;
        }public void ExitArea()
        {
            _enhancedArea = null;
        }
        
        private IEnumerator Start()
        {
            _stopwatch = new Stopwatch();
            _waitTime = 1000L;
            oneBlink = false;
            twoBlink = false;
            while (true)
            {
                if (_blinkCounter == 1)
                {
                    blinkInfo.SetText("One blink detected...\nWaiting...");
                    _stopwatch.Start();
                    yield return new WaitUntil(()=> _stopwatch.ElapsedMilliseconds>_waitTime);
                    _stopwatch.Reset();
                    if (_blinkCounter == 2)
                    {
                        _blinkCounter = 0;
                        twoBlink = true;
                        yield return new WaitForSeconds(0.4f);
                        twoBlink = false;
                    }else if (_blinkCounter == 1)
                    {
                        _blinkCounter = 0;
                        oneBlink = true;
                        if (_enhancedArea!=null)
                        {
                            _enhancedArea.UpdateStatus();
                        }
                        blinkInfo.SetText("One Blink Confirmed");
                        yield return new WaitForSeconds(0.1f);
                        oneBlink = false;
                    }
                }
                else
                {
                    _blinkCounter = 0;
                    blinkInfo.SetText("No Blink detected..");
                }
                yield return new WaitForSeconds(0.2f);
            }
            
        }

        
        public void Add()
        {
            if (_blinkCounter < 2)
                _blinkCounter++;
        }

        public bool IsOneBlink()
        {
            if (oneBlink)
            {
                oneBlink = false;
                return true;
            }
            return false;
            
        }
        
        public bool IsTwoBlink()
        {
            if (!twoBlink) return false;
            twoBlink = false;
            _blinkCounter = 0;
            return true;
        }

        private void Update()
        {
            if (_stopwatch.IsRunning)
            {
                blinkInfo.SetText($"One blink detected...\nWaiting...{_waitTime - _stopwatch.ElapsedMilliseconds}");
            }
        }
    }
}