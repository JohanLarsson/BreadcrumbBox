namespace BreadCrumbBox
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using BreadCrumbBox.Annotations;

    public class Project : INode, INotifyPropertyChanged
    {
        private IEnumerable<INode> selectedPath;
        private bool isSelected = true;

        public Project(IEnumerable<INode> children)
        {
            this.Children = children;
            this.Path = new SelectedPath(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static Project PrePopulated { get; } = CreatePrePopulated();

        public IEnumerable<INode> SelectedPath
        {
            get { return this.selectedPath; }
            private set
            {
                if (Equals(value, this.selectedPath)) return;
                this.selectedPath = value;
                this.OnPropertyChanged();
            }
        }

        public string Name { get; } = "Project";

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

        public IEnumerable<INode> Children { get; }

        public SelectedPath Path { get; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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