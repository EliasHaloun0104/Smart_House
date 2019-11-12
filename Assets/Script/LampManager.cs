using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Script
{
    public class LampManager : MonoBehaviour
    {
        private string _ip;
        private string _lampLink;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite lampOn;
        [SerializeField] private Sprite lampOff;
        private bool _lampStatus;
        
        // Start is called before the first frame update
        void Start()
        {
            _ip = "10.0.0.2";
            _lampLink = $"http://{_ip}/:8080/SmartHouseApi/devices/1";
            
            
            _spriteRenderer = GetComponent<SpriteRenderer>();
            //On Initialize
            if (GetLampStatus()) TurnOn();
            else TurnOff();
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.O))
            {
                TurnOn();
            }

            if (Input.GetKey(KeyCode.F))
            {
                TurnOff();
            }
        }

        void TurnOn()
        {
            
            _spriteRenderer.sprite = lampOn;
        }
        void TurnOff()
        {
            _spriteRenderer.sprite = lampOff;
        }
        
        //TODO Connect to the server and Get the status
        //Return true if on
        bool GetLampStatus()
        {
            var client = new RestClient {endPoint = _lampLink};
            var strResponse = client.makeRequest();
            var json = JObject.Parse(strResponse);
            var lightStatus = json["deviceStatus"];
            return lightStatus.ToString().ToLower().Equals("on");
        }
        
        //TODO Connect to the server and Update the status
        public void ChangeLamp()
        {
            if (GetLampStatus())
            {
                string[] values = {"1", "indoorlamp", "off", _ip};
                RunJavaRestClient(values);
                TurnOff();
            }
            else
            {
                string[] values = {"1", "indoorlamp", "on", _ip};
                RunJavaRestClient(values);
                TurnOn();
            }
        }
        
        private static void RunJavaRestClient(string[] args)
        {
            //Create process
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            //strCommand is path and file name of command to run
            pProcess.StartInfo.FileName = "java";
            //strCommandParameters are parameters to pass to program
            pProcess.StartInfo.Arguments = " -jar C:\\Users\\ELHA0104\\UnityPorject\\Smart House\\Assets\\Java\\target\\javaRestClient-1.0-SNAPSHOT-jar-with-dependencies.jar " + args[0] + " " + args[1] + " " + args[2]  + " "+ args[3];
            pProcess.StartInfo.UseShellExecute = false;
            //Set output of program to be written to process output stream
            //Process.StartInfo.RedirectStandardOutput = true;
            //Start the process
            pProcess.Start();
            //Get program output
            //string strOutput = pProcess.StandardOutput.ReadToEnd();
            //Wait for process to finish
            pProcess.WaitForExit();
        }
    }
}
