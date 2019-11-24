using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AxisOrange {
    public interface IAxisOrangeFactory {
        IAxisOrangeSensor CreateWithId(int id);
    }
}
