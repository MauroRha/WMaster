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
namespace WMaster.Manager
{
    using System;
    using System.Collections.Generic;
    using WMaster.ClassOrStructurToImplement;
    using WMaster.Entity.Item;
    using WMaster.Enums;

    // Keeps track of all the available (not used by player) girls in the game.
    public class cGirls : cAbstractGirls, System.IDisposable
    {
        public cGirls()
        { throw new NotImplementedException(); }
        public void Dispose()
        { throw new NotImplementedException(); }

        void Free()
        { throw new NotImplementedException(); }

        /*
        *	load the templated girls
        *	(if loading a save game doesn't load from the global template,
        *	loads from the save games' template)
        *
        *	LoadGirlsDecider is a wrapper function that decides to load XML or Legacy formats.
        //  `J` Legacy support has been removed
        *	LoadGirlsXML loads the XML files
        */
        void LoadGirlsDecider(string filename)
        { throw new NotImplementedException(); }
        void LoadGirlsXML(string filename)
        { throw new NotImplementedException(); }
        /*
        *	SaveGirls doesn't seem to be the inverse of LoadGirls
        *	but rather writes girl data to the save file
        */
        IXmlElement SaveGirlsXML(IXmlElement pRoot)
        { throw new NotImplementedException(); } // Saves the girls to a file
        bool LoadGirlsXML(IXmlHandle hGirls)
        { throw new NotImplementedException(); }

        void AddGirl(sGirl girl)
        { throw new NotImplementedException(); } // adds a girl to the list
        void RemoveGirl(sGirl girl, bool deleteGirl = false)
        { throw new NotImplementedException(); } // Removes a girl from the list (only used with editor where all girls are available)

        sGirl GetGirl(int girl)
        { throw new NotImplementedException(); } // gets the girl by count

        void GirlFucks(sGirl girl, bool Day0Night1, sCustomer customer, bool group, string message, uint SexType)
        { throw new NotImplementedException(); } // does the logic for fucking
        // MYR: Millions of ways to say, [girl] does [act] to [customer]
        string GetRandomGroupString()
        { throw new NotImplementedException(); }
        string GetRandomSexString()
        { throw new NotImplementedException(); }
        string GetRandomOralSexString()
        { throw new NotImplementedException(); }
        string GetRandomLesString()
        { throw new NotImplementedException(); }
        string GetRandomBDSMString()
        { throw new NotImplementedException(); }
        string GetRandomBeastString()
        { throw new NotImplementedException(); }
        string GetRandomAnalString()
        { throw new NotImplementedException(); }

        // MYR: More functions for attack/defense/agility-style combat.
        bool GirlInjured(sGirl girl, uint modifier)
        { throw new NotImplementedException(); }
        public int GetCombatDamage(sGirl girl, int CombatType)
        { throw new NotImplementedException(); }
        public int TakeCombatDamage(sGirl girl, int amt)
        { throw new NotImplementedException(); }

        void LevelUp(sGirl girl)
        { throw new NotImplementedException(); }// advances a girls level
        void LevelUpStats(sGirl girl)
        { throw new NotImplementedException(); } // Functionalized stat increase for LevelUp

        void EndDayGirls(sBrothel brothel, sGirl girl)
        { throw new NotImplementedException(); }

        public int GetStat(sGirl girl, int stat)
        { throw new NotImplementedException(); }
        void SetStat(sGirl girl, int stat, int amount)
        { throw new NotImplementedException(); }
        public void UpdateStat(sGirl girl, int stat, int amount, bool usetraits = true)
        { throw new NotImplementedException(); } // updates a stat
        public void UpdateStatTemp(sGirl girl, int stat, int amount)
        { throw new NotImplementedException(); } // updates a stat temporarily
        void UpdateStatMod(sGirl girl, int stat, int amount)
        { throw new NotImplementedException(); } // updates a statmod usually from items
        void UpdateStatTr(sGirl girl, int stat, int amount)
        { throw new NotImplementedException(); } // updates a statTr from traits

