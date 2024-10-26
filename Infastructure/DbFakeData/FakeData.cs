namespace Library.Business.Infastructure.DbFakeData
{
    /// <summary>
    /// Класс установки начальных данных в базу данных
    /// </summary>
    public class FakeData : UserFakeData
    {
        public FakeData(ManagersFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Устанавливает начальные данные в базу данных.
        /// </summary>
        public override bool InstallData()
        {
            return base.InstallData();
        }
    }
}
