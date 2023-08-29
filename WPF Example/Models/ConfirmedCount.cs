using System;

namespace WPF_Example.Models
{
    /// <summary>
    /// Время и кол-во заражений
    /// </summary>
    internal struct ConfirmedCount
    {
        public DateTime Date { get; set; }

        public int Count { get; set; }
    }
}
