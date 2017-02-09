using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Enums;
using WMaster.Concept;
using WMaster.Entity.Item;
using WMaster.Entity.Living;
using WMaster.Manager;
using WMaster.Concept.Attributs;

namespace WMaster.ClassOrStructurToImplement
{
    // Represents a single girl
    public class sGirl : System.IDisposable, IComparable<sGirl>
    {
        sGirl()
        { throw new NotImplementedException(); }
        public void Dispose()
        { throw new NotImplementedException(); }

        public int m_newRandomFixed;

        /// <summary>
        /// Girls name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Name displayed in text
        /// </summary>
        public string Realname { get; set; }
        /*	`J` adding first and surnames for future use.
        *	m_Realname will be used for girl tracking until first and surnames are fully integrated
        *	a girl id number system may be added in the future to allow for absolute tracking
        */
        /// <summary>
        /// Girl's first name
        /// </summary>
        public string FirstName { get; set; } 
        /// <summary>
        /// Girl's middle name
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// Girl's surname
        /// </summary>
        public string Surname { get; set; }
        //    `J` added mother and father names
        /// <summary>
        /// Mother names
        /// </summary>
        public string MotherName { get; set; }
        /// <summary>
        /// Father names
        /// </summary>
        public string FatherName { get; set; }
        /*
        *	MOD: changed from char* -- easier to change from lua -- doc
        */
        public string m_Desc; // Short story about the girl

        public byte m_NumTraits; // current number of traits they have
        public sTrait[] m_Traits = new sTrait[Constants.MAXNUM_TRAITS]; // List of traits they have
        public int[] m_TempTrait = new int[Constants.MAXNUM_TRAITS]; // a temp trait if not 0. Trait removed when == 0. traits last for 20 weeks.

        public byte m_NumRememTraits; // number of traits that are apart of the girls starting traits
        public sTrait[] m_RememTraits = new sTrait[Constants.MAXNUM_TRAITS * 2]; // List of traits they have inbuilt

        /// <summary>
        /// What job the girl is currently doing the day.
        /// </summary>
        public Jobs DayJob { get; set; }
        /// <summary>
        /// What job the girl is currently doing the night.
        /// </summary>
        public Jobs NightJob { get; set; }
        /// <summary>
        /// What job the girl was doing the day.
        /// </summary>
        public Jobs PrevDayJob { get; set; }
        /// <summary>
        /// What job the girl was doing the night.
        /// </summary>
        public Jobs PrevNightJob { get; set; }
        /// <summary>
        /// What job the girl did yesterday day.
        /// </summary>
        public Jobs YesterDayJob { get; set; }
        /// <summary>
        /// What job the girl did yesterday night.
        /// </summary>
        public Jobs YesterNightJob { get; set; }

        //ADB needs to be int because player might have more than 256
        public int m_NumInventory; // current amount of inventory they have
        public sInventoryItem[] m_Inventory = new sInventoryItem[Constants.MAXNUM_GIRL_INVENTORY]; // List of inventory items they have (40 max)
        public byte[] m_EquipedItems = new byte[Constants.MAXNUM_GIRL_INVENTORY]; // value of > 0 means equipped (wearing) the item

        public int m_States; // Holds the states the girl has
        public int m_BaseStates; // `J` Holds base states the girl has for use with equipable items

        // Abstract stats (not shown as numbers but as a raiting)
        public int[] m_Stats = new int[(int)EnumStats.NUM_STATS];
        public int[] m_StatTr = new int[(int)EnumStats.NUM_STATS]; // Trait modifiers to stats
        public int[] m_StatMods = new int[(int)EnumStats.NUM_STATS]; // perminant modifiers to stats
        public int[] m_StatTemps = new int[(int)EnumStats.NUM_STATS]; // these go down (or up) by 30% each week until they reach 0

        public int[] m_Enjoyment = new int[(int)ActionTypes.NUM_ACTIONTYPES]; // these values determine how much a girl likes an action
        public int[] m_EnjoymentTR = new int[(int)ActionTypes.NUM_ACTIONTYPES]; // `J` added for traits to affect enjoyment
        public int[] m_EnjoymentMods = new int[(int)ActionTypes.NUM_ACTIONTYPES]; // `J` added perminant modifiers to stats
        public int[] m_EnjoymentTemps = new int[(int)ActionTypes.NUM_ACTIONTYPES]; // `J` added these go down (or up) by 30% each week until they reach 0
        // (-100 is hate, +100 is loves)
        public int m_Virgin; // is she a virgin, 0=false, 1=true, -1=not checked

