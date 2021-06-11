namespace Hotel_JustFriend.Models
{
    public class DataProvider
    {
        private static DataProvider _Instance;

        public static DataProvider Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new DataProvider();
                return _Instance;
            }
            set { _Instance = value; }
        }
        public Hotel_JustFriend1Entities DB { get; set; }

        private DataProvider()
        {
            DB = new Hotel_JustFriend1Entities();
        }
    }
}
