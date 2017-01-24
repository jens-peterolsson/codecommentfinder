namespace Itb.CodeCommentFinder.GithubRepository.Entities
{
    public class Blob
    {
        public string sha { get; set; }
        public int size { get; set; }
        public string url { get; set; }
        public string content { get; set; }
        public string encoding { get; set; }
    }
}
