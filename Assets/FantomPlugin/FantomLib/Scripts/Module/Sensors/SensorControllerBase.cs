﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace FantomLib
{
    /// <summary>
    /// Sensor Controller base class (for making controllers for each sensor)
    /// 
    ///(Sensor Type)
    /// https://developer.android.com/reference/android/hardware/Sensor.html#TYPE_ACCELEROMETER
    ///(Sensor Values)
    /// https://developer.android.com/reference/android/hardware/SensorEvent.html#values
    ///(Sensor Delay)
    /// https://developer.android.com/reference/android/hardware/SensorManager.html#SENSOR_DELAY_FASTEST
    ///(Sensors Overview)
    /// https://developer.android.com/guide/topics/sensors/sensors_overview.html
    /// </summary>
    public abstract class SensorControllerBase : MonoBehaviour
    {
#pragma warning disable 0414    //The private field is assigned but its value is never used. (*In fact, it uses on the Android platform.)

        //(*) Required override sensor type.
        protected virtual SensorType sensorType {
            get { return SensorType.None; }
        }

        //Inspector Settings
        [SerializeField] private SensorDelay sensorDelay = SensorDelay.Normal;

        //Inspector settings
        public bool startListeningOnEnable = false;     //Automatically set listener with 'OnEnable()' (Always removed in 'OnDisable()').    //OnEnable() でリスナーを自動で登録する（OnDisable() では常に解除する）。

        //Callbacks
        [Serializable] public class SensorChangedHandler : UnityEvent<int, float[]> { }   //SensorType, Values (Common to all sensors)
        public SensorChangedHandler OnSensorChanged;

        [Serializable] public class ErrorHandler : UnityEvent<string> { }       //error state messate
        public ErrorHandler OnError;

#region Properties and Local values Section

        //Properties
        private bool isSupportedSensor = false;     //Cached supported Sensor.
        private bool isSupportedChecked = false;    //Already checked.

        public bool IsSupportedSensor {
            get {
                if (!isSupportedChecked)
                {
#if UNITY_EDITOR
                    isSupportedSensor = true;       //For Editor
#elif UNITY_ANDROID
                    isSupportedSensor = AndroidPlugin.IsSupportedSensor(sensorType);
#endif
                    isSupportedChecked = true;
                }
                return isSupportedSensor;
            }
        }

#endregion

        // Use this for initialization
        protected void Start()
        {
            if (!IsSupportedSensor)
            {
                if (OnError != null)
                    OnError.Invoke("Not supported: " + sensorType.ToString());
            }
        }

        protected void OnEnable()
        {
            if (startListeningOnEnable)
                StartListening();
        }

        protected void OnDisable()
        {
            StopListening();
        }

        protected void OnDestroy()
        {
            StopListening();
        }

        protected void OnApplicationQuit()
        {
#if UNITY_EDITOR
            Debug.Log("AndroidPlugin.ReleaseSensors called.");
#elif UNITY_ANDROID
            AndroidPlugin.ReleaseSensors();
#endif
        }


        // Update is called once per frame
        //protected void Update()
        //{

        //}


        //Set listener for sensor values acquisition.
        //センサーの値取得のリスニングを開始する
        public virtual void StartListening()
        {
            if (!IsSupportedSensor)
                return;
#if UNITY_EDITOR
            Debug.Log(sensorType.ToString() + "Controller.StartListening called");
#elif UNITY_ANDROID
            AndroidPlugin.SetSensorListener(sensorType, sensorDelay, gameObject.name, "ReceiveValues");
#endif
        }

        //Remove (release) listener for sensor values acquisition.
        //センサーの値取得のリスニングを停止（解放）する
        public virtual void StopListening()
        {
            if (!IsSupportedSensor)
                return;
#if UNITY_EDITOR
            Debug.Log(sensorType.ToString() + "Controller.StopListening called");
#elif UNITY_ANDROID
            AndroidPlugin.RemoveSensorListener(sensorType);
#endif
        }

        protected SensorInfo info;

        //Callback handler for sensor values.
        protected virtual void ReceiveValues(string json)
        {
            if (string.IsNullOrEmpty(json))
                return;

            info = JsonUtility.FromJson<SensorInfo>(json);

            if (OnSensorChanged != null)
                OnSensorChanged.Invoke(info.type, info.values);
        }
    }
}
