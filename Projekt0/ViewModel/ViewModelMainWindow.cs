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
        public ICommand SimulationButtonClicked { get; set; }

        public string BallsNumber
        {
            get { return _ballsNumber; }
            set { _ballsNumber = value; }
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

            _modelAbstractApi = ModelAbstractApi.CreateApi(200, 200);
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

    public class Ball
    {
        public string BallPosition { get; set; }
    }

    
}
