// **************************************************************************************************
//		CSensorOC
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
  public class CSensorOC : HlaObjectClass
  {
    #region Declarations
    public HlaAttribute SensorID;
    public HlaAttribute SubjectID;
    public HlaAttribute SensorLocation;
    public HlaAttribute SensorType;
    #endregion //Declarations
    
    #region Constructor
    public CSensorOC() : base()
    {
      // Initialize Class Properties
      Name = "HLAobjectRoot.Sensor";
      ClassPS = PSKind.PublishSubscribe;
      
      // Create Attributes
      // SensorID
      SensorID = new HlaAttribute("SensorID", PSKind.PublishSubscribe);
      Attributes.Add(SensorID);
      // SubjectID
      SubjectID = new HlaAttribute("SubjectID", PSKind.PublishSubscribe);
      Attributes.Add(SubjectID);
      // SensorLocation
      SensorLocation = new HlaAttribute("SensorLocation", PSKind.PublishSubscribe);
      Attributes.Add(SensorLocation);
      // SensorType
      SensorType = new HlaAttribute("SensorType", PSKind.PublishSubscribe);
      Attributes.Add(SensorType);
    }
    #endregion //Constructor
  }
}
