using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WearableSensorFederation;
using WearableSensorFederation.Som;
using Racon;
using Racon.RtiLayer;
namespace DataCollectorFdApp
{

    class Program
    {
        static CDataColelctorFdApp federate; //Application-specific federate 
                                             // Local data structures
                                             // TODO: user-defined data structures are declared here

        static BindingList<CSensorHlaObject> sensors;
        static BindingList<CSinkDeviceHlaObject> sinkDevices;
        static BindingList<CDataCollectorHlaObject> cDataCollectors;
        bool sensorSyncPoint;

        static void Main(string[] args)
        {
            CSimulationManager manager = new CSimulationManager();
            /*sensors = new BindingList<CSensorHlaObject>();
            sinkDevices = new BindingList<CSinkDeviceHlaObject>();
            cDataCollectors = new BindingList<CDataCollectorHlaObject>();
            // Initialize the application-specific federate
            federate = new CDataColelctorFdApp();
            // Initialize the federation execution
            federate.FederationExecution.Name = "WearableSensorFederation";
            federate.FederationExecution.FederateType = "DataCollectorFederate";
            federate.FederationExecution.ConnectionSettings = "rti://127.0.0.1";
            // Handle RTI type variation
            federate.LogLevel = LogLevel.ALL;
            federate.InteractionReceived += federate.FdAmb_InteractionReceivedHandler;
            federate.StatusMessageChanged += new EventHandler(StatusMessage);
            federate.FederateStateChanged += Federate_StatusMessageChanged;
            federate.Connect(callbackModel: Racon.CallbackModel.IMMEDIATE, "");
            federate.CreateFederationExecution("WearableSensorFederation", "WSFedFOM.xml", "");
            federate.JoinFederationExecution("DataCollectorFederate", federate.FederationExecution.FederateType, "WearableSensorFederation");
            BeginSimulation();
        }
        private static void BeginSimulation()
        {

            }
        }
        private static void Federate_StatusMessageChanged(object sender, EventArgs e)
        {
            Console.WriteLine((sender as CDataColelctorFdApp).StatusMessage);
        }
        private static void StatusMessage(object sender, EventArgs e)
        {
            Console.WriteLine(federate.StatusMessage);
        }
        */

        }
    }
}
