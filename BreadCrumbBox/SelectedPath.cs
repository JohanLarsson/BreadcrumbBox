namespace BreadCrumbBox
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using BreadCrumbBox.Annotations;

    public class SelectedPath : INotifyPropertyChanged
    {
        private readonly Project project;
        private IEnumerable<INode> currentPath;

        public SelectedPath(Project project)
        {
            this.project = project;
            this.currentPath = new[] { project };
            foreach (var node in Flatten(project))
            {
                node.PropertyChanged += this.OnNodePropertyChanged;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<INode> CurrentPath
        {
            get { return this.currentPath; }
            private set
            {
                if (Equals(value, this.currentPath)) return;
                this.currentPath = value;
                this.OnPropertyChanged();
            }
        }

        private static IEnumerable<INode> Flatten(INode node)
        {
            foreach (var child in node.Children)
            {
                yield return child;
                foreach (var nested in Flatten(child))
                {
                    yield return nested;
                }
            }
        }

        private static List<INode> GetSelectedPath(INode node, List<INode> path)
        {
            var selectedChild = node.Children.FirstOrDefault(c => c.IsSelected);
            if (selectedChild != null)
            {
                path.Add(selectedChild);
                return path;
            }

            foreach (var child in node.Children)
            {
                var withSelectedChild = Flatten(child).FirstOrDefault(c => c.IsSelected);
                if (withSelectedChild != null)
                {
                    path.Add(withSelectedChild);
                    return GetSelectedPath(withSelectedChild, path);
                }
            }

            return path;
        }

        private void OnNodePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var path = new List<INode> { this.project };
            this.CurrentPath = GetSelectedPath(this.project, path);
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
