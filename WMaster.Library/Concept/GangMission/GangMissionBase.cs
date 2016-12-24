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
//  <copyright file="GangMissions.cs" company="The Pink Petal Devloment Team">
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
using WMaster.Entity.Living;
namespace WMaster.Concept.GangMission
{
    /// <summary>
    /// Abstract base class of gang mission. Provide Concret factory to Concret gang mission class with auto association with target gang.
    /// </summary>
    public abstract class GangMissionBase
    {
        /// <summary>
        /// Get a no mission to affect to gang.
        /// </summary>
        public static GangMissionBase None
        {
            get { return new GangMissionNone(null); }
        }

        /// <summary>
        /// Static factory for concret <see cref="GangMissionBase"/>. <see cref="GangMissionBase"/> is automaticly affected to <see cref="Gang"/>.
        /// </summary>
        /// <param name="mission"><see cref="EnuGangMissions"/> to affect to <see cref="Gang>"/>.</param>
        /// <param name="gang"><see cref="Gang"/> to affect to <see cref="EnuGangMissions"/>.</param>
        /// <returns>True if GangMission was correctly associate to gang.</returns>
        public static bool SetGangMission(EnuGangMissions mission, Gang gang)
        {
            if (gang == null)
            { return false; }

            GangMissionBase gangMission;
            switch (mission)
            {
                case EnuGangMissions.Guarding:
                    // TODO : Implementing GangMissionGuarding function
                    gangMission = new GangMissionGuarding(gang);
                    break;
                case EnuGangMissions.Sabotage:
                    gangMission = new GangMissionSabotage(gang);
                    break;
                case EnuGangMissions.SpyGirls:
                    // TODO : Implementing GangMissionSpyGirls function
                    gangMission = new GangMissionSpyGirls(gang);
                    break;
                case EnuGangMissions.RecaptureGirls:
                    gangMission = new GangMissionRecaptureGirls(gang);
                    break;
                case EnuGangMissions.Extortion:
                    gangMission = new GangMissionExtortion(gang);
                    break;
                case EnuGangMissions.PettyTheft:
                    gangMission = new GangMissionPettyTheft(gang);
                    break;
                case EnuGangMissions.GrandTheft:
                    gangMission = new GangMissionGrandTheft(gang);
                    break;
                case EnuGangMissions.KidnappGirls:
                    gangMission = new GangMissionKidnappGirls(gang);
                    break;
                case EnuGangMissions.Catacombs:
                    gangMission = new GangMissionCatacombs(gang);
                    break;
                case EnuGangMissions.Training:
                    gangMission = new GangMissionTraining(gang);
                    break;
                case EnuGangMissions.Recruit:
                    gangMission = new GangMissionRecruit(gang);
                    break;
                case EnuGangMissions.Service:
                    gangMission = new GangMissionService(gang);
                    break;
                case EnuGangMissions.Dungeon:
                    gangMission = new GangMissionDungeon(gang);
                    break;
                case EnuGangMissions.None:
                    gangMission = new GangMissionNone(gang);
                    break;
                default:
                    gangMission = new GangMissionNone(gang);
                    WMLog.Trace(string.Format("GangMissionBase factory wasn't configure to creat GangMissions.{0} instance!", mission), WMLog.TraceLog.ERROR);
                    break;
            }
            if (gangMission == null)
            { return false; }

            gang.CurrentMission = gangMission;
            return true;
        }

        #region Fields and Properties
        /// <summary>
        /// <see cref="Gang"/> performing mission.
        /// </summary>
        private Gang m_GangCible;
        /// <summary>
        /// Get the <see cref="Gang"/> performing this mission.
        /// </summary>
        protected Gang GangCible
        {
            get { return this.m_GangCible; }
        }

        /// <summary>
        /// Type of mission performing
        /// </summary>
        private EnuGangMissions m_mission;
        /// <summary>
        /// Get the <see cref="EnuGangMissions"/> of this instance.
        /// </summary>
        public EnuGangMissions Mission
        {
            get { return this.m_mission; }
        }
        #endregion

        #region CTor
        /// <summary>
        /// Initialise base instant=ce of gang mission.
        /// </summary>
        /// <param name="mission">Mission type to perform.</param>
        /// <param name="gang"><see cref="Gang"/> who perform the mission.</param>
        protected GangMissionBase(EnuGangMissions mission, Gang gang)
        {
            this.m_GangCible = gang;
            this.m_mission = mission;
        }
        #endregion

        /// <summary>
        /// Performinig mission.
        /// <remarks><para>Executing precondition mission, then mission if precondition is ok, and finaly postcondition if mission is ok.</para></remarks>
        /// </summary>
        /// <returns><b>True</b> if mission is a success.</returns>
        public bool DoTheJob()
        {
            if (BeforePerformingMission())
            {
                bool result = PerformingMission();
                result &= AfterPerformingMission(result);

                return result;
            }
            return false;
        }

        /// <summary>
        /// Executing before performing mission. Must be override in SubClass as needed : For example to check préconditions, initialise variable...
        /// <remarks>
        ///     <para><b>True</b> if PerformingMission can be execute. <b>False</b> stop DoTheJob() funtion returning <b>False</b>.</para>
        ///     <para>It's BeforePerformingMission responsibility to log and set/send error message.</para>
        /// </remarks>
        /// </summary>
        /// <returns><b>True</b> if PerformingMission can be execute. <b>False</b> stop DoTheJob() funtion returning <b>False</b>.</returns>
        protected virtual bool BeforePerformingMission()
        { return true; }

        /// <summary>
        /// Executing the mission. Must be override by SubClass.
        /// </summary>
        /// <returns><b>True</b> if the mission is a success. <b>False</b> implie DoTheJob() funtion returning <b>False</b>.</returns>
        protected virtual bool PerformingMission()
        { return true; }

        /// <summary>
        /// Executing after performing mission. Must be oberride in SubClass as needed : For example to restore variable, appli modifier...
        /// <remarks>
        ///     <para>Was alway executed after mission, even if mission return false. So, can do somthing if mission fail. Result mission is pass on parameter.</para>
        /// </remarks>
        /// </summary>
        /// <param name="missionResult">Result of performing mission.</param>
        /// <returns><b>True</b> id traitement was donne without error.</returns>
        protected virtual bool AfterPerformingMission(bool missionResult)
        { return true; }

        /// <summary>
        /// Return localized mission name.
        /// <remarks><para>Will be use like "GangName is [...]."</para></remarks>
        /// </summary>
        /// <returns>The name of mission.</returns>
        public abstract string GetMissionName();
    }
}
