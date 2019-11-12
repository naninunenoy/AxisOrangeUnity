using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AxisOrange {
    public readonly struct AxisOrangeData {
        public readonly long timestamp;
        public readonly Vector3 acc;
        public readonly Vector3 gyro;
        public readonly Quaternion quaternion;

        public AxisOrangeData(long timestamp, Vector3 acc, Vector3 gyro, Quaternion quaternion) {
            this.timestamp = timestamp;
            this.acc = acc;
            this.gyro = gyro;
            this.quaternion = quaternion;
        }
    }
}
