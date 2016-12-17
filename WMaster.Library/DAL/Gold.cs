using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WMaster.GameManager;

namespace WMaster.DAL
{
    public class Gold
    {
        [XmlAttribute("value")]
        public string Value
        {
            get { return GameEngine.Game.Gold.Value.ToString("0.000000"); }
            set
            {
                double gold = GameEngine.Game.Gold.Value;
                double.TryParse(value, out gold);
                GameEngine.Game.Gold.Value = gold;
            }
        }

        [XmlAttribute("income")]
        public string Income
        {
            get { return GameEngine.Game.Gold.Income.ToString("0.000000"); }
            set
            {
                double gold = GameEngine.Game.Gold.Income;
                double.TryParse(value, out gold);
                GameEngine.Game.Gold.Income = gold;
            }
        }

        [XmlAttribute("upkeep")]
        public string Upkeep
        {
            get { return GameEngine.Game.Gold.Upkeep.ToString("0.000000"); }
            set
            {
                double gold = GameEngine.Game.Gold.Upkeep;
                double.TryParse(value, out gold);
                GameEngine.Game.Gold.Upkeep = gold;
            }
        }

        [XmlAttribute("cash_in")]
        public string CashIn
        {
            get { return GameEngine.Game.Gold.CashIn.ToString("0.000000"); }
            set
            {
                double gold = GameEngine.Game.Gold.CashIn;
                double.TryParse(value, out gold);
                GameEngine.Game.Gold.CashIn = gold;
            }
        }

        [XmlAttribute("cash_out")]
        public string CashOut
        {
            get { return GameEngine.Game.Gold.CashOut.ToString("0.000000"); }
            set
            {
                double gold = GameEngine.Game.Gold.CashOut;
                double.TryParse(value, out gold);
                GameEngine.Game.Gold.CashOut = gold;
            }
        }
    }
}
