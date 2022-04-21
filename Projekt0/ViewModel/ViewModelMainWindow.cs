using Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Input;

using Model;

namespace ViewModel
{
    public class ViewModelMainWindow : BindableBase
    {
        private string _ballsNumber;
        private bool _beginSimulationClicked;
        private ModelAbstractApi _modelAbstractApi;
        public ICommand SimulationButtonClicked { get; set; }

        public List<Ball> Balls // TODO to chyba nie jest dobrze?
        {
            get => _modelAbstractApi.GetBalls().ConvertAll(ball => new Ball { BallX = ball.x, BallY = ball.y, BallSize = ball.size });
        }

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
            //_balls.Add(new Ball() { BallPosition = "0,0,0,0" });
            //_balls.Add(new Ball() { BallPosition = "200,0,0,0" });

            _modelAbstractApi = ModelAbstractApi.CreateApi(200, 200);
        }

        private void StartSimulation(object value)
        {
            BeginSimulationClicked = true;
        }

        private bool CanStartSimulation(object value)
        {
            return !BeginSimulationClicked;
        }
    }

    public class Ball
    {
        public float BallX { get; set; }
        public float BallY { get; set; }
        public float BallSize { get; set; }
    }
}
