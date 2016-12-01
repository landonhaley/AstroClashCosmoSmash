using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class InputControlData
    {
        public int DeviceNumber;
        public string XAxisName;
        public string YAxisName;
        public string ButtonName;
        public float XAxis = 0;
        public float YAxis = 0;
		public bool ButtonDown = false;
        public bool ButtonUp = false;
        public delegate void InputAction(float x, float y, bool buttonDown, bool buttonUp);    
        public InputAction InputActionHandler;
        public bool UpdateInputs()
        {
            bool isChanged = false;

            float tempXAxis = Input.GetAxis (XAxisName);
			float tempYAxis = Input.GetAxis (YAxisName);
			ButtonDown= Input.GetButtonDown(ButtonName);
			ButtonUp= Input.GetButtonUp(ButtonName);

            if(tempXAxis != XAxis || YAxis != tempYAxis || ButtonUp || ButtonDown)
                isChanged = true;

            XAxis = Input.GetAxis (XAxisName);
			YAxis = Input.GetAxis (YAxisName);

            return isChanged;
        }

        public void RemoveInputAction(InputAction action)
        {
            if (InputActionHandler != null)
            {
                InputActionHandler -= action;
            }
        }

        public void RegisterInputAction(InputAction action)
        {
            if(InputActionHandler == null)
            {
                InputActionHandler = action;
            }
            else
            {
                InputActionHandler += action;
            }
        }

        public void RaiseInputAction()
        {
                if(InputActionHandler != null)
                    InputActionHandler(XAxis,YAxis,ButtonDown,ButtonUp);
        }
    }

    public class InputControl:Singleton<InputControl>
    {
		public bool inputEnabled = true;
        List<InputControlData> _inputDataDictionary = new List<InputControlData>();

        public void Awake()
        {
            InputControlData data1  = new InputControlData(); 
            data1.DeviceNumber = 0;
            data1.XAxisName = "Horiz_P1";
            data1.YAxisName = "Vert_P1";
            data1.ButtonName = "Push_P1";
			
            InputControlData data2  = new InputControlData(); 
            data2.DeviceNumber = 1;
            data2.XAxisName = "Horiz_P2";
            data2.YAxisName = "Vert_P2";
            data2.ButtonName = "Push_P2";

            InputControlData data3  = new InputControlData(); 
            data3.DeviceNumber = 2;
            data3.XAxisName = "Horiz_P3";
            data3.YAxisName = "Vert_P3";
            data3.ButtonName = "Push_P3";

            InputControlData data4  = new InputControlData(); 
            data4.DeviceNumber = 3;
            data4.XAxisName = "Horiz_P4";
            data4.YAxisName = "Vert_P4";
            data4.ButtonName = "Push_P4";

            _inputDataDictionary.Add(data1);
            _inputDataDictionary.Add(data2);
            _inputDataDictionary.Add(data3);
            _inputDataDictionary.Add(data4);
        }

        public void RegisterInputEvent(int index, InputControlData.InputAction action)
        {
            foreach(InputControlData data in _inputDataDictionary)
            {
                if(data.DeviceNumber == index)
                {
                    data.RegisterInputAction(action);
                }
            }
        }

        public void UnRegisterInputEvent(int index, InputControlData.InputAction action)
        {
            foreach (InputControlData data in _inputDataDictionary)
            {
                if (data.DeviceNumber == index)
                {
                    data.RemoveInputAction(action);
                }
            }
        }

        public void Update()
        {
			if (inputEnabled) {
				foreach (InputControlData data in _inputDataDictionary) {
					if (data.UpdateInputs ()) {
						data.RaiseInputAction ();
					}
				}
			}
        }

        public override     void Destroyed()
        { 
            //
        }

}
