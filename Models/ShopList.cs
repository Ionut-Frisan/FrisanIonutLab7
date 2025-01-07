using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FrisanIonutLab7.Models
{
    public class ShopList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(250), Unique]
        public string Description { get; set; }

        public DateTime Date {  get; set; }

        [ForeignKey(typeof(Shop))]
        public int ShopID { get; set; }
    }
}
