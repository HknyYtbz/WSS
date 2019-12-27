// **************************************************************************************************
//		CDataCollectorOC
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
  public class CDataCollectorOC : HlaObjectClass
  {
    #region Declarations
    #endregion //Declarations
    
    #region Constructor
    public CDataCollectorOC() : base()
    {
      // Initialize Class Properties
      Name = "HLAobjectRoot.DataCollector";
      ClassPS = PSKind.PublishSubscribe;
      
      // Create Attributes
    }
    #endregion //Constructor
  }
}
