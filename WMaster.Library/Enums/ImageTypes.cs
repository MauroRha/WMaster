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
//  <copyright file="ImageTypes.cs" company="The Pink Petal Devloment Team">
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
namespace WMaster.Enums
{
    using System;

    // TODO : REFACTORING - Rename enum value to lower case UC first

    /// <summary>
    /// Character image.
    /// <remarks><para>`J` When modifying Image types, search for "J-Change-Image-Types"  :  found in >> Constants.h</para></remarks>
    /// </summary>
    public enum ImageTypes
    {
        ANAL = 0,
        BDSM,
        SEX,
        BEAST,
        GROUP,
        LESBIAN,
        TORTURE,    // `J` added
        DEATH,
        PROFILE,
        COMBAT,
        ORAL,
        ECCHI,
        STRIP,
        MAID,
        SING,
        WAIT,
        CARD,
        BUNNY,
        NUDE,
        MAST,
        TITTY,
        MILK,
        HAND,
        FOOT,
        BED,
        FARM,
        HERD,
        COOK,
        CRAFT,
        SWIM,
        BATH,
        NURSE,
        FORMAL,
        SHOP,
        MAGIC,
        SIGN,       // Going be used for advertising
        PRESENTED,  // Going be used for Slave Market
        DOM,
        DEEPTHROAT,
        EATOUT,
        DILDO,
        SUB,
        STRAPON,
        LES69ING,
        LICK,
        SUCKBALLS,
        COWGIRL,
        REVCOWGIRL,
        SEXDOGGY,
        JAIL,
        PUPPYGIRL,
        PONYGIRL,
        CATGIRL,

        /*
            WATER			- "water*."			- "Watersports"		- watersports
            PETPROFILE		- "pet*."			- "Pet"				- profile, nude
            PETORAL			- "petoral*."		- "PetOral"			- oral, lick, deepthroat, titty, petprofile, nude, bdsm
            PETSEX			- "petsex*."		- "PetSex"			- sex, nude, anal, petprofile, bdsm
            PETBEAST		- "petbeast*."		- "PetBeastiality"	- beast, sex, anal, bdsm, nude
            PETFEED			- "petfeed*."		- "PetFeed"			- oral, lick, petprofile, bdsm, nude
            PETPLAY			- "petplay*."		- "PetPlay"			- petprofile, bdsm, nude
            PETTOY			- "pettoy*."		- "PetToy"			- dildo, petprofile, oral, mast, bdsm, nude
            PETWALK			- "petwalk*."		- "PetWalk"			- petprofile, bdsm, nude
            PETLICK			- "petlick*."		- "PetLick"			- lick, oral, petprofile, bdsm, nude, titty
        */

        // PREGNANT needs to be the last of the nonpregnant image types.
        PREGNANT,

        // `J` All image types can have a pregnant alternative now
        PREGANAL,
        PREGBDSM,
        PREGSEX,
        PREGBEAST,
        PREGGROUP,
        PREGLESBIAN,
        PREGTORTURE,
        PREGDEATH,
        PREGPROFILE,
        PREGCOMBAT,
        PREGORAL,
        PREGECCHI,
        PREGSTRIP,
        PREGMAID,
        PREGSING,
        PREGWAIT,
        PREGCARD,
        PREGBUNNY,
        PREGNUDE,
        PREGMAST,
        PREGTITTY,
        PREGMILK,
        PREGHAND,
        PREGFOOT,
        PREGBED,
        PREGFARM,
        PREGHERD,
        PREGCOOK,
        PREGCRAFT,
        PREGSWIM,
        PREGBATH,
        PREGNURSE,
        PREGFORMAL,
        PREGSHOP,
        PREGMAGIC,
        PREGSIGN,
        PREGPRESENTED,
        PREGDOM,
        PREGDEEPTHROAT,
        PREGEATOUT,
        PREGDILDO,
        PREGSUB,
        PREGSTRAPON,
        PREGLES69ING,
        PREGLICK,
        PREGSUCKBALLS,
        PREGCOWGIRL,
        PREGREVCOWGIRL,
        PREGSEXDOGGY,
        PREGJAIL,
        PREGPUPPYGIRL,
        PREGPONYGIRL,
        PREGCATGIRL,
        // `J` All image types can have a pregnant variation (except pregnant-pregnant)

        [Obsolete("The NUM_IMGTYPES enum value of eImageTypes must be replace by enum extention function returning the number of value in enum. Enum value must be only enum value!", false)]
        NUM_IMGTYPES
    };
}
