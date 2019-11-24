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
                        plugin.SensorDataUpdateEventFilterBy(id)
                            .Subscribe(x => {
                                presenter.SetM5StickCRotation(x.quaternion);
                            })
                            .AddTo(this);
                        plugin.SensorButtonAPushTriggerObservableFilterBy(id)
                            .Subscribe(x => {
                                presenter.SetButtonAAppeared(true);
                            })
                            .AddTo(this);
                        plugin.SensorButtonAReleaseTriggerObservableFilterBy(id)
                            .Subscribe(x => {
                                presenter.SetButtonAAppeared(false);
                            })
                            .AddTo(this);
                        plugin.SensorButtonBPushTriggerObservableFilterBy(id)
                            .Subscribe(x => {
                                presenter.SetButtonBAppeared(true);
                            })
                            .AddTo(this);
                        plugin.SensorButtonBReleaseTriggerObservableFilterBy(id)
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
