using System;
using System.Windows.Input;

namespace WPF_Example.Infrastructure.Commands.Base
{
    internal abstract class Command : ICommand
    {
        /// <summary>
        /// Событие для команды
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Метод отвечающий за возможность выполнении команды
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public abstract bool CanExecute(object parameter);

        /// <summary>
        /// То что должно быть выполнено командой, логика
        /// </summary>
        /// <param name="parameter"></param>
        /// <exception cref="NotImplementedException"></exception>
        public abstract void Execute(object parameter);
    }
}
