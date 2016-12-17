using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public class cConfig
    {
        private static cConfig _instance;
        public static cConfig Instance
        {
            get
            {
                if (cConfig._instance == null)
                { cConfig._instance = new cConfig(); }
                return cConfig._instance;
            }
        }
        private static sConfigData data;

        public cConfig()
        { throw new NotImplementedException(); }
        void reload(string filename)
        { throw new NotImplementedException(); }

        public class InFactors
        {
            public double extortion()
            {
                return data.in_fact.extortion;
            }
            public double brothel_work()
            {
                return data.in_fact.brothel_work;
            }
            public double street_work()
            {
                return data.in_fact.street_work;
            }
            public double movie_income()
            {
                return data.in_fact.movie_income;
            }
            public double stripper_work()
            {
                return data.in_fact.stripper_work;
            }
            public double barmaid_work()
            {
                return data.in_fact.barmaid_work;
            }
            public double slave_sales()
            {
                return data.in_fact.slave_sales;
            }
            public double item_sales()
            {
                return data.in_fact.item_sales;
            }
            public double clinic_income()
            {
                return data.in_fact.clinic_income;
            }
            public double arena_income()
            {
                return data.in_fact.arena_income;
            }
            public double farm_income()
            {
                return data.in_fact.farm_income;
            }
        }
        public InFactors in_fact = new InFactors();
        /*
        *	outgoings factors
        */
        public class OutFactors
        {
            public double training()
            {
                return data.out_fact.training;
            }
            public double actress_wages()
            {
                return data.out_fact.actress_wages;
            }
            public double movie_cost()
            {
                return data.out_fact.movie_cost;
            }
            public double goon_wages()
            {
                return data.out_fact.goon_wages;
            }
            public double matron_wages()
            {
                return data.out_fact.matron_wages;
            }
            public double staff_wages()
            {
                return data.out_fact.staff_wages;
            }
            public double girl_support()
            {
                return data.out_fact.girl_support;
            }
            public double consumables()
            {
                return data.out_fact.consumables;
            }
            public double item_cost()
            {
                return data.out_fact.item_cost;
            }
            public double slave_cost()
            {
                return data.out_fact.slave_cost;
            }
            public double brothel_cost()
            {
                return data.out_fact.brothel_cost;
            }
            public double brothel_support()
            {
                return data.out_fact.brothel_support;
            }
            public double bar_cost()
            {
                return data.out_fact.bar_cost;
            }
            public double casino_cost()
            {
                return data.out_fact.casino_cost;
            }
            public double bribes()
            {
                return data.out_fact.bribes;
            }
            public double fines()
            {
                return data.out_fact.fines;
            }
            public double advertising()
            {
                return data.out_fact.advertising;
            }
        }
        public OutFactors out_fact = new OutFactors();

        public class ProstitutionData
        {
            public double rape_streets()
            {
                return data.prostitution.rape_streets;
            }
            public double rape_brothel()
            {
                return data.prostitution.rape_brothel;
            }
        }
        public ProstitutionData prostitution = new ProstitutionData();

        private class CatacombsData
        {
            public double unique_catacombs()
            {
                return data.catacombs.unique_catacombs;
            }
            public bool control_girls()
            {
                return data.catacombs.control_girls;
            }
            public bool control_gangs()
            {
                return data.catacombs.control_gangs;
            }
            public double girl_gets_girls()
            {
                return data.catacombs.girl_gets_girls;
            }
            public double girl_gets_items()
            {
                return data.catacombs.girl_gets_items;
            }
            public double girl_gets_beast()
            {
                return data.catacombs.girl_gets_beast;
            }
            public double gang_gets_girls()
            {
                return data.catacombs.gang_gets_girls;
            }
            public double gang_gets_items()
            {
                return data.catacombs.gang_gets_items;
            }
            public double gang_gets_beast()
            {
                return data.catacombs.gang_gets_beast;
            }
        }
        private CatacombsData catacombs = new CatacombsData();

        private class SlaveMarketData
        {
            public double unique_market()
            {
                return data.slavemarket.unique_market;
            }
            public int slavesnewweeklymin()
            {
                return data.slavemarket.slavesnewweeklymin;
            }
            public int slavesnewweeklymax()
            {
                return data.slavemarket.slavesnewweeklymax;
            }
        }
        private SlaveMarketData slavemarket = new SlaveMarketData();

        private class font_data
        {
            public string normal()
            {
                return data.fonts.normal;
            }
            public string @fixed()
            {
                return data.fonts.normal;
            }
            public bool antialias()
            {
                return data.fonts.antialias;
            }
            public bool showpercent()
            {
                return data.fonts.showpercent;
            }
            public int detailfontsize()
            {
                return data.fonts.detailfontsize;
            }
        }
        private font_data fonts = new font_data();

        private class item_data
        {
            //IHM Component
            //public SDL_Color rarity_color(int num)
            //{
            //    return data.items.rarity_color[num];
            //}
        }
        private item_data items = new item_data();

        private class TaxData
        {
            public double rate()
            {
                return data.tax.rate;
            }
            public double minimum()
            {
                return data.tax.minimum;
            }
            public double laundry()
            {
                return data.tax.laundry;
            }
        }
        private TaxData tax = new TaxData();

        private class GambleData
        {
            public int odds()
            {
                return data.gamble.odds;
            }
            public int @base()
            {
                return data.gamble.@base;
            }
            public int spread()
            {
                return data.gamble.spread;
            }
            public double house_factor()
            {
                return data.gamble.house_factor;
            }
            public double customer_factor()
            {
                return data.gamble.customer_factor;
            }
        }
        private GambleData gamble = new GambleData();

        private class PregnancyData
        {
            public int player_chance()
            {
                return data.pregnancy.player_chance;
            }
            public int customer_chance()
            {
                return data.pregnancy.customer_chance;
            }
            public int monster_chance()
            {
                return data.pregnancy.monster_chance;
            }
            public double good_sex_factor()
            {
                return data.pregnancy.good_sex_factor;
            }
            public int chance_of_girl()
            {
                return data.pregnancy.chance_of_girl;
            }
            public int weeks_pregnant()
            {
                return data.pregnancy.weeks_pregnant;
            }
            public int weeks_monster_p()
            {
                return data.pregnancy.weeks_monster_p;
            }
            public double miscarriage_chance()
            {
                return data.pregnancy.miscarriage_chance;
            }
            public double miscarriage_monster()
            {
                return data.pregnancy.miscarriage_monster;
            }
            public int weeks_till_grown()
            {
                return data.pregnancy.weeks_till_grown;
            }
            public int cool_down()
            {
                return data.pregnancy.cool_down;
            }
            public double anti_preg_failure()
            {
                return data.pregnancy.anti_preg_failure;
            }
            public double multi_birth_chance()
            {
                return data.pregnancy.multi_birth_chance;
            }
        }
        private PregnancyData pregnancy = new PregnancyData();
        public class GangData
        {
            /// <summary>
            /// Max number of gang in hireable list.
            /// </summary>
            public int MaxRecruitList
            {
                get { return data.Gangs.MaxRecruitList; }
            }
            public int StartRandom
            {
                get { return data.Gangs.StartRandom; }
            }
            public int StartBoosted
            {
                get { return data.Gangs.StartBoosted; }
            }
            /// <summary>
            /// Minimum number of gang member for initialising gang.
            /// </summary>
            public int InitMemberMin
            {
                get { return data.Gangs.InitMemberMin; }
            }
            /// <summary>
            /// Maximum number of gang member for initialising gang.
            /// </summary>
            public int InitMemberMax
            {
                get { return data.Gangs.InitMemberMax; }
            }
            public int ChanceRemoveUnwanted
            {
                get { return data.Gangs.ChanceRemoveUnwanted; }
            }
            public int AddNewWeeklyMin
            {
                get { return data.Gangs.AddNewWeeklyMin; }
            }
            public int AddNewWeeklyMax
            {
                get { return data.Gangs.AddNewWeeklyMax; }
            }
        }
        public GangData _gangs = new GangData();
        public GangData Gangs { get { return this._gangs; } }

        //C++ TO C# CONVERTER NOTE: Classes must be named in C#, so the following class has been named AnonymousClass:
        private class AnonymousClass
        {
            public int gold()
            {
                return data.initial.gold;
            }
            public int girl_meet()
            {
                return data.initial.girl_meet;
            }
            public int girls_house_perc()
            {
                return data.initial.girls_house_perc;
            }
            public bool girls_keep_tips()
            {
                return data.initial.girls_keep_tips;
            }
            public bool slave_pay_outofpocket()
            {
                return data.initial.slave_pay_outofpocket;
            }
            public int slave_house_perc()
            {
                return data.initial.slave_house_perc;
            }
            public bool slave_keep_tips()
            {
                return data.initial.slave_keep_tips;
            }
            public int girls_accom()
            {
                return data.initial.girls_accom;
            }
            public int slave_accom()
            {
                return data.initial.slave_accom;
            }
            public bool auto_use_items()
            {
                return data.initial.auto_use_items;
            }
            public bool auto_combat_equip()
            {
                return data.initial.auto_combat_equip;
            }
            public int torture_mod()
            {
                return data.initial.torture_mod;
            }
            public int horoscopetype()
            {
                return data.initial.horoscopetype;
            }
        }
        private AnonymousClass initial = new AnonymousClass();

        private class Folders
        {
            public string characters()
            {
                return data.folders.characters;
            }
            public bool configXMLch()
            {
                return data.folders.configXMLch;
            }
            public string saves()
            {
                return data.folders.saves;
            }
            public bool configXMLsa()
            {
                return data.folders.configXMLsa;
            }
            public string items()
            {
                return data.folders.items;
            }
            public bool configXMLil()
            {
                return data.folders.configXMLil;
            }
            public bool backupsaves()
            {
                return data.folders.backupsaves;
            }
            public string defaultimageloc()
            {
                return data.folders.defaultimageloc;
            }
            public bool configXMLdi()
            {
                return data.folders.configXMLdi;
            }
            public bool preferdefault()
            {
                return data.folders.preferdefault;
            }

        }
        private Folders folders = new Folders();

        private class Resolution
        {
            public string resolution()
            {
                return data.resolution.resolution;
            }
            public int width()
            {
                return data.resolution.width;
            }
            public int height()
            {
                return data.resolution.height;
            }
            public int scalewidth()
            {
                return data.resolution.scalewidth;
            }
            public int scaleheight()
            {
                return data.resolution.scaleheight;
            }
            public bool fixedscale()
            {
                return data.resolution.fixedscale;
            }
            public bool fullscreen()
            {
                return data.resolution.fullscreen;
            }
            public bool configXML()
            {
                return data.resolution.configXML;
            }
            public int list_scroll()
            {
                return data.resolution.list_scroll;
            }
            public int text_scroll()
            {
                return data.resolution.text_scroll;
            }
            public bool next_turn_enter()
            {
                return data.resolution.next_turn_enter;
            }
        }
        private Resolution resolution = new Resolution();

        private class Debug
        {
            public bool log_girls()
            {
                return data.debug.log_girls;
            }
            public bool log_rgirls()
            {
                return data.debug.log_rgirls;
            }
            public bool log_girlfights()
            {
                return data.debug.log_girl_fights;
            }
            public bool log_items()
            {
                return data.debug.log_items;
            }
            public bool log_fonts()
            {
                return data.debug.log_fonts;
            }
            public bool log_torture()
            {
                return data.debug.log_torture;
            }
            public bool log_debug()
            {
                return data.debug.log_debug;
            }
            public bool log_extradetails()
            {
                return data.debug.log_extra_details;
            }
            public bool log_show_numbers()
            {
                return data.debug.log_show_numbers;
            }
        }
        private Debug debug = new Debug();

        private string override_dir()
        {
            return data.override_dir;
        }
    }

}
