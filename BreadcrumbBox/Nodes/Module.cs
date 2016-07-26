namespace BreadcrumbBox
{
    using System.Collections.Generic;

    public class Module : INode
    {
        public Module(string name, IEnumerable<INode> children)
        {
            this.Name = name;
            this.Children = children;
        }

        public string Name { get; }

        public IEnumerable<INode> Children { get; }

        public override string ToString() => $"Module: {this.Name}";
    }
}
