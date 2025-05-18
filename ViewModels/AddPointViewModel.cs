using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GeometricShapesApp.Models;
using Avalonia.Controls;
using System;

namespace GeometricShapesApp.ViewModels
{
    // ViewModel для окна добавления точки
    public partial class AddPointViewModel : ObservableObject
    {
        // Ссылка на главную ViewModel приложения
        private readonly MainWindowViewModel _mainVm;

        // Ссылка на окно, которое будет закрываться после добавления точки
        private Window? _window;

        // Свойство X координаты точки с начальным значением 100
        // Автоматически генерирует уведомления об изменении для UI
        [ObservableProperty]
        private double _x = 100;

        // Свойство Y координаты точки с начальным значением 100
        [ObservableProperty]
        private double _y = 100;

        // Свойство имени точки с начальным значением "Новая точка"
        [ObservableProperty]
        private string _name = "Новая точка";

        // Конструктор, принимающий главную ViewModel и окно
        public AddPointViewModel(MainWindowViewModel mainVm, Window window)
        {
            _mainVm = mainVm;
            _window = window;
        }

        // Команда для добавления точки
        [RelayCommand]
        private void AddPoint()
        {
            // Если имя не задано, устанавливаем значение по умолчанию
            if (string.IsNullOrWhiteSpace(Name))
                Name = "Точка";

            // Добавляем новую точку в коллекцию фигур главной ViewModel
            _mainVm.AddShape(new Point(X, Y) { Name = Name });

            // Закрываем окно добавления точки
            _window?.Close();
        }
    }
}