        public bool m_UseAntiPreg; // if true she will use anit preg measures

        public byte m_Withdrawals; // if she is addicted to something this counts how many weeks she has been off

        public int Money { get; set; }

        public int m_AccLevel; // how good her Accommodation is, 0 is slave like and non-slaves will really hate it

        public int[] m_Skills = new int[(int)EnumSkills.NUM_SKILLS];
        public int[] m_SkillTr = new int[(int)EnumSkills.NUM_SKILLS];
        public int[] m_SkillMods = new int[(int)EnumSkills.NUM_SKILLS];
        public int[] m_SkillTemps = new int[(int)EnumSkills.NUM_SKILLS]; // these go down (or up) by 1 each week until they reach 0

        public int[] m_Training = new int[(int)TrainingTypes.NUM_TRAININGTYPES]; // these values determine how far a girl is into her training CRAZY
        public int[] m_TrainingTR = new int[(int)TrainingTypes.NUM_TRAININGTYPES];
        public int[] m_TrainingMods = new int[(int)TrainingTypes.NUM_TRAININGTYPES];
        public int[] m_TrainingTemps = new int[(int)TrainingTypes.NUM_TRAININGTYPES];
        // (starts at 0, 100 if fully trained)

        public int m_RunAway; // if 0 then off, if 1 then girl is removed from list,
        // otherwise will count down each week
        public byte m_Spotted; // if 1 then she has been seen stealing but not punished yet

        public uint m_WeeksPast; // number of weeks in your service
        public uint m_BDay; // number of weeks in your service since last aging

        public int BirthMonth;
        public int BirthDay;


        public uint m_NumCusts; // number of customers this girl has slept with
        public uint m_NumCusts_old; // number of customers this girl has slept with before this week

        public bool m_Tort; // if true then have already tortured today
        public bool m_JustGaveBirth; // did she give birth this current week?

        /// <summary>
        /// Keep track of pay this turn.
        /// </summary>
        public int Pay { get; set; }
        /// <summary>
        /// Keep track of tips this turn.
        /// </summary>
        public int Tips { get; set; }

        public int m_FetishTypes; // the types of fetishes this girl has

        public string m_Flags = new string(new char[Constants.NUM_GIRLFLAGS]); // flags used by scripts

        /// <summary>
        /// Each girl keeps track of all her events that happened to her in the last turn.
        /// </summary>
        private EventManager m_Events;
        /// <summary>
        /// Get <see cref="EventManager"/>of all her events that happened to her in the last turn.
        /// </summary>
        public EventManager Events
        {
            get
            {
                if (this.m_Events == null)
                { m_Events = new EventManager(); }
                return m_Events;
            }
        }


        public cTriggerList m_Triggers = new cTriggerList(); // triggers for the girl

        public byte m_DaysUnhappy; // used to track how many days they are really unhappy for

        public int m_WeeksPreg; // number of weeks pregnant or inseminated
        public int PregCooldown { get; set; } // number of weeks until can get pregnant again
        public cChildList m_Children = new cChildList();
        public int[] m_ChildrenCount = new int[Constants.CHILD_COUNT_TYPES];

        public List<string> m_Canonical_Daughters = new List<string>();

        public bool m_InStudio;
        public bool m_InArena;
        public bool m_InCentre;
        public bool m_InClinic;
        public bool m_InFarm;
        public bool m_InHouse;
        public int where_is_she;
        public int m_PrevWorkingDay; // `J` save the last count of the number of working days
        public int m_WorkingDay; // count the number of working day
        public int m_SpecialJobGoal; // `J` Special Jobs like surgeries will have a specific goal
        public bool m_Refused_To_Work_Day; // `J` to track better if she refused to work her assigned job
        public bool m_Refused_To_Work_Night; // `J` to track better if she refused to work her assigned job


        void dump(System.IO.Stream os)
        { throw new NotImplementedException(); }

