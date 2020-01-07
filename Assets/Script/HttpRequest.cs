using System;
using System.Text.RegularExpressions;
using Script;
using UnityEngine;
using RestClient = Proyecto26.RestClient;

namespace Script
{
    public class HttpRequest
    {
        private readonly string path = "http://192.168.43.117:8080/SmartHouseApi/devices";
        private Device device = new Device();
        private string json = "";

        public void getRequest()
        {
            RestClient.Get(path).Then(response =>
            {
                Debug.Log("GET request" + response.StatusCode);
                
                Regex regex = new Regex(@"\:(.+?)\,"); // looks for values between : and ,
                var matches = regex.Matches(response.Text);

                device.indoorLamp = Int32.Parse(matches[0].Groups[1].ToString());
                device.outdoorLamp = Int32.Parse(matches[1].Groups[1].ToString());
                device.indoorTemp = Int32.Parse(matches[2].Groups[1].ToString());
                device.outdoorTemp = Int32.Parse(matches[3].Groups[1].ToString());
                device.radiator = Int32.Parse(matches[4].Groups[1].ToString());
                device.power = Int32.Parse(matches[5].Groups[1].ToString());
                device.fireAlarm = Int32.Parse(matches[6].Groups[1].ToString());
                device.doorAlarm = Int32.Parse(matches[7].Groups[1].ToString());
                device.fan = Int32.Parse(matches[8].Groups[1].ToString());
                device.waterLeakage = Int32.Parse(matches[9].Groups[1].ToString());
                device.stove = Int32.Parse(matches[10].Groups[1].ToString());
                device.window = Int32.Parse(matches[11].Groups[1].ToString());
                device.timer1 = Int32.Parse(matches[12].Groups[1].ToString());
                device.timer2 = Int32.Parse(matches[13].Groups[1].ToString());
                device.lightSensor = Int32.Parse(response.Text.Substring(response.Text.Length - 2, 1));
                
            }).Catch(err => { Debug.LogError("Request failed"); });
        }

        public Device Device
        {
            get => device;
            set => device = value;
        }

        public void putRequest()
        {
            RestClient.Put(path, json).Then(response =>
            {
                Debug.Log("PUT request status code:" + response.StatusCode.ToString());
            }).Catch(err => { Debug.LogError("Request failed"); });
            ;
        }

        public void changeIndoorLamp()
        {
            device.indoorLamp = (device.indoorLamp == 0) ? 1 : 0;
            json = "{\"1\":" + device.indoorLamp + "}";
            putRequest();
        }

        public void changeOutdoorLamp()
        {
            device.outdoorLamp = (device.outdoorLamp == 0) ? 1 : 0;
            json = "{\"2\":" + device.outdoorLamp + "}";
            putRequest();
        }
        
        public void changeRadiator()
        {
            device.radiator = (device.radiator == 0) ? 1 : 0;
            json = "{\"5\":" + device.radiator + "}";
            putRequest();
        }
        public void changeFan()
        {
            device.fan = (device.fan == 0) ? 255 : 0;
            json = "{\"9\":" + device.fan + "}";
            putRequest();
        }
    }
}
