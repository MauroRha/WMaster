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
//  <copyright file="Jobs.cs" company="The Pink Petal Devloment Team">
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
namespace WMaster.Enum
{
    using System;

    // TODO : REFACTORING - Rename enum value to lower case UC first

    // `J` When adding new Studio Scenes, search for "J-Add-New-Scenes"  :  found in >> Constants.h > JOBS

    // *****IMPORTANT**** If you add more scene types, they must go somewhere between
    // STAGEHAND and FILMRANDOM, or it will cause the random job to stop working..
    // the job after STAGEHAND must be the first film job, FILMRANDOM must be the last one.

    public enum Jobs
    {
        // `J` Job Brothel - General
        /// <summary>
        /// <b>Brothel (General)</b> : relaxes and takes some time off.
        /// </summary>
        RESTING = 0,
        /// <summary>
        /// <b>Brothel (General)</b> : trains skills at a basic level.
        /// </summary>
        TRAINING,
        /// <summary>
        /// <b>Brothel (General)</b> : cleans the building.
        /// </summary>
        CLEANING,
        /// <summary>
        /// <b>Brothel (General)</b> : protects the building and its occupants.
        /// </summary>
        SECURITY,
        /// <summary>
        /// <b>Brothel (General)</b> : goes onto the streets to advertise the buildings services.
        /// </summary>
        ADVERTISING,
        /// <summary>
        /// <b>Brothel (General)</b> : looks after customers needs (customers are happier when people are doing this job).
        /// </summary>
        CUSTOMERSERVICE,
        /// <summary>
        /// <b>Brothel (General)</b> : looks after the needs of the girls (only 1 allowed).
        /// </summary>
        MATRON,
        /// <summary>
        /// <b>Brothel (General)</b> : tortures the people in the dungeons to help break their will (this is in addition to player torture) (only 1 allowed).
        /// </summary>
        TORTURER,
        /// <summary>
        /// <b>Brothel (General)</b> : goes adventuring in the catacombs.
        /// </summary>
        EXPLORECATACOMBS,
        /// <summary>
        /// <b>Brothel (General)</b> : takes care of beasts that are housed in the brothel.
        /// </summary>
        BEASTCARER,

        // `J` Job Brothel - Bar
        /// <summary>
        /// <b>Brothel (Bar)</b> : serves at the bar.
        /// </summary>
        BARMAID,
        /// <summary>
        /// <b>Brothel (Bar)</b> : waits on the tables.
        /// </summary>
        WAITRESS,
        /// <summary>
        /// <b>Brothel (Bar)</b> : sings in the bar.
        /// </summary>
        SINGER,
        /// <summary>
        /// <b>Brothel (Bar)</b> : plays the piano for customers.
        /// </summary>
        PIANO,
        /// <summary>
        /// <b>Brothel (Bar)</b> : high lvl whore.  Sees less clients but needs higher skill high lvl items and the such to make them happy.
        /// </summary>
        ESCORT,
        /// <summary>
        /// <b>Brothel (Bar)</b> : cooks at the bar.
        /// </summary>
        BARCOOK,
        //const unsigned int WETTSHIRT		= ;

        // `J` Job Brothel - Hall
        /// <summary>
        /// <b>Brothel (Hall)</b> : dealer for gambling tables.
        /// </summary>
        DEALER,
        /// <summary>
        /// <b>Brothel (Hall)</b> : sings, dances and other shows for patrons.
        /// </summary>
        ENTERTAINMENT,
        /// <summary>
        /// <b>Brothel (Hall)</b> : naughty shows for patrons.
        /// </summary>
        XXXENTERTAINMENT,
        /// <summary>
        /// <b>Brothel (Hall)</b> : looks after customers sexual needs.
        /// </summary>
        WHOREGAMBHALL,
        //const unsigned int FOXYBOXING		= ;

        // `J` Job Brothel - Sleazy Bar
        SLEAZYBARMAID,
        SLEAZYWAITRESS,
        BARSTRIPPER,
        BARWHORE,

        // `J` Job Brothel - Brothel
        /// <summary>
        /// <b>Brothel</b> : gives massages to patrons and sometimes sex.
        /// </summary>
        MASSEUSE,
        /// <summary>
        /// <b>Brothel</b> : strips for customers and sometimes sex.
        /// </summary>
        BROTHELSTRIPPER,
        /// <summary>
        /// <b>Brothel</b> : Peep show
        /// </summary>
        PEEP,
        /// <summary>
        /// <b>Brothel</b> : whore herself inside the building.
        /// </summary>
        WHOREBROTHEL,
        /// <summary>
        /// <b>Brothel</b> : whore self on the city streets.
        /// </summary>
        WHORESTREETS,

