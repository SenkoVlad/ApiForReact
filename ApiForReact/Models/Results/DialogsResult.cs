using System.Collections.Generic;

namespace ApiForReact.Models.Results
{
    public class DialogsResult
    {
        public IEnumerable<Dialog> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
