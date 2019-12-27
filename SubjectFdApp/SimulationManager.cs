// **************************************************************************************************
//		CSimulationManager
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
/// The Simulation Manager manages the (multiple) federation execution(s) and the (multiple instances of) joined federate(s).
/// </summary>

// System
using System;
using System.Collections.Generic; // for List
// Racon
using Racon;
using Racon.RtiLayer;
// Application
using System.ComponentModel;
using WearableSensorFederation.Som;
using SubjectFdApp;

namespace WearableSensorFederation
{
    public class CSimulationManager
    {
        #region Declarations
        // Communication layer related structures
        public string subject_id = Guid.NewGuid().ToString();



        public CSubjectFdApp federate; //Application-specific federate 
        public BindingList<CSensorHlaObject> sensors;
        public BindingList<CSinkDeviceHlaObject> sDevices;
        private bool sensorSyncPoint = false;
        public bool test = false;
        public bool objects_registered = false;
        internal object timer;
        public bool mainloop = true;
        public bool first_point = false;
        public bool second_point = false;



        #endregion //Declarations

        #region Constructor
        public CSimulationManager()
        {
            // Initialize the application-specific federate
            federate = new CSubjectFdApp(this);
            sensors = new BindingList<CSensorHlaObject>();
            sDevices = new BindingList<CSinkDeviceHlaObject>();
            // Initialize the federation execution
            federate.LogLevel = LogLevel.ALL;
            federate.InteractionReceived += federate.FdAmb_InteractionReceivedHandler;
            federate.StatusMessageChanged += new EventHandler(StatusMessage);
            federate.FederateStateChanged += Federate_StatusMessageChanged;
            federate.FederationExecution.Name = "WearableSensorFederation";
            federate.FederationExecution.FederateType = "SubjectFederate";
            federate.FederationExecution.ConnectionSettings = "rti://127.0.0.1";
            // Handle RTI type variation
            initialize();
            federate.Connect(callbackModel: Racon.CallbackModel.IMMEDIATE, "");
            federate.CreateFederationExecution("WearableSensorFederation", "WSFedFOM.xml", "");
            federate.JoinFederationExecution("Subject" + subject_id, federate.FederationExecution.FederateType, "WearableSensorFederation");

            BeginSimulation();
        }
        #endregion //Constructor

