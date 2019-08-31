using System;
using System.Collections.Generic;
using System.Text;

namespace PS.FritzBox.API.WANDevice
{
    /// <summary>
    /// Gets or sets the dsl diagnose state
    /// </summary>
    public enum DSLDiagnoseState
    {
        /// <summary>
        /// State NONE
        /// </summary>
        NONE,
        /// <summary>
        /// Satte NO_CALIB
        /// </summary>        
        NO_CALIB,
        /// <summary>
        /// State RUNNING
        /// </summary>
        RUNNING,
        /// <summary>
        /// State DONE
        /// </summary>
        DONE,
        /// <summary>
        /// State DONW Cable NOK
        /// </summary>
        DONE_CABLE_NOK,
        /// <summary>
        /// State DONE Cable OK
        /// </summary>
        DONE_CABLE_OK
    }
}
