using System;

namespace Microsoft.Advocacy
{
    public class RepoItem
    {
        public string repo { get; set; }
        public string group { get; set; }
        public string owner { get; set; }
        public DateTime date { get; set; }
    }

    public class CloneItem : RepoItem
    {
        public int clones { get; set; }
    }

    public class ViewItem : RepoItem
    {
        public int views { get; set; }
    }
}