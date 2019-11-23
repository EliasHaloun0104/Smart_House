using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Script
{
    public class EegReader : MonoBehaviour
    {
        [SerializeField] private Slider sliderVer;
        [SerializeField] private Slider sliderHor;
        
        //The output data
        [SerializeField] private int attention;
        [SerializeField] private int meditation;
        [SerializeField] private int eyeBlinking;
        
        //Internal instance for this class
        private TcpClient _client;
        private Stream _stream;
        private byte[] _buffer;
        private int _bytesRead;
        // Building command to enable JSON output from ThinkGear Connector (TGC)
        private byte[] _myWriteBuffer;

        // Start is called before the first frame update

        private void Awake()
        {
            StartCoroutine(Reading());
        }
        
        //This method used to communicate w the real EEG Connecter
        private void Connect()
        {
            _buffer = new byte[2048];
            // Building command to enable JSON output from ThinkGear Connector (TGC)
            _myWriteBuffer = Encoding.ASCII.GetBytes(@"{""enableRawOutput"": true,
""format"": ""Json""}");
            try
            {
                _client = new TcpClient("127.0.0.1", 13854);
                _stream = _client.GetStream();
                
                // Sending configuration packet to TGC
                if(_stream.CanWrite) {
                    _stream.Write(_myWriteBuffer, 0, _myWriteBuffer.Length);
                }
                
                if(_stream.CanRead) {
                    Debug.Log("reading bytes");
                    // This is a special thread to read data continuously 
                    StartCoroutine(Reading());
                }
            }
            catch (SocketException)
            {
            }
        }
        
        private IEnumerator Reading()
        {
            while (true)
            {
                _bytesRead = _stream.Read(_buffer, 0, 2048);
                var packets = Encoding.UTF8.GetString(_buffer, 0,
                    _bytesRead).Split('\r');
                foreach (var s in packets)
                {
                    var temp = s.Trim();
                    if (temp.Length > 50)
                    {
                        try
                        {
                            var json = JObject.Parse(temp);
                            var eSense = json["eSense"];
                            attention = (int) eSense["attention"];
                            sliderHor.value = attention;
                            meditation = (int) eSense["meditation"];
                            sliderVer.value = meditation;
                        }
                        catch (JsonException)
                        {
                        }
                    }
                    else if (temp.Contains("blinkStrength"))
                    {
                        var json = JObject.Parse(temp);
                        eyeBlinking = (int) json["blinkStrength"];
                        yield return new WaitForSeconds(2);
                        eyeBlinking = 0;
                    }
                }
                //yield return new WaitForSeconds(0.2f); // Read from device every 0.2 sec
            }
        }

        public override string ToString()
        {
            return $"Attention: {attention}, Meditation {meditation}, Blink: {eyeBlinking}";
        }

        public int EyeBlinking
        {
            get => eyeBlinking;
            set => eyeBlinking = value;
        }
    }
}
