namespace BreadcrumbBox
{
    using System.Collections.Generic;
    using System.Linq;

    public class Method : INode
    {
        public Method(string name)
        {
            this.Name = name;
        }

        public string Name { get;  }

        IEnumerable<INode> INode.Children => Enumerable.Empty<INode>();

        public override string ToString() => $"Method: {this.Name}";
    }
}