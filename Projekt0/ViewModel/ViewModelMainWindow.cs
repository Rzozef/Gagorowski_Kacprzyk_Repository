using Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModelMainWindow : BindableBase
    {
        private string _ballsNumber;
        private bool _beginSimulationClicked;
        private List<Ball> _balls = new List<Ball>();
        public ICommand SimulationButtonClicked { get; set; }

        public List<Ball> Balls
        {
            get => _balls;
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
            _balls.Add(new Ball() { BallPosition = "0,0,0,0" });
            _balls.Add(new Ball() { BallPosition = "200,0,0,0" });


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
        public string BallPosition { get; set; }
    }
}
