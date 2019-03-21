using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class ArticleComparator : IComparer<Article>
{
    public int Compare(Article x, Article y)
    {
        return DateTime.Compare(y.Date, x.Date);
    }
}
