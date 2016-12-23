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
//  <copyright file="Inventory.cs" company="The Pink Petal Devloment Team">
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
namespace WMaster.Entity.Item
{
    using System;
    using System.Collections.Generic;
    using WMaster.ClassOrStructurToImplement;

    public class Inventory
    {
        public Inventory()
        {
            for (int i = 0; i < Constants.NUM_SHOPITEMS; i++)
            {
                m_ShopItems[i] = null;
            }
            m_NumShopItems = 0;
        }


        public void Free()
        { throw new NotImplementedException(); }

        public void LoadItems(string filename)
        { throw new NotImplementedException(); }
        public bool LoadItemsXML(string filename)
        { throw new NotImplementedException(); }
        public void UpdateShop()
        { throw new NotImplementedException(); } // re-randomizes the shops inventory
        public sInventoryItem GetItem(string name)
        { throw new NotImplementedException(); }
        public sInventoryItem GetShopItem(int num)
        { throw new NotImplementedException(); }
        public int GetRandomShopItem()
        { throw new NotImplementedException(); }
        public sInventoryItem GetRandomItem()
        { throw new NotImplementedException(); }
        public sInventoryItem GetRandomCatacombItem()
        { throw new NotImplementedException(); }
        public sInventoryItem GetRandomCraftableItem(sGirl girl, int job, int points)
        { throw new NotImplementedException(); }
        public string CraftItem(sGirl girl, int job, int points)
        { throw new NotImplementedException(); }

        public int CheckShopItem(string name)
        { throw new NotImplementedException(); } // checks if a item is in shop inventory, returns -1 if not and the id if it is
        public sInventoryItem BuyShopItem(int num)
        { throw new NotImplementedException(); } // removes and returns the item from the shop
        public bool GirlBuyItem(sGirl girl, int ShopItem, int MaxItems, bool AutoEquip)
        { throw new NotImplementedException(); } // girl buys selected item if possible; returns true if bought

        public void Equip(sGirl girl, int num, bool force)
        { throw new NotImplementedException(); }
        public void Equip(sGirl girl, sInventoryItem item, bool force)
        { throw new NotImplementedException(); }
        public void Unequip(sGirl girl, int num)
        { throw new NotImplementedException(); }

        public void AddItem(sInventoryItem item)
        { throw new NotImplementedException(); }
        public void CalculateCost(sInventoryItem newItem)
        { throw new NotImplementedException(); }

        public int HappinessFromItem(sInventoryItem item)
        { throw new NotImplementedException(); } // determines how happy a girl will be to receive an item (or how unhappy to lose it)

        public void GivePlayerAllItems()
        { throw new NotImplementedException(); }

        public bool IsItemEquipable(sInventoryItem item)
        {
            if (item.ItemType != sInventoryItem.Type.Misc)
            {
                return true;
            }
            return false;
        }
        public void sort()
        { throw new NotImplementedException(); }

        public bool equip_limited_item_ok(sGirl NamelessParameter1, int NamelessParameter2, bool NamelessParameter3, int NamelessParameter4)
        { throw new NotImplementedException(); }
        public bool equip_pair_ok(sGirl NamelessParameter1, int NamelessParameter2, bool NamelessParameter3)
        { throw new NotImplementedException(); }
        public bool equip_ring_ok(sGirl NamelessParameter1, int NamelessParameter2, bool NamelessParameter3)
        { throw new NotImplementedException(); }
        public bool equip_singleton_ok(sGirl NamelessParameter1, int NamelessParameter2, bool NamelessParameter3)
        { throw new NotImplementedException(); }
        public bool ok_2_equip(sGirl NamelessParameter1, int NamelessParameter2, bool NamelessParameter3)
        { throw new NotImplementedException(); }
        public void remove_trait(sGirl NamelessParameter1, int NamelessParameter2, int NamelessParameter3)
        { throw new NotImplementedException(); }

        /*bool has_dildo()
        {
            return	g_Girls.HasItemJ(girl, "Compelling Dildo") != -1) ||
                g_Girls.HasItemJ(girl, "Dildo") != -1) ||
                g_Girls.HasItemJ(girl, "Studded Dildo") != -1) ||
                g_Girls.HasItemJ(girl, "Double Dildo") != -1) ||
                g_Girls.HasItemJ(girl, "Dreidel Dildo") != -1);
        }*/

        private List<sInventoryItem> items = new List<sInventoryItem>(); // Master list of items?
        private int m_NumShopItems; // number of items in the shop
        [Obsolete("Convert sInventoryItem[] to List<sInventoryItem>")]
        private sInventoryItem[] m_ShopItems = new sInventoryItem[Constants.NUM_SHOPITEMS]; // pointers to all items, the shop can only hold 30 random items
    }
}
