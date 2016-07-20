namespace BreadCrumbBox
{
    using System.Collections.Generic;

    public interface INode
    {
        string Name { get; }

        IEnumerable<INode> Children { get; }
    }
}