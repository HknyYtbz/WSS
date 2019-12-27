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
using System.ComponentModel;
// Racon
using Racon;
using Racon.RtiLayer;
// Application
using WearableSensorFederation.Som;

namespace WearableSensorFederation
{
    public class CSimulationManager
    {
        #region Declarations
        // Communication layer related structures
        public CDataColelctorFdApp federate; //Application-specific federate 
                                             // Local data structures
                                             // TODO: user-defined data structures are declared here
        public BindingList<CSensorHlaObject> sensors;
        public BindingList<CSinkDeviceHlaObject> sinkDevices;
        public BindingList<CDataCollectorHlaObject> cDataCollectors;
        #endregion //Declarations
        public bool first_point = false;
        public bool second_point = false;
        #region Constructor
        public CSimulationManager()
        {
            
            sensors = new BindingList<CSensorHlaObject>();
            sinkDevices = new BindingList<CSinkDeviceHlaObject>();
            cDataCollectors = new BindingList<CDataCollectorHlaObject>();
            // Initialize the application-specific federate
            federate = new CDataColelctorFdApp(this);
            // Initialize the federation execution
            federate.FederationExecution.Name = "WearableSensorFederation";
            federate.FederationExecution.FederateType = "DataCollectorFederate";
            federate.FederationExecution.ConnectionSettings = "rti://127.0.0.1";
            // Handle RTI type variation
            initialize();
            federate.LogLevel = LogLevel.ALL;
            federate.InteractionReceived += federate.FdAmb_InteractionReceivedHandler;
            federate.StatusMessageChanged += new EventHandler(StatusMessage);
            federate.FederateStateChanged += Federate_StatusMessageChanged;
            federate.Connect(callbackModel: Racon.CallbackModel.IMMEDIATE, "");
            federate.CreateFederationExecution("WearableSensorFederation", "WSFedFOM.xml", "");
            federate.JoinFederationExecution("DataCollectorFederate", federate.FederationExecution.FederateType, "WearableSensorFederation");
            BeginSimulation();

        }
        #endregion //Constructor
        private static void Federate_StatusMessageChanged(object sender, EventArgs e)
        {
            Console.WriteLine((sender as CDataColelctorFdApp).StatusMessage);
        }
        private void StatusMessage(object sender, EventArgs e)
        {
            Console.WriteLine(federate.StatusMessage);
        }
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
        private void BeginSimulation()
        {
            federate.DeclareCapability();
            federate.Run();
            bool mainloop = true;
            Console.WriteLine("\n\n\n\n-----------Welcome to the Wearable Sensor Simulation-----------\n\n\n\n");
            Console.WriteLine("\nPlease enter 1 to start or 0 for exit!\nEnter: " +
                "");
            if (Console.ReadKey().Key == ConsoleKey.D1)
            {
                CDataCollectorHlaObject datac = new CDataCollectorHlaObject(federate.Som.DataCollectorOC);
                cDataCollectors.Add(datac);
                federate.DeclareCapability();
                cDataCollectors.Add(datac);
                do
                {
                    federate.Run();
                    if(federate.federate_count >= 2 && !first_point && !second_point)
                    {
                        Console.Write("Registering the first point!\n\n");
                        federate.RegisterFederationSynchronizationPoint("AllSensorsConnected", "RegisteringForSensorData");
                      
                    }
                    else
                    {
                        //mainloop = false;
                        Console.WriteLine("Press 1 to continue, 2 to exit!\n");
                        if(Console.ReadKey().Key == ConsoleKey.D2)
                        {
                            mainloop = false;
                        }
                    }

                } while (mainloop);
                //timer.Stop(); // stop reporting the ship position

                federate.ResignFederationExecution(action: ResignAction.CANCEL_PENDING_OWNERSHIP_ACQUISITIONS);
                federate.DestroyFederationExecution("WearableSensorFederation");
                federate.Disconnect();
                Console.WriteLine("Mission Complete!\nTerminating Federation!!\n");
                Console.WriteLine("Press any key to close!");
                Console.ReadLine();
            }
        }
        #endregion //Methods
    }
}