        public int GetSkill(sGirl girl, int skill)
        { throw new NotImplementedException(); }
        void SetSkill(sGirl girl, int skill, int amount)
        { throw new NotImplementedException(); }
        public void UpdateSkill(sGirl girl, int skill, int amount)
        { throw new NotImplementedException(); } // updates a skill
        public void UpdateSkillTemp(sGirl girl, int skill, int amount)
        { throw new NotImplementedException(); } // updates a skill temporarily
        void UpdateSkillMod(sGirl girl, int skill, int amount)
        { throw new NotImplementedException(); } // updates a skillmods usually from items
        void UpdateSkillTr(sGirl girl, int skill, int amount)
        { throw new NotImplementedException(); } // updates a skillTr from traits

        public int GetEnjoyment(sGirl girl, int a_Enjoy)
        { throw new NotImplementedException(); } // `J` added
        void SetEnjoyment(sGirl girl, int a_Enjoy, int amount)
        { throw new NotImplementedException(); } // `J` added
        void SetEnjoymentTR(sGirl girl, int a_Enjoy, int amount)
        { throw new NotImplementedException(); } // `J` added for traits
        public void UpdateEnjoyment(sGirl girl, int whatSheEnjoys, int amount)
        { throw new NotImplementedException(); } // updates what she enjoys
        void UpdateEnjoymentTR(sGirl girl, int whatSheEnjoys, int amount)
        { throw new NotImplementedException(); } // `J` added for traits
        void UpdateEnjoymentMod(sGirl girl, int whatSheEnjoys, int amount)
        { throw new NotImplementedException(); } // `J` added for traits
        public void UpdateEnjoymentTemp(sGirl girl, int whatSheEnjoys, int amount)
        { throw new NotImplementedException(); } // `J` added for traits

        public int GetTraining(sGirl girl, int a_Training)
        { throw new NotImplementedException(); } // `CRAZY` added
        void SetTraining(sGirl girl, int a_Training, int amount)
        { throw new NotImplementedException(); } // `CRAZY` added
        void SetTrainingTR(sGirl girl, int a_Training, int amount)
        { throw new NotImplementedException(); } // `CRAZY` added for traits
        public void UpdateTraining(sGirl girl, int whatSheTrains, int amount)
        { throw new NotImplementedException(); } // updates what she enjoys
        void UpdateTrainingTR(sGirl girl, int whatSheTrains, int amount)
        { throw new NotImplementedException(); } // `CRAZY` added for traits
        void UpdateTrainingMod(sGirl girl, int whatSheTrains, int amount)
        { throw new NotImplementedException(); } // `CRAZY` added for traits
        public void UpdateTrainingTemp(sGirl girl, int whatSheTrains, int amount)
        { throw new NotImplementedException(); } // `CRAZY` added for traits
        double GetAverageOfAllSkills(sGirl girl)
        { throw new NotImplementedException(); } // `J` added
        double GetAverageOfSexSkills(sGirl girl)
        { throw new NotImplementedException(); } // `J` added
        double GetAverageOfNSxSkills(sGirl girl)
        { throw new NotImplementedException(); } // `J` added

        public bool HasTrait(sGirl girl, string trait)
        { throw new NotImplementedException(); }
        bool HasRememberedTrait(sGirl girl, string trait)
        { throw new NotImplementedException(); }
        int HasTempTrait(sGirl girl, string trait)
        { throw new NotImplementedException(); }
        bool RestoreRememberedTrait(sGirl girl, string trait)
        { throw new NotImplementedException(); }


        void ApplyTraits(sGirl girl, sTrait trait = null)
        { throw new NotImplementedException(); } // applys the stat bonuses for traits to a girl
        void MutuallyExclusiveTraits(sGirl girl, bool apply, sTrait trait = null, bool rememberflag = false)
        { throw new NotImplementedException(); }

        bool PossiblyGainNewTrait(sGirl girl, string Trait, int Threshold, int ActionType, string Message, bool Day0Night1, EventType eventtype = EventType.GOODNEWS)
        { throw new NotImplementedException(); }
        bool PossiblyLoseExistingTrait(sGirl girl, string Trait, int Threshold, int ActionType, string Message, bool Day0Night1)
        { throw new NotImplementedException(); }

        // `J` When adding new traits, search for "J-Add-New-Traits"  :  found in >> cGirls.h > AdjustTraitGroup

