/*
 * Establishes an object that can be used to generate the table
 * of the db
 * */
using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MobileApp
{
    [Table("Hairstyling")]
    class Appointment
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [Column("day")]
        public String Day { get; set; }
        [Column("time")]
        public String Time { get; set; }
        [Column("service")]
        public String Service { get; set; }
        [Column("isConfirmed")]
        public Boolean IsConfirmed { get; set; }


    }
}
