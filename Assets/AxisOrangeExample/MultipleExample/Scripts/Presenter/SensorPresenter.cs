﻿using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AxisOrangeExample {
    public class SensorPresenter : ISensorPresenter {
        readonly ISensorView view;
        Quaternion baseRotaion = Quaternion.identity;

        public SensorPresenter(ISensorView view) { this.view = view; }

        public IObservable<Unit> AddButtonClickObservable => view.AddButton.OnClickAsObservable();
        public IObservable<Unit> CloseButtonClickObservable => view.CloseButton.OnClickAsObservable();
        public IObservable<Unit> StartButtonClickObservable => view.StartButton.OnClickAsObservable();

        public IObservable<Unit> InstallGyroOffsetButtonClickObservable => view.InstallGyroOffsetButton.OnClickAsObservable();


        public string GetIdInputText() {
            return view.IdInput.text;
        }

        public void CloseSensorView() {
            view.EmptyView.gameObject.SetActive(true);
            view.ContentView.gameObject.SetActive(false);
        }

        public void OpenSensorView() {
            view.ContentView.gameObject.SetActive(true);
            view.EmptyView.gameObject.SetActive(false);
        }

        public void SetStartButtonText(string text) {
            view.StartButtonText.text = text;
        }

        public void SetButtonAAppeared(bool appeared) {
            view.ButtonAImage.gameObject.SetActive(appeared);
            // button A で quaternion 初期化
            if (appeared) {
                baseRotaion = Quaternion.identity;
            }
        }

        public void SetButtonBAppeared(bool appeared) {
            view.ButtonBImage.gameObject.SetActive(appeared);
        }

        public void SetComPortText(string text) {
            view.ComPortText.text = text;
        }

        public void SetM5StickCRotation(Quaternion rotation) {
            if (baseRotaion == Quaternion.identity) {
                baseRotaion = rotation;
                return;
            }
            view.M5StickC.rotation = Quaternion.Inverse(baseRotaion) * rotation;
        }
    }
}
