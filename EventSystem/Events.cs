using UnityEngine;
using System.Collections;

public class Events {

    /// <summary>
    /// Every type of event available for use
    /// </summary>
    public enum Type {
        #region General EventHandler

        /// <summary>
        /// On clearing of non-global events
        /// </summary>
        OnEventHandlerClear,

        #endregion

        #region Level and settings management

        /// <summary>
        /// When any UI menu opens
        /// </summary>
        OnMenuOpen,

        /// <summary>
        /// When any UI menu closes
        /// </summary>
        OnMenuClose,

        /// <summary>
        /// When level ends succesfully (eg. player reaches goal)
        /// </summary>
        OnLevelEndSuccess,

        /// <summary>
        /// When settings change
        /// </summary>
        OnSettingsChange,

        #endregion

        #region Timer

        /// <summary>
        /// When level timer starts
        /// </summary>
        OnTimerStart,

        /// <summary>
        /// When level timer stops
        /// </summary>
        OnTimerStop,

        #endregion

        #region Tool related
        OnGravityToolEnable,
        OnGravityToolDisable,
        #endregion

        #region Teleport

        /// <summary>
        /// Triggered when player teleports
        /// </summary>
        OnTeleportTrigger

        #endregion



    }
}
