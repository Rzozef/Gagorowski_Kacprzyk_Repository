using Prism.Mvvm;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModelMainWindow : BindableBase
    {
        private string _ballsNumber;
        private bool _beginSimulationClicked;
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
}
