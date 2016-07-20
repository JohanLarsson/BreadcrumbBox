namespace BreadCrumbBox
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class BreadCrumbPath : ReadOnlyObservableCollection<PathItem>
    {
        private readonly ObservableCollection<PathItem> path;
        private readonly PathItem root;

        public BreadCrumbPath(INode root)
            : this(root, new ObservableCollection<PathItem>())
        {
        }

        private BreadCrumbPath(INode root, ObservableCollection<PathItem> path)
            : base(path)
        {
            this.path = path;
            this.root = new PathItem(root);
            CurrentSelection.Instance.SelectedNodeChanged += this.OnSelectedNodeChanged;
            this.OnSelectedNodeChanged(null, CurrentSelection.Instance.SelectedNode);
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

        private static List<INode> GetSelectionPath(INode node, List<INode> path)
        {
            if (path == null)
            {
                path = new List<INode>();
            }

            path.Add(node);

            var selectedChild = node.Children.FirstOrDefault(c => c == CurrentSelection.Instance.SelectedNode);
            if (selectedChild != null)
            {
                path.Add(selectedChild);
                return path;
            }

            foreach (var child in node.Children)
            {
                if (Flatten(child).Any(c => c == CurrentSelection.Instance.SelectedNode))
                {
                    return GetSelectionPath(child, path);
                }
            }

            return path;
        }

        private void OnSelectedNodeChanged(object sender, INode selectedNode)
        {
            var newPath = CurrentSelection.Instance.SelectedNode == null
                              ? new List<INode>(1) { this.root.Node }
                              : GetSelectionPath(this.root.Node, null);

            for (var i = this.path.Count - 1; i >= newPath.Count; i--)
            {
                this.path.RemoveAt(i);
            }

            for (var i = 0; i < newPath.Count; i++)
            {
                if (this.Count <= i)
                {
                    this.path.Add(new PathItem(newPath[i]));
                }
                else if (!ReferenceEquals(this.path[i].Node, newPath[i]))
                {
                    this.path[i].Node = newPath[i];
                }
            }

            for (var i = 0; i < this.path.Count; i++)
            {
                this.path[i].WithNextItem(this.path.ElementAtOrDefault(i + 1));
            }
        }
    }
}
