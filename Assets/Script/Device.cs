namespace Script
{
  public class Device
    {
        public int indoorLamp { get; set; } // value 0 = off, value 1 = on
        public int outdoorLamp { get; set; } // value 0 = off, value 1 = on
        public int indoorTemp { get; set; } // returns temp in celsius
        public int outdoorTemp { get; set; } // returns temp in celsius
        public int radiator { get; set; } // value 0 = off, value 1 = on
        public int power { get; set; } // ??
        public int fireAlarm { get; set; } // value 0 = off, value 1 = on
        public int doorAlarm { get; set; } // value 0 = off, value 1 = on
        public int fan { get; set; } // value 0 = off, value 1 - 255 fan speed
        public int waterLeakage { get; set; } // value 0 = off, value 1 = on
        public int stove { get; set; } // value 0 = off, value 1 = on
        public int window { get; set; } // value 0 = off, value 1 = on
        public int timer1 { get; set; } // value 0 = off, value 1 = on
        public int timer2 { get; set; } // value 0 = off, value 1 = on
        public int lightSensor { get; set; } // value 0 = night, value 1 = day
}