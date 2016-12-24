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

//<!-- -------------------------------------------------------------------------------------------------------------------- -->
//<file>
//  <copyright file="ConfigData.cs" company="The Pink Petal Devloment Team">
//      Copyright © 2009, 2010 - The Pink Petal Devloment Team.
//  </copyright>
//  <author>Graben</author>
//  <datecreated>2016-12-13</datecreated>
//  <summary>
//  </summary>
//  <remarks>
//      <para name="Rem">Extract from GitHub : relased find in December 2016</para>
//      <para name="Review_2016-12">Gbn - 12/2016 : Create / Convert to C# / Refactoring</para>
//  </remarks>
//</file>
//<!-- -------------------------------------------------------------------------------------------------------------------- -->
namespace WMaster
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ConfigData
    {
        /*
        *	initialisation
        */
        //C++ TO C# CONVERTER NOTE: Classes must be named in C#, so the following class has been named AnonymousClass:
        public class Initialise
        {
            public int gold;
            public int girl_meet;
            public int girls_house_perc;
            public bool girls_keep_tips;
            public bool slave_pay_outofpocket;
            public int slave_house_perc;
            public bool slave_keep_tips;
            public int girls_accom;
            public int slave_accom;
            public bool auto_use_items;
            public bool auto_combat_equip;
            public int torture_mod;
            public int horoscopetype;
        }
        public Initialise initial = new Initialise();


        /*
        *	Folders
        */
        public class Folders
        {
            public string characters;
            public bool configXMLch;
            public string saves;
            public bool configXMLsa;
            public string items;
            public bool configXMLil;
            public bool backupsaves;
            public string defaultimageloc;
            public bool configXMLdi;
            public bool preferdefault;
        }
        public Folders folders = new Folders();

        /*
        *	resolution
        */
        public class Resolution
        {
            public string resolution;
            public int width;
            public int height;
            public int scalewidth;
            public int scaleheight;
            public bool fullscreen;
            public bool fixedscale;
            public bool configXML;
            public int list_scroll;
            public int text_scroll;
            public bool next_turn_enter;
        }
        public Resolution resolution = new Resolution();
        /*
    *	income factors
    */

        public class InFactors
        {
            public double extortion;
            public double brothel_work;
            public double street_work;
            public double movie_income;
            public double stripper_work;
            public double barmaid_work;
            public double slave_sales;
            public double item_sales;
            public double clinic_income;
            public double arena_income;
            public double farm_income;
        }
        public InFactors in_fact = new InFactors();
        /*
        *	outgoings factors
        */
        public class OutFactors
        {
            public double training;
            public double actress_wages;
            public double movie_cost;
            public double goon_wages;
            public double matron_wages;
            public double staff_wages; // `J` ?not used?
            public double girl_support;
            public double consumables;
            public double item_cost;
            public double slave_cost;
            public double brothel_cost;
            public double brothel_support; // `J` ?not used?
            public double bar_cost; // `J` ?not used?
            public double casino_cost; // `J` ?not used?
            public double bribes;
            public double fines;
            public double advertising;
        }
        public OutFactors out_fact = new OutFactors();

        public class GambleData
        {
            public int odds;
            public int @base;
            public int spread;
            public double house_factor;
            public double customer_factor;
        }
        public GambleData gamble = new GambleData();

        public class TaxData
        {
            public double rate;
            public double minimum;
            public double laundry;
        }
        public TaxData tax = new TaxData();

        public class PregnancyData
        {
            public int player_chance;
            public int customer_chance;
            public int monster_chance;
            public double good_sex_factor;
            public int chance_of_girl;
            public int weeks_pregnant;
            public int weeks_monster_p;
            public double miscarriage_chance;
            public double miscarriage_monster;
            public int weeks_till_grown;
            public int cool_down;
            public double anti_preg_failure;
            public double multi_birth_chance;
        }
        public PregnancyData pregnancy = new PregnancyData();

        public class GangData
        {
            public int MaxRecruitList { get; set; }
            public int StartRandom { get; set; }
            public int StartBoosted { get; set; }
            public int InitMemberMin { get; set; }
            public int InitMemberMax { get; set; }
            public int ChanceRemoveUnwanted { get; set; }
            public int AddNewWeeklyMin { get; set; }
            public int AddNewWeeklyMax { get; set; }
        }
        public GangData Gangs = new GangData();

        public class ProstitutionData
        {
            public double rape_streets;
            public double rape_brothel;
        }
        public ProstitutionData prostitution = new ProstitutionData();

        public class CatacombsData
        {
            public double unique_catacombs;
            public bool control_girls;
            public bool control_gangs;
            public double girl_gets_girls;
            public double girl_gets_items;
            public double girl_gets_beast;
            public double gang_gets_girls;
            public double gang_gets_items;
            public double gang_gets_beast;
        }
        public CatacombsData catacombs = new CatacombsData();

        public class SlaveMarketData
        {
            public double unique_market;
            public int slavesnewweeklymin;
            public int slavesnewweeklymax;
        }
        public SlaveMarketData slavemarket = new SlaveMarketData();


        public class item_data
        {
            // IHM component
            //public SDL_Color[] rarity_color = Arrays.InitializeWithDefaultInstances<SDL_Color>(9);
        }
        public item_data items = new item_data();

        public class font_data
        {
            public string normal;
            public string @fixed;
            public bool antialias;
            public bool showpercent;
            public int detailfontsize;
            public font_data()
            {
                this.normal = "";
                this.@fixed = "";
                this.antialias = false;
            }
        }
        public font_data fonts = new font_data();

        public class DebugData
        {
            public bool log_all;
            public bool log_girls;
            public bool log_rgirls;
            public bool log_girl_fights;
            public bool log_items;
            public bool log_fonts;
            public bool log_torture;
            public bool log_debug;
            public bool log_extra_details;
            public bool log_show_numbers;
        }
        public DebugData debug = new DebugData();

        public class FarmData
        {
            public bool active;
        }
        public FarmData farm = new FarmData();


        public string override_dir;

        ConfigData(string filename = "config.xml")
        { throw new NotImplementedException(); }

        //void set_defaults()
        //{ throw new NotImplementedException(); }
        //void get_income_factors(IXmlElement el)
        //{ throw new NotImplementedException(); }
        //void get_expense_factors(IXmlElement el)
        //{ throw new NotImplementedException(); }
        //void get_tax_factors(IXmlElement el)
        //{ throw new NotImplementedException(); }
        //void get_gambling_factors(IXmlElement el)
        //{ throw new NotImplementedException(); }
        //void get_preg_factors(IXmlElement el)
        //{ throw new NotImplementedException(); }
        //void get_gang_factors(IXmlElement el)
        //{ throw new NotImplementedException(); }
        //void get_pros_factors(IXmlElement el)
        //{ throw new NotImplementedException(); }
        //void get_catacombs_data(IXmlElement el)
        //{ throw new NotImplementedException(); }
        //void get_slave_market_data(IXmlElement el)
        //{ throw new NotImplementedException(); }
        //void get_item_data(IXmlElement el)
        //{ throw new NotImplementedException(); }
        //void get_font_data(IXmlElement el)
        //{ throw new NotImplementedException(); }
        //void get_initial_values(IXmlElement el)
        //{ throw new NotImplementedException(); }
        //void get_folders_data(IXmlElement el)
        //{ throw new NotImplementedException(); }
        //void get_resolution_data(IXmlElement el)
        //{ throw new NotImplementedException(); }
        //void get_att(IXmlElement el, string name, ref int data)
        //{ throw new NotImplementedException(); }
        //void get_att(IXmlElement el, string name, ref double data)
        //{ throw new NotImplementedException(); }
        //void get_att(IXmlElement el, string name, string s)
        //{ throw new NotImplementedException(); }
        //void get_att(IXmlElement el, string name, ref bool bval)
        //{ throw new NotImplementedException(); }
        //void get_debug_flags(IXmlElement el)
        //{ throw new NotImplementedException(); }
    }
}
