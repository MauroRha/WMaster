using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public interface cAbstractGirls
    {
        int GetStat(sGirl girl, int stat);
        int GetSkill(sGirl girl, int skill);
        int GetEnjoyment(sGirl girl, int skill);
        int GetTraining(sGirl girl, int skill);
        void UpdateStat(sGirl girl, int stat, int amount, bool usetraits = true);
        void UpdateSkill(sGirl girl, int skill, int amount);
        void UpdateEnjoyment(sGirl girl, int skill, int amount);
        void UpdateTraining(sGirl girl, int skill, int amount);
        bool CalcPregnancy(sGirl girl, int chance, int type, int[] stats, int[] skills);
        bool AddTrait(sGirl girl, string name, int temptime = 0, bool removeitem = false, bool remember = false);
        bool RemoveTrait(sGirl girl, string name, bool removeitem = false, bool remember = false, bool keepinrememberlist = false);
        bool HasTrait(sGirl girl, string name);
        bool LoseVirginity(sGirl girl, bool removeitem = false, bool remember = false);
        bool RegainVirginity(sGirl girl, int temptime = 0, bool removeitem = false, bool remember = false);
        bool CheckVirginity(sGirl girl);
        void UpdateSkillTemp(sGirl girl, int skill, int amount); // updates a skill temporarily
        void UpdateStatTemp(sGirl girl, int stat, int amount);
        void UpdateEnjoymentTemp(sGirl girl, int stat, int amount);
        void UpdateTrainingTemp(sGirl girl, int stat, int amount);
    }

}
