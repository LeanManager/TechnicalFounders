using System;

namespace TechnicalFounders.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public byte[] CipherText { get; set; }

        // int -> string in DB
        public ItemCategory Category { get; set; }

        public override string ToString()
        {
            //return base.ToString();
            return $"{Text} - {Description}";
        }
    }

    public enum ItemCategory
    {
        Shopping,
        Work,
        Private
    }
}