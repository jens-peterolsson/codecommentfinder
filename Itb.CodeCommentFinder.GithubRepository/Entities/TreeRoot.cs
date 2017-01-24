using System.Collections.Generic;

namespace Itb.CodeCommentFinder.GithubRepository.Entities
{

    public class TreeRoot
    {
        public string sha { get; set; }
        public string url { get; set; }
        public List<TreeNode> tree { get; set; }
        public bool truncated { get; set; }
    }
}