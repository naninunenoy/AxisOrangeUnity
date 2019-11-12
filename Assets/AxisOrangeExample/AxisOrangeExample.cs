using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AxisOrange;

namespace AxisOrangeExample {
    public class AxisOrangeExample : MonoBehaviour {

        AxisOrange.AxisOrange axisOrange;
        Quaternion baseQuaternion = Quaternion.identity;

        // Start is called before the first frame update
        void Start() {
            axisOrange = GetComponent<AxisOrange.AxisOrange>();
            if (axisOrange != null) {
                axisOrange.DataReceived += UpdateQuaternion;
            }
        }

        void OnDestroy() {
            if (axisOrange != null) {
                axisOrange.DataReceived -= UpdateQuaternion;
            }
        }

        void UpdateQuaternion(AxisOrangeData data) {
            var quat = data.ToUnityAxis().quaternion;
            if (baseQuaternion == Quaternion.identity) {
                baseQuaternion = quat;
            }
            transform.rotation = Quaternion.Inverse(baseQuaternion) * quat;
        }
    }
}
