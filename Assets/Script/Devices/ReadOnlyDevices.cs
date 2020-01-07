using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Script.Devices
{
    public class ReadOnlyDevices : MonoBehaviour
    {
        private TextMeshProUGUI text;

        private float _waitTime = 1;
        // Start is called before the first frame update
        IEnumerator Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            while (true)
            {
                var devices = ServerConnection.Get();
                yield return new WaitForSeconds(_waitTime);
                
                text.SetText($"Indoor Temp: {devices.indoorTemp}C");
                yield return new WaitForSeconds(_waitTime);
                
                text.SetText($"Outdoor Temp: {devices.outdoorTemp}C");
                yield return new WaitForSeconds(_waitTime);

                var status = devices.power == 0 ? "Off" : "On";
                text.SetText($"Power: {status}");
                yield return new WaitForSeconds(_waitTime);
                
                status = devices.fireAlarm == 0 ? "Off" : "On";
                text.SetText($"Fire Alarm: {status}");
                yield return new WaitForSeconds(_waitTime);
                
                status = devices.doorAlarm == 0 ? "Off" : "On";
                text.SetText($"Door Alarm: {status}");
                yield return new WaitForSeconds(_waitTime);
                
                status = devices.waterLeakage == 0 ? "Off" : "On";
                text.SetText($"Water Leakage: {status}");
                yield return new WaitForSeconds(_waitTime);
                
                status = devices.stove == 0 ? "Off" : "On";
                text.SetText($"Stove Alarm: {status}");
                yield return new WaitForSeconds(_waitTime);
                
                status = devices.window == 0 ? "Off" : "On";
                text.SetText($"Window Alarm: {status}");
                yield return new WaitForSeconds(_waitTime);
                
                status = devices.lightSensor == 0 ? "Night" : "Day";
                text.SetText($"Light Sensor: {status}");
                yield return new WaitForSeconds(_waitTime);
                
                
                

            }
        }


        
        
        
        

        
    }
}
