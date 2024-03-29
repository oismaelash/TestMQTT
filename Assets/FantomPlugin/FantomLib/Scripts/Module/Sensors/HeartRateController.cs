﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace FantomLib
{
    //(*) Requierd: '<uses-permission android:name="android.permission.BODY_SENSORS"/>' in 'AndroidManifest.xml'.
    /// <summary>
    /// Heart Rate Sensor Controller (API 20 or higher)
    ///
    ///(Sensor Type)
    /// https://developer.android.com/reference/android/hardware/Sensor.html#TYPE_HEART_RATE
    ///(Sensor Values)
    /// https://developer.android.com/reference/android/hardware/SensorEvent.html#values
    ///(Sensor Delay)
    /// https://developer.android.com/reference/android/hardware/SensorManager.html#SENSOR_DELAY_FASTEST
    /// </summary>

    public class HeartRateController : SensorControllerBase
    {
        protected override SensorType sensorType {
            get { return SensorType.HeartRate; }
        }

        //Callbacks
        [Serializable] public class HeartRateSensorChangedHandler : UnityEvent<float> { }   //[bpm]
        public HeartRateSensorChangedHandler OnHeartRateSensorChanged;

#region Properties and Local values Section

        //Whether necessary permissions are granted.
        public bool IsPermissionGranted {
            get {
#if UNITY_EDITOR
                return true;    //For Editor (* You can rewrite it as you like.)
#elif UNITY_ANDROID
                return AndroidPlugin.CheckPermission("android.permission.BODY_SENSORS");
#endif
            }
        }

#endregion

        // Use this for initialization
        protected new void Start()
        {
            base.Start();

            if (!IsPermissionGranted)
            {
                if (OnError != null)
                    OnError.Invoke("Permission denied: BODY_SENSORS");
            }
        }

        //Set listener for sensor values acquisition.
        public override void StartListening()
        {
            if (!IsPermissionGranted)
                return;

            base.StartListening();
        }

        //Callback handler for sensor values.
        protected override void ReceiveValues(string json)
        {
            if (string.IsNullOrEmpty(json))
                return;

            base.ReceiveValues(json);

            if (OnHeartRateSensorChanged != null)
                OnHeartRateSensorChanged.Invoke(info.values[0]);    //[bpm]
        }
    }
}
