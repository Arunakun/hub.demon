namespace hub.demon
{
    internal class MenuItem
    {
        public string Name { get; }
        public Action? Execute { get; }
        public List<MenuItem>? Children { get; }

        public bool IsLeaf => Execute != null;

        // Constructor for executable items
        public MenuItem(string name, Action execute)
        {
            Name = name;
            Execute = execute;
        }

        // Constructor for submenus
        public MenuItem(string name, List<MenuItem> children)
        {
            Name = name;
            Children = children;
        }
    }
}