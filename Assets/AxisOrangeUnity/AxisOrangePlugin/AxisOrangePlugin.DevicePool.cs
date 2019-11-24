using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AxisOrange {
    public partial class AxisOrangePlugin {
        static readonly IDictionary<int, IAxisOrangeSensor> sensorDict;
        static readonly IAxisOrangeFactory factory;

        public bool CreateAxisOrange(int id) {
            var sensor = factory.CreateWithId(id);
            if (sensor == null) {
                return false;
            }
            sensor.Open();
            sensor.Listen();
            return true;
        }

        public bool DeleteAxisOrange(int id) {
            if (!sensorDict.ContainsKey(id)) {
                return false;
            }
            var sensor = sensorDict[id];
            sensor.Unlisten();
            sensor.Close();
            sensorDict.Remove(id);
            return true;

        }

    }
}
