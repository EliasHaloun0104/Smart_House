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
            Debug.Log(GetLampStatus());
            string[] args = {"1", "indoorlamp", "off"};
            Put();
            

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
            var client = new RestClient {endPoint = "http://10.0.0.2:8080/SmartHouseApi/devices/1"};
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
                string[] values = {"1", "indoorlamp", "off"};
                RunJavaRestClient(values);
                TurnOff();
            }
            else
            {
                string[] values = {"1", "indoorlamp", "on"};
                RunJavaRestClient(values);
                TurnOn();
            }
        }


        private void RunJavaRestClient(string[] args)
        {
            var cmd = "java -jar C:\\Users\\ELHA0104\\Desktop\\target\\javaRestClient-1.0-SNAPSHOT-jar-with-dependencies.jar 1 lamp off ";
            var target = $" -jar C:\\Users\\ELHA0104\\Desktop\\target\\javaRestClient-1.0-SNAPSHOT-jar-with-dependencies.jar {args[0]} {args[1]} {args[2]}";
            /*
            Debug.Log(target);
            //Create process
            var process = new ProcessStartInfo("java", target)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                

            };
            //strCommand is path and file name of command to run
            //strCommandParameters are parameters to pass to program
            //Set output of program to be written to process output stream
            //Process.StartInfo.RedirectStandardOutput = true;
            //Start the process
            var proc = Process.Start(process);
            if(proc== null)
            {
                throw new InvalidOperationException("??");
            }

            proc.WaitForExit();
            int exitCode = proc.ExitCode;
            proc.Close();
            
            
            //Get program output
            //string strOutput = pProcess.StandardOutput.ReadToEnd();
            //Wait for process to finish
            */
            try
            {
                ProcessStartInfo p = new ProcessStartInfo("cmd", "/C " + cmd);
                p.RedirectStandardOutput = true;
                p.UseShellExecute = false;
                p.CreateNoWindow = true;
                Process proc = new Process();
                proc.StartInfo = p;
                proc.Start();
                var result = proc.StandardOutput.ReadToEnd();
                Debug.Log(result.Length);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
           
            

        }
        
        private RequestHelper currentRequest;
        private readonly string basePath = "http://10.0.0.2:8080/SmartHouseApi";
        public void Put(){

            currentRequest = new RequestHelper {
                Uri = basePath + "/devices/1", 
                Body = new Post {
                    title = "foo",
                    body = "bar",
                    userId = 1
                },
                Retries = 5,
                RetrySecondsDelay = 1,
                RetryCallback = (err, retries) => {
                    Debug.Log (string.Format ("Retry #{0} Status {1}\nError: {2}", retries, err.StatusCode, err));
                }
            };
            
            Proyecto26.RestClient.Put<Post>(currentRequest, (err, res, body) => {
                if (err != null){
                    Debug.LogError(err.Message);
                }
                else {
                    Debug.Log(err.Message);
                }
            });
        }

        
        
        
    }
}
