using System;
using System.Collections;
using System.Diagnostics;
using Models;
using Newtonsoft.Json.Linq;
using Proyecto26;
using UnityEngine;
using Debug = UnityEngine.Debug;


using UnityEngine.Networking;

namespace Script
{
    public class LampManager : MonoBehaviour
    {
        [SerializeField] private string ip;
        [SerializeField] private string lampLink;
        [SerializeField] private string javaPath;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite lampOn;
        [SerializeField] private Sprite lampOff;
        private bool _lampStatus;
        
        // Start is called before the first frame update
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            //On Initialize
            //if (GetLampStatus()) TurnOn();
            //else TurnOff();
            if(GetLampStatus()) TurnOn();
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
            Debug.Log("GetStatus");
            var client = new RestClient {endPoint = "http://10.0.0.4:8080/SmartHouseApi/devices/1"};
            var strResponse = client.makeRequest();
            var json = JObject.Parse(strResponse);
            var lightStatus = json["deviceStatus"];
            return lightStatus.ToString().ToLower().Equals("on");
        }
        
        //TODO Connect to the server and Update the status
        public void UpdateLamp()
        {
            if (GetLampStatus())
            {
                RunJavaRestClient(true);
                TurnOff();
            }
            else
            {
                RunJavaRestClient(false);
                TurnOn();
            }
        }


        private void RunJavaRestClient(bool status)
        {
            var batFileOff = "C:\\Users\\ELHA0104\\Desktop\\device1TurnOff.bat";
            var batFileOn = "C:\\Users\\ELHA0104\\Desktop\\device1TurnOn.bat";
            
            try {
                Process myProcess = new Process();
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = "C:\\Windows\\system32\\cmd.exe";
               
                myProcess.StartInfo.Arguments = "/c" + (status? batFileOff: batFileOn);
                myProcess.EnableRaisingEvents = true;
                myProcess.Start();
                myProcess.WaitForExit();
                var exitCode = myProcess.ExitCode;
                Debug.Log(exitCode);
            } catch (Exception e){
                
                Debug.Log(e);        
            }
           
            

        }
        
        
        
        
    }
}