        /*
        *	MOD: docclox. attach the skill and stat names to the
        *	class that uses them. Plus an XML load method and
        *	an ostream << operator to pretty print the struct for
        *	debug purposes.
        *
        *	Sun Nov 15 05:58:55 GMT 2009
        */
        public readonly string[] stat_names;
        [Obsolete("Use skill GetName() function", false)]
        public static readonly string[] skill_names;
        public readonly string[] status_names;
        public readonly string[] enjoy_names;
        public readonly string[] enjoy_jobs;
        public readonly string[] training_names;
        public readonly string[] training_jobs;
        public readonly string[] children_type_names; // `J` added
        /*
        *	again, might as well make them part of the struct that uses them
        */
        public readonly uint max_stats;
        public readonly uint max_skills;
        public readonly uint max_statuses;
        public readonly uint max_enjoy;
        public readonly uint max_jobs;
        public readonly uint max_training;
        /*
    *	we need to be able to go the other way, too:
    *	from string to number. The maps map stat/skill names
    *	onto index numbers. The setup flag is so we can initialise
    * 	the maps the first time an sGirl is constructed
    */
        public static bool m_maps_setup;
        public static SortedDictionary<string, uint> stat_lookup = new SortedDictionary<string, uint>();
        public static SortedDictionary<string, uint> skill_lookup = new SortedDictionary<string, uint>();
        public static SortedDictionary<string, uint> status_lookup = new SortedDictionary<string, uint>();
        public static SortedDictionary<string, uint> enjoy_lookup = new SortedDictionary<string, uint>();
        public static SortedDictionary<string, uint> jobs_lookup = new SortedDictionary<string, uint>();
        public static SortedDictionary<string, uint> training_lookup = new SortedDictionary<string, uint>();

        static void setup_maps()
        { throw new NotImplementedException(); }

        static int lookup_stat_code(string s)
        { throw new NotImplementedException(); }
        static int lookup_skill_code(string s)
        { throw new NotImplementedException(); }
        static int lookup_status_code(string s)
        { throw new NotImplementedException(); }
        static int lookup_enjoy_code(string s)
        { throw new NotImplementedException(); }
        static int lookup_jobs_code(string s)
        { throw new NotImplementedException(); }
        static int lookup_training_code(string s)
        { throw new NotImplementedException(); }
        /*
        *	Strictly speaking, methods don't belong in structs.
        *	I've always thought that more of a guideline than a hard and fast rule
        */
        void load_from_xml(IXmlElement el)
        { throw new NotImplementedException(); }// uses sGirl::load_from_xml
        IXmlElement SaveGirlXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadGirlXML(IXmlHandle hGirl)
        { throw new NotImplementedException(); }

        /*
        *	stream operator - used for debug
        */
        //C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
        //ORIGINAL LINE: friend ostream& operator <<(ostream& os, sGirl &g);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //	ostream operator <<(ostream os, sGirl g);
        /*
        *	it's a bit daft that we have to go through the global g_Girls
        *	every time we want a stat.
        *
        *	I mean the sGirl type is the one we're primarily concerned with.
        *	that ought to be the base for the query.
        *
        *	Of course, I could just index into the stat array,
        *	but I'm not sure what else the cGirls method does.
        *	So this is safer, if a bit inefficient.
        */
        public int get_stat(int stat_id)
        {
            return GameEngine.Instance.g_GirlsPtr.GetStat(this, stat_id);
        }
        public int upd_temp_stat(EnumStats stat, int amount)
        {
            GameEngine.Instance.g_GirlsPtr.UpdateStatTemp(this, stat, amount);
            return GameEngine.Instance.g_GirlsPtr.GetStat(this, (int)stat);
        }
        public int upd_stat(EnumStats stat, int amount, bool usetraits = true)
        {
            GameEngine.Instance.g_GirlsPtr.UpdateStat(this, stat, amount, usetraits);
            return GameEngine.Instance.g_GirlsPtr.GetStat(this, (int)stat);
        }