        // `J` adding these to allow single step adjustment of linked traits
        string AdjustTraitGroupGagReflex(sGirl girl, int steps, bool showmessage = false, bool Day0Night1 = false)
        { throw new NotImplementedException(); }
        string AdjustTraitGroupBreastSize(sGirl girl, int steps, bool showmessage = false, bool Day0Night1 = false)
        { throw new NotImplementedException(); }
        string AdjustTraitGroupFertility(sGirl girl, int steps, bool showmessage = false, bool Day0Night1 = false)
        { throw new NotImplementedException(); }

        int DrawGirl(sGirl girl, int x, int y, int width, int height, int ImgType, bool random = true, int img = 0)
        { throw new NotImplementedException(); } // draws a image of a girl

        // IHM specific function
        //CSurface GetImageSurface(sGirl girl, int ImgType, bool random, ref int img, bool gallery = false)
        //{ throw new NotImplementedException(); } // draws a image of a girl
        //cAnimatedSurface GetAnimatedSurface(sGirl girl, int ImgType, ref int img)
        //{ throw new NotImplementedException(); }

        bool IsAnimatedSurface(sGirl girl, int ImgType, ref int img)
        { throw new NotImplementedException(); }

        int GetNumSlaveGirls()
        { throw new NotImplementedException(); }
        int GetNumCatacombGirls()
        { throw new NotImplementedException(); }
        int GetNumArenaGirls()
        { throw new NotImplementedException(); }
        int GetNumYourDaughterGirls()
        { throw new NotImplementedException(); }
        int GetNumIsDaughterGirls()
        { throw new NotImplementedException(); }
        int GetSlaveGirl(int from)
        { throw new NotImplementedException(); }
        int GetRebelValue(sGirl girl, bool matron)
        { throw new NotImplementedException(); }
        int CheckEquipment(sGirl girl)
        { throw new NotImplementedException(); } // Check what combat equipment the girl has equipped
        void EquipCombat(sGirl girl)
        { throw new NotImplementedException(); } // girl makes sure best armor and weapons are equipped, ready for combat
        void UnequipCombat(sGirl girl)
        { throw new NotImplementedException(); } // girl unequips armor and weapons, ready for brothel work or other non-aggressive jobs
        bool RemoveInvByNumber(sGirl girl, int Pos)
        { throw new NotImplementedException(); }

        byte girl_fights_girl(sGirl a, sGirl b)
        { throw new NotImplementedException(); }

        bool InheritTrait(sTrait trait)
        { throw new NotImplementedException(); }

        void AddRandomGirl(sRandomGirl girl)
        { throw new NotImplementedException(); }
        /*
        *	mod - docclox
        *	same deal here: LoadRandomGirl is a wrapper
        *	The "-XML" version is the new one that loads from XML files
        */
        void LoadRandomGirl(string filename)
        { throw new NotImplementedException(); }
        void LoadRandomGirlXML(string filename)
        { throw new NotImplementedException(); }
        // end mod

        public sGirl CreateRandomGirl(int age, bool addToGGirls, bool slave = false, bool undead = false, bool Human0Monster1 = false, bool childnaped = false, bool arena = false, bool daughter = false, bool isdaughter = false, string findbyname = "")
        { throw new NotImplementedException(); }

        public sGirl GetRandomGirl(bool slave = false, bool catacomb = false, bool arena = false, bool daughter = false, bool isdaughter = false)
        { throw new NotImplementedException(); }
        sGirl GetUniqueYourDaughterGirl(int Human0Monster1 = -1)
        { throw new NotImplementedException(); } // -1 either, 0 human, 1 monster

        bool NameExists(string name)
        { throw new NotImplementedException(); }
        bool SurnameExists(string surname)
        { throw new NotImplementedException(); }
        string CreateRealName(string first, string middle = "", string last = "")
        { throw new NotImplementedException(); } // if you have first, middle and last
        bool CreateRealName(sGirl girl)
        { throw new NotImplementedException(); }
        void DivideName(sGirl girl)
        { throw new NotImplementedException(); }
        bool BuildName(sGirl girl)
        { throw new NotImplementedException(); }

        string GetHoroscopeName(int month, int day)
        { throw new NotImplementedException(); }


