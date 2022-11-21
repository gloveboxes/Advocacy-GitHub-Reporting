using System;

namespace Microsoft.Advocacy
{
    public class RepoItem
    {
        public UInt64 repo_id { get; set; }
        public string repo { get; set; }
        public string group { get; set; }
        public string owner { get; set; }
        public DateTime date { get; set; }
    }

    public class CloneItem : RepoItem
    {
        public int clones { get; set; }
        public int stars { get; set; }
    }

    public class ViewItem : RepoItem
    {
        public int views { get; set; }
    }
}