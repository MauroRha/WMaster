using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WMaster.DAL
{
    public class Gold
    {
        [XmlAttribute("value")]
        public string Value
        {
            get { return Game.Gold.Value.ToString("0.000000"); }
            set
            {
                double gold = Game.Gold.Value;
                double.TryParse(value, out gold);
                Game.Gold.Value = gold;
            }
        }

        [XmlAttribute("income")]
        public string Income
        {
            get { return Game.Gold.Income.ToString("0.000000"); }
            set
            {
                double gold = Game.Gold.Income;
                double.TryParse(value, out gold);
                Game.Gold.Income = gold;
            }
        }

        [XmlAttribute("upkeep")]
        public string Upkeep
        {
            get { return Game.Gold.Upkeep.ToString("0.000000"); }
            set
            {
                double gold = Game.Gold.Upkeep;
                double.TryParse(value, out gold);
                Game.Gold.Upkeep = gold;
            }
        }

        [XmlAttribute("cash_in")]
        public string CashIn
        {
            get { return Game.Gold.CashIn.ToString("0.000000"); }
            set
            {
                double gold = Game.Gold.CashIn;
                double.TryParse(value, out gold);
                Game.Gold.CashIn = gold;
            }
        }

        [XmlAttribute("cash_out")]
        public string CashOut
        {
            get { return Game.Gold.CashOut.ToString("0.000000"); }
            set
            {
                double gold = Game.Gold.CashOut;
                double.TryParse(value, out gold);
                Game.Gold.CashOut = gold;
            }
        }
    }
}
