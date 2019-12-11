using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Script.Devices
{
    public static class DeviceManager
    {
        public static string Get(DeviceIndex deviceIndex)
        {
            var client = new RestClient {endPoint = $"http://10.0.0.4:8080/SmartHouseApi/devices/{(int)deviceIndex}"};
            var strResponse = client.makeRequest();
            var json = JObject.Parse(strResponse);
            var lightStatus = json["deviceStatus"];
            return lightStatus.ToString();
        }

        public static string DeviceText(DeviceIndex deviceIndex)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (deviceIndex)
            {
                case DeviceIndex.Relax:
                    return "Relax";
                case DeviceIndex.IndoorLamp:
                    return "Indoor Lamp";
                case DeviceIndex.OutdoorLamp:
                    return "Outdoor Lamp";
                case DeviceIndex.IndoorTemp:
                    return "Indoor Temp";
                case DeviceIndex.OutdoorTemp:
                    return "Outdoor Temp";
                case DeviceIndex.Radiator:
                    return "Radiator";
                case DeviceIndex.Power:
                    return "Power";
                case DeviceIndex.FireAlarm:
                    return "FireAlarm";
                case DeviceIndex.DoorAlarm:
                    return "FireAlarm";
                case DeviceIndex.Fan:
                    return "Fan";
                case DeviceIndex.WaterLeakage:
                    return "Water Leakage";
                case DeviceIndex.Stove:
                    return "Stove";;
                case DeviceIndex.Window:
                    return "Window";;
                case DeviceIndex.Timer1:
                    return "Timer 1";;
                case DeviceIndex.Timer2:
                    return "Timer 2";;
                case DeviceIndex.LightSensor:
                    return "Light Sensor";
                case DeviceIndex.Music:
                    return "Play Music";
                case DeviceIndex.Call112:
                    return "Call 112";
                case DeviceIndex.Call1177:
                    return "Cal l1177";
            }
            return null;
        }
        

        public static void Put(DeviceIndex deviceIndex, int status)
        {
            //Set only the updated devices
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (deviceIndex)
            {
                case DeviceIndex.IndoorLamp:
                    break;
                case DeviceIndex.OutdoorLamp:
                    break;
                case DeviceIndex.Radiator:
                    break;
                case DeviceIndex.Fan:
                    break;
                case DeviceIndex.Timer1:
                    break;
                case DeviceIndex.Timer2:
                    break;
                case DeviceIndex.Music:
                    break;
                case DeviceIndex.Call112:
                    break;
                case DeviceIndex.Call1177:
                    break;
                case DeviceIndex.UnderContraction:
                    break;
            }
        }
    }
}
