using System;
using System.Text.RegularExpressions;
using Proyecto26;
using UnityEngine;

namespace DefaultNamespace
{
    public class HttpRequest
    {
        private readonly string path = "http://localhost:8080/SmartHouseApi/devices";
        private Device device = new Device();
        private string json = "";

        private void getRequest()
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

                // debugging values
                // Debug.Log(device.indoorLamp);
                // Debug.Log(device.outdoorLamp);
                // Debug.Log(device.indoorTemp);
                // Debug.Log(device.outdoorTemp);
                // Debug.Log(device.radiator);
                // Debug.Log(device.power);
                // Debug.Log(device.fireAlarm);
                // Debug.Log(device.doorAlarm);
                // Debug.Log(device.fan);
                // Debug.Log(device.waterLeakage);
                // Debug.Log(device.stove);
                // Debug.Log(device.window);
                // Debug.Log(device.timer1);
                // Debug.Log(device.timer2);
                // Debug.Log(device.lightSensor);
            }).Catch(err => { Debug.LogError("Request failed"); });
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
    }
}