        #region Methods
        // Handles naming variation according to HLA specification
        private void initialize()
        {
            switch (federate.RTILibrary)
            {
                case RTILibraryType.HLA13_DMSO:
                case RTILibraryType.HLA13_Portico:
                case RTILibraryType.HLA13_OpenRti:
                    federate.Som.SensorOC.Name = "objectRoot.Sensor";
                    federate.Som.SensorOC.PrivilegeToDelete.Name = "privilegeToDelete";
                    federate.Som.SinkDeviceOC.Name = "objectRoot.SinkDevice";
                    federate.Som.SinkDeviceOC.PrivilegeToDelete.Name = "privilegeToDelete";
                    federate.Som.DataCollectorOC.Name = "objectRoot.DataCollector";
                    federate.Som.DataCollectorOC.PrivilegeToDelete.Name = "privilegeToDelete";
                    federate.Som.MessageIC.Name = "interactionRoot.Message";
                    federate.Som.SinkDeviceMessageIC.Name = "interactionRoot.Message.SinkDeviceMessage";
                    federate.Som.SensoryMessageIC.Name = "interactionRoot.Message.SensoryMessage";
                    federate.FederationExecution.FDD = @".\WSFedFOM.fed";
                    break;
                case RTILibraryType.HLA1516e_Portico:
                case RTILibraryType.HLA1516e_OpenRti:
                    federate.Som.SensorOC.Name = "HLAobjectRoot.Sensor";
                    federate.Som.SensorOC.PrivilegeToDelete.Name = "HLAprivilegeToDeleteObject";
                    federate.Som.SinkDeviceOC.Name = "HLAobjectRoot.SinkDevice";
                    federate.Som.SinkDeviceOC.PrivilegeToDelete.Name = "HLAprivilegeToDeleteObject";
                    federate.Som.DataCollectorOC.Name = "HLAobjectRoot.DataCollector";
                    federate.Som.DataCollectorOC.PrivilegeToDelete.Name = "HLAprivilegeToDeleteObject";
                    federate.Som.MessageIC.Name = "HLAinteractionRoot.Message";
                    federate.Som.SinkDeviceMessageIC.Name = "HLAinteractionRoot.Message.SinkDeviceMessage";
                    federate.Som.SensoryMessageIC.Name = "HLAinteractionRoot.Message.SensoryMessage";
                    federate.FederationExecution.FDD = @".\WSFedFOM.xml";
                    break;
            }
        }
        private static void Federate_StatusMessageChanged(object sender, EventArgs e)
        {
            Console.WriteLine((sender as CSubjectFdApp).StatusMessage);
        }
        private void StatusMessage(object sender, EventArgs e)
        {
            Console.WriteLine(federate.StatusMessage);
        }
        private void BeginSimulation()
        {

            federate.DeclareCapability();
            federate.Run();
            
            Console.WriteLine("\n\n\n\n-----------Welcome to the Wearable Sensor Simulation-----------\n\n\n\n");
            Console.WriteLine("\nPlease enter 1 to start or 0 for exit!\nEnter: " +
                "");
            if (Console.ReadKey().Key == ConsoleKey.D1)
            {
                Random rng = new Random(Guid.NewGuid().GetHashCode());



                CSinkDeviceHlaObject sdev = new CSinkDeviceHlaObject(federate.Som.SinkDeviceOC);



                for (int i = 0; i < 5; i++)
                {
                    /*Type update*/
                    CSensorHlaObject sens = new CSensorHlaObject(federate.Som.SensorOC);
                    sensors.Add(sens);
                    int temp = rng.Next(1, 8);
                    sensors[i].sensor.SensorID = subject_id + "_" + Guid.NewGuid().ToString();
                    if (temp == 1)
                    {
                        sensors[i].sensor.SensorType = "Accelerometer";
                        sensors[i].sensor.SensorLocation = "Chest";
                    }
                    else if (temp == 2)
                    {
                        sensors[i].sensor.SensorLocation = "Heart";
                        sensors[i].sensor.SensorType = "ECG";
                    }
                    else if (temp == 3)
                    {
                        sensors[i].sensor.SensorType = "Accelerometer";
                        sensors[i].sensor.SensorLocation = "Left-Ankle";
                    }

                    else if (temp == 4)
                    {
                        sensors[i].sensor.SensorType = "Gyroscope";
                        sensors[i].sensor.SensorLocation = "Left-Ankle";
                    }

                    else if (temp == 5)
                    {
                        sensors[i].sensor.SensorType = "Magnetometer";
                        sensors[i].sensor.SensorLocation = "Left-Ankle";
                    }

                    else if (temp == 6)
                    {
                        sensors[i].sensor.SensorType = "Accelerometer";
                        sensors[i].sensor.SensorLocation = "Right-Lower-Arm";
                    }
                    else if (temp == 7)
                    {
                        sensors[i].sensor.SensorType = "Gyroscope";
                        sensors[i].sensor.SensorLocation = "Right-Lower-Arm";
                    }

                    else if (temp == 8)
                    {
                        sensors[i].sensor.SensorType = "Magnetometer";
                        sensors[i].sensor.SensorLocation = "Right-Lower-Arm";
                    }
                    Console.WriteLine("{0} Sensor at {1} is generated!\n", sensors[i].sensor.SensorType, sensors[i].sensor.SensorLocation);
                }
                sdev.device = new CSinkDevice();
                sdev.device.subjectID = subject_id;
                sdev.device.deviceID = subject_id + "_" + Guid.NewGuid().ToString();
                sDevices.Add(sdev);
                Console.WriteLine("{0} Device at is generated!\n", sdev.device.deviceID);
                do
                {
                    federate.Run();
                    if(first_point && second_point)
                    {
                        Console.WriteLine("All Sync Points Achieved!\nDo you want to exit) (Y/n): ");
                        if(Console.ReadKey().Key == ConsoleKey.Y)
                        {
                            mainloop = false;
                        }
                    }

                } while (mainloop);

                federate.ResignFederationExecution(action: ResignAction.CANCEL_PENDING_OWNERSHIP_ACQUISITIONS);
                federate.DestroyFederationExecution("WearableSensorFederation");
                federate.Disconnect();
                Console.WriteLine("Press any key to close!");
                Console.ReadLine();
            }
        }
        #endregion //Methods
    }
}
