﻿using System;

namespace Rest_API.Models.DTOs
{
    public class AddLentDebt
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime DebtStartDate { get; set; }
        public int BorrowerId { get; set; }
        public bool IsBorrowerLocal { get; set; }
        public bool IsPayed { get; set; }
    }
}