        public int upd_temp_Enjoyment(int stat_id, int amount)
        {
            GameEngine.Instance.g_GirlsPtr.UpdateEnjoymentTemp(this, stat_id, amount);
            return GameEngine.Instance.g_GirlsPtr.GetEnjoyment(this, stat_id);
        }
        public int upd_Enjoyment(int stat_id, int amount, bool usetraits = true)
        {
            GameEngine.Instance.g_GirlsPtr.UpdateEnjoyment(this, stat_id, amount);
            return GameEngine.Instance.g_GirlsPtr.GetEnjoyment(this, stat_id);
        }

        public int upd_temp_Training(int stat_id, int amount)
        {
            GameEngine.Instance.g_GirlsPtr.UpdateTrainingTemp(this, stat_id, amount);
            return GameEngine.Instance.g_GirlsPtr.GetTraining(this, stat_id);
        }
        public int upd_Training(int stat_id, int amount, bool usetraits = true)
        {
            GameEngine.Instance.g_GirlsPtr.UpdateTraining(this, stat_id, amount);
            return GameEngine.Instance.g_GirlsPtr.GetTraining(this, stat_id);
        }
        /*
    *	Now then:
    */
        // `J` When modifying Stats or Skills, search for "J-Change-Stats-Skills"  :  found in >> cGirls.h
        public int charisma()
        {
            return get_stat((int)EnumStats.Charisma);
        }
        public int charisma(int n)
        {
            return upd_stat(EnumStats.Charisma, n);
        }
        public int happiness()
        {
            return get_stat((int)EnumStats.Happiness);
        }
        public int happiness(int n)
        {
            return upd_stat(EnumStats.Happiness, n);
        }
        public int libido()
        {
            return get_stat((int)EnumStats.Libido);
        }
        public int libido(int n)
        {
            return upd_stat(EnumStats.Libido, n);
        }
        public int constitution()
        {
            return get_stat((int)EnumStats.Constitution);
        }
        public int constitution(int n)
        {
            return upd_stat(EnumStats.Constitution, n);
        }
        public int intelligence()
        {
            return get_stat((int)EnumStats.Intelligence);
        }
        public int intelligence(int n)
        {
            return upd_stat(EnumStats.Intelligence, n);
        }
        public int confidence()
        {
            return get_stat((int)EnumStats.Confidence);
        }
        public int confidence(int n)
        {
            return upd_stat(EnumStats.Confidence, n);
        }
        public int mana()
        {
            return get_stat((int)EnumStats.Mana);
        }
        public int mana(int n)
        {
            return upd_stat(EnumStats.Mana, n);
        }
        public int agility()
        {
            return get_stat((int)EnumStats.Agility);
        }
        public int agility(int n)
        {
            return upd_stat(EnumStats.Agility, n);
        }
        public int strength()
        {
            return get_stat((int)EnumStats.Strength);
        }
        public int strength(int n)
        {
            return upd_stat(EnumStats.Strength, n);
        }
        public int fame()
        {
            return get_stat((int)EnumStats.Fame);
        }
        public int fame(int n)
        {
            return upd_stat(EnumStats.Fame, n);
        }
        public int level()
        {
            return get_stat((int)EnumStats.Level);
        }
        public int level(int n)
        {
            return upd_stat(EnumStats.Level, n);
        }
        public int askprice()
        {
            return get_stat((int)EnumStats.AskPrice);
        }
        public int askprice(int n)
        {
            return upd_stat(EnumStats.AskPrice, n);
        }
        /* It's NOT lupus! */
        public int house()
        {
            return get_stat((int)EnumStats.House);
        }
        public int house(int n)
        {
            return upd_stat(EnumStats.House, n);
        }
        public int exp()
        {
            return get_stat((int)EnumStats.Exp);
        }
        public int exp(int n)
        {
            return upd_stat(EnumStats.Exp, n);
        }
        public int age()
        {
            return get_stat((int)EnumStats.Age);
        }
        public int age(int n)
        {
            return upd_stat(EnumStats.Age, n);
        }
        public int obedience()
        {
            return get_stat((int)EnumStats.Obedience);
        }
        public int obedience(int n)
        {
            return upd_stat(EnumStats.Obedience, n);
        }
        public int spirit()
        {
            return get_stat((int)EnumStats.Spirit);
        }
        public int spirit(int n)
        {
            return upd_stat(EnumStats.Spirit, n);
        }
        public int beauty()
        {
            return get_stat((int)EnumStats.Beauty);
        }
        public int beauty(int n)
        {
            return upd_stat(EnumStats.Beauty, n);
        }
        public int tiredness()
        {
            return get_stat((int)EnumStats.Tiredness);
        }
        public int tiredness(int n)
        {
            return upd_stat(EnumStats.Tiredness, n);
        }
        public int health()
        {
            return get_stat((int)EnumStats.Health);
        }
        public int health(int n)
        {
            return upd_stat(EnumStats.Health, n);
        }
        public int pcfear()
        {
            return get_stat((int)EnumStats.PCFear);
        }
        public int pcfear(int n)
        {
            return upd_stat(EnumStats.PCFear, n);
        }
        public int pclove()
        {
            return get_stat((int)EnumStats.PCLove);
        }
        public int pclove(int n)
        {
            return upd_stat(EnumStats.PCLove, n);
        }
        public int pchate()
        {
            return get_stat((int)EnumStats.PCHate);
        }
        public int pchate(int n)
        {
            return upd_stat(EnumStats.PCHate, n);
        }
        public int morality()
        {
            return get_stat((int)EnumStats.Morality);
        }
        public int morality(int n)
        {
            return upd_stat(EnumStats.Morality, n);
        }
        public int refinement()
        {
            return get_stat((int)EnumStats.Refinement);
        }
        public int refinement(int n)
        {
            return upd_stat(EnumStats.Refinement, n);
        }
        public int dignity()
        {
            return get_stat((int)EnumStats.Dignity);
        }
        public int dignity(int n)
        {
            return upd_stat(EnumStats.Dignity, n);
        }
        public int lactation()
        {
            return get_stat((int)EnumStats.Lactation);
        }
        public int lactation(int n)
        {
            return upd_stat(EnumStats.Lactation, n);
        }
        public int npclove()
        {
            return get_stat((int)EnumStats.NPCLove);
        }
        public int npclove(int n)
        {
            return upd_stat(EnumStats.NPCLove, n);
        }
        public int sanity()
        {
            return get_stat((int)EnumStats.Sanity);
        }
        public int sanity(int n)
        {
            return upd_stat(EnumStats.Sanity, n);
        }


