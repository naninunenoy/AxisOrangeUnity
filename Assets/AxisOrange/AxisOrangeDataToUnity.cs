﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AxisOrange {
    public static class AxisOrangeDataToUnity {
        public static AxisOrangeData ToUnityAxis(this AxisOrangeData raw) {
            return new AxisOrangeData(
                raw.timestamp,
                new Vector3(raw.acc.x, raw.acc.z, raw.acc.y),
                new Vector3(raw.gyro.x, raw.gyro.z, raw.gyro.y),
                new Quaternion(raw.quaternion.x, raw.quaternion.z, raw.quaternion.y, -raw.quaternion.w)
            );
        }
    }
}