        // `J` Job Movie Studio - Crew
        /// <summary>
        /// <b>Movie Studio (Crew)</b> : free time
        /// </summary>
        FILMFREETIME,
        /// <summary>
        /// <b>Movie Studio (Crew)</b> : does same work as matron plus adds quality to films.
        /// </summary>
        DIRECTOR,
        /// <summary>
        /// <b>Movie Studio (Crew)</b> : advertising -- This helps film sales after it is created.
        /// </summary>
        PROMOTER,
        /// <summary>
        /// <b>Movie Studio (Crew)</b> : uses magic to record the scenes to crystals (requires at least 1)
        /// </summary>
        CAMERAMAGE,
        /// <summary>
        /// <b>Movie Studio (Crew)</b> : post editing to get the best out of the film (requires at least 1)
        /// </summary>
        CRYSTALPURIFIER,
        /// <summary>
        /// <b>Movie Studio (Crew)</b> : correct audio and add in music to the scenes (not required but helpful).
        /// </summary>
        //SOUNDTRACK,
        /// <summary>
        /// <b>Movie Studio (Crew)</b> : keeps the porn stars and animals aroused.
        /// </summary>
        FLUFFER,
        /// <summary>
        /// <b>Movie Studio (Crew)</b> : currently does the same as a cleaner.
        /// </summary>
        STAGEHAND,


        // Studio - Non-Sex Scenes
        /// <summary>
        /// <b>Movie Studio (Non-Sex Scenes)</b> : for sexy combatants.
        /// </summary>
        FILMACTION,
        /// <summary>
        /// <b>Movie Studio (Non-Sex Scenes)</b> : for sexy cooking.
        /// </summary>
        FILMCHEF,
        /// <summary>
        /// <b>Movie Studio (Non-Sex Scenes)</b> : for Comedy scenes.
        /// </summary>
        //FILMCOMEDY,
        /// <summary>
        /// <b>Movie Studio (Non-Sex Scenes)</b> : for Drama scenes.
        /// </summary>
        //FILMDRAMA,
        /// <summary>
        /// <b>Movie Studio (Non-Sex Scenes)</b> : for Horror scenes.
        /// </summary>
        //FILMHORROR,
        /// <summary>
        /// <b>Movie Studio (Non-Sex Scenes)</b> : for cool, sexy & cute girls.
        /// </summary>
        //FILMIDOL,
        /// <summary>
        /// <b>Movie Studio (Non-Sex Scenes)</b> : for cute music videos.
        /// </summary>
        FILMMUSIC,

        // Studio - Softcore Porn
        FILMMAST,			// films this type of scene CRAZY
        FILMSTRIP,			// films this type of scene CRAZY
        FILMTEASE,			//Fex sensual and cute

        // Studio - Porn
        /// <summary>
        /// <b>Movie Studio (Porn)</b> : films this sort of scene in the movie.
        /// </summary>
        FILMANAL,
        /// <summary>
        /// <b>Movie Studio (Porn)</b> : films this type of scene CRAZY.
        /// </summary>
        FILMFOOTJOB,
        /// <summary>
        /// <b>Movie Studio (Porn)</b> : films scenes with/as futa.
        /// </summary>
        //FILMFUTA,
        /// <summary>
        /// <b>Movie Studio (Porn)</b> : films this type of scene CRAZY.
        /// </summary>
        FILMHANDJOB,
        /// <summary>
        /// <b>Movie Studio (Porn)</b> : films this sort of scene in the movie. thinking about changing to Lesbian.
        /// </summary>
        FILMLESBIAN,
        /// <summary>
        /// <b>Movie Studio (Porn)</b> : films this type of scene CRAZY.
        /// </summary>
        FILMORAL,
        /// <summary>
        /// <b>Movie Studio (Porn)</b> : films this sort of scene in the movie.
        /// </summary>
        FILMSEX,
        /// <summary>
        /// <b>Movie Studio (Porn)</b> : films this type of scene CRAZY.
        /// </summary>
        FILMTITTY,

        // Studio - Hardcore porn
        /// <summary>
        /// <b>Movie Studio (Hardcore porn)</b> : films this sort of scene in the movie (uses beast resource).
        /// </summary>
        FILMBEAST,
        /// <summary>
        /// <b>Movie Studio (Hardcore porn)</b> : films this sort of scene in the movie.
        /// </summary>
        FILMBONDAGE,
        FILMBUKKAKE,
        FILMFACEFUCK,
        /// <summary>
        /// <b>Movie Studio (Hardcore porn)</b> : films this sort of scene in the movie.
        /// </summary>
        FILMGROUP,
        FILMPUBLICBDSM,
        FILMDOM,

