// **************************************************************************************************
//		CSensorHlaObject
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
/// This is a wrapper class for local data structures. This class is extended from the object model of RACoN API
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
  public class CSensorHlaObject : HlaObject
  {
        public DataCollectorFdApp.CSensor sensor;

        #region Constructor
        public CSensorHlaObject(HlaObjectClass _type) : base(_type)
        {
            // TODO: Instantiate local data here
            // var Data = new Your_LocalData_Type();
            sensor = new DataCollectorFdApp.CSensor();

        }
        // Copy constructor - used in callbacks
        public CSensorHlaObject(HlaObject _obj) : base(_obj)
        {
            // TODO: Instantiate local data here
            // var Data = new Your_LocalData_Type();
            sensor = new DataCollectorFdApp.CSensor();
        }
        #endregion //Constructor
    }
}