        int rebel()
        { throw new NotImplementedException(); }
        string JobRating(double value, string type = "", string name = "")
        { throw new NotImplementedException(); }
        string JobRatingLetter(double value)
        { throw new NotImplementedException(); }
        public bool FixFreeTimeJobs()
        { throw new NotImplementedException(); }
        /*
        *	notice that if we do tweak get_stat to reference the stats array
        *	direct, the above still work.
        *
        *	similarly...
        */
        public int get_skill(EnumSkills skill)
        {
            return GameEngine.Instance.g_GirlsPtr.GetSkill(this, skill);
        }
        public int upd_temp_skill(EnumSkills skill, int amount)
        {
            GameEngine.Instance.g_GirlsPtr.UpdateSkillTemp(this, (int)skill, amount);
            return GameEngine.Instance.g_GirlsPtr.GetSkill(this, skill);
        }
        public int upd_skill(EnumSkills skill, int amount)
        {
            GameEngine.Instance.g_GirlsPtr.UpdateSkill(this, skill, amount);
            return GameEngine.Instance.g_GirlsPtr.GetSkill(this, skill);
        }
        public int anal()
        {
            return get_skill(EnumSkills.Anal);
        }
        public int anal(int n)
        {
            return upd_skill(EnumSkills.Anal, n);
        }
        public int bdsm()
        {
            return get_skill(EnumSkills.BDSM);
        }
        public int bdsm(int n)
        {
            return upd_skill(EnumSkills.BDSM, n);
        }
        public int beastiality()
        {
            return get_skill(EnumSkills.Beastiality);
        }
        public int beastiality(int n)
        {
            return upd_skill(EnumSkills.Beastiality, n);
        }
        public int combat()
        {
            return get_skill(EnumSkills.Combat);
        }
        public int combat(int n)
        {
            return upd_skill(EnumSkills.Combat, n);
        }
        public int group()
        {
            return get_skill(EnumSkills.GroupSex);
        }
        public int group(int n)
        {
            return upd_skill(EnumSkills.GroupSex, n);
        }
        public int lesbian()
        {
            return get_skill(EnumSkills.Lesbian);
        }
        public int lesbian(int n)
        {
            return upd_skill(EnumSkills.Lesbian, n);
        }
        public int magic()
        {
            return get_skill(EnumSkills.Magic);
        }
        public int magic(int n)
        {
            return upd_skill(EnumSkills.Magic, n);
        }
        public int normalsex()
        {
            return get_skill(EnumSkills.NormalSex);
        }
        public int normalsex(int n)
        {
            return upd_skill(EnumSkills.NormalSex, n);
        }
        public int oralsex()
        {
            return get_skill(EnumSkills.OralSex);
        }
        public int oralsex(int n)
        {
            return upd_skill(EnumSkills.OralSex, n);
        }
        public int tittysex()
        {
            return get_skill(EnumSkills.TittySex);
        }
        public int tittysex(int n)
        {
            return upd_skill(EnumSkills.TittySex, n);
        }
        public int handjob()
        {
            return get_skill(EnumSkills.HandJob);
        }
        public int handjob(int n)
        {
            return upd_skill(EnumSkills.HandJob, n);
        }
        public int footjob()
        {
            return get_skill(EnumSkills.FootJob);
        }
        public int footjob(int n)
        {
            return upd_skill(EnumSkills.FootJob, n);
        }
        public int service()
        {
            return get_skill(EnumSkills.Service);
        }
        public int service(int n)
        {
            return upd_skill(EnumSkills.Service, n);
        }
        public int strip()
        {
            return get_skill(EnumSkills.Striptease);
        }
        public int strip(int n)
        {
            return upd_skill(EnumSkills.Striptease, n);
        }
        public int medicine()
        {
            return get_skill(EnumSkills.Medicine);
        }
        public int medicine(int n)
        {
            return upd_skill(EnumSkills.Medicine, n);
        }
        public int performance()
        {
            return get_skill(EnumSkills.Performance);
        }
        public int performance(int n)
        {
            return upd_skill(EnumSkills.Performance, n);
        }
        public int crafting()
        {
            return get_skill(EnumSkills.Crafting);
        }
        public int crafting(int n)
        {
            return upd_skill(EnumSkills.Crafting, n);
        }
        public int herbalism()
        {
            return get_skill(EnumSkills.Herbalism);
        }
        public int herbalism(int n)
        {
            return upd_skill(EnumSkills.Herbalism, n);
        }
        public int farming()
        {
            return get_skill(EnumSkills.Farming);
        }
        public int farming(int n)
        {
            return upd_skill(EnumSkills.Farming, n);
        }
        public int brewing()
        {
            return get_skill(EnumSkills.Brewing);
        }
        public int brewing(int n)
        {
            return upd_skill(EnumSkills.Brewing, n);
        }
        public int animalhandling()
        {
            return get_skill(EnumSkills.AnimalHandling);
        }
        public int animalhandling(int n)
        {
            return upd_skill(EnumSkills.AnimalHandling, n);
        }
        public int cooking()
        {
            return get_skill(EnumSkills.Cooking);
        }
        public int cooking(int n)
        {
            return upd_skill(EnumSkills.Cooking, n);
        }

