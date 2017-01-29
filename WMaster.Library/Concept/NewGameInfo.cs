namespace WMaster.Concept
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Provide basic new game information like player name, his date of birth or brothel name.
    /// </summary>
    public struct NewGameInfo
    {
        /// <summary>
        /// The player firstname.
        /// </summary>
        private string m_PlayerFirstname;
        /// <summary>
        /// Get or set the player firstname.
        /// </summary>
        public string PlayerFirstname
        {
            get { return this.m_PlayerFirstname; }
            set
            {
                this.m_PlayerFirstname = value ?? string.Empty;
                this.m_PlayerFirstname = this.m_PlayerFirstname.Trim();
            }
        }

        /// <summary>
        /// The player lastname.
        /// </summary>
        private string m_PlayerLastname;
        /// <summary>
        /// Get or set the player lastname.
        /// </summary>
        public string PlayerLastname
        {
            get { return this.m_PlayerLastname; }
            set
            {
                this.m_PlayerLastname = value ?? string.Empty;
                this.m_PlayerLastname = this.m_PlayerLastname.Trim();
            }
        }

        /// <summary>
        /// The player's date of birth.
        /// </summary>
        private DateTime m_PlayerBirth;
        /// <summary>
        /// Get or set the player's date of birth.
        /// </summary>
        public DateTime PlayerBirth
        {
            get { return m_PlayerBirth; }
            set
            {
                if (this.m_PlayerBirth != value)
                {
                    this.m_PlayerBirth = value;
                }
            }
        }

        // TODO : Calc astral sign from date of birth

        /// <summary>
        /// The name of the first brothel of player.
        /// </summary>
        private string m_BrothelName;
        /// <summary>
        /// Get or set the name of the first brothel of player.
        /// </summary>
        public string BrothelName
        {
            get { return this.m_BrothelName; }
            set
            {
                this.m_BrothelName = value ?? string.Empty;
                this.m_BrothelName = this.m_BrothelName.Trim();
            }
        }
    }
}
