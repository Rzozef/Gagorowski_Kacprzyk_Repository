using Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Input;

using Prezentacja.Model;

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Prezentacja
{
    namespace ViewModel
    {
        public class ViewModelMainWindow : BindableBase
        {
            private ModelAbstractApi _modelAbstractApi;

            public ICommand SimulationButtonClicked { get; set; }

            public ObservableCollection<AbstractBall> Balls
            {
                get => _modelAbstractApi.GetBalls();
            }

            public string BallsNumber
            {
                get { return _modelAbstractApi.BallsNumber; }
                set { _modelAbstractApi.BallsNumber = value; }
            }

            public uint WindowWidth
            {
                get => _modelAbstractApi.WindowWidth;
                set => _modelAbstractApi.WindowWidth = value;
            }
            public uint WindowHeight
            {
                get => _modelAbstractApi.WindowHeight;
                set => _modelAbstractApi.WindowHeight = value;
            }

            public bool BeginSimulationClicked
            {
                get { return _modelAbstractApi.BeginSimulationClicked; }
                set { _modelAbstractApi.BeginSimulationClicked = value; }
            }

            public ViewModelMainWindow()
            {
                SimulationButtonClicked = new CommandHandler(StartSimulation, CanStartSimulation);

                uint win_width = 300;
                uint win_height = 300;

                _modelAbstractApi = ModelAbstractApi.CreateApi(win_width, win_height);
            }

            private async void StartSimulation(object value)
            {
                await Task.Factory.StartNew(() =>
                {
                    uint ballsNumber;
                    if (uint.TryParse(BallsNumber, out ballsNumber))
                    {
                        BeginSimulationClicked = true;
                        _modelAbstractApi.CreateBalls(ballsNumber);
                    }
                });
            }

            private bool CanStartSimulation(object value)
            {
                return !BeginSimulationClicked;
            }
        }


    }
}