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
namespace WMaster.Game.Manager
{
    using System;
    using WMaster.ClassOrStructurToImplement;

    /*
    * manages the farm
    *
    * extend cBrothelManager
    */
    public class cFarmManager : cBrothelManager, System.IDisposable
    {
        public cFarmManager()
        { throw new NotImplementedException(); } // constructor
        public void Dispose()
        { throw new NotImplementedException(); } // destructor

        void AddGirl(int brothelID, sGirl girl)
        { throw new NotImplementedException(); }
        void RemoveGirl(int brothelID, sGirl girl, bool deleteGirl = false)
        { throw new NotImplementedException(); } // Removes a girl from the list (only used with editor where all girls are available)
        void UpdateFarm()
        { throw new NotImplementedException(); }
        void UpdateGirls(sBrothel brothel, bool Day0Night1)
        { throw new NotImplementedException(); }
        //void	AddBrothel(sBrothel* newBroth);
        IXmlElement SaveDataXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadDataXML(IXmlHandle hBrothelManager)
        { throw new NotImplementedException(); }
        void Free()
        { throw new NotImplementedException(); }
        public int m_NumFarms;
        public cJobManager m_JobManager = new cJobManager();

        /*
        int FoodAnimal[6] = { 0, 0, 0, 0, 0, 0 };
        string FoodAnimalName[6] = { "Egg", "Chicken", "Goat", "Sheep", "Ostrich", "Cow" };
        int FoodAnimalFoodValue[6] = { 1, 2, 3, 3, 6, 10 };
	
        int FoodPlant[6] = { 0, 0, 0, 0, 0, 0 };
        string FoodPlantName[6] = { "Wheat", "Corn", "Potato", "Tomato", "Lettuce", "Hops" };

        int GardenPlant[10] = { 10, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        string GardenPlantName[10] = { "Weeds", "Easy", "Simple", "Common", "Uncommon", "Special", "Very Special", "Rare", "Very Rare", "Unique" };
        */

    }

}
