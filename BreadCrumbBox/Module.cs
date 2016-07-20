namespace BreadCrumbBox
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using BreadCrumbBox.Annotations;

    public class Module : INode
    {
        private bool isSelected;

        public Module(string name, IEnumerable<INode> children)
        {
            this.Name = name;
            this.Children = children;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; }

        public IEnumerable<INode> Children { get; }

        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (value == this.isSelected) return;
                this.isSelected = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
