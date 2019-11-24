using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace AxisOrange {
    public partial class AxisOrangePlugin {
        public event Action<int, AxisOrangeUnityData> SensorDataUpdateEvent = delegate { };
        public event Action<int, AxisOrangeButton> SensorButtonUpdateEvent = delegate { };

        static readonly IDictionary<int, IAxisOrangeSensor> sensorDict;
        static readonly IDictionary<int, CompositeDisposable> disposables;
        static readonly IAxisOrangeFactory factory;

        public IEnumerable<int> SensorIds => sensorDict.Select(x => x.Key);

        public bool CreateAxisOrange(int id) {
            var sensor = factory.CreateWithId(id);
            if (sensor == null) {
                return false;
            }
            sensor.Open();
            sensor.Listen();
            var disposable = new CompositeDisposable();
            SubscriteEvents(sensor, disposable);
            sensorDict.Add(id, sensor);
            disposables.Add(id, disposable);
            return true;
        }

        public bool DeleteAxisOrange(int id) {
            if (!sensorDict.ContainsKey(id)) {
                return false;
            }
            // dispose
            if (disposables.ContainsKey(id)) {
                disposables[id].Dispose();
                disposables.Remove(id);
            }
            var sensor = sensorDict[id];
            sensor.Unlisten();
            sensor.Close();
            sensorDict.Remove(id);
            return true;
        }

        public bool InstallGyroOffset(int id) {
            if (!sensorDict.ContainsKey(id)) {
                return false;
            }
            sensorDict[id].RequestInstallGyroOfset();
            return true;
        }

        void SubscriteEvents(IAxisOrangeSensor sensor, CompositeDisposable disposables) {
            sensor
                .ReactiveSensorData()
                .ObserveOnMainThread()
                .Subscribe(x => {
                    SensorDataUpdateEvent.Invoke(sensor.SensorId, new AxisOrangeUnityData(x));
                })
                .AddTo(disposables);
            sensor
                .ReactiveButton()
                .ObserveOnMainThread()
                .Subscribe(x => {
                    SensorButtonUpdateEvent.Invoke(sensor.SensorId, x);
                })
                .AddTo(disposables);
        }
    }
}
