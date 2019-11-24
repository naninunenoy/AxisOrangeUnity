using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AxisOrange {
    public readonly struct AxisOrangeUnityData {
        public readonly uint timestamp;
        public readonly Vector3 acc;
        public readonly Vector3 gyro;
        public readonly Quaternion quaternion;

        public AxisOrangeUnityData(AxisOrangeData raw) {
            timestamp = raw.timestamp;
            acc = new Vector3(raw.acc[0], raw.acc[2], raw.acc[1]);
            gyro = new Vector3(raw.gyro[0], raw.gyro[2], raw.gyro[1]);
            quaternion = new Quaternion(raw.quaternion[1], raw.quaternion[3], raw.quaternion[2], -raw.quaternion[0]).normalized;
        }
    }
}
