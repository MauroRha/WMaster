/*
 * Original source code in C++ from :
 * Copyright 2009, 2010, The Pink Petal Development Team.
 * The Pink Petal Devloment Team are defined as the game's coders 
 * who meet on http://pinkpetal.org     // old site: http://pinkpetal .co.cc
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
namespace WMaster.Manager
{
    using System;
    using WMaster.ClassOrStructurToImplement;
    using WMaster.Entity.Item;
    using WMaster.Manager;
    using WMaster.Tool;

    /// <summary>
    /// Game engin. Start point of game
    /// </summary>
    public sealed class GameEngine
    {
        /// <summary>
        /// Singleton of GameEngine
        /// </summary>
        private static GameEngine _instance;
        /// <summary>
        /// Get Game engine instance
        /// </summary>
        public static GameEngine Instance
        {
            get
            {
                if (GameEngine._instance == null)
                {
                    GameEngine._instance = new GameEngine();
                    GameEngine._instance.Init();
                }
                return GameEngine._instance;
            }
        }

        /// <summary>
        /// Resources manager.
        /// </summary>
        private IResourceManager _resources;
        /// <summary>
        /// Get the resource manager.
        /// </summary>
        public IResourceManager Resources
        {
            get { return this._resources; }
        }

        /// <summary>
        /// Initialise a game.
        /// Not creating or loading a game, just basic initialisation. Load referentiel, ressources and globals datas.
        /// </summary>
        /// <returns></returns>
        public bool Init()
        { throw new NotImplementedException(); }

        // Function Defs
        /// <summary>
        /// Start treatment for week + 1.
        /// </summary>
        public void NextWeek()
        { throw new NotImplementedException(); }

        /// <summary>
        /// Ask game to end. Terminate all current task and save current party for continue issue.
        /// </summary>
        public void Shutdown()
        { throw new NotImplementedException(); }

        private bool quitAccepted = false;

        public Config cfg = new Config();
        private int g_CurrBrothel = 0;
        private int g_Building = 0;
        private int g_CurrClinic = 0;
        private int g_CurrStudio = 0;
        private int g_CurrArena = 0;
        private int g_CurrCentre = 0;
        private int g_CurrHouse = 0;
        private int g_CurrFarm = 0;
        private uint g_LastSeed = 0; // for seeding the random number generater every 3 seconds (3000 ticks)

        private bool eventrunning = false;
        private bool newWeek = false;

        /// <summary>
        /// TODO : TRADUCTION - Localise month names
        /// </summary>
        string[] monthnames = { "No Month", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        bool playershopinventory = false;

        public sGirl[] MarketSlaveGirls = new sGirl[20];
        public int[] MarketSlaveGirlsDel = new int[20];


        public cAbstractGirls g_GirlsPtr; // TODO : initialiser g_GirlsPtr = g_Girls;

        /// <summary>
        /// Current year in game.
        /// </summary>
        private int m_Year;
        /// <summary>
        /// Get or set the current year in game.
        /// </summary>
        public int Year
        {
            get { return this.m_Year; }
            set { this.m_Year = value; }
        }
        /// <summary>
        /// Current month in game.
        /// </summary>
        private int m_Month;
        /// <summary>
        /// Get or set the current month in game.
        /// </summary>
        public int Month
        {
            get { return this.m_Month; }
            set { this.m_Month = value; }
        }
        /// <summary>
        /// Current day in game.
        /// </summary>
        private int m_Day;
        /// <summary>
        /// Get or set the current day in game.
        /// </summary>
        public int Day
        {
            get { return this.m_Day; }
            set { this.m_Day = value; }
        }

        /// <summary>
        /// Player gold
        /// </summary>
        private cGold g_Gold = new cGold();
        /// <summary>
        /// Player gold
        /// </summary>
        [Obsolete("Move gold to player instance?", false)]
        public cGold Gold
        { get { return this.g_Gold; } }

        public NameList g_NameList = new NameList();
        public NameList g_SurnameList = new NameList();

        #region IHM component to translate to Specific project
        //cScrollBar g_DragScrollBar = 0; // if a scrollbar is being dragged, this points to it
        //cSlider g_DragSlider = 0; // if a slider is being dragged, this points to it
        //cWindowManager g_WinManager = new cWindowManager();
        //// Keeping track of what screen is currently showing
        //int g_CurrentScreen = 0;
        //CSurface g_BrothelImages[7];
        //bool g_InitWin;
        //CGraphics g_Graphics = new CGraphics();

        //// the background image
        //CSurface g_BackgroundImage = 0;
        #endregion

        #region System specific component to translate to Specific project
        /*
            private bool g_ShiftDown = false;
            private bool g_CTRLDown = false;

            private bool g_LeftArrow = false;
            private bool g_RightArrow = false;
            private bool g_UpArrow = false;
            private bool g_DownArrow = false;
            private bool g_EnterKey = false;
            private bool g_SpaceKey = false;
            private bool g_TabKey = false;
            private bool g_EscapeKey = false;
            private bool g_HomeKey = false;
            private bool g_EndKey = false;
            private bool g_PageUpKey = false;
            private bool g_PageDownKey = false;
            private bool g_PeriodKey = false;
            private bool g_SlashKey = false;
            private bool g_BackSlashKey = false;

            bool g_1_Key = false;
            bool g_2_Key = false;
            bool g_3_Key = false;
            bool g_4_Key = false;
            bool g_5_Key = false;
            bool g_6_Key = false;
            bool g_7_Key = false;
            bool g_8_Key = false;
            bool g_9_Key = false;
            bool g_0_Key = false;

            bool g_F1_Key = false;
            bool g_F2_Key = false;
            bool g_F3_Key = false;
            bool g_F4_Key = false;
            bool g_F5_Key = false;
            bool g_F6_Key = false;
            bool g_F7_Key = false;
            bool g_F8_Key = false;
            bool g_F9_Key = false;
            bool g_F10_Key = false;
            bool g_F11_Key = false;
            bool g_F12_Key = false;

            bool g_A_Key = false;
            bool g_B_Key = false;
            bool g_C_Key = false;
            bool g_D_Key = false;
            bool g_E_Key = false;
            bool g_F_Key = false;
            bool g_G_Key = false;
            bool g_H_Key = false;
            bool g_I_Key = false;
            bool g_J_Key = false;
            bool g_K_Key = false;
            bool g_L_Key = false;
            bool g_M_Key = false;
            bool g_N_Key = false;
            bool g_O_Key = false;
            bool g_P_Key = false;
            bool g_Q_Key = false;
            bool g_R_Key = false;
            bool g_S_Key = false;
            bool g_T_Key = false;
            bool g_U_Key = false;
            bool g_V_Key = false;
            bool g_W_Key = false;
            bool g_X_Key = false;
            bool g_Y_Key = false;
            bool g_Z_Key = false;

            bool g_AltKeys = true; // Toggles the alternate hotkeys --PP
        */

        //void handle_hotkeys()
        //{
        //    switch (vent.key.keysym.sym)
        //    {
        //    case SDLK_RSHIFT:
        //    case SDLK_LSHIFT:
        //        g_ShiftDown = true;		// enable multi select
        //        break;
        //    case SDLK_RCTRL:
        //    case SDLK_LCTRL:
        //        g_CTRLDown = true;		// enable multi select
        //        break;

        //    case SDLK_RETURN:
        //    case SDLK_KP_ENTER:	g_EnterKey = true;	break;

        //    case SDLK_UP:		g_UpArrow = true;		break;
        //    case SDLK_DOWN:		g_DownArrow = true;		break;
        //    case SDLK_LEFT:		g_LeftArrow = true;		break;
        //    case SDLK_RIGHT:	g_RightArrow = true;	break;
        //    case SDLK_SPACE:	g_SpaceKey = true;		break;
        //    case SDLK_HOME:		g_HomeKey = true;		break;
        //    case SDLK_END:		g_EndKey = true;		break;
        //    case SDLK_PAGEUP:	g_PageUpKey = true;		break;
        //    case SDLK_PAGEDOWN:	g_PageDownKey = true;	break;
        //    case SDLK_TAB:		g_TabKey = true;		break;
        //    case SDLK_ESCAPE:	g_EscapeKey = true;		break;
        //    case SDLK_PERIOD:	g_PeriodKey = true;		break;
        //    case SDLK_SLASH:	g_SlashKey = true;		break;
        //    case SDLK_BACKSLASH:g_BackSlashKey = true;	break;

        //    case SDLK_1:		g_1_Key = true;			break; 
        //    case SDLK_2:		g_2_Key = true;			break; 
        //    case SDLK_3:		g_3_Key = true;			break;
        //    case SDLK_4:		g_4_Key = true;			break; 
        //    case SDLK_5:		g_5_Key = true;			break; 
        //    case SDLK_6:		g_6_Key = true;			break;
        //    case SDLK_7:		g_7_Key = true;			break;
        //    case SDLK_8:		g_8_Key = true;			break;
        //    case SDLK_9:		g_9_Key = true;			break;
        //    case SDLK_0:		g_0_Key = true;			break;

        //    case SDLK_F1:		g_F1_Key = true;		break;
        //    case SDLK_F2:		g_F2_Key = true;		break;
        //    case SDLK_F3:		g_F3_Key = true;		break;
        //    case SDLK_F4:		g_F4_Key = true;		break;
        //    case SDLK_F5:		g_F5_Key = true;		break;
        //    case SDLK_F6:		g_F6_Key = true;		break;
        //    case SDLK_F7:		g_F7_Key = true;		break;
        //    case SDLK_F8:		g_F8_Key = true;		break;
        //    case SDLK_F9:		g_F9_Key = true;		break;
        //    case SDLK_F10:		g_F10_Key = true;		break;
        //    case SDLK_F11:		g_F11_Key = true;		break;
        //    case SDLK_F12:		g_F12_Key = true;		break;

        //    case SDLK_a:		g_A_Key = true;			break;
        //    case SDLK_b:		g_B_Key = true;			break;
        //    case SDLK_c:		g_C_Key = true;			break;
        //    case SDLK_d:		g_D_Key = true;			break;
        //    case SDLK_e:		g_E_Key = true;			break;
        //    case SDLK_f:		g_F_Key = true;			break;
        //    case SDLK_g:		g_G_Key = true;			break;
        //    case SDLK_h:		g_H_Key = true;			break;
        //    case SDLK_i:		g_I_Key = true;			break;
        //    case SDLK_j:		g_J_Key = true;			break;
        //    case SDLK_k:		g_K_Key = true;			break;
        //    case SDLK_l:		g_L_Key = true;			break;
        //    case SDLK_m:		g_M_Key = true;			break;
        //    case SDLK_n:		g_N_Key = true;			break;
        //    case SDLK_o:		g_O_Key = true;			break;
        //    case SDLK_p:		g_P_Key = true;			break;
        //    case SDLK_q:		g_Q_Key = true;			break;
        //    case SDLK_r:		g_R_Key = true;			break;
        //    case SDLK_s:		g_S_Key = true;			break;
        //    case SDLK_t:		g_T_Key = true;			break;
        //    case SDLK_u:		g_U_Key = true;			break;
        //    case SDLK_v:		g_V_Key = true;			break;
        //    case SDLK_w:		g_W_Key = true;			break;
        //    case SDLK_x:		g_X_Key = true;			break;
        //    case SDLK_y:		g_Y_Key = true;			break;
        //    case SDLK_z:		g_Z_Key = true;			break;
        //    default:	break;
        //    }




        //    // Process the keys for every screen except MainMenu, LoadGame and NewGame - they have their own keys
        //    if (g_WinManager.GetWindow() != &g_MainMenu && g_WinManager.GetWindow() != &g_LoadGame && g_WinManager.GetWindow() != &g_NewGame)
        //    {
        //        int br_no = 0;
        //        string msg = "";

        //        switch (vent.key.keysym.sym) {                  // Select Brothel
        //        case SDLK_1: case SDLK_2: case SDLK_3:
        //        case SDLK_4: case SDLK_5: case SDLK_6:
        //        case SDLK_7:
        //            br_no = vent.key.keysym.sym - SDLK_1;
        //            if (g_Brothels.GetNumBrothels() > br_no) {
        //                g_CurrBrothel = br_no;
        //                g_InitWin = true;
        //            }
        //            break;

        //        case SDLK_TAB:  //cycle through brothles
        //            if (g_ShiftDown)
        //            {
        //                g_CurrBrothel--;
        //                if (g_CurrBrothel < 0)
        //                    g_CurrBrothel = g_Brothels.GetNumBrothels() - 1;
        //            }
        //            else
        //            {
        //                g_CurrBrothel++;
        //                if (g_Brothels.GetNumBrothels() <= g_CurrBrothel)
        //                    g_CurrBrothel = 0;
        //            }
        //            g_InitWin = true;
        //            break;

        //        case SDLK_ESCAPE:       // Go back to previous screen
        //            if (g_CurrentScreen == SCREEN_BROTHEL	||
        //                g_CurrentScreen == SCREEN_MAINMENU	||
        //                g_CurrentScreen == SCREEN_NEWGAME	||
        //                g_CurrentScreen == SCREEN_PREPARING)
        //                break;
        //            g_Building = BUILDING_BROTHEL;
        //            g_InitWin = true;
        //            g_WinManager.Pop();
        //            break;

        //            // girl management screen
        //        case SDLK_g:    if (g_AltKeys)  break;
        //        case SDLK_F1:
        //            if (cfg.debug.log_debug())	g_LogFile.write("Entering Brothel");
        //            if (g_Building != BUILDING_BROTHEL) selected_girl = 0;
        //            g_Building = BUILDING_BROTHEL;
        //            g_WinManager.PopToWindow(&g_BrothelManagement);
        //            g_CurrentScreen = SCREEN_GIRLMANAGEMENT;
        //            g_InitWin = true;
        //            if (g_ShiftDown)		break;
        //            else if (g_CTRLDown)	g_WinManager.push("Building Setup");
        //            else					g_WinManager.push("Girl Management");
        //            break;

        //            // staff management screen (gang management)
        //        case SDLK_t:    if (g_AltKeys)  break;
        //        case SDLK_F2:
        //            if (cfg.debug.log_debug())	g_LogFile.write("Entering  Gang Management");
        //            selected_girl = 0;
        //            g_Building = BUILDING_BROTHEL;
        //            g_WinManager.PopToWindow(&g_BrothelManagement);
        //            g_CurrentScreen = SCREEN_GANGMANAGEMENT;
        //            g_InitWin = true;
        //            g_WinManager.push("Gangs");
        //            break;

        //            // Dungeon
        //        case SDLK_d:    if (g_AltKeys)  break;
        //        case SDLK_F3:
        //            if (cfg.debug.log_debug())	g_LogFile.write("Entering  Dungeon");
        //            selected_girl = 0;
        //            g_Building = BUILDING_BROTHEL;
        //            g_WinManager.PopToWindow(&g_BrothelManagement);
        //            g_InitWin = true;
        //            g_CurrentScreen = SCREEN_DUNGEON;
        //            g_WinManager.push("Dungeon");
        //            break;

        //            // Slave market screen
        //        case SDLK_s:    if (g_AltKeys)  break;
        //        case SDLK_F4:
        //            if (cfg.debug.log_debug())	g_LogFile.write("Entering  Slave Market");
        //            selected_girl = 0;
        //            g_Building = BUILDING_BROTHEL;
        //            g_WinManager.PopToWindow(&g_BrothelManagement);
        //            g_InitWin = true;
        //            g_CurrentScreen = SCREEN_SLAVEMARKET;
        //            g_WinManager.push("Town");
        //            g_WinManager.push("Slave Market");
        //            break;

        //        case SDLK_F5:   // Studio
        //            if (g_Studios.GetNumBrothels() == 0)    // Does player own the Studio yet?
        //            {
        //                msg = "You do not own a Studio";
        //                g_MessageQue.AddToQue(msg, 0);
        //                break;
        //            }
        //            if (cfg.debug.log_debug())	g_LogFile.write("Entering  Studio");
        //            // Yes!
        //            if (g_Building != BUILDING_STUDIO) selected_girl = 0;
        //            g_Building = BUILDING_STUDIO;
        //            g_WinManager.PopToWindow(&g_BrothelManagement);
        //            g_CurrentScreen = SCREEN_STUDIO;
        //            g_InitWin = true;
        //            g_WinManager.push("Town");
        //            g_WinManager.push("Movie Screen");
        //            if (g_ShiftDown)		break;
        //            else if (g_CTRLDown)	g_WinManager.push("Building Setup");
        //            else					g_WinManager.push("Studio");
        //            break;

        //            // Arena
        //        case SDLK_e:    if (g_AltKeys)  break;
        //        case SDLK_F6:
        //            if (g_Arena.GetNumBrothels() == 0)      // Does player own the Arena yet?
        //            {
        //                msg = "You do not own a Arena";
        //                g_MessageQue.AddToQue(msg, 0);
        //                break;
        //            }
        //            // Yes!
        //            if (cfg.debug.log_debug())	g_LogFile.write("Entering  Arena");
        //            if (g_Building != BUILDING_ARENA) selected_girl = 0;
        //            g_Building = BUILDING_ARENA;
        //            g_WinManager.PopToWindow(&g_BrothelManagement);
        //            g_CurrentScreen = SCREEN_ARENA;
        //            g_InitWin = true;
        //            g_WinManager.push("Town");
        //            g_WinManager.push("Arena Screen");
        //            if (g_ShiftDown)		break;
        //            else if (g_CTRLDown)	g_WinManager.push("Building Setup");
        //            else					g_WinManager.push("Arena");
        //            break;

        //        case SDLK_F7:   // Centre
        //            if (g_Centre.GetNumBrothels() == 0)     // Does player own the Centre yet?
        //            {
        //                msg = "You do not own a Centre";
        //                g_MessageQue.AddToQue(msg, 0);
        //                break;
        //            }
        //            // Yes!
        //            if (cfg.debug.log_debug())	g_LogFile.write("Entering  Centre");
        //            if (g_Building != BUILDING_CENTRE) selected_girl = 0;
        //            g_Building = BUILDING_CENTRE;
        //            g_WinManager.PopToWindow(&g_BrothelManagement);
        //            g_CurrentScreen = SCREEN_CENTRE;
        //            g_InitWin = true;
        //            g_WinManager.push("Town");
        //            g_WinManager.push("Centre Screen");
        //            if (g_ShiftDown)		break;
        //            else if (g_CTRLDown)	g_WinManager.push("Building Setup");
        //            else					g_WinManager.push("Centre");
        //            break;

        //            // clinic
        //        case SDLK_c:    if (g_AltKeys)  break;
        //        case SDLK_F8:
        //            if (g_Clinic.GetNumBrothels() == 0)     // Does player own the clinic yet?
        //            {
        //                msg = "You do not own a Clinic";
        //                g_MessageQue.AddToQue(msg, 0);
        //                break;
        //            }
        //            // Yes!
        //            if (cfg.debug.log_debug())	g_LogFile.write("Entering  Clinic");
        //            if (g_Building != BUILDING_CLINIC) selected_girl = 0;
        //            g_Building = BUILDING_CLINIC;
        //            g_WinManager.PopToWindow(&g_BrothelManagement);
        //            g_CurrentScreen = SCREEN_CLINIC;
        //            g_InitWin = true;
        //            g_WinManager.push("Town");
        //            g_WinManager.push("Clinic Screen");
        //            if (g_ShiftDown)		break;
        //            else if (g_CTRLDown)	g_WinManager.push("Building Setup");
        //            else					g_WinManager.push("Clinic");
        //            break;

        //            // farm
        //        case SDLK_F9:
        //            if (g_Farm.GetNumBrothels() == 0)       // Does player own the farm yet?
        //            {
        //                msg = "You do not own a Farm";
        //                g_MessageQue.AddToQue(msg, 0);
        //                break;
        //            }
        //            // Yes!
        //            if (cfg.debug.log_debug())	g_LogFile.write("Entering  Farm");
        //            if (g_Building != BUILDING_FARM) selected_girl = 0;
        //            g_Building = BUILDING_FARM;
        //            g_WinManager.PopToWindow(&g_BrothelManagement);
        //            g_CurrentScreen = SCREEN_FARM;
        //            g_InitWin = true;
        //            g_WinManager.push("Town");
        //            g_WinManager.push("Farm Screen");
        //            if (g_ShiftDown)		break;
        //            else if (g_CTRLDown)	g_WinManager.push("Building Setup");
        //            else					g_WinManager.push("Farm");
        //            break;
        //            // shop screen
        //        case SDLK_p:    if (vent.key.keysym.sym == SDLK_p && g_AltKeys)  break;
        //        case SDLK_i:
        //            if (vent.key.keysym.sym == SDLK_i && g_CurrentScreen == SCREEN_INVENTORY) break;
        //            if (g_CTRLDown)
        //            {
        //                playershopinventory = true;
        //                g_AllTogle = false;
        //            }
        //            else
        //                g_AllTogle = true;
        //            if (cfg.debug.log_debug())	g_LogFile.write("Entering Inventory");
        //            g_Building = BUILDING_BROTHEL;
        //            g_WinManager.PopToWindow(&g_BrothelManagement);
        //            g_CurrentScreen = SCREEN_INVENTORY;
        //            g_InitWin = true;
        //            g_WinManager.push("Town");
        //            g_WinManager.push("Item Management");
        //            break;

        //            // town screen
        //        case SDLK_o:    if (g_AltKeys)  break;
        //        case SDLK_F10:
        //            if (cfg.debug.log_debug())	g_LogFile.write("Entering Town");
        //            g_Building = BUILDING_BROTHEL;
        //            g_WinManager.PopToWindow(&g_BrothelManagement);
        //            g_CurrentScreen = SCREEN_TOWN;
        //            g_InitWin = true;
        //            g_WinManager.push("Town");
        //            break;

        //            // turn summary screen
        //        case SDLK_END:
        //            if (g_CTRLDown)
        //            {
        //                g_CTRLDown = false;
        //                g_AltKeys = true;
        //                msg = "Alternate HotKeys Active\n";
        //                g_MessageQue.AddToQue(msg, 0);
        //                g_ChoiceManager.CreateChoiceBox(224, 825, 352, 600, 0, 1, 32, strlen(gettext("Close")));
        //                g_ChoiceManager.AddChoice(0, gettext("Close"), 0);
        //                g_ChoiceManager.SetActive(0);
        //                g_ChoiceManager.Free();
        //                break;
        //            }
        //        case SDLK_a:    if (g_AltKeys && vent.key.keysym.sym == SDLK_a)  break;
        //        case SDLK_F11:
        //            if (cfg.debug.log_debug())	g_LogFile.write("Entering Turn Summary");
        //            g_Building = BUILDING_BROTHEL;
        //            g_WinManager.PopToWindow(&g_BrothelManagement);
        //            g_CurrentScreen = SCREEN_TURNSUMMARY;
        //            g_InitWin = true;
        //            g_WinManager.push("TurnSummary");
        //            break;

        //        case SDLK_HOME:
        //            if (g_CTRLDown)
        //            {
        //                g_CTRLDown = false;
        //                g_AltKeys = false;
        //                msg = "Default HotKeys Active\n";
        //                g_MessageQue.AddToQue(msg, 0);
        //                g_ChoiceManager.CreateChoiceBox(224, 825, 352, 600, 0, 1, 32, strlen(gettext("Close")));
        //                g_ChoiceManager.AddChoice(0, gettext("Close"), 0);
        //                g_ChoiceManager.SetActive(0);
        //                g_ChoiceManager.Free();
        //                break;
        //            }
        //        case SDLK_F12:  // House
        //            if (cfg.debug.log_debug())	g_LogFile.write("Entering House");
        //            if (g_Building != BUILDING_HOUSE) selected_girl = 0;
        //            g_Building = BUILDING_HOUSE;
        //            g_WinManager.PopToWindow(&g_BrothelManagement);
        //            g_CurrentScreen = SCREEN_HOUSE;
        //            g_InitWin = true;
        //            g_WinManager.push("Town");
        //            g_WinManager.push("Player House");

        //            if (g_ShiftDown)		break;
        //            else if (g_CTRLDown)	g_WinManager.push("Building Setup");
        //            else					g_WinManager.push("House Management");
        //            break;

        //            // Non F-Key hotkeys (disabled by alt)
        //            // mayors office screen
        //        case SDLK_m:    if (g_AltKeys)  break;
        //            g_Building = BUILDING_BROTHEL;
        //            g_WinManager.PopToWindow(&g_BrothelManagement);
        //            g_CurrentScreen = SCREEN_BROTHEL;
        //            g_InitWin = true;
        //            /*
        //            *                       this will make "m" go to brothel management
        //            *                       you need "M" to go to the mayor screen now
        //            *                       which is far less used, I think, and easy to get
        //            *                       to from the town screen
        //            *
        //            *                       we should consider some customisable keyboard mapping
        //            *                       mechanism at some point
        //            */
        //            if (g_ShiftDown) {
        //                g_CurrentScreen = SCREEN_MAYOR;
        //                g_WinManager.push("Town");
        //                g_WinManager.push("Mayor");
        //            }
        //            break;

        //            // bank screen
        //        case SDLK_b:    if (g_AltKeys)  break;
        //            g_Building = BUILDING_BROTHEL;
        //            g_WinManager.PopToWindow(&g_BrothelManagement);
        //            g_CurrentScreen = SCREEN_BANK;
        //            g_InitWin = true;
        //            g_WinManager.push("Town");
        //            g_WinManager.push("Bank");
        //            break;

        //            // upgrades management screen
        //        case SDLK_u:    if (g_AltKeys)  break;
        //            g_Building = BUILDING_BROTHEL;
        //            g_WinManager.PopToWindow(&g_BrothelManagement);
        //            g_CurrentScreen = SCREEN_BUILDINGMANAGEMENT;
        //            g_InitWin = true;
        //            if (g_ShiftDown)	g_WinManager.push("Building Management");
        //            else				g_WinManager.push("Building Setup");
        //            break;

        //        case SDLK_9:
        //            msg = "These are the active hot keys:\n";
        //            if (g_AltKeys)
        //            {
        //                switch (g_CurrentScreen)
        //                {
        //                case SCREEN_BROTHEL:
        //                    msg += "Brothel Screen:\n";
        //                    msg += "Right Arrow     Next Brothel\n";
        //                    msg += "Left Arrow      Previous Brothel\n\n";
        //                    break;
        //                case SCREEN_TURNSUMMARY:
        //                    msg += "Turn Summary Screen:\n";
        //                    msg += "Up Arrow     Previous Girl\n";
        //                    msg += "Down Arrow   Next Girl\n";
        //                    msg += "Left Arrow   Previous Event\n";
        //                    msg += "Right Arrow  Next Event\n\n";
        //                    msg += "For left handed control:\n";
        //                    msg += "A     Previous Girl\n";
        //                    msg += "D     Next Girl\n";
        //                    msg += "W     Previous Event\n";
        //                    msg += "S     Next Event\n";
        //                    msg += "Q     Next Catagory\n";
        //                    msg += "E     Previous Catagory\n";
        //                    msg += "Space Change current picture\n";
        //                    if (cfg.resolution.next_turn_enter()) msg += "Enter  Goto Next Week\n";
        //                    msg += "\n";
        //                    break;
        //                case SCREEN_GALLERY:
        //                case SCREEN_GALLERY2:
        //                    msg += "Gallery:\n";
        //                    msg += "Left Arrow     Previous Picture\n";
        //                    msg += "Right Arrow    Next Picture\n";
        //                    msg += "Up Arrow     Previous Gallery\n";
        //                    msg += "Down Arrow     Next Gallery\n\n";
        //                    msg += "For left handed control:\n";
        //                    msg += "A     Previous Picture\n";
        //                    msg += "D     Next Picture\n";
        //                    msg += "W     Previous Gallery\n";
        //                    msg += "S     Next Gallery\n\n";
        //                    break;
        //                case SCREEN_TRANSFERGIRLS:
        //                    msg += "Transfer Screen:\n";
        //                    msg += "No special hotkeys\n";
        //                    msg += "Yet...\n\n";
        //                    break;
        //                case SCREEN_GIRLMANAGEMENT:
        //                    msg += "Girl Management:\n";
        //                    msg += "Up Arrow     Previous Girl\n";
        //                    msg += "Down Arrow   Next Girl\n\n";
        //                    msg += "For left handed control:\n";
        //                    msg += "A     Previous Girl\n";
        //                    msg += "D     Next Girl\n";
        //                    msg += "W     Previous Work Area\n";
        //                    msg += "S     Next Work Area\n";
        //                    msg += "Q     Previous Job\n";
        //                    msg += "E     Next Job\n";
        //                    msg += "Z     Day Shift\n";
        //                    msg += "C     Night Shift\n";
        //                    msg += "Space   Goto Girl Details\n";
        //                    msg += "Enter   Goto Girl Details\n\n";
        //                    break;
        //                case SCREEN_GIRLDETAILS:
        //                    msg += "Girl Details:\n";
        //                    msg += "Up Arrow    Previous Girl\n";
        //                    msg += "Down Arrow  Next Girl\n\n";
        //                    msg += "A    Previous Girl\n";
        //                    msg += "D    Next Girl\n";
        //                    msg += "S    More Details\n";
        //                    msg += "Space    Gallery\n\n";
        //                    msg += "J    House Percent Up\n";
        //                    msg += "H    House Percent Down\n\n";
        //                    break;
        //                case SCREEN_INVENTORY:
        //                    msg += "Inventory Screen:\n";
        //                    msg += "                                    Up         Down\n";
        //                    msg += "Inventory type :        R           F\n";
        //                    msg += "Owner list left :          T           G\n";
        //                    msg += "Owner list right :        Y           H\n";
        //                    msg += "Items list left :          U           J\n";
        //                    msg += "Items list right :         I           K\n\n\n";
        //                    msg += "There are no hotkeys for buy and sell buttons\n";
        //                    msg += "or for equip or unequip buttons\n";
        //                    msg += "to prevent accidental buying, selling or equiping of items\n";
        //                    msg += "\n\n";
        //                    break;
        //                case SCREEN_GANGMANAGEMENT:
        //                    msg += "Gang Management:\n";
        //                    msg += "Up Arrow    Previous Gang\n";
        //                    msg += "Down Arrow  Next Gang\n\n";
        //                    msg += "For left handed control:\n";
        //                    msg += "A     Previous Gang\n";
        //                    msg += "D     Next Gang\n";
        //                    msg += "W     Previous Mission\n";
        //                    msg += "S     Next Mission\n";
        //                    msg += "Q     Previous Recruits\n";
        //                    msg += "E     Next Recruits\n";
        //                    msg += "Space    Hire Gang\n\n";
        //                    break;
        //                case SCREEN_BROTHELMANAGEMENT:
        //                    msg += "Brothel Management:\n";
        //                    msg += "No special hotkeys\n";
        //                    msg += "Yet...\n\n";
        //                    break;
        //                case SCREEN_DUNGEON:
        //                    msg += "Dungeon:\n";
        //                    msg += "Up Arrow    Previous girl\n";
        //                    msg += "Down Arrow  Next Girl\n\n";
        //                    msg += "For left handed control:\n";
        //                    msg += "A     Previous Girl\n";
        //                    msg += "D     Next Girl\n\n";
        //                    break;
        //                case SCREEN_TOWN:
        //                    msg += "Town:\n";
        //                    msg += "No special hotkeys\n";
        //                    msg += "Yet...\n\n";
        //                    break;
        //                case SCREEN_MAYOR:
        //                    msg += "Mayor:\n";
        //                    msg += "No special hotkeys\n";
        //                    msg += "Yet...\n\n";
        //                    break;
        //                case SCREEN_BANK:
        //                    msg += "Bank:\n";
        //                    msg += "No special hotkeys\n";
        //                    msg += "Yet...\n\n";
        //                    break;
        //                case SCREEN_JAIL:
        //                    msg += "Jail:\n";
        //                    msg += "Up Arrow    Previous Gang\n";
        //                    msg += "Down Arrow  Next Gang\n\n";
        //                    msg += "For left handed control:\n";
        //                    msg += "A     Previous Girl\n";
        //                    msg += "D     Next Girl\n\n";
        //                    break;
        //                case SCREEN_HOUSE:
        //                    msg += "House:\n";
        //                    msg += "Space   Goto Girl Details\n";
        //                    msg += "Enter   Goto Girl Details\n\n";
        //                    break;
        //                case SCREEN_CLINIC:
        //                    msg += "Clinic:\n";
        //                    msg += "Up Arrow    Previous Gang\n";
        //                    msg += "Down Arrow  Next Gang\n\n";
        //                    msg += "For left handed control:\n";
        //                    msg += "A     Previous Girl\n";
        //                    msg += "D     Next Girl\n\n";
        //                    msg += "Space   Goto Girl Details\n";
        //                    msg += "Enter   Goto Girl Details\n\n";
        //                    break;
        //                case SCREEN_ARENA:
        //                    msg += "Arena:\n";
        //                    msg += "Up Arrow    Previous Gang\n";
        //                    msg += "Down Arrow  Next Gang\n\n";
        //                    msg += "For left handed control:\n";
        //                    msg += "A     Previous Girl\n";
        //                    msg += "D     Next Girl\n\n";
        //                    msg += "Space   Goto Girl Details\n";
        //                    msg += "Enter   Goto Girl Details\n\n";
        //                    break;
        //                case SCREEN_TRYOUTS:
        //                    msg += "Try Outs:\n";
        //                    msg += "No special hotkeys\n";
        //                    msg += "Yet...\n\n";
        //                    break;
        //                case SCREEN_CENTRE:
        //                    msg += "Centre:\n";
        //                    msg += "Up Arrow    Previous Gang\n";
        //                    msg += "Down Arrow  Next Gang\n\n";
        //                    msg += "For left handed control:\n";
        //                    msg += "A     Previous Girl\n";
        //                    msg += "D     Next Girl\n\n";
        //                    msg += "Space   Goto Girl Details\n";
        //                    msg += "Enter   Goto Girl Details\n\n";
        //                    break;
        //                case SCREEN_STUDIO:
        //                    msg += "Studio:\n";
        //                    msg += "Up Arrow    Previous Gang\n";
        //                    msg += "Down Arrow  Next Gang\n\n";
        //                    msg += "For left handed control:\n";
        //                    msg += "A     Previous Girl\n";
        //                    msg += "D     Next Girl\n\n";
        //                    msg += "Space   Goto Girl Details\n";
        //                    msg += "Enter   Goto Girl Details\n\n";
        //                    break;
        //                case SCREEN_CREATEMOVIE:
        //                    msg += "Create Movie:\n";
        //                    msg += "No special hotkeys\n";
        //                    msg += "Yet...\n\n";
        //                    break;
        //                case SCREEN_BUILDINGMANAGEMENT:
        //                    msg += "Building Management:\n";
        //                    msg += "This screen is not implimented yet\n\n";
        //                    break;
        //                case SCREEN_SLAVEMARKET:
        //                    msg += "Slave Market:\n";
        //                    msg += "Up Arrow    Previous girl\n";
        //                    msg += "Down Arrow  Next Girl\n\n";
        //                    msg += "For left handed control:\n";
        //                    msg += "A     Previous Girl\n";
        //                    msg += "D     Next Girl\n";
        //                    msg += "S     More Details\n";
        //                    msg += "Space   Purchase Girl\n\n";
        //                    break;
        //                }
        //            }
        //            else
        //            {
        //                switch (g_CurrentScreen)
        //                {
        //                case SCREEN_BROTHEL:
        //                    msg += "Brothel Screen";
        //                    msg += "Right Arrown    Next Brothel\n";
        //                    msg += "Left Arrow      Previous Brothel\n\n";
        //                    break;
        //                case SCREEN_TURNSUMMARY:
        //                    msg += "Up Arrow     Previous Girl\n";
        //                    msg += "Down Arrow   Next Girl\n";
        //                    msg += "Left Arrow   Previous Event\n";
        //                    msg += "Right Arrow  Next Event\n\n";
        //                    break;
        //                case SCREEN_GALLERY:
        //                    msg += "Gallery:\n";
        //                    msg += "Left Arrow     Previous Picture\n";
        //                    msg += "Right Arrow    Next Picture\n\n";
        //                    break;
        //                case SCREEN_TRANSFERGIRLS:
        //                    msg += "Transfer Screen:\n";
        //                    msg += "No special hotkeys\n";
        //                    msg += "Yet...\n\n";
        //                    break;
        //                case SCREEN_GIRLMANAGEMENT:
        //                    msg += "Girl Management:\n";
        //                    msg += "Up Arrow     Previous Girl\n";
        //                    msg += "Down Arrow   Next Girl\n\n";
        //                    break;
        //                case SCREEN_GIRLDETAILS:
        //                    msg += "Girl Details\n";
        //                    msg += "Up Arrow    Previous Girl\n";
        //                    msg += "Down Arrow  Next Girl\n\n";
        //                    break;
        //                case SCREEN_INVENTORY:
        //                    msg += "Inventory Screen:\n";
        //                    msg += "No special hotkeys\n";
        //                    msg += "Yet...\n\n";
        //                    break;
        //                case SCREEN_GANGMANAGEMENT:
        //                    msg += "Gang Management:\n";
        //                    msg += "Up Arrow    Previous Gang\n";
        //                    msg += "Down Arrow  Next Gang\n";
        //                    msg += "Space     Hire gang\n\n";
        //                    break;
        //                case SCREEN_BROTHELMANAGEMENT:
        //                    msg += "Brothel Management:\n";
        //                    msg += "No special hotkeys\n";
        //                    msg += "Yet...\n\n";
        //                    break;
        //                case SCREEN_DUNGEON:
        //                    msg += "Dungeon:\n";
        //                    msg += "Up Arrow    Previous girl\n";
        //                    msg += "Down Arrow  Next Girl\n\n";
        //                    break;
        //                case SCREEN_TOWN:
        //                    msg += "Town:\n";
        //                    msg += "No special hotkeys\n";
        //                    msg += "Yet...\n\n";
        //                    break;
        //                case SCREEN_MAYOR:
        //                    msg += "Mayor:\n";
        //                    msg += "No special hotkeys\n";
        //                    msg += "Yet...\n\n";
        //                    break;
        //                case SCREEN_BANK:
        //                    msg += "Bank:\n";
        //                    msg += "No special hotkeys\n";
        //                    msg += "Yet...\n\n";
        //                    break;
        //                case SCREEN_JAIL:
        //                    msg += "Jail:\n";
        //                    msg += "Up Arrow    Previous Gang\n";
        //                    msg += "Down Arrow  Next Gang\n\n";
        //                    break;
        //                case SCREEN_HOUSE:
        //                    msg += "House:\n";
        //                    msg += "No special hotkeys\n";
        //                    msg += "Yet...\n\n";
        //                    break;
        //                case SCREEN_CLINIC:
        //                    msg += "Clinic:\n";
        //                    msg += "Up Arrow    Previous Gang\n";
        //                    msg += "Down Arrow  Next Gang\n\n";
        //                    break;
        //                case SCREEN_ARENA:
        //                    msg += "Arena:\n";
        //                    msg += "Up Arrow    Previous Gang\n";
        //                    msg += "Down Arrow  Next Gang\n\n";
        //                    break;
        //                case SCREEN_TRYOUTS:
        //                    msg += "Try Outs:\n";
        //                    msg += "No special hotkeys\n";
        //                    msg += "Yet...\n\n";
        //                    break;
        //                case SCREEN_CENTRE:
        //                    msg += "Centre:\n";
        //                    msg += "Up Arrow    Previous Gang\n";
        //                    msg += "Down Arrow  Next Gang\n\n";
        //                    break;
        //                case SCREEN_STUDIO:
        //                    msg += "Studio:\n";
        //                    msg += "Up Arrow    Previous Gang\n";
        //                    msg += "Down Arrow  Next Gang\n\n";
        //                    break;
        //                case SCREEN_CREATEMOVIE:
        //                    msg += "Create Movie:\n";
        //                    msg += "No special hotkeys\n";
        //                    msg += "Yet...\n\n";
        //                    break;
        //                case SCREEN_BUILDINGMANAGEMENT:
        //                    msg += "Building Management:\n";
        //                    msg += "This screen is not implimented yet\n\n";
        //                    break;
        //                case SCREEN_SLAVEMARKET:
        //                    msg += "Slave Market:\n";
        //                    msg += "Up Arrow    Previous girl\n";
        //                    msg += "Down Arrow  Next Girl\n\n";
        //                    break;
        //                }
        //            }
        //            g_MessageQue.AddToQue(msg, 1);
        //            break;

        //        case SDLK_0:
        //        {

        //            msg = "Global Hotkeys:\n";
        //            msg += "1-7         Brothels\n";
        //            msg += "Tab         Cycle Brothels\n";
        //            msg += "Shift-Tab   Reverse\n";
        //            msg += "Escape      Back one screen\n";
        //            msg += "F1          Girl Management\n";
        //            msg += "F2          Gang management\n";
        //            msg += "F3          Dungeon\n";
        //            msg += "F4          SlaveMarket\n";
        //            msg += "F5          Studio\n";
        //            msg += "F6          Arena\n";
        //            msg += "F7          Centre\n";
        //            msg += "F8          Clinic\n";
        //            msg += "F9          Farm\n";
        //            msg += "F10         Town Screen\n";
        //            msg += "F11         Turn Summary\n";
        //            msg += "F12         House\n";
        //            msg += "9           List Hotkeys for this screen.\n";
        //            msg += "0           List Global Hotkeys.\n";
        //            msg += "I           Shop Screen (Inventory)\n";
        //            msg += "\n\n";
        //            msg += "Any Key     Clears message boxes.\n";
        //            msg += "Ctrl + Home Default HotKeys\n";
        //            msg += "Ctrl + End  Alternate HotKeys\n\n";
        //            msg += "Choice Boxes:\n";
        //            msg += "Up Arrow    Move Selection Up\n";
        //            msg += "Down Arrow  Move Selection Down\n";
        //            msg += "Enter       Make Selection\n";
        //            g_MessageQue.AddToQue(msg, 0);
        //            break;
        //        }

        //        case SDLK_SPACE:
        //            g_SpaceKey = true;
        //            break;

        //        default:
        //            // do nothing, but the "default" clause silences an irritating warning
        //            break;
        //        }
        //        switch (vent.key.keysym.sym)
        //        {
        //        case SDLK_RSHIFT:
        //        case SDLK_LSHIFT:
        //            g_ShiftDown = true;		// enable multi select
        //            break;
        //        case SDLK_RCTRL:
        //        case SDLK_LCTRL:
        //            g_CTRLDown = true;		// enable multi select
        //            break;

        //        case SDLK_UP:		g_UpArrow = true;		break;
        //        case SDLK_DOWN:		g_DownArrow = true;		break;
        //        case SDLK_LEFT:		g_LeftArrow = true;		break;
        //        case SDLK_RIGHT:	g_RightArrow = true;	break;
        //        case SDLK_SPACE:	g_SpaceKey = true;		break;
        //        case SDLK_HOME:		g_HomeKey = true;		break;
        //        case SDLK_END:		g_EndKey = true;		break;
        //        case SDLK_PAGEUP:	g_PageUpKey = true;		break;
        //        case SDLK_PAGEDOWN:	g_PageDownKey = true;	break;
        //        case SDLK_TAB:		g_TabKey = true;		break;
        //        case SDLK_ESCAPE:	g_EscapeKey = true;		break;
        //        case SDLK_PERIOD:	g_PeriodKey = true;		break;
        //        case SDLK_SLASH:	g_SlashKey = true;		break;
        //        case SDLK_BACKSLASH:g_BackSlashKey = true;	break;

        //        case SDLK_1:		g_1_Key = true;			break;
        //        case SDLK_2:		g_2_Key = true;			break;
        //        case SDLK_3:		g_3_Key = true;			break;
        //        case SDLK_4:		g_4_Key = true;			break;
        //        case SDLK_5:		g_5_Key = true;			break;
        //        case SDLK_6:		g_6_Key = true;			break;
        //        case SDLK_7:		g_7_Key = true;			break;
        //        case SDLK_8:		g_8_Key = true;			break;
        //        case SDLK_9:		g_9_Key = true;			break;
        //        case SDLK_0:		g_0_Key = true;			break;

        //        case SDLK_F1:		g_F1_Key = true;		break;
        //        case SDLK_F2:		g_F2_Key = true;		break;
        //        case SDLK_F3:		g_F3_Key = true;		break;
        //        case SDLK_F4:		g_F4_Key = true;		break;
        //        case SDLK_F5:		g_F5_Key = true;		break;
        //        case SDLK_F6:		g_F6_Key = true;		break;
        //        case SDLK_F7:		g_F7_Key = true;		break;
        //        case SDLK_F8:		g_F8_Key = true;		break;
        //        case SDLK_F9:		g_F9_Key = true;		break;
        //        case SDLK_F10:		g_F10_Key = true;		break;
        //        case SDLK_F11:		g_F11_Key = true;		break;
        //        case SDLK_F12:		g_F12_Key = true;		break;

        //        case SDLK_a:		g_A_Key = true;		break;
        //        case SDLK_b:		g_B_Key = true;		break;
        //        case SDLK_c:		g_C_Key = true;		break;
        //        case SDLK_d:		g_D_Key = true;		break;
        //        case SDLK_e:		g_E_Key = true;		break;
        //        case SDLK_f:		g_F_Key = true;		break;
        //        case SDLK_g:		g_G_Key = true;		break;
        //        case SDLK_h:		g_H_Key = true;		break;
        //        case SDLK_i:		g_I_Key = true;		break;
        //        case SDLK_j:		g_J_Key = true;		break;
        //        case SDLK_k:		g_K_Key = true;		break;
        //        case SDLK_l:		g_L_Key = true;		break;
        //        case SDLK_m:		g_M_Key = true;		break;
        //        case SDLK_n:		g_N_Key = true;		break;
        //        case SDLK_o:		g_O_Key = true;		break;
        //        case SDLK_p:		g_P_Key = true;		break;
        //        case SDLK_q:		g_Q_Key = true;		break;
        //        case SDLK_r:		g_R_Key = true;		break;
        //        case SDLK_s:		g_S_Key = true;		break;
        //        case SDLK_t:		g_T_Key = true;		break;
        //        case SDLK_u:		g_U_Key = true;		break;
        //        case SDLK_v:		g_V_Key = true;		break;
        //        case SDLK_w:		g_W_Key = true;		break;
        //        case SDLK_x:		g_X_Key = true;		break;
        //        case SDLK_y:		g_Y_Key = true;		break;
        //        case SDLK_z:		g_Z_Key = true;		break;
        //        default:	break;
        //        }
        //    }
        //}
        #endregion

        #region Commented components where utility wasn't defined
        //extern bool g_AllTogle;
        //string g_ReturnText;
        //// SDL Graphics interface

        //// Events
        //SDL_Event vent = new SDL_Event();

        //// TEmporary testing crap
        //int IDS = 0;
        //cRng g_Dice = new cRng();
        #endregion

        #region Game manager
        /// <summary>
        /// Resource Manager.
        /// </summary>
        CResourceManager rmanager = new CResourceManager();
        /// <summary>
        /// Get resource Manager.
        /// </summary>
        CResourceManager g_Resources = new CResourceManager();

        /// <summary>
        /// Girl manager.
        /// </summary>
        private cGirls m_Girls = new cGirls();
        /// <summary>
        /// Get girl manager.
        /// </summary>
        public cGirls g_Girls
        {
            get { return this.m_Girls; }
        }

        /// <summary>
        /// Brothel Manager.
        /// </summary>
        private cBrothelManager m_Brothels = new cBrothelManager();
        /// <summary>
        /// Get brothel Manager.
        /// </summary>
        public cBrothelManager g_Brothels
        {
            get { return this.m_Brothels; }
        }

        /// <summary>
        /// Gang Manager.
        /// </summary>
        private GangManager m_Gangs = new GangManager();
        /// <summary>
        /// Get gang Manager.
        /// </summary>
        public GangManager g_Gangs
        {
            get { return this.m_Gangs; }
        }

        /// <summary>
        /// Customer Manager.
        /// </summary>
        private cCustomers m_Customers = new cCustomers();
        /// <summary>
        /// Get customer Manager.
        /// </summary>
        public cCustomers g_Customers
        {
            get { return this.m_Customers; }
        }

        /// <summary>
        /// Clinic Manager.
        /// </summary>
        private cClinicManager m_Clinic = new cClinicManager();
        /// <summary>
        /// Get clinic Manager.
        /// </summary>
        public cClinicManager g_Clinic
        {
            get { return this.m_Clinic; }
        }

        /// <summary>
        /// Movie Studio Manager.
        /// </summary>
        private cMovieStudioManager m_Studios = new cMovieStudioManager();
        /// <summary>
        /// Get movie Studio Manager.
        /// </summary>
        public cMovieStudioManager g_Studios
        {
            get { return this.m_Studios; }
        }

        /// <summary>
        /// Arena Manager.
        /// </summary>
        private cArenaManager m_Arena = new cArenaManager();
        /// <summary>
        /// Get arena Manager.
        /// </summary>
        public cArenaManager g_Arena
        {
            get { return this.m_Arena; }
        }

        /// <summary>
        /// Centre Manager.
        /// </summary>
        private cCentreManager m_Centre = new cCentreManager();
        /// <summary>
        /// Get centre Manager.
        /// </summary>
        public cCentreManager g_Centre
        {
            get { return this.m_Centre; }
        }

        /// <summary>
        /// House Manager.
        /// </summary>
        private cHouseManager m_House = new cHouseManager();
        /// <summary>
        /// Get house Manager.
        /// </summary>
        public cHouseManager g_House
        {
            get { return this.m_House; }
        }

        /// <summary>
        /// Farm Manager.
        /// </summary>
        private cFarmManager m_Farm = new cFarmManager();
        /// <summary>
        /// Get farm Manager.
        /// </summary>
        public cFarmManager g_Farm
        {
            get { return this.m_Farm; }
        }

        /// <summary>
        /// Inventory manager.
        /// </summary>
        private Inventory m_InvManager = new Inventory();
        /// <summary>
        /// Get inventory manager.
        /// </summary>
        public Inventory g_InvManager
        {
            get { return this.m_InvManager; }
        }

        /// <summary>
        /// The global trigger manager.
        /// </summary>
        private cTriggerList m_GlobalTriggers = new cTriggerList();
        /// <summary>
        /// Get the global trigger manager.
        /// </summary>
        private cTriggerList g_GlobalTriggers
        {
            get { return this.m_GlobalTriggers; }
        }
 
        /// <summary>
        /// Trait list.
        /// </summary>
        private cTraits m_Traits = new cTraits();
        /// <summary>
        /// Get trait list.
        /// </summary>
        private cTraits g_Traits
        {
            get { return this.m_Traits; }
        }
        #endregion
    }
}
