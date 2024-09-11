using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Infastructure.DbFakeData
{
    /// <summary>
    /// Класс устанвоки начальных данных в базу данных
    /// </summary>
    public class FakeData : UserFakeData
    {
        public FakeData(ManagersFactory factory, string jsonDataName) : base(factory, jsonDataName)
        {
        }

        /// <summary>
        /// Устанавливает начальные данные в базу данных.
        /// </summary>
        public override void InstallData()
        {
            base.InstallData(); 
        }
    }
}
