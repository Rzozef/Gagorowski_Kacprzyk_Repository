using Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Input;

using Model;

using System;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class ViewModelMainWindow : BindableBase
    {
        private string _ballsNumber;
        private bool _beginSimulationClicked;
        private ModelAbstractApi _modelAbstractApi;
        private uint _windowWidth;
        private uint _windowHeight;
        public ICommand SimulationButtonClicked { get; set; }

        public ObservableCollection<BallAbstract> Balls
        {
            get => _modelAbstractApi.GetBalls();
        }

        public string BallsNumber
        {
            get { return _ballsNumber; }
            set { _ballsNumber = value; }
        }

        public uint WindowWidth
        {
            get => _windowWidth;
            set => _windowWidth = value;
        }
        public uint WindowHeight
        {
            get => _windowHeight;
            set => _windowHeight = value;
        }

        public bool BeginSimulationClicked
        {
            get { return _beginSimulationClicked; }
            set { _beginSimulationClicked = value; }
        }

        public ViewModelMainWindow()
        {
            SimulationButtonClicked = new CommandHandler(StartSimulation, CanStartSimulation);
            BallsNumber = "0";

            WindowWidth = 300;
            WindowHeight = 300;

            _modelAbstractApi = ModelAbstractApi.CreateApi(WindowWidth, WindowHeight);
        }

        private void StartSimulation(object value)
        {
            BeginSimulationClicked = true;
            _modelAbstractApi.CreateBalls(Convert.ToUInt32(BallsNumber));
        }

        private bool CanStartSimulation(object value)
        {
            return !BeginSimulationClicked;
        }
    }

    
}
