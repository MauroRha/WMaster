﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WMaster.Enum;

namespace WMaster.GameConcept
{
    /// <summary>
    /// Skill value of en entity (player, girl, gang, rival...)
    /// <remarks><para>Class used to fix EntityAttribute&lt;Skills&gt;</para></remarks>
    /// </summary>
    public class EntitySkill : WMaster.GameConcept.AbstractConcept.EntityAttribute<EnumSkills>
    {
    }
}