        //Must go last
        /// <summary>
        /// <b>Movie Studio (Hardcore porn)</b> : Films a random sex scene ... it does NOT work like most jobs, see following note.
        /// </summary>
        FILMRANDOM,

        // `J` Job Arena - Fighting
        /// <summary>
        /// <b>Arena (Fighting)</b> : customers come to place bets on who will win, girl may die (uses beasts resource).
        /// </summary>
        FIGHTBEASTS,
        FIGHTARENAGIRLS,
        FIGHTTRAIN,
        //JOUSTING		= ;
        //MAGICDUEL		= ;
        //ARMSDUEL		= ;
        //FIGHTBATTLE	= ;
        //ATHELETE		= ;
        RACING,

        // `J` Job Arena - Staff
        /// <summary>
        /// <b>Arena (Staff)</b> : free time of arena.
        /// </summary>
        ARENAREST,
        /// <summary>
        /// <b>Arena (Staff)</b> : matron of arena
        /// </summary>
        DOCTORE,
        CITYGUARD,
        BLACKSMITH,
        COBBLER,
        JEWELER,
        //BATTLEMASTER	= ;
        //ARENAPROMOTER	= ;
        //BEASTMASTER	= ;
        //VENDOR			= ;
        //BOOKIE			= ;
        //GROUNDSKEEPER	= ;
        //MINER			= ;
        CLEANARENA,

        // `J` Job Centre - General
        /// <summary>
        /// <b>Centre (General)</b> : free time.
        /// </summary>
        CENTREREST,
        /// <summary>
        /// <b>Centre (General)</b> : matron of centre.
        /// </summary>
        CENTREMANAGER,
        /// <summary>
        /// <b>Centre (General)</b> : work in a soup kitchen.
        /// </summary>
        FEEDPOOR,
        /// <summary>
        /// <b>Centre (General)</b> : Goes around town helping where they can.
        /// </summary>
        COMUNITYSERVICE,
        CLEANCENTRE,
        // TODO ideas:Run a charity, with an option for the player to steal from charity (with possible bad outcome). Run schools/orphanages.. this should give a boost to the stats of new random girls, and possibly be a place to recruit new uniques.
        // Homeless shelter... once again a possible place to find new girls.

        // `J` Job Centre - Rehab
        COUNSELOR,
        REHAB,
        ANGER,
        EXTHERAPY,
        THERAPY,

        // `J` Job Clinic - Surgery
        /// <summary>
        /// <b>Clinic (Surgery)</b> : takes 1 days for each wound trait received.
        /// </summary>
        GETHEALING,
        /// <summary>
        /// <b>Clinic (Surgery)</b> : construct girls can get repaired quickly.
        /// </summary>
        GETREPAIRS,
        /// <summary>
        /// <b>Clinic (Surgery)</b> : gets an abortion (takes 2 days).
        /// </summary>
        GETABORT,
        /// <summary>
        /// <b>Clinic (Surgery)</b> : magical plastic surgery (takes 5 days).
        /// </summary>
        COSMETICSURGERY,
        /// <summary>
        /// <b>Clinic (Surgery)</b> : magical plastic surgery (takes 5 days).
        /// </summary>
        LIPO,
        /// <summary>
        /// <b>Clinic (Surgery)</b> : magical plastic surgery (takes 5 days).
        /// </summary>
        BREASTREDUCTION,
        /// <summary>
        /// <b>Clinic (Surgery)</b> : magical plastic surgery (takes 5 days).
        /// </summary>
        BOOBJOB,
        /// <summary>
        /// <b>Clinic (Surgery)</b> : magical plastic surgery (takes 5 days).
        /// </summary>
        VAGINAREJUV,
        /// <summary>
        /// <b>Clinic (Surgery)</b> : magical plastic surgery (takes 5 days).
        /// </summary>
        FACELIFT,
        /// <summary>
        /// <b>Clinic (Surgery)</b> : magical plastic surgery (takes 5 days).
        /// </summary>
        ASSJOB,
        /// <summary>
        /// <b>Clinic (Surgery)</b> : magical plastic surgery (takes 5 days).
        /// </summary>
        TUBESTIED,
        /// <summary>
        /// <b>Clinic (Surgery)</b> : magical plastic surgery (takes 5 days).
        /// </summary>
        FERTILITY,

