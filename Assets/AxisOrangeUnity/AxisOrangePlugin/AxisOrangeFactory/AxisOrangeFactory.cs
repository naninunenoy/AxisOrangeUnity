using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AxisOrange {
    public class AxisOrangeFactory : IAxisOrangeFactory {
        public IAxisOrangeSensor CreateWithId(int id) {
            return new AxisOrangeSensor(id);
        }
    }
}
