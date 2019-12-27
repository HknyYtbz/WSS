// **************************************************************************************************
//		CDataColelctorFdApp
//
//		generated
//			by		: 	Simulation Generator (SimGe) v.0.3.2
//			at		: 	18 December, 2019 3:43:05 PM
//		compatible with		: 	RACoN v.0.0.2.5
//
//		copyright		: 	(C) 
//		email			: 	
// **************************************************************************************************
/// <summary>
/// The application specific federate that is extended from the Generic Federate Class of RACoN API. This file is intended for manual code operations.
/// </summary>

// System
using System;
using System.Collections.Generic; // for List
using System.Linq;
// Racon
using Racon;
using Racon.RtiLayer;
// Application
using WearableSensorFederation.Som;

namespace WearableSensorFederation
{
    public partial class CDataColelctorFdApp : Racon.CGenericFederate
    {
        #region Manually Added Code
        
        // Local Data
        private CSimulationManager manager;
        private object thisLock;
        private int _federate_count;
        public int federate_count
        {
            get { return _federate_count; }
            set
            {
                _federate_count = value;

            }
        }
        private int _sensor_count;
        public int sensor_count
        {
            get { return _sensor_count; }
            set
            {
                _sensor_count = value;
                if(_sensor_count >= 2)
                {
                    manager.second_point = true;
                }
            }
        }
        #region Constructor
        public CDataColelctorFdApp(CSimulationManager parent) : this()
        {
            manager = parent; // Set simulation manager
                              // Create regions manually
        }
        #endregion //Constructor
        #region Declaration Management Callbacks
        // FdAmb_StartRegistrationForObjectClassAdvisedHandler
        public override void FdAmb_StartRegistrationForObjectClassAdvisedHandler(object sender, HlaDeclarationManagementEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_StartRegistrationForObjectClassAdvisedHandler(sender, data);

            Console.WriteLine("Start Registraion Advised Handler for DataCollectorApp\n");

        }
        // FdAmb_StopRegistrationForObjectClassAdvisedHandler
        public override void FdAmb_StopRegistrationForObjectClassAdvisedHandler(object sender, HlaDeclarationManagementEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_StopRegistrationForObjectClassAdvisedHandler(sender, data);

            #region User Code
            Console.WriteLine("Stop Registraion Advised Handler for DataCollectorApp\n");
            #endregion //User Code
        }
        // FdAmb_TurnInteractionsOffAdvisedHandler
        public override void FdAmb_TurnInteractionsOffAdvisedHandler(object sender, HlaDeclarationManagementEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_TurnInteractionsOffAdvisedHandler(sender, data);

            #region User Code
            Console.WriteLine("Interactions off Advised Handler for DataCollectorApp\n");
            #endregion //User Code
        }
        // FdAmb_TurnInteractionsOnAdvisedHandler
        public override void FdAmb_TurnInteractionsOnAdvisedHandler(object sender, HlaDeclarationManagementEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_TurnInteractionsOnAdvisedHandler(sender, data);

            Console.WriteLine("Interactions on Advised Handler for DataCollectorApp\n");
        }
        #endregion //Declaration Management Callbacks
        public override void FdAmb_ObjectDiscoveredHandler(object sender, HlaObjectEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_ObjectDiscoveredHandler(sender, data);

            #region User Code
            #region User Code
            foreach (var item in manager.sensors)
            {

                #region User Code
                if (data.ClassHandle == Som.SensorOC.Handle)
                {
                    CSensorHlaObject test = new CSensorHlaObject(data.ObjectInstance);
                    test.Type = Som.SensorOC;
                    manager.sensors.Add(test);
                    RegisterHlaObject(test);
                }

            }
            foreach (var item in manager.sinkDevices)
            {
                if (data.ClassHandle == Som.SinkDeviceOC.Handle)
                {
                    CSinkDeviceHlaObject test = new CSinkDeviceHlaObject(data.ObjectInstance);
                    test.Type = Som.SinkDeviceOC;
                    manager.sinkDevices.Add(test);
                    RegisterHlaObject(test);
                  
                }
            }
            federate_count += 1;
            #endregion //User Code
        }
        // FdAmb_ObjectRemovedHandler
        public override void FdAmb_ObjectRemovedHandler(object sender, HlaObjectEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_ObjectRemovedHandler(sender, data);

            #region User Code
            object[] snap;
            lock (thisLock)
            {
                snap = manager.sensors.ToArray();
            }
            foreach (CSensorHlaObject sens in snap)
            {
                manager.sensors.Remove(sens);
                Console.WriteLine($"Removed Sensor: {sens.sensor.SensorID} !!\n");
            }
            lock (thisLock)
            {
                snap = manager.sinkDevices.ToArray();
            }
            foreach (CSinkDeviceHlaObject sdev in snap)
            {
                manager.sinkDevices.Remove(sdev);
                Console.WriteLine($"Removed Sink Device {sdev.device.deviceID} !!\n");
            }

            #endregion //User Code
        }
        // FdAmb_AttributeValueUpdateRequestedHandler
        public override void FdAmb_AttributeValueUpdateRequestedHandler(object sender, HlaObjectEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_AttributeValueUpdateRequestedHandler(sender, data);

            #region User Code
            for (int i = 0; i < manager.sensors.Count; i++)
            {
                if (data.ObjectInstance.Handle == manager.sensors[i].Handle)
                {
                    foreach (var item in data.ObjectInstance.Attributes)
                    {
                        if (item.Handle == Som.SensorOC.SensorID.Handle)
                        {
                            UpdateSensorID(manager.sensors[i]);
                        }
                        if (item.Handle == Som.SensorOC.SensorLocation.Handle)
                        {
                            UpdateSensorLocation(manager.sensors[i]);
                        }
                        if (item.Handle == Som.SensorOC.SubjectID.Handle)
                        {
                            UpdateSensorSubjectID(manager.sensors[i]);
                        }
                        if (item.Handle == Som.SensorOC.SensorType.Handle)
                        {
                            UpdateSensorType(manager.sensors[i]);
                        }
                    }
                }
            }
            for (int i = 0; i < manager.sinkDevices.Count; i++)
            {
                if (data.ObjectInstance.Handle == manager.sinkDevices[i].Handle)
                {
                    foreach (var item in data.ObjectInstance.Attributes)
                    {
                        if (item.Handle == Som.SinkDeviceOC.SubjectID.Handle)
                        {
                            UpdateSinkDeviceSubjectID(manager.sinkDevices[i]);
                        }
                        if (item.Handle == Som.SinkDeviceOC.DeviceID.Handle)
                        {
                            UpdateDeviceID(manager.sinkDevices[i]);
                        }
                    }
                }
            }

            #endregion //User Code
        }
        // FdAmb_ObjectAttributesReflectedHandler
        public override void FdAmb_ObjectAttributesReflectedHandler(object sender, HlaObjectEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_ObjectAttributesReflectedHandler(sender, data);

            #region User Code
            foreach (var item in manager.sensors)
            {
                if (item.Handle == data.ObjectInstance.Handle)
                {
                    if (data.IsValueUpdated(Som.SensorOC.SensorLocation))
                    {
                        item.sensor.SensorLocation = data.GetAttributeValue<string>(Som.SensorOC.SensorLocation);
                    }
                    if (data.IsValueUpdated(Som.SensorOC.SensorID))
                    {
                        item.sensor.SensorID = data.GetAttributeValue<string>(Som.SensorOC.SensorID);
                    }
                    if (data.IsValueUpdated(Som.SensorOC.SensorType))
                    {
                        item.sensor.SensorID = data.GetAttributeValue<string>(Som.SensorOC.SensorType);
                    }
                    if (data.IsValueUpdated(Som.SensorOC.SubjectID))
                    {
                        item.sensor.SensorID = data.GetAttributeValue<string>(Som.SensorOC.SubjectID);
                    }
                }
            }
            foreach (var item in manager.sinkDevices)
            {
                if (item.Handle == data.ObjectInstance.Handle)
                {
                    if (data.IsValueUpdated(Som.SinkDeviceOC.SubjectID))
                    {
                        item.device.subjectID = data.GetAttributeValue<string>(Som.SinkDeviceOC.SubjectID);
                    }
                    if (data.IsValueUpdated(Som.SinkDeviceOC.DeviceID))
                    {
                        item.device.deviceID = data.GetAttributeValue<string>(Som.SinkDeviceOC.DeviceID);
                    }
                }
            }
            #endregion //User Code
        }
        // FdAmb_InteractionReceivedHandler
        public override void FdAmb_InteractionReceivedHandler(object sender, HlaInteractionEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_InteractionReceivedHandler(sender, data);

            #region User Code
            string send = "";
            string val = "";
            var time = new DateTime();
            #region User Code
            if (data.Interaction.ClassHandle == Som.SinkDeviceMessageIC.Handle)
            {
                Console.Write("Message Interaction Class has been recieved!\n");
                if (data.IsValueUpdated(Som.SinkDeviceMessageIC.SenderID))
                {
                    send = data.GetParameterValue<string>(Som.SinkDeviceMessageIC.SenderID);
                }
                if (data.IsValueUpdated(Som.SinkDeviceMessageIC.Content))
                {
                    val = data.GetParameterValue<string>(Som.SinkDeviceMessageIC.Content);

                }
                if (data.IsValueUpdated(Som.SinkDeviceMessageIC.TimeStamp))
                {
                    time = data.GetParameterValue<DateTime>(Som.SinkDeviceMessageIC.TimeStamp);

                }
                Console.WriteLine(" \n" + send + " " + val + time.ToString() +" \n");
                sensor_count += 1;
                if(sensor_count == 5)
                {
                    manager.second_point = true;
                }
            }
            #endregion //User Code
        }
        
