using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.PageManager
{
    public class CalculatePages
    {
        public static int myCalculatePages(int totalSize, int pageSize)
        {
            int pages = totalSize / pageSize;

            if (totalSize % pageSize != 0)
                pages += 1;

            return pages;
        }
    }
}