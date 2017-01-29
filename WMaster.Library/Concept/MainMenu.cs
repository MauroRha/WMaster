namespace WMaster.Concept
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Main menu functionality like new game, load, save and so.
    /// </summary>
    public sealed class MainMenu
    {
        #region Singleton
        /// <summary>
        /// Singleton of <see cref="MainMenu"/>.
        /// </summary>
        private static MainMenu m_Instance;

        /// <summary>
        /// Get unique instance of <see cref="MainMenu"/>.
        /// </summary>
        public static MainMenu Instance
        {
            get
            {
                if (MainMenu.m_Instance == null)
                { MainMenu.m_Instance = new MainMenu(); }
                return MainMenu.m_Instance;
            }
        }

        /// <summary>
        /// Private constructor for singleton template.
        /// </summary>
        private MainMenu()
        { }
        #endregion

        /// <summary>
        /// Indicate if a bacup of last game exists.
        /// </summary>
        /// <returns><b>True</b> if a bacup of last game exists.</returns>
        public bool CanContinueGame()
        { return true; }

        /// <summary>
        /// Load last backup game, lose all information of current game... but it's the current game we load :)
        /// </summary>
        /// <returns><b>True</b> if a new game was correctly create.</returns>
        public bool ContinueGame()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indicate if a new game can be set.
        /// </summary>
        /// <returns><b>True</b> if a new game can be set.</returns>
        public bool CanCreateGame()
        { return true; }

        /// <summary>
        /// Create a new game, lose all information of current game.
        /// <remarks><para>Must call BackupCurrent() before to store current game progression.</para></remarks>
        /// </summary>
        /// <param name="gameInfo">Game information like player name, birth day and brothel name.</param>
        /// <returns><b>True</b> if a new game was correctly create.</returns>
        public bool NewGame(NewGameInfo gameInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indicate if a game can be load. (a save game was found)
        /// </summary>
        /// <returns><b>True</b> if a game can be load.</returns>
        public bool CanLoadGame()
        { return false; }

        /// <summary>
        /// load a new game, lose all information of current game.
        /// <remarks><para>Must call BackupCurrent() before to store current game progression.</para></remarks>
        /// </summary>
        /// <param name="saveGameResourceName">Resource name of game to load.</param>
        /// <returns><b>True</b> if the game was correctly loaded.</returns>
        public bool LoadGame(string saveGameResourceName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indicate if a game can be save. (may alway return <b>True</b> but to keep logic and in case off??)
        /// </summary>
        /// <returns><b>True</b> if a game can be save.</returns>
        public bool CanSaveGame()
        { return true; }

        /// <summary>
        /// Save current game, the resource name is the 1st brothel name but passint in parametre to prevent evolution where player can name his save.
        /// <remarks><para>Must call BackupCurrent() before to store current game progression.</para></remarks>
        /// </summary>
        /// <param name="saveGameResourceName">Resource name of game to save.</param>
        /// <returns><b>True</b> if the game was correctly loaded.</returns>
        public bool SaveGame(string saveGameResourceName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Wrapper to Configuration.LoadConfiguration to load configuration settings from menu access.
        /// </summary>
        /// <param name="configurationResourceName">Name of configuration resource name.</param>
        /// <returns><b>True</b> if configuration was correctly loaded.</returns>
        public bool LoadConfiguration(string configurationResourceName)
        {
            try
            {
                Configuration.LoadConfiguration(configurationResourceName);
                return true;
            }
            catch (Exception ex)
            {
                WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                return false;
            }
        }

        /// <summary>
        /// Wrapper to Configuration.SaveConfiguration to save configuration settings from menu access.
        /// </summary>
        /// <param name="configurationResourceName">Name of configuration resource name.</param>
        /// <returns><b>True</b> if configuration was correctly saved.</returns>
        public bool SaveConfiguration(string configurationResourceName)
        {
            try
            {
                Configuration.SaveConfiguration(configurationResourceName);
                return false;
            }
            catch (Exception ex)
            {
                WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                return false;
            }
        }

        /// <summary>
        /// Indicate if the game can be quit. (may alway return <b>True</b> but to keep logic and in case off??)
        /// </summary>
        /// <returns><b>True</b> if a game can be quit.</returns>
        public bool CanQuitGame()
        { return true; }

        /// <summary>
        /// Exit game, perform bacup of current game if there is one avalaible.
        /// </summary>
        public void QuitGame()
        {
            throw new NotImplementedException();
        }
    }
}
