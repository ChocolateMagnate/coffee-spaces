namespace Records {
    public class Rank {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Permissions { get; set; }
        public Rank(string title, string description, List<string> permissions) {
            Title = title;
            Description = description;
            Permissions = permissions;
        }
    }
}