        public override void FdAmb_OnSynchronizationPointRegistrationConfirmedHandler(object sender, HlaFederationManagementEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_OnSynchronizationPointRegistrationConfirmedHandler(sender, data);

            #region User Code
            Report($"Point: ({data.Label}) is accepted by RTI." + Environment.NewLine);
            #endregion //User Code
        }
        // FdAmb_OnSynchronizationPointRegistrationFailedHandler
        public override void FdAmb_OnSynchronizationPointRegistrationFailedHandler(object sender, HlaFederationManagementEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_OnSynchronizationPointRegistrationFailedHandler(sender, data);

            #region User Code
            Console.WriteLine($"{data.Label} Syncronization Failed!\n");
            #endregion //User Code
        }
        // FdAmb_SynchronizationPointAnnounced
        public override void FdAmb_SynchronizationPointAnnounced(object sender, HlaFederationManagementEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_SynchronizationPointAnnounced(sender, data);
            if(string.Compare(data.Label, "AllSensorsConnected") ==0)
            {
                SynchronizationPointAchieved("AllSensorsConnected", true);
                manager.first_point = true;
            }  
            #region User Code
            
            #endregion //User Code
        }
        // FdAmb_FederationSynchronized
        public override void FdAmb_FederationSynchronized(object sender, HlaFederationManagementEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_FederationSynchronized(sender, data);

            #region User Code
            if(string.Compare(data.Label,"AllSensorsConnected")==0)
            {
                manager.first_point = true;
                Console.WriteLine("First Sync Point Achieved!}\n");
                Console.WriteLine("Advancing thorugh Second Point");
                Console.Write("Registering the second point!\n\n");
                RegisterFederationSynchronizationPoint("AllUserDataObtained", "RegisteringForUserData");
            }
            else
            {
                manager.second_point = true;
                Console.WriteLine("First Sync Point Achieved!}\n");
                Console.WriteLine("Advancing thorugh Second Point");
            }
            #endregion //User Code
        }


