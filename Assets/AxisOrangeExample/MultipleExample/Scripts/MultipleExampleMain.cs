﻿using System;
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
                CompositeDisposable disposables = default;
                var id = -1;
                presenter
                    .AddButtonClickObservable
                    .Subscribe(_ => {
                        if (!int.TryParse(presenter.GetIdInputText(), out id)) {
                            return;
                        }
                        if (!plugin.CreateAxisOrange(id)) {
                            return;
                        }
                        presenter.OpenSensorView();
                        presenter.SetComPortText($"COM{id}");
                        presenter.SetButtonAAppeared(false);
                        presenter.SetButtonBAppeared(false);
                        presenter.SetStartButtonText("Start");
                    })
                    .AddTo(this);
                presenter
                    .StartButtonClickObservable
                    .Subscribe(_ => {
                        if (id == -1) {
                            return;
                        }
                        if (disposables == null) {
                            // 購読開始
                            plugin.ListenUpdate(id);
                            disposables = new CompositeDisposable();
                            plugin.SensorDataUpdateEventFilterBy(id)
                                .Subscribe(x => {
                                    presenter.SetM5StickCRotation(x.quaternion);
                                })
                                .AddTo(disposables);
                            plugin.SensorButtonAPushTriggerObservableFilterBy(id)
                                .Subscribe(x => {
                                    presenter.SetButtonAAppeared(true);
                                })
                                .AddTo(disposables);
                            plugin.SensorButtonAReleaseTriggerObservableFilterBy(id)
                                .Subscribe(x => {
                                    presenter.SetButtonAAppeared(false);
                                })
                                .AddTo(disposables);
                            plugin.SensorButtonBPushTriggerObservableFilterBy(id)
                                .Subscribe(x => {
                                    presenter.SetButtonBAppeared(true);
                                })
                                .AddTo(disposables);
                            plugin.SensorButtonBReleaseTriggerObservableFilterBy(id)
                                .Subscribe(x => {
                                    presenter.SetButtonBAppeared(false);
                                })
                                .AddTo(disposables);
                            presenter
                                .InstallGyroOffsetButtonClickObservable
                                .Subscribe(__ => {
                                    plugin.InstallGyroOffset(id);
                                })
                                .AddTo(disposables);
                            presenter.SetStartButtonText("Stop");
                        } else {
                            // 購読停止
                            plugin.UnlistenUpdate(id);
                            disposables.Dispose();
                            disposables = null;
                            presenter.SetStartButtonText("Start");
                        }
                    })
                   .AddTo(this);
                presenter
                    .CloseButtonClickObservable
                    .Subscribe(_ => {
                        disposables?.Dispose();
                        if (!int.TryParse(presenter.GetIdInputText(), out id)) {
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
