﻿/*
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using System.Collections.Generic;
    using WMaster.ClassOrStructurToImplement;

    /*
     * manages the studio
     *
     * extend cBrothelManager
     */
    public class cMovieStudioManager : cBrothelManager, System.IDisposable
    {
        public cMovieStudioManager()
        { throw new NotImplementedException(); } // constructor
        public void Dispose()
        { throw new NotImplementedException(); } // destructor

        public int m_MovieRunTime; // see above, counter for the 7 week effect
        public int m_NumMovies;
        public int RunWeeks;
        public sMovie m_Movies; // the movies currently selling
        public sMovie m_LastMovies;
        public sFilm m_CurrFilm;
        // added the following so movie crew could effect quality of each scene. --PP
        public int m_FlufferQuality; // Bonus to film quality based on performance of Fluffers this shift.
        public int m_CameraQuality; // Bonus to film quality based on performance of Cameramages this shift.
        public int m_PurifierQaulity; // Bonus to film quality based on performance of CrystalPurifiers this shift.
        public int m_DirectorQuality; // Bonus to film quality based on performance of  the Director this shift.
        public string m_DirectorName; // The Director's name.
        public int m_StagehandQuality; // Bonus to film quality based on performance of stagehands this shift.
        public double m_PromoterBonus; // Bonus added directly to film sales by promoter.

        void StartMovie(int brothelID, int Time)
        { throw new NotImplementedException(); }
        int GetTimeToMovie(int brothelID)
        { throw new NotImplementedException(); }
        void NewMovie(sMovieStudio brothel, int Init_Quality, int Quality, int Promo_Quality, int Money_Made, int RunWeeks)
        { throw new NotImplementedException(); }
        void EndMovie(sBrothel brothel)
        { throw new NotImplementedException(); }
        bool CheckMovieGirls(sBrothel brothel)
        { throw new NotImplementedException(); } // checks if any girls are working on the movie
        int calc_movie_quality()
        { throw new NotImplementedException(); }
        void ReleaseCurrentMovie()
        { throw new NotImplementedException(); }

        sMovieScene GetScene(int num)
        { throw new NotImplementedException(); }
        int GetNumScenes()
        { throw new NotImplementedException(); }
        public List<sMovieScene> m_movieScenes = new List<sMovieScene>();
        int AddScene(sGirl girl, int Job, int Bonus = 0)
        { throw new NotImplementedException(); } // Added job parameter so different types of sex could effect film quality. --PP
        void AddGirl(int brothelID, sGirl girl)
        { throw new NotImplementedException(); }
        void RemoveGirl(int brothelID, sGirl girl, bool deleteGirl = false)
        { throw new NotImplementedException(); }
        void UpdateMovieStudio()
        { throw new NotImplementedException(); }
        void UpdateGirls(sBrothel brothel)
        { throw new NotImplementedException(); }
        //void	AddBrothel(sBrothel* newBroth);
        IXmlElement SaveDataXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadDataXML(IXmlHandle hBrothelManager)
        { throw new NotImplementedException(); }
        void Free()
        { throw new NotImplementedException(); }
        public int m_NumMovieStudios;
        public cJobManager m_JobManager = new cJobManager();

        int Num_Actress(int brothel)
        { throw new NotImplementedException(); }
        bool is_Actress_Job(int testjob)
        { throw new NotImplementedException(); }
        bool CrewNeeded()
        { throw new NotImplementedException(); }
    }

}