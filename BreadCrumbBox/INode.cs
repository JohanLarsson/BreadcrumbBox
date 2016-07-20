namespace BreadCrumbBox
{
    using System.Collections.Generic;
    using System.ComponentModel;

    public interface INode : INotifyPropertyChanged
    {
        string Name { get; }

        bool IsSelected { get; }

        IEnumerable<INode> Children { get; }
    }
}