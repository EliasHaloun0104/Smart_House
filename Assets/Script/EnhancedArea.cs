using System;
using System.Collections;
using Script.Devices;
using TMPro;
using TMPro.Examples;
using UnityEngine;

namespace Script
{
    public class EnhancedArea: MonoBehaviour
    {
        public static HttpRequest HttpRequest;
        public static Device Devices;
        
        private TextMeshProUGUI _text;
        private VertexJitter _vertexJitter;
        [SerializeField] private BlinksManager blinkManager;
        [SerializeField] private EegReader eegReader;
        [SerializeField] private DeviceIndex deviceIndex;
        [SerializeField] private bool inside;
        private IEnumerator Start()
        {
            if (Devices == null)
            {
                HttpRequest = new HttpRequest();
                HttpRequest.getRequest();
                Devices = HttpRequest.Device;
            }
            yield return new WaitForSeconds(1);
            _text = GetComponent<TextMeshProUGUI>();
            _vertexJitter = GetComponent<VertexJitter>();
            inside = false;
            var status = "";
            switch (deviceIndex)
            {
                case DeviceIndex.IndoorLamp:
                    status = Devices.indoorLamp == 0 ? "Off" : "On";
                    _text.SetText($"Indoor Lamp: {status}");
                    break;
                case DeviceIndex.OutdoorLamp:
                    status = Devices.outdoorLamp == 0 ? "Off" : "On";
                    _text.SetText($"Outdoor Lamp: {status}");
                    break;
                case DeviceIndex.Radiator:
                    status = Devices.radiator == 0 ? "Off" : "On";
                    _text.SetText($"Radiator: {status}");
                    break;
                case DeviceIndex.Fan:
                    status = Devices.fan == 0 ? "Off" : "On";
                    _text.SetText($"Fan: {status}");
                    break;
                case DeviceIndex.Timer1:
                    break;
                case DeviceIndex.Timer2:
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _vertexJitter.AngleMultiplier = 2;
            _vertexJitter.CurveScale = 4;
            blinkManager.EnterArea(this);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _vertexJitter.AngleMultiplier = 0;
            _vertexJitter.CurveScale = 0;
            blinkManager.ExitArea();
        }

        public void UpdateStatus()
        {
            string status;
            switch (deviceIndex)
            {
                case DeviceIndex.IndoorLamp:
                    HttpRequest.changeIndoorLamp();
                    status = Devices.indoorLamp == 0 ? "Off" : "On";
                    _text.SetText($"Indoor Lamp: {status}");
                    break;
                case DeviceIndex.OutdoorLamp:
                    HttpRequest.changeOutdoorLamp();
                    status = Devices.outdoorLamp == 0 ? "Off" : "On";
                    _text.SetText($"Outdoor Lamp: {status}");
                    break;
                case DeviceIndex.Radiator:
                    HttpRequest.changeRadiator();
                    status = Devices.radiator == 0 ? "Off" : "On";
                    _text.SetText($"Radiator: {status}");
                    break;
                case DeviceIndex.Fan:
                    HttpRequest.changeFan();
                    status = Devices.fan == 0 ? "Off" : "On";
                    _text.SetText($"Fan: {status}");
                    break;
                case DeviceIndex.Timer1:
                    break;
                case DeviceIndex.Timer2:
                    break;
            }




        }
    }
}