        public bool CheckInvSpace(sGirl girl)
        {
            if (girl.m_NumInventory == 40)
            {
                return false;
            }
            return true;
        }
        int AddInv(sGirl girl, sInventoryItem item)
        { throw new NotImplementedException(); }
        bool EquipItem(sGirl girl, int num, bool force)
        { throw new NotImplementedException(); }
        bool CanEquip(sGirl girl, int num, bool force)
        { throw new NotImplementedException(); }
        int GetWorseItem(sGirl girl, int type, int cost)
        { throw new NotImplementedException(); }
        int GetNumItemType(sGirl girl, int Type, bool splitsubtype = false)
        { throw new NotImplementedException(); }
        void SellInvItem(sGirl girl, int num)
        { throw new NotImplementedException(); }
        void UseItems(sGirl girl)
        { throw new NotImplementedException(); }
        int HasItem(sGirl girl, string name)
        { throw new NotImplementedException(); }
        int HasItemJ(sGirl girl, string name)
        { throw new NotImplementedException(); } // `J` added
        //	void RemoveTrait(sGirl* girl, string name, bool addrememberlist = false, bool force = false);
        public bool RemoveTrait(sGirl girl, string name, bool addrememberlist = false, bool force = false, bool keepinrememberlist = false)
        { throw new NotImplementedException(); }
        void RemoveRememberedTrait(sGirl girl, string name)
        { throw new NotImplementedException(); }
        void RemoveAllRememberedTraits(sGirl girl)
        { throw new NotImplementedException(); } // WD: Cleanup remembered traits on new girl creation
        int GetNumItemEquiped(sGirl girl, int Type)
        { throw new NotImplementedException(); }
        bool IsItemEquipable(sGirl girl, int num)
        { throw new NotImplementedException(); }
        bool IsInvFull(sGirl girl)
        { throw new NotImplementedException(); }

        int GetSkillWorth(sGirl girl)
        { throw new NotImplementedException(); }

        bool DisobeyCheck(sGirl girl, int action, sBrothel brothel = null)
        { throw new NotImplementedException(); }
        bool AskedOutChance(sGirl girl, int action, sBrothel brothel = null)
        { throw new NotImplementedException(); }
        bool SayYesChance(sGirl girl, int action, sBrothel brothel = null)
        { throw new NotImplementedException(); }

        string GetDetailsString(sGirl girl, bool purchace = false)
        { throw new NotImplementedException(); }
        string GetMoreDetailsString(sGirl girl, bool purchace = false)
        { throw new NotImplementedException(); }
        string GetThirdDetailsString(sGirl girl)
        { throw new NotImplementedException(); }
        string GetGirlMood(sGirl girl)
        { throw new NotImplementedException(); }

        public bool AddTrait(sGirl girl, string name, int temptime = 0, bool removeitem = false, bool inrememberlist = false)
        { throw new NotImplementedException(); }
        void AddRememberedTrait(sGirl girl, string name)
        { throw new NotImplementedException(); }
        public bool LoseVirginity(sGirl girl, bool removeitem = false, bool remember = false)
        { throw new NotImplementedException(); }
        public bool RegainVirginity(sGirl girl, int temptime = 0, bool removeitem = false, bool inrememberlist = false)
        { throw new NotImplementedException(); }
        public bool CheckVirginity(sGirl girl)
        { throw new NotImplementedException(); }

        void CalculateAskPrice(sGirl girl, bool vari)
        { throw new NotImplementedException(); }

        void AddTiredness(sGirl girl)
        { throw new NotImplementedException(); }

        public void SetAntiPreg(sGirl girl, bool useAntiPreg)
        {
            girl.m_UseAntiPreg = useAntiPreg;
        }

        void CalculateGirlType(sGirl girl)
        { throw new NotImplementedException(); } // updates a girls fetish type based on her traits and stats
        bool CheckGirlType(sGirl girl, int type)
        { throw new NotImplementedException(); }// Checks if a girl has this fetish type

