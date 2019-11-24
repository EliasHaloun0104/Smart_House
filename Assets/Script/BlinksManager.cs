using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Script
{
    public class BlinksManager: MonoBehaviour
    {
        
        [SerializeField] private int _blinkCounter;
        private Stopwatch _stopwatch;
        [SerializeField] private bool _oneBlink;
        [SerializeField] private bool _twoBlink;
        
        private IEnumerator Start()
        {
            _stopwatch = new Stopwatch();
            _oneBlink = false;
            _twoBlink = false;
            while (true)
            {
                if (_blinkCounter == 1)
                {
                    Debug.Log($"BlinkCounter = {_blinkCounter}");
                    _stopwatch.Start();
                    yield return new WaitUntil(()=> _stopwatch.ElapsedMilliseconds>1650L);
                    _stopwatch.Reset();
                    if (_blinkCounter == 2)
                    {
                        _blinkCounter = 0;
                        _twoBlink = true;
                        Debug.Log($"twoBlink detected");
                        yield return new WaitForSeconds(0.4f);
                        _twoBlink = false;
                    }else if (_blinkCounter == 1)
                    {
                        _blinkCounter = 0;
                        _oneBlink = true;
                        Debug.Log($"OneBlink detected");
                        yield return new WaitForSeconds(0.4f);
                        _oneBlink = false;
                    }
                }
                yield return new WaitForSeconds(0.3f);
            }
            
        }

        
        public void Add()
        {
            if (_blinkCounter < 2)
                _blinkCounter++;
        }

        public bool IsOneBlink()
        {
            if (!_oneBlink) return false;
            _oneBlink = false;
            return true;
        }
        
        public bool IsTwoBlink()
        {
            if (!_twoBlink) return false;
            _twoBlink = false;
            return true;
        }
    }
}