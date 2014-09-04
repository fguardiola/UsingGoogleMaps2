using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsingGoogleMaps2.Models
{
    public class SearchResults
    {
        private List<SearchResult> resulsts=new List<SearchResult>();

        public void AddResult(SearchResult result)
        {
            resulsts.Add(result);
        }

        public List<SearchResult> GetSearchListSearchResult { get { return resulsts; } }
    }
}