        void do_abnormality(sGirl sprog, int chance)
        { throw new NotImplementedException(); }
        void HandleChild(sGirl girl, sChild child, string summary)
        { throw new NotImplementedException(); }
        void HandleChild_CheckIncest(sGirl mum, sGirl sprog, sChild child, string summary)
        { throw new NotImplementedException(); }
        bool child_is_grown(sGirl girl, sChild child, string summary, bool PlayerControlled = true)
        { throw new NotImplementedException(); }
        bool child_is_due(sGirl girl, sChild child, string summary, bool PlayerControlled = true)
        { throw new NotImplementedException(); }
        void HandleChildren(sGirl girl, string summary, bool PlayerControlled = true)
        { throw new NotImplementedException(); } // ages children and handles pregnancy
        public bool CalcPregnancy(sGirl girl, int chance, int type, int[] stats, int[] skills)
        { throw new NotImplementedException(); } // checks if a girl gets pregnant
        void CreatePregnancy(sGirl girl, int numchildren, int type, int[] stats, int[] skills)
        { throw new NotImplementedException(); } // create the actual pregnancy

        void UncontrolledPregnancies()
        { throw new NotImplementedException(); } // ages children and handles pregnancy for all girls not controlled by player

        // mod - docclox - func to return random girl N in the chain
        // returns null if n out of range
        sRandomGirl random_girl_at(int n)
        { throw new NotImplementedException(); }
        /*
        *	while I'm on, a few funcs to factor out some common code in DrawImages
        */
        int draw_with_default(sGirl girl, int x, int y, int width, int height, int ImgType, bool random, int img)
        { throw new NotImplementedException(); }
        int calc_abnormal_pc(sGirl mom, sGirl sprog, bool is_players)
        { throw new NotImplementedException(); }

        List<sGirl> get_girls(GirlPredicate pred)
        { throw new NotImplementedException(); }

        // end mod

        void updateTemp(sGirl girl)
        { throw new NotImplementedException(); } // `J` group all the temp updates into one area

        // WD:	Consolidate common code in BrothelUpdate and DungeonUpdate to fn's
        void updateGirlAge(sGirl girl, bool inc_inService = false)
        { throw new NotImplementedException(); }
        void updateTempStats(sGirl girl)
        { throw new NotImplementedException(); }
        void updateTempSkills(sGirl girl)
        { throw new NotImplementedException(); }
        void updateTempTraits(sGirl girl)
        { throw new NotImplementedException(); }
        void updateTempEnjoyment(sGirl girl)
        { throw new NotImplementedException(); }
        void updateTempTraining(sGirl girl)
        { throw new NotImplementedException(); }
        void updateTempTraits(sGirl girl, string trait, int amount)
        { throw new NotImplementedException(); }
        void updateSTD(sGirl girl)
        { throw new NotImplementedException(); }
        void updateHappyTraits(sGirl girl)
        { throw new NotImplementedException(); }
        void updateGirlTurnStats(sGirl girl)
        { throw new NotImplementedException(); }

        bool girl_has_matron(sGirl girl, int shift = 0)
        { throw new NotImplementedException(); }
        bool detect_disease_in_customer(sBrothel brothel, sGirl girl, sCustomer cust, double mod = 0.0)
        { throw new NotImplementedException(); }

        string Accommodation(int acc)
        { throw new NotImplementedException(); }
        string AccommodationDetails(sGirl girl, int acc)
        { throw new NotImplementedException(); }
        int PreferredAccom(sGirl girl)
        { throw new NotImplementedException(); }
        public string catacombs_look_for(int girls, int items, int beast)
        { throw new NotImplementedException(); }

        sCustomer GetBeast()
        { throw new NotImplementedException(); }


        private uint m_NumGirls; // number of girls in the class
        private sGirl m_Parent; // first in the list of girls who are dead, gone or in use
        private sGirl m_Last; // last in the list of girls who are dead, gone or in use

        private int m_NumRandomGirls;
        private int m_NumHumanRandomGirls;
        private int m_NumNonHumanRandomGirls;

        private int m_NumRandomYourDaughterGirls;
        private int m_NumHumanRandomYourDaughterGirls;
        private int m_NumNonHumanRandomYourDaughterGirls;

        private sRandomGirl m_RandomGirls;
        private sRandomGirl m_LastRandomGirls;

        int test_child_name(string name)
        { throw new NotImplementedException(); }

        sGirl make_girl_child(sGirl mom, bool playerisdad = false)
        { throw new NotImplementedException(); }
        sGirl find_girl_by_name(string name, ref int index_pt)
        { throw new NotImplementedException(); }
        sRandomGirl find_random_girl_by_name(string name, ref int index_pt)
        { throw new NotImplementedException(); }
    }
}
