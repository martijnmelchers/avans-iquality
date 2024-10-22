﻿namespace IQuality.Models.Authentication
{
    public class FullName
    {
        public string First { get; set; }
        public string Infix { get; set; }
        public string Last { get; set; }

        public override string ToString()
        {
            return $"{First} {Infix} {Last}";
        }
    }
}