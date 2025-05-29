using System;

namespace Scripts.Utilities
{
    public static class GeneralEvents
    {
        #region Round Events

        public static Action OnCombatStarted;

        #endregion

        #region SpinWheel Events

        public static Action OnSpinStopped;

        #endregion

        #region Game States

        public static Action OnGameWon;
        public static Action OnGameLost;

        #endregion

        #region Main Menu

        public static Action OnLoadingScreenStart;
        public static Action OnEnemyFoundAtLoadingScreen;
        public static Action OnEvolvePurshased;

        #endregion

        #region Upgrades

        public static Action OnSkillPurchased;
        public static Action OpenSkillChoosingUi;

        #endregion

        #region Tile Actions

        public static Action OnCombatTileAction;
        public static Action OnCombatFinished;

        #endregion
    }
}