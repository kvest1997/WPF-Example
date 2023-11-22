using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WPF_Example.Infrastructure.Commands;
using WPF_Example.Models;
using WPF_Example.Models.Decanat;
using WPF_Example.ViewModels.Base;
using Group = WPF_Example.Models.Decanat.Group;

namespace WPF_Example.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public ObservableCollection<Group> Groups { get; }

        #region SelectedGroup : Group - Выбранная группа

        private Group _SelectedGroup;
        public Group SelectedGroup { get => _SelectedGroup; set => Set(ref _SelectedGroup, value); }

        #endregion

        #region SelectedPageIndex : int - Номер выбранной вкладки
        /// <summary>
        /// Индекс страницы
        /// </summary>
        private int _SelectedPageIndex;

        public int SelectedPageIndex
        {
            get => _SelectedPageIndex;
            set => Set(ref _SelectedPageIndex, value);
        }
        #endregion

        #region TestDataPoint : IEnumerable<DataPoint> - Тестовый набор данных для визуализации графиков
        /// <summary>
        /// Тестовый набор данных для визуализации графика
        /// </summary>
        private IEnumerable<DataPoint> _TestDataPoints;

        public IEnumerable<DataPoint> TestDataPoints { get => _TestDataPoints; set => Set(ref _TestDataPoints, value); }
        #endregion

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

        #region ChangeTabIndexCommand
        public ICommand ChangeTabIndexCommand { get; }
        private bool CanChangeTabIndexCommandExecute(object p) => _SelectedPageIndex >= 0;
        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null) return;

            SelectedPageIndex += Convert.ToInt32(p);
        }
        #endregion

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

            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted,
                CanChangeTabIndexCommandExecute);

            #endregion

            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x+=0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y }); 
            }
            TestDataPoints = data_points;

            var student_index = 1;
            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name = $"Name {student_index}",
                Surname = $"Surname {student_index}",
                Patronymic = $"Patronymic {student_index++}",
                Birthday = DateTime.Now,
                Rating = 0
            });

            var groups = new ObservableCollection<Group>(Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(students)
            }));
            Groups = new ObservableCollection<Group>(groups);

        }
    }
}
