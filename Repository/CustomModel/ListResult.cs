﻿
using System.Collections.Generic;

namespace Repository.CustomModel
{
    public class ListResult<T>
    {
        public List<T> items { get; set; }
        public long total { get; set; }

        public ListResult()
        {
            items = null;
            total = 0;
        }
        public ListResult(List<T> items, long totalItems)
        {
            this.items = items;
            this.total = totalItems;
        }
    }
}
