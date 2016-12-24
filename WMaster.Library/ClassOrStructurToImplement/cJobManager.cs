using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Concept;
using WMaster.Concept.Attributs;
using WMaster.Entity.Living;

namespace WMaster.ClassOrStructurToImplement
{
    //mainly a list of functions 
    public class cJobManager
    {
        private List<sFilm> film_list = new List<sFilm>();
        // bah 2d array time for speed
        private List<List<int>> job_groups = new List<List<int>>();
        //static vector<sJobBase *> job_list; - Changed until it is working - necro
        // using an array of function pointers
        //	WorkJobF JobFunc[NUM_JOBS];
        public delegate bool JobFuncDelegate(sGirl NamelessParameter1, sBrothel NamelessParameter2, bool NamelessParameter3, string NamelessParameter4);
        public JobFuncDelegate[] JobFunc = new JobFuncDelegate[(int)WMaster.Enums.Jobs.NUM_JOBS];
        public delegate double JobPerfDelegate(sGirl NamelessParameter1, bool estimate); // `J` a replacement for job performance - work in progress
        public JobPerfDelegate[] JobPerf = new JobPerfDelegate[(int)WMaster.Enums.Jobs.NUM_JOBS];

        public string[] JobName = new string[(int)WMaster.Enums.Jobs.NUM_JOBS]; // short descriptive name of job
        public string[] JobQkNm = new string[(int)WMaster.Enums.Jobs.NUM_JOBS]; // a shorter name of job
        public string[] JobDesc = new string[(int)WMaster.Enums.Jobs.NUM_JOBS]; // longer description of job
        public string[] JobFilterName = new string[(int)WMaster.Enums.JobFilter.NUMJOBTYPES]; // short descriptive name of job filter
        public string[] JobFilterDesc = new string[(int)WMaster.Enums.JobFilter.NUMJOBTYPES]; // longer description of job filter
        public int[] JobFilterIndex = new int[(int)WMaster.Enums.JobFilter.NUMJOBTYPES + 1]; // starting job index # for job filter

        string JobDescriptionCount(int job_id, int brothel_id, int day = (int)WMaster.Enums.DayShift.Day, bool isClinic = false, bool isStudio = false, bool isArena = false, bool isCentre = false, bool isHouse = false, bool isFarm = false)
        { throw new NotImplementedException(); } // return a job description along with a count of how many girls are on it

        bool HandleSpecialJobs(int TargetBrothel, sGirl Girl, int JobID, int OldJobID, bool Day0Night1, bool fulltime = false)
        { throw new NotImplementedException(); } // check for and handle special job assignments

        void Setup()
        { throw new NotImplementedException(); }

        // - Misc
        static bool WorkVoid(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); } // used for jobs that are not yet implemented

        // `J` When modifying Jobs, search for "J-Change-Jobs"  :  found in >> cJobManager.h > class cJobManager

        // - General

