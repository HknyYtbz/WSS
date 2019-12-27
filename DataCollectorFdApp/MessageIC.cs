// **************************************************************************************************
//		CMessageIC
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
    public class CMessageIC : HlaInteractionClass
    {
        #region Declarations
        public HlaParameter TimeStamp;
        public HlaParameter Content;
        public HlaParameter SenderID;
        #endregion //Declarations

        #region Constructor
        public CMessageIC() : base()
        {
            // Initialize Class Properties
            Name = "HLAinteractionRoot.Message";
            ClassPS = PSKind.PublishSubscribe;

            // Create Parameters
            // TimeStamp
            TimeStamp = new HlaParameter("TimeStamp");
            Parameters.Add(TimeStamp);
            // Content
            Content = new HlaParameter("Content");
            Parameters.Add(Content);
            // SenderID
            SenderID = new HlaParameter("SenderID");
            Parameters.Add(SenderID);
        }
        #endregion //Constructor
    }
}
