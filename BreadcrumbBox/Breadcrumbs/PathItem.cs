namespace BreadcrumbBox
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using JetBrains.Annotations;

    public class PathItem : INotifyPropertyChanged
    {
        private INode node;
        private INode selectedChildNode;

        public PathItem(INode node)
        {
            this.Node = node;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public INode Node
        {
            get
            {
                return this.node;
            }
            internal set
            {
                if (Equals(value, this.node))
                {
                    return;
                }

                this.node = value;
                this.OnPropertyChanged();
            }
        }

        public INode SelectedChildNode
        {
            get
            {
                return this.selectedChildNode;
            }
            set
            {
                if (Equals(value, this.selectedChildNode))
                {
                    return;
                }
                if (value != null)
                {
                    CurrentSelection.Instance.SelectedNode = value;
                }

                this.selectedChildNode = value;
                this.OnPropertyChanged();
            }
        }

        internal void WithNextItem(PathItem pathItem)
        {
            if(Equals(this.selectedChildNode, pathItem?.node))
            {
                return;
            }

            this.selectedChildNode = pathItem?.node;
            this.OnPropertyChanged(nameof(this.SelectedChildNode));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}