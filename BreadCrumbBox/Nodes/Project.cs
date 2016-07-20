namespace BreadcrumbBox
{
    using System.Collections.Generic;

    public class Project : INode
    {
        public Project(IEnumerable<INode> children)
        {
            this.Children = children;
            this.Path = new BreadcrumbPath(this);
        }

        public static Project PrePopulated { get; } = CreatePrePopulated();

        public string Name { get; } = "Project";

        public IEnumerable<INode> Children { get; }

        public BreadcrumbPath Path { get; }

        public override string ToString() => $"Project: {this.Name}";

        private static Project CreatePrePopulated()
        {
            var modules = new[]
            {
                new Module("Module1", new[] {new Method("Method1.1"), new Method("Method1.2") }),
                new Module("Module2", new[] {new Method("Method2.1"), new Method("Method2.2") })
            };

            return new Project(modules);
        }
    }
}