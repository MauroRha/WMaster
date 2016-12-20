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
//  <copyright file="InventoryItem.cs" company="The Pink Petal Devloment Team">
//      Copyright © 2009, 2010 - The Pink Petal Devloment Team.
//  </copyright>
//  <author>Graben</author>
//  <datecreated>2016-12-14</datecreated>
//  <summary>
//  </summary>
//  <remarks>
//      <para name="Rem">Extract from GitHub : relased find in December 2016</para>
//      <para name="Review_2016-12">Gbn - 12/2016 : Create / Convert to C# / Refactoring</para>
//  </remarks>
//</file>
//<!-- -------------------------------------------------------------------------------------------------------------------- -->
namespace WMaster.Game.Entity.Item
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WMaster.ClassOrStructurToImplement;

    /// <summary>
    /// Represent an item in inventory (player, girl)
    /// </summary>
    public class sInventoryItem
    {
        #region Embeded entity
        /// <summary>
        /// Item type enum. Representing type a item into inventory.
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// Worn on fingers (max 8).
            /// </summary>
            Ring = 1,
            /// <summary>
            /// Worn on body, (max 1).
            /// </summary>
            Dress = 2,
            /// <summary>
            /// Worn on feet, (max 1).
            /// </summary>
            Shoes = 3,
            /// <summary>
            /// Eaten, single use.
            /// </summary>
            Food = 4,
            /// <summary>
            /// Worn on neck, (max 1).
            /// </summary>
            Necklace = 5,
            /// <summary>
            /// Equiped on body, (max 2).
            /// </summary>
            Weapon = 6,
            /// <summary>
            /// Worn on face, single use.
            /// </summary>
            Makeup = 7,
            /// <summary>
            /// Worn on body over dresses (max 1).
            /// </summary>
            Armor = 8,
            /// <summary>
            /// Random stuff. may cause a constant effect without having to be equiped.
            /// </summary>
            Misc = 9,
            /// <summary>
            /// Worn around arms, (max 2).
            /// </summary>
            Armband = 10,
            /// <summary>
            /// Hidden on body, (max 2).
            /// </summary>
            SmWeapon = 11,
            /// <summary>
            /// Worn under dress, (max 1).
            /// </summary>
            Underwear = 12,
            /// <summary>
            /// Noncombat worn on the head, (max 1).
            /// <remarks><para>CRAZY added this</para></remarks>
            /// </summary>
            Hat = 13,
            /// <summary>
            /// Combat worn on the head, (max 1)
            /// <remarks><para>CRAZY added this</para></remarks>
            /// </summary>
            Helmet = 14,
            /// <summary>
            /// Glasses, (max 1)
            /// <remarks><para>CRAZY added this</para></remarks>
            /// </summary>
            Glasses = 15,
            /// <summary>
            /// Swimsuit (max 1 in use but can have as many as they want).
            /// <remarks><para>CRAZY added this</para></remarks>
            /// </summary>
            Swimsuit = 16,
            /// <summary>
            /// Combat Shoes (max 1) often unequipped outside of combat.
            /// <remarks><para>`J` added this</para></remarks>
            /// </summary>
            Combatshoes = 17,
            /// <summary>
            /// Shields (max 1) often unequipped outside of combat.
            /// <remarks><para>`J` added this</para></remarks>
            /// </summary>
            Shield = 18
        };

        /// <summary>
        /// item special values enum. Representing special values of item.
        /// </summary>
        public enum Special
        {
            /// <summary>
            /// No special value.
            /// </summary>
            None = 0,
            AffectsAll = 1,
            /// <summary>
            /// Effect affect temporary.
            /// </summary>
            Temporary = 2
        };

        /// <summary>
        /// Item rarity
        /// <remarks>
        ///     <list type="bullet">
        ///         <item>0 means common,</item>
        ///         <item>1 means 50% chance of appearing in shops,</item>
        ///         <item>2 means 25% chance,</item>
        ///         <item>3 means 5% chance,</item>
        ///         <item>4 means only 15% chance of being found in catacombs,</item>
        ///         <item>5 means ONLY given in scripts,</item>
        ///         <item>6 means same as 5 but also may be given as a reward for objective,</item>
        ///         <item>7 means only 5% chance in catacombs (catacombs05),</item>
        ///         <item>8 means only 1% chance in catacombs (catacombs01).</item>
        ///     </list>
        /// </remarks>
        /// </summary>
        public enum Rarity
        {
            /// <summary>
            /// Common item.
            /// </summary>
            Common = WMaster.Enum.ItemRarity.COMMON,
            /// <summary>
            /// 50% chance of appearing in shops.
            /// </summary>
            Shop50 = WMaster.Enum.ItemRarity.SHOP50,
            /// <summary>
            /// 25% chance of appearing in shops.
            /// </summary>
            Shop25 = WMaster.Enum.ItemRarity.SHOP25,
            /// <summary>
            /// 5% chance of appearing in shops.
            /// </summary>
            Shop05 = WMaster.Enum.ItemRarity.SHOP05,
            /// <summary>
            /// 15% chance of being found in catacombs.
            /// </summary>
            Catacomb15 = WMaster.Enum.ItemRarity.CATACOMB15,
            /// <summary>
            /// 5% chance of being found in catacombs.
            /// <remarks><para>MYR: Added 05 and 01 for the really, really valuable things like invulnerability</para></remarks>
            /// </summary>
            Catacomb05 = WMaster.Enum.ItemRarity.CATACOMB05,
            /// <summary>
            /// 1% chance of being found in catacombs.
            /// <remarks><para>MYR: Added 05 and 01 for the really, really valuable things like invulnerability</para></remarks>
            /// </summary>
            Catacomb01 = WMaster.Enum.ItemRarity.CATACOMB01,
            /// <summary>
            /// ONLY given in scripts.
            /// </summary>
            ScriptOnly = WMaster.Enum.ItemRarity.SCRIPTONLY,
            /// <summary>
            /// ONLY given in scripts or reward for objective.
            /// </summary>
            ScriptOrReward = WMaster.Enum.ItemRarity.SCRIPTORREWARD
        };
        #endregion

        #region Fields
        /// <summary>
        /// Item name.
        /// </summary>
        private string m_Name;
        /// <summary>
        /// Get or ser item name.
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        } 
        private string m_Desc;
        /// <summary>
        /// Item type
        /// </summary>
        private Type m_Type;
        #endregion

        #region Properties
        public Type ItemType
        {
            get { return this.m_Type; }
        }
        #endregion


        private Special m_Special;
        /*
         *	if 1 then this item doesn't run out if stocked in shop inventory
         */
        private bool m_Infinite;
        /*
         *	the number of effects this item has
         */
        private List<sEffect> m_Effects;
        /*
         *	how much the item is worth?
         */
        private long m_Cost;

        /// <summary>
        /// Badness of item
        /// <remarks>
        ///     <list type="bullet">
        ///         <item>0 is good, while badness > is evil,</item>
        ///         <item>Girls may fight back if badness > 0,</item>
        ///         <item>Girls won't normally buy items > 20 on their own, default formula is -5% chance to buy on their own per Badness point (5 Badness = 75% chance),</item>
        ///         <item>Girls with low obedience may take off something that is bad for them.</item>
        ///     </list>
        /// </remarks>
        /// </summary>
        private short m_Badness;
        /// <summary>
        /// Chance that a girl on break will buy this item if she's looking at it in the shop
        /// </summary>
        private short m_GirlBuyChance;
        private Rarity m_Rarity;

        /// <summary>
        /// Set item rarity from it's string representation.
        /// </summary>
        /// <param name="s">String representation of item rarity.</param>
        public void set_rarity(string s)
        {
            if (s == "Common")
            {
                m_Rarity = Rarity.Common;
            }
            else if (s == "Shop50")
            {
                m_Rarity = Rarity.Shop50;
            }
            else if (s == "Shop25")
            {
                m_Rarity = Rarity.Shop25;
            }
            else if (s == "Shop05")
            {
                m_Rarity = Rarity.Shop05;
            }
            else if (s == "Catacomb15")
            {
                m_Rarity = Rarity.Catacomb15;
            }
            else if (s == "Catacomb05")
            {
                m_Rarity = Rarity.Catacomb05;
            }
            else if (s == "Catacomb01")
            {
                m_Rarity = Rarity.Catacomb01;
            }
            else if (s == "ScriptOnly")
            {
                m_Rarity = Rarity.ScriptOnly;
            }
            else if (s == "ScriptOrReward")
            {
                m_Rarity = Rarity.ScriptOrReward;
            }
            else
            {
                WMaster.WMLog.Trace(string.Format("set_rarity: unexpected value '{0}'", s), WMLog.TraceLog.ERROR);
                m_Rarity = Rarity.Shop05;
            }
        }

        /// <summary>
        /// Set item special from it's string representation.
        /// </summary>
        /// <param name="s">String representation of item special.</param>
        void set_special(string s)
        {
            if (s == "None")
            { m_Special = Special.None; }
            else if (s == "AffectsAll")
            { m_Special = Special.AffectsAll; }
            else if (s == "Temporary")
            { m_Special = Special.Temporary; }
            else
            {
                m_Special = Special.None;
                WMaster.WMLog.Trace(string.Format("Unexpected special string: '{0}'", s), WMLog.TraceLog.ERROR);
            }
        }

        // `J` Incomplete Craftable code - commenting out
        #region `J` Incomplete Craftable code - commenting out
