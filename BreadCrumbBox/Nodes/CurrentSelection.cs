namespace BreadCrumbBox
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using JetBrains.Annotations;

    public class CurrentSelection : INotifyPropertyChanged
    {
        public static readonly CurrentSelection Instance = new CurrentSelection();

        private INode selectedNode;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<INode> SelectedNodeChanged;

        public INode SelectedNode
        {
            get
            {
                return this.selectedNode;
            }
            set
            {
                if (Equals(value, this.selectedNode))
                {
                    return;
                }

                this.selectedNode = value;
                this.OnSelectedNodeChanged(this.selectedNode);
                this.OnPropertyChanged();
            }
        }

        protected virtual void OnSelectedNodeChanged(INode e)
        {
            this.SelectedNodeChanged?.Invoke(this, e);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
