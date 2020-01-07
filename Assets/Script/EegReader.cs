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
        [Header("Connection state")]
        [SerializeField] private TextMeshProUGUI info;

        [Header("Slider")] 
        [SerializeField] private Slider sliderHor;
        
        [Header("Output Data")]
        [SerializeField] private int attention;
        [SerializeField] private BlinksManager eyeBlinking;
        

        [Header("Indicator")]
        [SerializeField] private Transform hor;
        [SerializeField] private GameObject indicator;

        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform[] points; 
        private int _jump = 0;

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
                            attention = (int) eSense["attention"];
                            sliderHor.value = attention;
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
        
        private void Update()
        {
            indicator.transform.position = Vector3.MoveTowards(
                indicator.transform.position,
                 hor.transform.position,
                3*Time.deltaTime);

            if (eyeBlinking.IsTwoBlink())
            {
                _jump++;
                if (_jump == points.Length) _jump = 0;
                mainCamera.transform.position = points[_jump].position;
            }
            
            //TODO delete this it simulate eyeBlink
            if (Input.GetKeyDown(KeyCode.R))
            {
                eyeBlinking.Add();
            }
        }
    }
}
