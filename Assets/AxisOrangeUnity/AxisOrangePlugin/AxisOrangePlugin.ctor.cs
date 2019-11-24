using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AxisOrange {
    public partial class AxisOrangePlugin {
        static AxisOrangePlugin() {
            sensorDict = new Dictionary<int, IAxisOrangeSensor>();
            factory = new AxisOrangeFactory();
        }
    }
}
