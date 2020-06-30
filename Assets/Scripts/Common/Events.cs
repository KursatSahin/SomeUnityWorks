namespace Common
{
    public static class Events
    {
        #region Custom Events

        // POWER UP
        public const string PowerUpIsActivated = "POWERUP_IS_ACTIVATED";
        public const string PowerUpIsDeactivated = "POWERUP_IS_DEACTIVATED";
        public const string PowerUpInUseUpdated = "POWERUP_IN_USE_UPDATED";
        public const string PowerUpLimitHasReached = "POWERUP_LIMIT_HAS_REACHED";
        
        // MAIN MENU
        public const string StartGame = "START_GAME";

        #endregion Custom Events
    }
}