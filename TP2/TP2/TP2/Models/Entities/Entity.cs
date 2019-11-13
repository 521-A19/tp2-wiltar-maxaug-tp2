using SQLite;

namespace TP2.Models.Entities
{
    public class Entity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}