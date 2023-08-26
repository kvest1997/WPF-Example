using System.Windows;
using System.Windows.Input;
using WPF_Example.Infrastructure.Commands;
using WPF_Example.ViewModels.Base;

namespace WPF_Example.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Заголовок окна
        /// <summary>Заголовок окна</summary>
        private string _Title = "Анализ статистики";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Status : string - Статус программы

        /// <summary>Статус программы</summary>
        private string _Status = "Готово!";

        /// <summary>Статус программы</summary>
        public string Status 
        { 
            get => _Status; 
            set => Set(ref _Status, value); 
        }

        #endregion

        //Эфективнее чем создание класса, меньше тратится ресурсов
        #region Commands

        #region CloseApplicationCommand
        /// <summary>
        /// Комманда
        /// </summary>
        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;

        /// <summary>
        /// Метод выполняющийся в момент выполнения комманды
        /// </summary>
        /// <param name="p"></param>
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
            }
            #endregion

            #endregion

        public MainWindowViewModel()
        {
            #region Commands

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted,
                CanCloseApplicationCommandExecute);

            #endregion
        }
    }
}
