using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using AxisOrange;

namespace AxisOrangeExample {
    public class AxisOrangeExample : MonoBehaviour {
        [SerializeField] int serialPortNo = 0;
        [SerializeField] Transform m5stickC = default;

        IAxisOrangeSensor sensor;
        Quaternion baseQuaternion = Quaternion.identity;
        SynchronizationContext context;

        void Awake() {
            sensor = new AxisOrangeSensor(serialPortNo);
        }

        void Start() {
            context = SynchronizationContext.Current;
            if (sensor != null) {
                sensor.Open();
                sensor.Listen();
                sensor.OnSensorDataUpdate += UpdateQuaternion;
                sensor.OnSensorButtonUpdate += UpdateButton;
            }
        }

        void OnDestroy() {
            if (sensor != null) {
                sensor.OnSensorDataUpdate -= UpdateQuaternion;
                sensor.OnSensorButtonUpdate -= UpdateButton;
                sensor.Unlisten();
                sensor.Close();
                sensor.Dispose();
            }
        }

        void UpdateQuaternion(AxisOrangeRawData data) {
            var unityData = new AxisOrangeData(data);
            if (baseQuaternion == Quaternion.identity) {
                baseQuaternion = unityData.quaternion;
            }
            // メインスレッドで実行しないと反映されない
            context?.Post(_ => {
                m5stickC.rotation = Quaternion.Inverse(baseQuaternion) * unityData.quaternion;
            }, null);
        }

        void UpdateButton(AxisOrangeButton button) {
            if (button.buttonA == ButtonState.Push) {
                baseQuaternion = Quaternion.identity;
            }
        }

        public void OnClickButton() {
            sensor?.RequestInstallGyroOfset();
        }
    }
}