        public int get_enjoyment(int actiontype)
        {
            return GameEngine.Instance.g_GirlsPtr.GetEnjoyment(this, actiontype);
        }
        public int get_training(int actiontype)
        {
            return GameEngine.Instance.g_GirlsPtr.GetTraining(this, actiontype);
        }

        /*
        *	convenience func. Also easier to read like this
        */
        bool carrying_monster()
        { throw new NotImplementedException(); }
        bool carrying_human()
        { throw new NotImplementedException(); }
        bool carrying_players_child()
        { throw new NotImplementedException(); }
        bool carrying_customer_child()
        { throw new NotImplementedException(); }
        bool is_pregnant()
        { throw new NotImplementedException(); }
        bool is_mother()
        { throw new NotImplementedException(); }
        bool is_poisoned()
        { throw new NotImplementedException(); }
        bool has_weapon()
        { throw new NotImplementedException(); }
        void clear_pregnancy()
        { throw new NotImplementedException(); }
        void clear_dating()
        { throw new NotImplementedException(); }

        int preg_chance(int base_pc, bool good = false, double factor = 1.0)
        { throw new NotImplementedException(); }

        bool calc_pregnancy(int NamelessParameter1, Player NamelessParameter2)
        { throw new NotImplementedException(); }
        bool calc_pregnancy(Player player, bool good = false, double factor = 1.0)
        { throw new NotImplementedException(); }
        bool calc_insemination(Player player, bool good = false, double factor = 1.0)
        { throw new NotImplementedException(); }
        bool calc_group_pregnancy(Player player, bool good = false, double factor = 1.0)
        { throw new NotImplementedException(); }

