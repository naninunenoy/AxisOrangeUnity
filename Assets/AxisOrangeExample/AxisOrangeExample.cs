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
                axisOrange.ButtonUpdated += UpdateButton;
            }
        }

        void OnDestroy() {
            if (axisOrange != null) {
                axisOrange.DataReceived -= UpdateQuaternion;
                axisOrange.ButtonUpdated -= UpdateButton;
            }
        }

        void UpdateQuaternion(AxisOrangeData data) {
            var quat = data.ToUnityAxis().quaternion;
            if (baseQuaternion == Quaternion.identity) {
                baseQuaternion = quat;
            }
            transform.rotation = Quaternion.Inverse(baseQuaternion) * quat;
        }

        void UpdateButton(AxisOrangeButton button) {
            Debug.Log($"{button.timestamp} {button.buttonA} {button.buttonB}");
        }
    }
}
