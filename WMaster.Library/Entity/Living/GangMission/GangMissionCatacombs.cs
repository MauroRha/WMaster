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
//  <copyright file="GangMissionCatacombs.cs" company="The Pink Petal Devloment Team">
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
namespace WMaster.Entity.Living.GangMission
{
    using System;
    using System.Collections.Generic;
    using WMaster.ClassOrStructurToImplement;
    using WMaster.Entity.Item;
    using WMaster.Enums;
    using WMaster.Manager;

    /// <summary>
    /// Catacombs mission affecte to gang.
    /// </summary>
    public sealed class GangMissionCatacombs : GangMissionBase
    {
        /// <summary>
        /// Initialise base instantce of gang mission.
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> who perform the mission.</param>
        internal GangMissionCatacombs(Gang gang)
            : base(EnuGangMissions.Catacombs, gang)
        {
        }

        /// <summary>
        /// Return localized mission name.
        /// <remarks><para>Will be use like "GangName is [...]."</para></remarks>
        /// </summary>
        /// <returns>The name of mission.</returns>
        public override string GetMissionName()
        {
            return LocalString.GetString(LocalString.ResourceStringCategory.GangMission, "MissionCatacombs");
        }

        /// <summary>
        /// Performe a gang mission into catacombes.
        /// <remarks><para>`J` returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <returns><b>True</b> if mission is a sucess</returns>
        protected override bool PerformingMission()
        {
            LocalString catacombsMissionEvent = new LocalString();
            this.GangCible.HasSeenCombat = true;
            int num = this.GangCible.MemberNum;
            catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.GangMission,
                "Gang[GangName]IsExploringTheCatacombs",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name) });

            if (!GangManager.ControlGangs) // use old code
            {
                catacombsMissionEvent.AppendLine(LocalString.ResourceStringCategory.GangMission, "YouTellThemToGetWhateverTheyCanFind");

                // determine losses
                this.GangCible.HasSeenCombat = true;
                for (int i = 0; i < num; i++)
                {
                    if (WMRand.Percent(this.GangCible.Combat))
                    {
                        continue;
                    }
                    if (this.GangCible.HealLimit == 0)
                    {
                        this.GangCible.MemberNum--;
                        continue;
                    }
                    if (WMRand.Percent(5)) // `J` 5% chance they will not get the healing potion in time.
                    {
                        this.GangCible.MemberNum--; // needed to have atleast some chance or else they are totally invincable.
                    }
                    this.GangCible.AdjustHealLimit(-1);
                    GangManager.NumHealingPotions--;
                }

                if (this.GangCible.MemberNum <= 0)
                {
                    return false;
                }
                else
                {
                    if (num == this.GangCible.MemberNum)
                    {
                        catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.GangMission,
                            "All[Number]OfThemReturn",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", this.GangCible.MemberNum) });
                    }
                    else
                    {
                        catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.GangMission,
                            "[Number]OfThe[GangNumber]WhoWentOutReturn",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", this.GangCible.MemberNum), new FormatStringParameter("GangNumber", num) });
                    }

                    // determine loot
                    int gold = this.GangCible.MemberNum;
                    gold += WMRand.Random(this.GangCible.MemberNum * 100);
                    catacombsMissionEvent.AppendFormat(LocalString.ResourceStringCategory.GangMission,
                        "TheyBringBackWithThem[Number]Gold",
                        new List<FormatStringParameter>() { new FormatStringParameter("Number", gold) });
                    Game.Gold.CatacombLoot(gold);

                    int items = 0;
                    while (WMRand.Percent((this.GangCible.Intelligence / 2) + 30) && items <= (this.GangCible.MemberNum / 3)) // item chance
                    {
                        bool quit = false;
                        bool add = false;
                        sInventoryItem temp = Game.Inventory.GetRandomCatacombItem();
                        if (temp != null)
                        {
                            catacombsMissionEvent.Comma();
                            catacombsMissionEvent.NewLine();
                            int curI = Game.Brothels.HasItem(temp.Name, -1);
                            bool loop = true;
                            while (loop)
                            {
                                if (curI != -1)
                                {
                                    if (Game.Brothels.m_NumItem[curI] >= 999)
                                    {
                                        curI = Game.Brothels.HasItem(temp.Name, curI + 1);
                                    }
                                    else
                                    {
                                        loop = false;
                                    }
                                }
                                else
                                {
                                    loop = false;
                                }
                            }

                            if (Game.Brothels.m_NumInventory < Constants.MAXNUM_INVENTORY || curI != -1)
                            {
                                if (curI != -1)
                                {
                                    catacombsMissionEvent.AppendFormat(LocalString.ResourceStringCategory.Global,
                                        "One[ItemName]",
                                        new List<FormatStringParameter>() { new FormatStringParameter("ItemName", temp.Name) });
                                    Game.Brothels.m_NumItem[curI]++;
                                    items++;
                                }
                                else
                                {
                                    for (int j = 0; j < Constants.MAXNUM_INVENTORY; j++)
                                    {
                                        if (Game.Brothels.m_Inventory[j] == null)
                                        {
                                            catacombsMissionEvent.AppendFormat(LocalString.ResourceStringCategory.Global,
                                                "One[ItemName]",
                                                new List<FormatStringParameter>() { new FormatStringParameter("ItemName", temp.Name) });
                                            Game.Brothels.m_Inventory[j] = temp;
                                            items++;
                                            Game.Brothels.m_EquipedItems[j] = 0;
                                            Game.Brothels.m_NumInventory++;
                                            Game.Brothels.m_NumItem[j]++;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                quit = true;
                                catacombsMissionEvent.AppendLine(LocalString.ResourceStringCategory.Player, "YourInventoryIsFull");
                            }
                        }

                        if (quit)
                        {
                            break;
                        }
                    }
                    catacombsMissionEvent.Dot();


                    int girl = 0;
                    // determine if get a catacomb girl (is "monster" if trait not human)
                    if (WMRand.Percent((this.GangCible.Intelligence / 4) + 25))
                    {
                        sGirl ugirl = null;
                        bool unique = false;
                        if (WMRand.Percent(50))
                        {
                            unique = true; // chance of getting unique girl
                        }
                        if (unique)
                        {
                            ugirl = Game.Girls.GetRandomGirl(false, true);
                            if (ugirl == null)
                            {
                                unique = false;
                            }
                        }

                        if ((Game.Brothels.CurrentObjective != null) && Game.Brothels.CurrentObjective.Objective == Objectives.CAPTUREXCATACOMBGIRLS)
                        {
                            Game.Brothels.CurrentObjective.SoFar++;
                        }

                        catacombsMissionEvent.NewLine();

                        LocalString NGmsg = new LocalString();
                        if (unique)
                        {
                            girl++;
                            catacombsMissionEvent.AppendFormat(LocalString.ResourceStringCategory.GangMission,
                                "YourMenAlsoCapturedAGirlNamed[GirlName]",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", ugirl.Realname) });

                            // TODO : Need comprehention
                            ugirl.m_States &= ~(1 << (int)Status.CATACOMBS);
                            ugirl.add_trait("Kidnapped", 2 + WMRand.Random(10));
                            NGmsg.AppendFormat(LocalString.ResourceStringCategory.Girl,
                                "[GirlName]WasCapturedInTheCatacombsBy[GangName]",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", ugirl.Realname), new FormatStringParameter("GangName", this.GangCible.Name) });
                            ugirl.Events.AddMessage(NGmsg.ToString(), ImageType.PROFILE, EventType.Gang);
                            Game.Dungeon.AddGirl(ugirl, DungeonReasons.GIRLCAPTURED);
                        }
                        else
                        {
                            ugirl = Game.Girls.CreateRandomGirl(0, false, false, false, true);
                            if (ugirl != null) // make sure a girl was returned
                            {
                                girl++;
                                catacombsMissionEvent.Append(LocalString.ResourceStringCategory.GangMission, "YourMenAlsoCapturedAGirl");
                                ugirl.add_trait("Kidnapped", 2 + WMRand.Random(10));
                                NGmsg.AppendFormat(LocalString.ResourceStringCategory.Girl,
                                    "[GirlName]WasCapturedInTheCatacombsBy[GangName]",
                                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", ugirl.Realname), new FormatStringParameter("GangName", this.GangCible.Name) });
                                ugirl.Events.AddMessage(NGmsg.ToString(), ImageType.PROFILE, EventType.Gang);
                                Game.Dungeon.AddGirl(ugirl, DungeonReasons.GIRLCAPTURED);
                            }
                        }
                    }
                    // `J` determine if they bring back any beasts
                    int beasts = Math.Max(0, WMRand.Random(5) - 2);
                    if (girl == 0 && this.GangCible.MemberNum > 13)
                    {
                        beasts++;
                    }
                    if (beasts > 0 && WMRand.Percent(this.GangCible.MemberNum * 5))
                    {
                        catacombsMissionEvent.NewLine();
                        catacombsMissionEvent.AppendFormat(LocalString.ResourceStringCategory.GangMission,
                            "YourMenAlsoBringBack[Number]Beasts",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", beasts) });
                        Game.Brothels.add_to_beasts(beasts);
                    }
                }
            }
            else	// use new code
            {
                int totalGirls = 0;
                int totalItems = 0;
                int totalBeast = 0;
                int bringBackNum = 0;
                int gold = 0;

                // do the intro text
                string girlCatacombsLookFor = Game.Girls.catacombs_look_for(GangManager.GangGetsGirls, GangManager.GangGetsItems, GangManager.GangGetsBeast);
                // TODO : Add string to event (verify g_Girls.catacombs_look_for use resources language string)
                //catacombsMissionEvent.Append(girlCatacombsLookFor);

                // do the bring back loop
                while (this.GangCible.MemberNum >= 1 && bringBackNum < this.GangCible.MemberNum * Math.Max(1, this.GangCible.Strength / 20))
                {
                    double choice = WMRand.Random(10001) / 100.0;
                    gold += WMRand.Random(this.GangCible.MemberNum * 20);

                    if (choice < GangManager.GangGetsGirls) // get girl = 10 point
                    {
                        bool gotGirl = false;
                        sGirl tempGirl = Game.Girls.CreateRandomGirl(18, false, false, false, true); // `J` Legal Note: 18 is the Legal Age of Majority for the USA where I live
                        if (this.GangCible.NetLimit > 0) // try to capture using net
                        {
                            int tries = 0;
                            while (this.GangCible.NetLimit > 0 && !gotGirl) // much harder to net a girl in the catacombs
                            {
                                int damagechance = 40; // higher damage net chance in the catacombs
                                if (WMRand.Percent(this.GangCible.Combat)) // hit her with the net
                                {
                                    if (!WMRand.Percent((double)(tempGirl.agility() - tries))) // she can't avoid or get out of the net
                                    {
                                        gotGirl = true;
                                    }
                                    else
                                    {
                                        damagechance = 80;
                                    }
                                }

                                if (WMRand.Percent(damagechance))
                                {
                                    this.GangCible.AdjustNetLimit(-1);
                                    GangManager.NumNets--;
                                }
                                tries++;
                            }
                        }
                        if (!gotGirl) // fight the girl if not netted
                        {
                            if (!GangManager.GangCombat(tempGirl, this.GangCible))
                            {
                                gotGirl = true;
                            }
                        }
                        tempGirl = null;
                        if (gotGirl)
                        {
                            bringBackNum += 10;
                            totalGirls++;
                        }
                        else
                        {
                            bringBackNum += 5;
                        }
                    }
                    else if (choice < GangManager.GangGetsGirls + GangManager.GangGetsItems) // get item = 4 points
                    {
                        bool gotitem = false;
                        if (WMRand.Percent(33)) // item is guarded
                        {
                            sGirl tempgirl = Game.Girls.CreateRandomGirl(18, false, false, false, true); // `J` Legal Note: 18 is the Legal Age of Majority for the USA where I live
                            if (!GangManager.GangCombat(tempgirl, this.GangCible))
                            {
                                gotitem = true;
                            }
                            if (WMRand.Percent(20))
                            {
                                totalItems++;
                                bringBackNum += 2;
                            }
                            else if (WMRand.Percent(50))
                            {
                                gold += 1 + WMRand.Random(200);
                            }
                        }
                        else
                        {
                            gotitem = true;
                        }
                        if (gotitem)
                        {
                            bringBackNum += 4;
                            totalItems++;
                        }
                        else
                        {
                            bringBackNum += 2;
                        }
                    }
                    else // get beast = 2 point
                    {
                        bool gotBeast = false;
                        if (this.GangCible.NetLimit > 0) // try to capture using net
                        {
                            while (this.GangCible.NetLimit > 0 && !gotBeast)
                            {
                                int damageChance = 50; // higher damage net chance in the catacombs
                                if (WMRand.Percent(this.GangCible.Combat)) // hit it with the net
                                {
                                    if (!WMRand.Percent(60))
                                    {
                                        gotBeast = true;
                                    }
                                    else
                                    {
                                        damageChance = 80;
                                    }
                                }
                                if (WMRand.Percent(damageChance))
                                {
                                    this.GangCible.AdjustNetLimit(-1);
                                    GangManager.NumNets--;
                                }
                            }
                        }
                        if (!gotBeast) // fight it
                        {
                            // the last few members will runaway or allow the beast to run away so that the can still bring back what they have
                            while (this.GangCible.MemberNum > 1 + WMRand.Random(3) && !gotBeast)
                            {
                                if (WMRand.Percent(Math.Min(90, this.GangCible.Combat)))
                                {
                                    gotBeast = true;
                                    continue;
                                }
                                if (this.GangCible.HealLimit == 0)
                                {
                                    this.GangCible.MemberNum--;
                                    continue;
                                }
                                // `J` 5% chance they will not get the healing potion in time.
                                // needed to have atleast some chance or else they are totally invincable.
                                if (WMRand.Percent(5))
                                {
                                    this.GangCible.MemberNum--;
                                }
                                this.GangCible.AdjustHealLimit(-1);
                                GangManager.NumHealingPotions--;
                            }
                        }
                        if (gotBeast)
                        {
                            int numbeasts = 1 + WMRand.Random(3);
                            bringBackNum += numbeasts * 2;
                            totalBeast += numbeasts;
                        }
                        else
                        {
                            bringBackNum++;
                        }
                    }
                }

                // determine loot
                if (this.GangCible.MemberNum < 1)
                {
                    return false; // they all died
                }
                else
                {
                    if (num == this.GangCible.MemberNum)
                    {
                        catacombsMissionEvent.AppendFormat(LocalString.ResourceStringCategory.GangMission,
                            "All[Number]OfThemReturn",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", this.GangCible.MemberNum) });
                    }
                    else
                    {
                        catacombsMissionEvent.AppendFormat(LocalString.ResourceStringCategory.GangMission,
                            "[Number]OfThe[GangNumber]WhoWentOutReturn",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", this.GangCible.MemberNum), new FormatStringParameter("GangNumber", num) });
                    }

                    if (gold > 0)
                    {
                        catacombsMissionEvent.AppendFormat(LocalString.ResourceStringCategory.GangMission,
                            "TheyBringBackWithThem[Number]Gold",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", gold) });
                        Game.Gold.CatacombLoot(gold);
                    }

                    // get catacomb girls (is "monster" if trait not human)
                    if (totalGirls > 0)
                    {
                        if (totalGirls == 1)
                        {
                            catacombsMissionEvent.AppendLine(LocalString.ResourceStringCategory.GangMission, "YourMenCapturedOneGirl");
                        }
                        else
                        {
                            catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.GangMission,
                                "YourMenCaptured[Number]Girls",
                                new List<FormatStringParameter>() { new FormatStringParameter("Number", totalGirls) });
                        }

                        for (int i = 0; i < totalGirls; i++)
                        {
                            sGirl ugirl = null;
                            bool unique = WMRand.Percent(Configuration.Catacombs.UniqueCatacombs); // chance of getting unique girl
                            if (unique)
                            {
                                ugirl = Game.Girls.GetRandomGirl(false, true);
                                if (ugirl == null)
                                {
                                    unique = false;
                                }
                            }
                            if (unique)
                            {
                                catacombsMissionEvent.AppendLitteral("   " + ugirl.Realname);
                                catacombsMissionEvent.AppendLine(LocalString.ResourceStringCategory.Global, "Unique");

                                // TODO : Need comprehention
                                ugirl.m_States &= ~(1 << (int)Status.CATACOMBS);

                                LocalString NGmsg = new LocalString();
                                ugirl.add_trait("Kidnapped", 2 + WMRand.Random(10));
                                NGmsg.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                                    "[GirlName]WasCapturedInTheCatacombsBy[GangName]",
                                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", ugirl.Realname), new FormatStringParameter("GangName", this.GangCible.Name) });
                                ugirl.Events.AddMessage(NGmsg.ToString(), ImageType.PROFILE, EventType.Gang);
                                Game.Dungeon.AddGirl(ugirl, DungeonReasons.GIRLCAPTURED);
                            }
                            else
                            {
                                ugirl = Game.Girls.CreateRandomGirl(0, false, false, false, true);
                                if (ugirl != null) // make sure a girl was returned
                                {
                                    catacombsMissionEvent.AppendLineLitteral("   " + ugirl.Realname);
                                    LocalString NGmsg = new LocalString();
                                    ugirl.add_trait("Kidnapped", 2 + WMRand.Random(10));
                                    NGmsg.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                                        "[GirlName]WasCapturedInTheCatacombsBy[GangName]",
                                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", ugirl.Realname), new FormatStringParameter("GangName", this.GangCible.Name) });
                                    ugirl.Events.AddMessage(NGmsg.ToString(), ImageType.PROFILE, EventType.Gang);
                                    Game.Brothels.GetDungeon().AddGirl(ugirl, DungeonReasons.GIRLCAPTURED);
                                }
                            }
                            if ((ugirl != null) && (Game.Brothels.CurrentObjective != null)
                                && (Game.Brothels.CurrentObjective.Objective == Objectives.CAPTUREXCATACOMBGIRLS))
                            {
                                Game.Brothels.CurrentObjective.SoFar++;
                            }
                        }
                    }
                    catacombsMissionEvent.NewLine();

                    // get items
                    if (totalItems > 0)
                    {
                        if (totalItems == 1)
                        {
                            catacombsMissionEvent.AppendLine(LocalString.ResourceStringCategory.GangMission, "YourMenBringBackOneItem");
                        }
                        else
                        {
                            catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.GangMission,
                                "YourMenBringBack[Number]Items",
                                new List<FormatStringParameter>() { new FormatStringParameter("Number", totalItems) });
                        }

                        for (int i = 0; i < totalItems; i++)
                        {
                            bool quit = false;
                            bool add = false;
                            sInventoryItem temp = Game.Inventory.GetRandomCatacombItem();
                            if (temp != null)
                            {
                                catacombsMissionEvent.AppendLineLitteral("   " + temp.Name);
                                int curI = Game.Brothels.HasItem(temp.Name, -1);
                                bool loop = true;
                                while (loop)
                                {
                                    if (curI != -1)
                                    {
                                        if (Game.Brothels.m_NumItem[curI] >= 999)
                                        {
                                            curI = Game.Brothels.HasItem(temp.Name, curI + 1);
                                        }
                                        else
                                        {
                                            loop = false;
                                        }
                                    }
                                    else
                                    {
                                        loop = false;
                                    }
                                }

                                if (Game.Brothels.m_NumInventory < Constants.MAXNUM_INVENTORY || curI != -1)
                                {
                                    if (curI != -1)
                                    {
                                        Game.Brothels.m_NumItem[curI]++;
                                    }
                                    else
                                    {
                                        for (int j = 0; j < Constants.MAXNUM_INVENTORY; j++)
                                        {
                                            if (Game.Brothels.m_Inventory[j] == null)
                                            {
                                                Game.Brothels.m_Inventory[j] = temp;
                                                Game.Brothels.m_EquipedItems[j] = 0;
                                                Game.Brothels.m_NumInventory++;
                                                Game.Brothels.m_NumItem[j]++;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    quit = true;
                                    catacombsMissionEvent.AppendLine(LocalString.ResourceStringCategory.Player, "YourInventoryIsFull");
                                }
                            }
                            if (quit)
                            {
                                break;
                            }
                        }
                    }

                    // bring back any beasts
                    if (totalBeast > 0)
                    {
                        if (totalGirls + totalItems > 0)
                        {
                            catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.GangMission,
                                "YourMenAlsoBringBack[Number]Beasts",
                                new List<FormatStringParameter>() { new FormatStringParameter("Number", totalBeast) });
                        }
                        else
                        {
                            catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.GangMission,
                                "YourMenBringBack[Number]Beasts",
                                new List<FormatStringParameter>() { new FormatStringParameter("Number", totalBeast) });
                        }
                        Game.Brothels.add_to_beasts(totalBeast);
                    }
                }
            }
            this.GangCible.Events.AddMessage(catacombsMissionEvent.ToString(), ImageType.PROFILE, EventType.Gang);
            return true;
        }
    }
}
