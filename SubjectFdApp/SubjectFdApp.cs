// **************************************************************************************************
//		CSubjectFdApp
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

    public partial class CSubjectFdApp : Racon.CGenericFederate
    {
        #region Manually Added Code
        private object thisLock = new object();
        // Local Data
        private CSimulationManager manager;
        private int maximum = 7;
        private int minimum = 1;
        private int _federate_count;
        public int federate_count
        {
            get { return _federate_count; }
            set
            {
                _federate_count = value;
            }
        }
        #region Constructor
        public CSubjectFdApp(CSimulationManager parent) : this()
        {
            manager = parent; // Set simulation manager
                              // Create regions manually
        }
        #endregion //Constructor
        public override void FdAmb_StartRegistrationForObjectClassAdvisedHandler(object sender, HlaDeclarationManagementEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_StartRegistrationForObjectClassAdvisedHandler(sender, data);
            #region User Code
            foreach (var item in manager.sensors)
            {
                if (data.ObjectClassHandle == Som.SensorOC.Handle)
                {
                    RegisterHlaObject(item);

                }
            }
            if (data.ObjectClassHandle == Som.SinkDeviceOC.Handle)
            {
                RegisterHlaObject(manager.sDevices[0]);
            }

            #endregion //User Code
        }
        public override void FdAmb_TurnInteractionsOnAdvisedHandler(object sender, HlaDeclarationManagementEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_TurnInteractionsOnAdvisedHandler(sender, data);

            #region User Code
            Console.WriteLine("Interactions on Advised Handler for SubjectFdApp\n");
            #endregion //User Code
        }
        public override void FdAmb_InteractionReceivedHandler(object sender, HlaInteractionEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_InteractionReceivedHandler(sender, data);
            string send = "";
            string val = "";
            var time = new DateTime();
            #region User Code
            if (string.Compare(data.GetParameterValue<string>(Som.SensoryMessageIC.SenderID), manager.subject_id) == 0)
            {
                if (data.Interaction.ClassHandle == Som.SensoryMessageIC.Handle)
                {
                    Console.Write("Message Interaction Class has been recieved by the Sink Device!\n");
                    if (data.IsValueUpdated(Som.SensoryMessageIC.SenderID))
                    {
                        send = data.GetParameterValue<string>(Som.SensoryMessageIC.SenderID);
                    }
                    if (data.IsValueUpdated(Som.SensoryMessageIC.Content))
                    {
                        val = data.GetParameterValue<string>(Som.SensoryMessageIC.Content);

                    }
                    if (data.IsValueUpdated(Som.SensoryMessageIC.TimeStamp))
                    {
                        time = data.GetParameterValue<DateTime>(Som.SensoryMessageIC.TimeStamp);
                    }
                    Console.WriteLine(send + " " + val + "\n");
                    federate_count += 1;
                }
            }
            #endregion //User Code
        }
        // FdAmb_ObjectDiscoveredHandler
        public override void FdAmb_ObjectDiscoveredHandler(object sender, HlaObjectEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_ObjectDiscoveredHandler(sender, data);

            #region User Code
            if (data.ClassHandle == Som.SensorOC.Handle)
            {
                CSensorHlaObject test = new CSensorHlaObject(data.ObjectInstance);
                test.Type = Som.SensorOC;
                manager.sensors.Add(test);
                RegisterHlaObject(test);
                RequestAttributeValueUpdate(test, null, "");
            }
            else if (data.ClassHandle == Som.SinkDeviceOC.Handle)
            {
                string sid = data.GetAttributeValue<string>(Som.SinkDeviceOC.SubjectID);
                if (string.Compare(sid, manager.subject_id) == 0)
                {
                    CSinkDeviceHlaObject test = new CSinkDeviceHlaObject(data.ObjectInstance);
                    test.Type = Som.SinkDeviceOC;
                    manager.sDevices.Add(test);
                    RegisterHlaObject(test);
                    RequestAttributeValueUpdate(test, null, "");
                }

            }

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
                snap = manager.sDevices.ToArray();
            }
            foreach (CSinkDeviceHlaObject sdev in snap)
            {
                manager.sDevices.Remove(sdev);
                Console.WriteLine($"Removed Sink Device {sdev.device.deviceID} !!\n");
            }

            #endregion //User Code
        }
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
            foreach (var item in manager.sDevices)
            {
                if (item.Handle == data.ObjectInstance.Handle)
                {
                    if (data.IsValueUpdated(Som.SinkDeviceOC.DeviceID))
                    {
                        item.device.deviceID = data.GetAttributeValue<string>(Som.SinkDeviceOC.DeviceID);
                    }
                    if (data.IsValueUpdated(Som.SinkDeviceOC.SubjectID))
                    {
                        item.device.subjectID = data.GetAttributeValue<string>(Som.SinkDeviceOC.SubjectID);
                    }
                }
            }

            #endregion //User Code
        }
        // FdAmb_StopRegistrationForObjectClassAdvisedHandler
        public override void FdAmb_StopRegistrationForObjectClassAdvisedHandler(object sender, HlaDeclarationManagementEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_StopRegistrationForObjectClassAdvisedHandler(sender, data);

            #region User Code

            #endregion //User Code
        }
        // FdAmb_TurnInteractionsOffAdvisedHandler
        public override void FdAmb_TurnInteractionsOffAdvisedHandler(object sender, HlaDeclarationManagementEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_TurnInteractionsOffAdvisedHandler(sender, data);

            #region User Code

            #endregion //User Code
        }

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
            if (data.ObjectInstance.Handle == manager.sDevices[0].Handle)
            {
                foreach (var item in data.ObjectInstance.Attributes)
                {
                    if (item.Handle == Som.SinkDeviceOC.SubjectID.Handle)
                    {
                        UpdateSinkDeviceSubjectID(manager.sDevices[0]);
                    }
                    if (item.Handle == Som.SinkDeviceOC.DeviceID.Handle)
                    {
                        UpdateDeviceID(manager.sDevices[0]);
                    }
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

            #region User Code
            string sid = data.Tag;

            if (string.Compare(data.Label, "AllSensorsConnected") == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Random rng = new Random(Guid.NewGuid().GetHashCode());
                    string sensory_message;
                    if (string.Compare(manager.sensors[i].sensor.SensorType, "ECG") != 0)
                    {
                        double x = rng.NextDouble() * (maximum - minimum) + minimum;
                        double y = rng.NextDouble() * (maximum - minimum) + minimum;
                        double z = rng.NextDouble() * (maximum - minimum) + minimum;
                        sensory_message = "(" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ")";
                    }
                    else
                    {
                        double lead = rng.NextDouble() * (maximum - minimum) + minimum;
                        sensory_message = "(" + lead.ToString() + ")";

                    }

                    manager.federate.SendSensoryMessage(sensory_message, manager.subject_id);
                }
                manager.federate.SynchronizationPointAchieved("AllSensorsConnected", true);
                manager.test = true;

            }
            else
            {
                Random rng = new Random(Guid.NewGuid().GetHashCode());
                int model1_r = rng.Next(1, 3);
                int model2_r = rng.Next(1, 3);

                string message = "(" + model1_r.ToString() + "," + model2_r.ToString() + ")";
                manager.federate.SendSinkDeviceMessage(message, data.Tag);

                manager.federate.SynchronizationPointAchieved("AllUserDataObtained", true);

            }

            #endregion //User Code
        }
        // FdAmb_FederationSynchronized
        public override void FdAmb_FederationSynchronized(object sender, HlaFederationManagementEventArgs data)
        {
            // Call the base class handler
            base.FdAmb_FederationSynchronized(sender, data);

            #region User Code
            Report($"({data.Label}) is completed." + Environment.NewLine);
            if (string.Compare(data.Label, "AllUserDataObtained") == 0)
            {
                manager.mainloop = false;
            }
            #endregion //User Code
        }

        private void Report(string v)
        {
            Console.WriteLine(v);
        }
        public bool SendSensoryMessage(string sender_id, string sensory_data)
        {
            HlaInteraction sensory_message = new Racon.RtiLayer.HlaInteraction(Som.SensoryMessageIC, "SensoryMessage");
            sensory_message.AddParameterValue(Som.SensoryMessageIC.SenderID, sender_id);
            sensory_message.AddParameterValue(Som.SensoryMessageIC.Content, sensory_data);
            sensory_message.AddParameterValue(Som.SensoryMessageIC.TimeStamp, new DateTime());
            return SendInteraction(sensory_message, "");
        }
        public bool SendSinkDeviceMessage(string sender_id, string sensory_data)
        {
            HlaInteraction sensory_message = new Racon.RtiLayer.HlaInteraction(Som.SinkDeviceMessageIC, "SinkDeviceMessage");
            sensory_message.AddParameterValue(Som.SinkDeviceMessageIC.SenderID, sender_id);
            sensory_message.AddParameterValue(Som.SinkDeviceMessageIC.Content, sensory_data);
            sensory_message.AddParameterValue(Som.SinkDeviceMessageIC.TimeStamp, new DateTime());
            return SendInteraction(sensory_message, "");
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
