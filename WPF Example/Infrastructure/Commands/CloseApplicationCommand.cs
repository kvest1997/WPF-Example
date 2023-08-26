using System.Windows;
using WPF_Example.Infrastructure.Commands.Base;

namespace WPF_Example.Infrastructure.Commands
{
    internal class CloseApplicationCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}