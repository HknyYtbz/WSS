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
using WearableSensorFederation.Som;

namespace WearableSensorFederation
{
  public class CSimulationManager
  {
    #region Declarations
    // Communication layer related structures
    public CSubjectFdApp federate; //Application-specific federate 
    // Local data structures
    // TODO: user-defined data structures are declared here
    #endregion //Declarations
    
    #region Constructor
    public CSimulationManager()
    {
      // Initialize the application-specific federate
      federate = new CSubjectFdApp(this);
      // Initialize the federation execution
      federate.FederationExecution.Name = "WearableSensorFederation";
      federate.FederationExecution.FederateType = "SubjectFederate";
      federate.FederationExecution.ConnectionSettings = "rti://127.0.0.1";
      // Handle RTI type variation
      initialize();
    }
    #endregion //Constructor
    
    #region Methods
    // Handles naming variation according to HLA specification
    private void initialize()
    {
      switch (federate.RTILibrary)
      {
        case RTILibraryType.HLA13_DMSO: case RTILibraryType.HLA13_Portico: case RTILibraryType.HLA13_OpenRti:
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
        case RTILibraryType.HLA1516e_Portico: case RTILibraryType.HLA1516e_OpenRti:
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
    #endregion //Methods
  }
}
