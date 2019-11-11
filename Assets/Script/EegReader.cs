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
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI text;
        //The output data
        [SerializeField] private int _attention;
        private int _meditation;
        [SerializeField] private int _eyeBlinking;
        
        //Internal instance for this class
        private TcpClient _client = null;
        private Stream _stream;
        private byte[] _buffer;
        private int _bytesRead;
        // Building command to enable JSON output from ThinkGear Connector (TGC)
        private byte[] _myWriteBuffer;

        // Start is called before the first frame update
        void Start()
        {
            _attention = 50;
            //Real connection
            Connect();

            //false connection
            //StartCoroutine(FalseReading());
        }
        
        //This method used to communicate w the real EEG Connecter
        void Connect()
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
        

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator Reading()
        {
            _bytesRead = _stream.Read(_buffer, 0, 2048);
            var packets = Encoding.UTF8.GetString(_buffer, 0,
                _bytesRead).Split('\r');
            foreach(var s in packets)
            {
                var temp = s.Trim();
                if (temp.Length > 50)
                {
                    try
                    {
                        var json = JObject.Parse(temp);
                        var eSense = json["eSense"];
                        _attention = (int) eSense["attention"];
                        slider.value = _attention;
                        _meditation = (int) eSense["meditation"];
                    }
                    catch (JsonException)
                    {
                    }
                }
                else if (temp.Contains("blinkStrength"))
                {
                    var json = JObject.Parse(temp);
                    //var eyeStrength = (int) json["blinkStrength"];
                    //If the eye blinking strength > 150 (ignore all weak blink)
                    //_eyeBlinking = eyeStrength>120 ? eyeStrength : 0;
                    _eyeBlinking = (int) json["blinkStrength"];
                    yield return new WaitForSeconds(2);
                    _eyeBlinking = 0;
                }
            }
            text.SetText(ToString());
            yield return new WaitForSeconds(0.2f); // Read from device every 0.2 sec
            StartCoroutine(Reading());
        }

        IEnumerator FalseReading()
        {
            _attention = Random.Range(0, 100);
            //Update Slider
            var sliderValue = (int) slider.value;
            if (_attention > sliderValue) slider.value += 2;
            if (_attention < sliderValue) slider.value -= 2;
            
            _meditation = Random.Range(0, 100);
            _eyeBlinking = (Random.Range(0, 100) > 95) ? Random.Range(150, 200) : 0;
            text.SetText(ToString());
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(FalseReading());
        }
        
        public override string ToString()
        {
            return $"Attention: {_attention}, Meditation {_meditation}, Blink: {_eyeBlinking}";
        }

        public int EyeBlinking
        {
            get => _eyeBlinking;
            set => _eyeBlinking = value;
        }
    }
}