        static bool AddictBuysDrugs(string Addiction, string Drug, sGirl girl, sBrothel brothel, bool Day0Night1)
        { throw new NotImplementedException(); }
        static bool WorkFreetime(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); } // resting
        static double JP_Freetime(sGirl girl, bool estimate)
        { throw new NotImplementedException(); } // not used
        static bool WorkTraining(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Training(sGirl girl, bool estimate)
        { throw new NotImplementedException(); } // not used
        static bool WorkCleaning(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Cleaning(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkSecurity(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Security(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkAdvertising(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Advertising(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkCustService(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_CustService(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkMatron(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Matron(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkTorturer(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Torturer(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkExploreCatacombs(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_ExploreCatacombs(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkBeastCare(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_BeastCare(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }

        // - Bar
        static bool WorkBarmaid(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Barmaid(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkBarWaitress(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_BarWaitress(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkBarSinger(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_BarSinger(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkBarPiano(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_BarPiano(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkEscort(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Escort(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkBarCook(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Barcook(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }

        // - Gambling Hall
        static bool WorkHallDealer(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_HallDealer(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkHallEntertainer(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_HallEntertainer(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkHallXXXEntertainer(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_HallXXXEntertainer(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkHallWhore(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_HallWhore(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }

        // - Sleazy Bar
        static bool WorkSleazyBarmaid(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_SleazyBarmaid(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkSleazyWaitress(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_SleazyWaitress(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkBarStripper(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_BarStripper(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkBarWhore(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_BarWhore(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }

        // - Brothel
        static bool WorkBrothelMasseuse(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_BrothelMasseuse(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkBrothelStripper(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_BrothelStripper(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkPeepShow(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_PeepShow(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkWhore(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Whore(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static double JP_WhoreStreets(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }

        // - Movie Studio - Actress
        //BSIN
        //Xxtreme
        static bool WorkFilmBeast(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmBeast(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmBuk(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmBuk(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmThroat(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmThroat(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmBondage(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmBondage(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmPublicBDSM(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmPublicBDSM(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }

        /*static bool WorkFilmDominatrix(sGirl* girl, sBrothel* brothel, bool Day0Night1, string& summary);
        static double JP_FilmDom(sGirl* girl, bool estimate);*/

        //Adult
        static bool WorkFilmSex(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmSex(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmAnal(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmAnal(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmLesbian(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmLesbian(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmGroup(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmGroup(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmOral(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmOral(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmMast(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmMast(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmTitty(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmTitty(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmHandJob(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmHandJob(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmFootJob(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmFootJob(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }

        //Actress
        //static bool WorkFilmIdol(sGirl* girl, sBrothel* brothel, bool Day0Night1, string& summary);
        //static double JP_FilmIdol(sGirl* girl, bool estimate);
        static bool WorkFilmAction(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmAction(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmMusic(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmMusic(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmChef(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmChef(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmTease(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmTease(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmStrip(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmStrip(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }

        //Rand
        static bool WorkFilmRandom(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmRandom(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }

        // - Movie Studio - Crew
        static bool WorkFilmDirector(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmDirector(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmPromoter(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmPromoter(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkCameraMage(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_CameraMage(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkCrystalPurifier(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_CrystalPurifier(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFluffer(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Fluffer(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFilmStagehand(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FilmStagehand(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }

        // - Arena - Fighting
        static bool WorkFightBeast(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FightBeast(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFightArenaGirls(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FightArenaGirls(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkCombatTraining(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_CombatTraining(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkArenaJousting(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_ArenaJousting(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkArenaRacing(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_ArenaRacing(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }

        // - Arena - Staff
        static bool WorkDoctore(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Doctore(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkCityGuard(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_CityGuard(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkBlacksmith(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Blacksmith(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkCobbler(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Cobbler(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkJeweler(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Jeweler(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkCleanArena(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_CleanArena(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }

        //Comunity Centre
        static bool WorkCentreManager(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_CentreManager(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFeedPoor(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_FeedPoor(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkComunityService(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_ComunityService(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkCleanCentre(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_CleanCentre(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }

        // Counseling Centre
        static bool WorkCounselor(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Counselor(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkRehab(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Rehab(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkCentreAngerManagement(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_CentreAngerManagement(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkCentreExTherapy(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_CentreExTherapy(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkCentreTherapy(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_CentreTherapy(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }

        // house
        static bool WorkHeadGirl(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_HeadGirl(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkRecruiter(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_Recruiter(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkPersonalTraining(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_PersonalTraining(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkPersonalBedWarmer(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_PersonalBedWarmer(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkCleanHouse(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_CleanHouse(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkHouseVacation(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_HouseVacation(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkHouseCook(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_HouseCook(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkHousePet(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }
        static double JP_HousePet(sGirl girl, bool estimate)
        { throw new NotImplementedException(); }
        static bool WorkFarmPonyGirl(sGirl girl, sBrothel brothel, bool Day0Night1, string summary)
        { throw new NotImplementedException(); }

        // - stuff that does processing for jobs

        // MYR: New code for security.  All the old code is still here, commented out.
        static bool work_related_violence(sGirl NamelessParameter1, bool NamelessParameter2, bool NamelessParameter3)
        { throw new NotImplementedException(); }
        static int guard_coverage(List<Gang> v = null)
        { throw new NotImplementedException(); }
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //	static bool security_stops_rape(sGirl girl, sGang enemy_gang, int day_night);
        //static bool gang_stops_rape(sGirl* girl, sGang *gang, int chance, int day_night);
        static bool gang_stops_rape(sGirl girl, List<Gang> gangs_guarding, Gang enemy_gang, int coverage, int day_night)
        { throw new NotImplementedException(); }
        //static bool girl_fights_rape(sGirl*, int);
        static bool girl_fights_rape(sGirl girl, Gang enemy_gang, int day_night)
        { throw new NotImplementedException(); }
        static void customer_rape(sGirl girl, int numberofattackers)
        { throw new NotImplementedException(); }
        static string GetGirlAttackedString(EnumSkills attacktype = EnumSkills.COMBAT)
        { throw new NotImplementedException(); } // `J` added attacktype to be used with sextype for more specific attacks defaulting to combat


        static bool Preprocessing(int action, sGirl girl, sBrothel brothel, bool Day0Night1, string summary, string message)
        { throw new NotImplementedException(); }
        static void GetMiscCustomer(sBrothel brothel, sCustomer cust)
        { throw new NotImplementedException(); }

        bool work_show(sGirl girl, sBrothel brothel, string summary, bool Day0Night1)
        { throw new NotImplementedException(); }
        void update_film(sBrothel NamelessParameter)
        { throw new NotImplementedException(); }
        int make_money_films()
        { throw new NotImplementedException(); }
        void save_films(System.IO.Stream ofs)
        { throw new NotImplementedException(); }
        void load_films(System.IO.Stream ifs)
        { throw new NotImplementedException(); }
        bool apply_job(sGirl girl, int job, int brothel_id, bool Day0Night1, string message)
        { throw new NotImplementedException(); }
        int get_num_on_job(sBrothel brothel, int job_wanted, bool Day0Night1)
        { throw new NotImplementedException(); }
        static bool is_sex_type_allowed(uint sex_type, sBrothel brothel)
        { throw new NotImplementedException(); }
        static bool nothing_banned(sBrothel brothel)
        { throw new NotImplementedException(); }
#if ! DEBUG
        static void free()
        { throw new NotImplementedException(); }
#else
        static void freeJobs()
        { throw new NotImplementedException(); }
#endif

        //helpers
        static List<sGirl> girls_on_job(sBrothel brothel, int job_wanted, bool Day0Night1)
        { throw new NotImplementedException(); }
        //need a function for seeing if there is a girl working on a job
        bool is_job_employed(sBrothel brothel, int job_wanted, bool Day0Night1)
        { throw new NotImplementedException(); }
        static void get_training_set(List<sGirl> v, List<sGirl> set)
        { throw new NotImplementedException(); }
        static void do_training(sBrothel brothel, bool Day0Night1)
        { throw new NotImplementedException(); }
        static void do_training_set(List<sGirl> girls, bool Day0Night1)
        { throw new NotImplementedException(); }
        static void do_solo_training(sGirl girl, bool Day0Night1)
        { throw new NotImplementedException(); }
        static void do_advertising(sBrothel brothel, bool Day0Night1)
        { throw new NotImplementedException(); }
        static void do_whorejobs(sBrothel brothel, bool Day0Night1)
        { throw new NotImplementedException(); }
        static void do_custjobs(sBrothel brothel, bool Day0Night1)
        { throw new NotImplementedException(); }
        public void Dispose()
        {
        }

        bool is_job_Paid_Player(int Job)
        { throw new NotImplementedException(); } //    WD:    Test for all jobs paid by player
        bool FullTimeJob(int Job)
        { throw new NotImplementedException(); } //    `J`    Test if job is takes both shifts
        string GirlPaymentText(sBrothel brothel, sGirl girl, int totalTips, int totalPay, int totalGold, bool Day0Night1)
        { throw new NotImplementedException(); }
        void FreeSlaves(sGirl girl, bool multi = false)
        { throw new NotImplementedException(); }
        void ffsd_choice(int ffsd, List<int> girl_array, string buildingtype, int buildingnum)
        { throw new NotImplementedException(); }
        void ffsd_outcome(List<int> girl_array, string sub, int num)
        { throw new NotImplementedException(); }
    }
}