        // `J` Job Clinic - Staff
        /// <summary>
        /// <b>Clinic (Staff)</b> : is clinics free time.
        /// </summary>
        CLINICREST,
        /// <summary>
        /// <b>Clinic (Staff)</b> : matron of clinic.
        /// </summary>
        CHAIRMAN,
        /// <summary>
        /// <b>Clinic (Staff)</b> : becomes a doctor (requires 1) (will make some extra cash for treating locals).
        /// </summary>
        DOCTOR,
        /// <summary>
        /// <b>Clinic (Staff)</b> : helps girls recover from surgery on healing.
        /// </summary>
        NURSE,
        /// <summary>
        /// construct girls can get repaired quickly.
        /// </summary>
        MECHANIC,
        /// <summary>
        /// <b>Clinic (Staff)</b> : training for nurse job.
        /// </summary>
        INTERN,
        /// <summary>
        /// <b>Clinic (Staff)</b> : cleans clinic.
        /// </summary>
        JANITOR,

        // Job Clinic - Drug lab
        DRUGDEALER,

        // `J` Job Farm - Staff
        /// <summary>
        /// <b>Farm (Staff)</b> : farm rest.
        /// </summary>
        FARMREST,
        /// <summary>
        /// <b>Farm (Staff)</b> : matron of farm.
        /// </summary>
        FARMMANGER,
        /// <summary>
        /// <b>Farm (Staff)</b> : tends to the animals to keep them from dying - full time.
        /// </summary>
        VETERINARIAN,
        /// <summary>
        /// <b>Farm (Staff)</b> : buys and sells food - full time.
        /// </summary>
        MARKETER,
        /// <summary>
        /// <b>Farm (Staff)</b> : potions - unlock various types of potions and garden qualities - full time.
        /// </summary>
        RESEARCH,
        /// <summary>
        /// <b>Farm (Staff)</b> : cleaning of the farm.
        /// </summary>
        FARMHAND,

        // `J` Job Farm - Laborers
        /// <summary>
        /// <b>Farm (Laborers)</b> : tends crops.
        /// </summary>
        FARMER,
        /// <summary>
        /// <b>Farm (Laborers)</b> : produces herbs and potion ingredients.
        /// </summary>
        GARDENER,
        /// <summary>
        /// <b>Farm (Laborers)</b> : tends food animals - 100% food.
        /// </summary>
        SHEPHERD,
        /// <summary>
        /// <b>Farm (Laborers)</b> : tends animals for food or beast - % food/beast based on skills.
        /// </summary>
        RANCHER,
        /// <summary>
        /// <b>Farm (Laborers)</b> : tends strange beasts - 100% beast - dangerous.
        /// </summary>
        CATACOMBRANCHER,
        BEASTCAPTURE,
        /// <summary>
        /// <b>Farm (Laborers)</b> : produces milk from animals/beasts/girls - if food animals < beasts - can be dangerous.
        /// </summary>
        MILKER,
        /// <summary>
        /// <b>Farm (Laborers)</b> : milker not required but increases yield.
        /// </summary>
        MILK,

        // `J` Job Farm - Producers
        /// <summary>
        /// <b>Farm (Producers)</b> : produces food from animals.
        /// </summary>
        BUTCHER,
        /// <summary>
        /// <b>Farm (Producers)</b> : produces food from crops.
        /// </summary>
        BAKER,
        /// <summary>
        /// <b>Farm (Producers)</b> : produces beers and wines.
        /// </summary>
        BREWER,
        /// <summary>
        /// <b>Farm (Producers)</b> : produces beers and wines.
        /// </summary>
        TAILOR,
        /// <summary>
        /// <b>Farm (Producers)</b> : produce items for sale.
        /// </summary>
        MAKEITEM,
        /// <summary>
        /// <b>Farm (Producers)</b> : create potions with items gained from the garden.
        /// </summary>
        MAKEPOTIONS,

        // `J` Job House - General
        HOUSEREST,
        HEADGIRL,
        RECRUITER,
        PERSONALTRAINING,
        PERSONALBEDWARMER,
        HOUSEPET,
        /// <summary>
        /// <b>House (General)</b> : cooks for the harem, (helps keep them happy, and increase job performance)
        /// </summary>
        HOUSECOOK,
        PONYGIRL,
        //HOUSEDATE,
        //HOUSEVAC,
        CLEANHOUSE,

        // - extra unassignable
        INDUNGEON,
        RUNAWAY,

        /// <summary>
        /// Number of Jobs.
        /// </summary>
        [Obsolete("The NUM_JOBS enum value of eJobs must be replace by enum extention function returning the number of value in enum. Enum value must be only enum value!", false)]
        NUM_JOBS
    };
}
