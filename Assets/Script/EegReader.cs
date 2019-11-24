using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class EegReader : MonoBehaviour
    {
        private enum Mode
        {
            Horizontal , Vertical, NonMode,
        }
        [Header("Connection state")]
        [SerializeField] private TextMeshProUGUI info;

        [Header("Slider")] 
        [SerializeField] private Slider sliderHor;
        [SerializeField] private Slider sliderVer;

        [Header("Mode")] [SerializeField] private Mode mode;
        
        [Header("Output Data")]
        [SerializeField] private int attentionY;
        [SerializeField] private int attentionX;
        [SerializeField] private BlinksManager eyeBlinking;

        [Header("Indicator")]
        [SerializeField] private Transform hor;
        [SerializeField] private Transform ver;
        [SerializeField] private GameObject indicator;
        
        
        //Internal instance for this class
        private TcpClient _client;
        private Stream _stream;
        private byte[] _buffer;
        private int _bytesRead;
        // Building command to enable JSON output from ThinkGear Connector (TGC)
        private byte[] _myWriteBuffer;

        // Start is called before the first frame update


        private IEnumerator Start()
        {
            mode = Mode.NonMode;
            info.SetText("Connecting the headset...");
            yield return new WaitForSeconds(1);
            var isConnected = Connect();
            info.SetText(isConnected? "Headset connected successfully": "Failed to connect headset");
            yield return new WaitForSeconds(2);
            if (isConnected) StartCoroutine(Reading());
        }
   
        private bool Connect()
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
                return _stream.CanRead;
            }
            catch (SocketException)
            {
                return false;
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
                            var attention = (int) eSense["attention"];
                            switch (mode)
                            {
                                case Mode.Horizontal:
                                    attentionX = attention;
                                    sliderHor.value = attentionX;
                                    break;
                                case Mode.Vertical:
                                    attentionY = attention;
                                    sliderVer.value = attentionY;
                                    break;
                                case Mode.NonMode:
                                    //Nothing 
                                    break;
                            }
                        }catch(JsonException){}
                    }
                    else
                    {
                        try
                        {
                            if (temp.Contains("blinkStrength"))
                            {
                                var json = JObject.Parse(temp);
                                var blinking = (int) json["blinkStrength"];
                                if(blinking>55) eyeBlinking.Add();
                            }
                        }
                        catch (JsonException){}
                    }
                }
                yield return new WaitForSeconds(0.4f);
            }
        }

        public override string ToString()
        {
            return $"Attention: {attentionY}, Blink: {eyeBlinking}";
        }

        private void Update()
        {
            indicator.transform.position = Vector3.MoveTowards(
                indicator.transform.position,
                new Vector3(hor.transform.position.x, ver.transform.position.y
                    ,0),
                Time.deltaTime);

            if (mode == Mode.Horizontal && eyeBlinking.IsTwoBlink())
            {
                mode = Mode.Vertical;
            }

            if (mode == Mode.Vertical)
            {
                if(eyeBlinking.IsOneBlink()) mode = Mode.Horizontal;
                if(eyeBlinking.IsTwoBlink()) mode = Mode.NonMode;
            }

            if (mode == Mode.NonMode && eyeBlinking.IsTwoBlink())
            {
                mode = Mode.Vertical;
            }
        }

        
    }
}
