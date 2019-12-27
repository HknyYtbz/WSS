// **************************************************************************************************
//		FederateSom
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
/// This class is extended from the object model of RACoN API
/// </summary>

// System
using System;
using System.Collections.Generic; // for List
// Racon
using Racon;
using Racon.RtiLayer;
// Application
using WearableSensorFederation.Som;


namespace WearableSensorFederation.Som
{
  public class FederateSom : Racon.ObjectModel.CObjectModel
  {
    #region Declarations
    #region SOM Declaration
    public WearableSensorFederation.Som.CSensorOC SensorOC;
    public WearableSensorFederation.Som.CSinkDeviceOC SinkDeviceOC;
    public WearableSensorFederation.Som.CDataCollectorOC DataCollectorOC;
    public WearableSensorFederation.Som.CMessageIC MessageIC;
    public WearableSensorFederation.Som.CSinkDeviceMessageIC SinkDeviceMessageIC;
    public WearableSensorFederation.Som.CSensoryMessageIC SensoryMessageIC;
    #endregion
    #endregion //Declarations
    
    #region Constructor
    public FederateSom() : base()
    {
      // Construct SOM
      SensorOC = new WearableSensorFederation.Som.CSensorOC();
      AddToObjectModel(SensorOC);
      SinkDeviceOC = new WearableSensorFederation.Som.CSinkDeviceOC();
      AddToObjectModel(SinkDeviceOC);
      DataCollectorOC = new WearableSensorFederation.Som.CDataCollectorOC();
      AddToObjectModel(DataCollectorOC);
      MessageIC = new WearableSensorFederation.Som.CMessageIC();
      AddToObjectModel(MessageIC);
      SinkDeviceMessageIC = new WearableSensorFederation.Som.CSinkDeviceMessageIC();
      AddToObjectModel(SinkDeviceMessageIC);
      SensoryMessageIC = new WearableSensorFederation.Som.CSensoryMessageIC();
      AddToObjectModel(SensoryMessageIC);
    }
    #endregion //Constructor
  }
}