#if false
        public enum Craftable
        {
            No = 0,
            Any = 1,
            Baker = Enum.Jobs.BAKER,
            Blacksmith = Enum.Jobs.BLACKSMITH,
            Brewer = Enum.Jobs.BREWER,
            Butcher = Enum.Jobs.BUTCHER,
            MakeItem = Enum.Jobs.MAKEITEM,
            Milker = Enum.Jobs.MILKER,
        };
        public Craftable m_Craftable;	// Who can make it
        int m_CraftLevel;		// Girl Level needed to make it
        int m_CraftCraft;		// Craft skill needed to make it
        int m_CraftStrength;	// Strength needed to make it
        int m_CraftMagic;		// Magic skill needed to make it
        int m_CraftIntel;		// Intelligence needed to make it
        int m_CraftPoints;		// Craft points needed to make it

        void set_craftable(string s)
        {
            if (s == "No" || s == "no" || s == "False" || s == "false")
            { m_Craftable = Craftable.No; }
            else if (s == "Any")
            { m_Craftable = Craftable.Any; }
            else if (s == "Baker")
            { m_Craftable = Craftable.Baker; }
            else if (s == "Blacksmith")
            { m_Craftable = Craftable.Blacksmith; }
            else if (s == "Brewer")
            { m_Craftable = Craftable.Brewer; }
            else if (s == "Butcher")
            { m_Craftable = Craftable.Butcher; }
            else if (s == "MakeItem")
            { m_Craftable = Craftable.MakeItem; }
            else if (s == "Milker") { m_Craftable = Craftable.Milker; }
            else
            {
                WMaster.WMLog.Write(string.Format("Error in set_craftable: unexpected value '{0}'", s));
                m_Craftable = Craftable.No;
            }	// what to do?
        }