        private void Report(string v)
        {
            Console.WriteLine(v);
        }
        private void UpdateSensorID(CSensorHlaObject sens)
        {
            sens.AddAttributeValue(Som.SensorOC.SensorID, sens.sensor.SensorID);

            UpdateAttributeValues(sens, "");
        }
        private void UpdateSensorLocation(CSensorHlaObject sens)
        {
            sens.AddAttributeValue(Som.SensorOC.SensorLocation, sens.sensor.SensorLocation);
            UpdateAttributeValues(sens, "");
        }
        private void UpdateSensorSubjectID(CSensorHlaObject sens)
        {
            sens.AddAttributeValue(Som.SensorOC.SubjectID, sens.sensor.SubjectID);
            UpdateAttributeValues(sens, "");
        }
        private void UpdateSensorType(CSensorHlaObject sens)
        {
            sens.AddAttributeValue(Som.SensorOC.SensorType, sens.sensor.SensorType);
            UpdateAttributeValues(sens, "");
        }
        private void UpdateDeviceID(CSinkDeviceHlaObject sdev)
        {
            sdev.AddAttributeValue(Som.SinkDeviceOC.DeviceID, sdev.device.deviceID);
            UpdateAttributeValues(sdev, "");
        }
        private void UpdateSinkDeviceSubjectID(CSinkDeviceHlaObject sdev)
        {
            sdev.AddAttributeValue(Som.SinkDeviceOC.SubjectID, sdev.device.subjectID);
            UpdateAttributeValues(sdev, "");
        }
        #endregion //Manually Added Code
    }
}
#endregion
#endregion
#endregion