using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.ConfigCenter.Model
{
    public class PagerInBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int Skip
        {
            get { return (PageIndex - 1) * PageSize; }
        }

        public PagerInBase()
        {
            if (PageIndex == 0) PageIndex = 1;
            if (PageSize == 0) PageSize = 15;
        }
    }
}