        bool calc_pregnancy(int NamelessParameter1, sCustomer NamelessParameter2)
        { throw new NotImplementedException(); }
        bool calc_pregnancy(sCustomer cust, bool good = false, double factor = 1.0)
        { throw new NotImplementedException(); }
        bool calc_insemination(sCustomer cust, bool good = false, double factor = 1.0)
        { throw new NotImplementedException(); }
        bool calc_group_pregnancy(sCustomer cust, bool good = false, double factor = 1.0)
        { throw new NotImplementedException(); }
        /*
        *	let's overload that...
        *	should be able to do the same using sCustomer as well...
        */
        public void add_trait(string trait, int temptime = 0)
        { throw new NotImplementedException(); }
        void remove_trait(string trait)
        { throw new NotImplementedException(); }
        public bool has_trait(string trait)
        { throw new NotImplementedException(); }
        int breast_size()
        { throw new NotImplementedException(); }
        public bool is_dead(bool sendmessage = false)
        { throw new NotImplementedException(); } // `J` replaces a few DeadGirl checks
        public bool is_addict(bool onlyhard = false)
        { throw new NotImplementedException(); } // `J` added bool onlyhard to allow only hard drugs to be checked for
        bool has_disease()
        { throw new NotImplementedException(); }
        bool is_fighter(bool canbehelped = false)
        { throw new NotImplementedException(); }
        sChild next_child(sChild child, bool remove = false)
        { throw new NotImplementedException(); }
        int preg_type(int image_type)
        { throw new NotImplementedException(); }
        public sGirl run_away()
        { throw new NotImplementedException(); }

        public bool is_slave()
        {
            return (m_States & (1 << (int)Status.SLAVE)) != 0;
        }
        public bool is_free()
        {
            return !is_slave();
        }
        public void set_slave()
        {
            m_States |= (1 << (int)Status.SLAVE);
        }
        public bool is_monster()
        {
            return (m_States & (1 << (int)Status.CATACOMBS)) != 0;
        }
        public bool is_human()
        {
            return !is_monster();
        }
        public bool is_arena()
        {
            return (m_States & (1 << (int)Status.ARENA)) != 0;
        }
        public bool is_yourdaughter()
        {
            return (m_States & (1 << (int)Status.YOURDAUGHTER)) != 0;
        }
        public bool is_isdaughter()
        {
            return (m_States & (1 << (int)Status.ISDAUGHTER)) != 0;
        }
        public bool is_warrior()
        {
            return !is_arena();
        }

        public bool is_resting()
        { throw new NotImplementedException(); }

        void fight_own_gang(ref bool girl_wins)
        { throw new NotImplementedException(); }
        void win_vs_own_gang(List<Gang> v, int max_goons, ref bool girl_wins)
        { throw new NotImplementedException(); }
        void lose_vs_own_gang(List<Gang> v, int max_goons, int girl_stats, int gang_stats, ref bool girl_wins)
        { throw new NotImplementedException(); }

        void OutputGirlRow(string Data, List<string> columnNames)
        { throw new NotImplementedException(); }
        void OutputGirlDetailString(string Data, string detailName)
        { throw new NotImplementedException(); }

        // END MOD

        public override int GetHashCode()
        {
            return this.Realname.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            sGirl girl = obj as sGirl;
            if (obj == null) { return false; }
            return this.Realname.Equals(girl.Realname);
        }

        public int CompareTo(sGirl other)
        {
            if (other == null)
            {
                return -1;
            }

            return this.Realname.CompareTo(other.Realname);
            throw new NotImplementedException();
        }
    }
}