#endif
        #endregion

        /// <summary>
        /// Set item type from it's string representation.
        /// </summary>
        /// <param name="s">String representation of item type.</param>
        void set_type(string s)
        {
            if (s == "Ring")
            { m_Type = Type.Ring; }
            else if (s == "Dress")
            { m_Type = Type.Dress; }
            else if (s == "Under Wear" || s == "Underwear") { m_Type = Type.Underwear; }
            else if (s == "Shoes")
            { m_Type = Type.Shoes; }
            else if (s == "Food")
            { m_Type = Type.Food; }
            else if (s == "Necklace")
            { m_Type = Type.Necklace; }
            else if (s == "Weapon")
            { m_Type = Type.Weapon; }
            else if (s == "Small Weapon")
            { m_Type = Type.SmWeapon; }
            else if (s == "Makeup")
            { m_Type = Type.Makeup; }
            else if (s == "Armor")
            { m_Type = Type.Armor; }
            else if (s == "Misc")
            { m_Type = Type.Misc; }
            else if (s == "Armband")
            { m_Type = Type.Armband; }
            else if (s == "Hat")
            { m_Type = Type.Hat; }
            else if (s == "Glasses")
            { m_Type = Type.Glasses; }
            else if (s == "Swimsuit")
            { m_Type = Type.Swimsuit; }
            else if (s == "Helmet")
            { m_Type = Type.Helmet; }
            else if (s == "Shield")
            { m_Type = Type.Shield; }
            else if (s == "Combat Shoes")
            { m_Type = Type.Combatshoes; }
            else if (s == "CombatShoes")
            { m_Type = Type.Combatshoes; }
            else
            {
                WMaster.WMLog.Trace(string.Format("Unexpected item type: unexpected value '{0}'", s), WMLog.TraceLog.ERROR);
                m_Type = Type.Misc;
            }
        }

        // TODO : TRADUCTION - Prévoir le retour d'une chaine localisé.
        /// <summary>
        /// Return string representation of sInventoryItem.Special
        /// </summary>
        /// <param name="spec"><see cref="sInventoryItem.Special"/> to convert to string.</param>
        /// <returns>The string representation.</returns>
        public static string ToString(sInventoryItem.Special spec)
        {
            switch (spec)
            {
                case Special.None:
                    return "None";
                case Special.AffectsAll:
                    return "AffectsAll";
                case Special.Temporary:
                    return "Temporary";
                default:
                    WMaster.WMLog.Trace(string.Format("Error: unexpected special value: unexpected value '{0}'", spec), WMLog.TraceLog.ERROR);
                    return string.Format("Error({0})", spec);
            }
        }

        // TODO : Prévoir le retour d'une chaine localisé.
        /// <summary>
        /// Return string representation of sInventoryItem.Rarity
        /// </summary>
        /// <param name="r"><see cref="sInventoryItem.Rarity"/> to convert to string.</param>
        /// <returns>The string representation.</returns>
        public static string ToString(sInventoryItem.Rarity r)
        {
            switch (r)
            {
                case Rarity.Common:
                    return "Common";
                case Rarity.Shop50:
                    return "Shops, 50%";
                case Rarity.Shop25:
                    return "Shops, 25%";
                case Rarity.Shop05:
                    return "Shops, 05%";
                case Rarity.Catacomb15:
                    return "Catacombs, 15%";
                case Rarity.Catacomb05:
                    return "Catacombs, 05%";
                case Rarity.Catacomb01:
                    return "Catacombs, 01%";
                case Rarity.ScriptOnly:
                    return "Scripted Only";
                case Rarity.ScriptOrReward:
                    return "Scripts or Reward";
                default:
                    WMaster.WMLog.Trace(string.Format("Unexpected rarity value: unexpected value '{0}'", r), WMLog.TraceLog.ERROR);
                    return string.Format("Error({0})", r);
            }
        }

        // TODO : Prévoir le retour d'une chaine localisé.
        /// <summary>
        /// Return string representation of sInventoryItem.Type
        /// </summary>
        /// <param name="r"><see cref="sInventoryItem.Type"/> to convert to string.</param>
        /// <returns>The string representation.</returns>
        public static string ToString(sInventoryItem.Type typ)
        {
            switch (typ)
            {
                case Type.Ring:
                    return "Ring";
                case Type.Dress:
                    return "Dress";
                case Type.Underwear:
                    return "Underwear";
                case Type.Shoes:
                    return "Shoes";
                case Type.Food:
                    return "Food";
                case Type.Necklace:
                    return "Necklace";
                case Type.Weapon:
                    return "Weapon";
                case Type.SmWeapon:
                    return "Small Weapon";
                case Type.Makeup:
                    return "Makeup";
                case Type.Armor:
                    return "Armor";
                case Type.Misc:
                    return "Misc";
                case Type.Armband:
                    return "Armband";
                case Type.Hat:
                    return "Hat";
                case Type.Helmet:
                    return "Helmet";
                case Type.Glasses:
                    return "Glasses";
                case Type.Swimsuit:
                    return "Swimsuit";
                case Type.Combatshoes:
                    return "Combat Shoes";
                case Type.Shield:
                    return "Shield";
                default:
                    WMaster.WMLog.Trace(string.Format("Unexpected type value: unexpected value '{0}'", typ), WMLog.TraceLog.ERROR);
                    return string.Format("Error({0})", typ);
            }
        }

        // TODO : REFACTORING - Susstitute this to ToString() instance override function?
        /// <summary>
        /// Return a string representation of an <see cref="sInventoryItem"/>
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        public static string ToString(sInventoryItem it)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("Desc: {0}", it.m_Desc));
            sb.AppendLine(string.Format("Type: {0}", it.m_Type));
            sb.AppendLine(string.Format("Badness: {0}", it.m_Badness));
            sb.AppendLine(string.Format("Special: {0}", it.m_Special));
            sb.AppendLine(string.Format("Cost: {0}", it.m_Cost));
            sb.AppendLine(string.Format("Rarity: {0}", it.m_Rarity));
            sb.AppendLine(string.Format("Infinite: {0}", (it.m_Infinite ? "True" : "False")));
            foreach (sEffect item in it.m_Effects)
            {
                sb.AppendLine(item.ToString());

            }
            return sb.ToString();
        }
    }
}