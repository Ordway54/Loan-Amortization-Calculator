using System.Windows;
using System.Collections.Generic;

namespace FinancialCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public Dictionary<int, double> GetAmortizationSchedule()
        {
            Dictionary<int, double> myDict = new Dictionary<int, double>
            {
                {1, 1.23},
                {2, 1.24}
            };
            return myDict;
        }
    }
}