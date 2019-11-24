using System;
using UnityEngine;
using UniRx;
using AxisOrange;

namespace AxisOrangeExample {
    public class MultipleExampleMain : MonoBehaviour {
        [SerializeField] SensorView[] sensorViews = default;
        
        void Start() {
            var plugin = AxisOrangePlugin.Instance;
            foreach (var v in sensorViews) {
                ISensorPresenter presenter = new SensorPresenter(v);
                presenter
                    .AddButtonClickObservable
                    .Subscribe(_ => {
                        if (!int.TryParse(presenter.GetIdInputText(), out var id)) {
                            return;
                        }
                        if (!plugin.CreateAxisOrange(id)) {
                            return;
                        }
                        presenter.OpenSensorView();
                        presenter.SetComPortText($"COM{id}");
                        presenter.SetButtonAAppeared(false);
                        presenter.SetButtonBAppeared(false);
                        Observable
                            .FromEvent<Action<int, AxisOrangeData>, AxisOrangeUnityData>(
                                h => (i, x) => { if (i == id) { h.Invoke(new AxisOrangeUnityData(x)); } },
                                h => plugin.SensorDataUpdateEvent += h,
                                h => plugin.SensorDataUpdateEvent -= h)
                            .Subscribe(x => {
                                presenter.SetM5StickCRotation(x.quaternion);
                            })
                            .AddTo(this);
                        var btn = Observable.FromEvent<Action<int, AxisOrangeButton>, AxisOrangeButton>(
                            h => (i, x) => { if (i == id) { h.Invoke(x); } },
                            h => plugin.SensorButtonUpdateEvent += h,
                            h => plugin.SensorButtonUpdateEvent -= h);
                        btn.ButtonAPushTriggerObservable()
                            .Subscribe(x => {
                                presenter.SetButtonAAppeared(true);
                            })
                            .AddTo(this);
                        btn.ButtonAReleaseTriggerObservable()
                            .Subscribe(x => {
                                presenter.SetButtonAAppeared(false);
                            })
                            .AddTo(this);
                        btn.ButtonBPushTriggerObservable()
                            .Subscribe(x => {
                                presenter.SetButtonBAppeared(true);
                            })
                            .AddTo(this);
                        btn.ButtonBReleaseTriggerObservable()
                            .Subscribe(x => {
                                presenter.SetButtonBAppeared(false);
                            })
                            .AddTo(this);
                    })
                    .AddTo(this);
                presenter
                    .CloseButtonClickObservable
                    .Subscribe(_ => {
                        if (!int.TryParse(presenter.GetIdInputText(), out var id)) {
                            return;
                        }
                        if (!plugin.DeleteAxisOrange(id)) {
                            return;
                        }
                        presenter.CloseSensorView();
                    })
                    .AddTo(this);
                presenter.CloseSensorView();
            }
        }
    }
}
