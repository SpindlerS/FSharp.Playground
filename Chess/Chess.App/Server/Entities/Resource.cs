﻿using System.ComponentModel.DataAnnotations;

namespace Chess.App.Server.Entities
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
