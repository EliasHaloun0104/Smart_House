namespace Script.Devices
{
    public enum DeviceIndex
    {
        //Local
        Relax = 0,
        //Server
        IndoorLamp = 1, 
        OutdoorLamp = 2, 
        IndoorTemp = 3, 
        OutdoorTemp = 4,
        Radiator = 5,
        Power = 6,
        FireAlarm = 7,
        DoorAlarm = 8,
        Fan = 9,
        WaterLeakage = 10,
        Stove = 11,
        Window =12,
        Timer1 = 13,
        Timer2 = 14,
        LightSensor =15,
        //Local 
        Music = 16,
        Call112 = 17,
        Call1177 = 18,
        UnderContraction = 19
    }

    static class DeviceText
    {
        public static string GetText(this DeviceIndex deviceIndex)
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
                    return "Fire Alarm";
                case DeviceIndex.DoorAlarm:
                    return "Door Alarm";
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
                    return "Call 1177";
            }
            return null;
        }

    }
}