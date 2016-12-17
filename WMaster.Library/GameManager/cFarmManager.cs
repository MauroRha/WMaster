using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
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
