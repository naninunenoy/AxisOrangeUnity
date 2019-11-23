using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AxisOrange {
    public static class AxisOrangePlugin {
        public static INotifySensor CreateBySerialPort(int portNo) {
            return new AxisOrangeSensor(portNo);
        }
    }
}
