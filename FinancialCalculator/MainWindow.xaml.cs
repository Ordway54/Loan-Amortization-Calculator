using System.Windows;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using System.Xml.Linq;

namespace FinancialCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        public static List<Payment> GetAmortizationSchedule(double principal, double interestRate, int termYears)
        {
            List<Payment> amortizationSchedule = new List<Payment>();

            int numberOfPayments = termYears * 12;
            double monthlyInterestRate = interestRate / 12 / 100;
            double monthlyPayment = Math.Round((principal * monthlyInterestRate) / (1 - Math.Pow(1 + monthlyInterestRate, -numberOfPayments)), 2);

            double remainingBalance = principal;

            for (int paymentNumber = 0; paymentNumber < numberOfPayments; paymentNumber++)
            {
                double interestPayment = Math.Round(remainingBalance * monthlyInterestRate, 2);
                double principalPayment = Math.Round(monthlyPayment - interestPayment, 2);

                remainingBalance = Math.Round(remainingBalance - principalPayment, 2);

                Payment payment = new Payment() {
                    number = paymentNumber, principal = principalPayment, 
                    interest = interestPayment, remainingBalance = remainingBalance};

                amortizationSchedule.Add(payment);
            }

            return amortizationSchedule;

        }

        private void CalcButton_Click(object sender, RoutedEventArgs e)
        {
            double loanAmount = double.Parse(loanAmountBox.Text);
            double interestRate = double.Parse((interestRateBox.Text));
            int term = int.Parse((termBox.Text));

            List<Payment> schedule = GetAmortizationSchedule(loanAmount, interestRate, term);

            populateAmortizationTable(schedule);

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            AmortizationListView.Items.Clear();
            loanAmountBox.Clear();
            interestRateBox.Clear();
            termBox.Clear();
        }

        private void populateAmortizationTable(List<Payment> table)
        {
            AmortizationListView.Items.Clear();
            
            foreach (Payment payment in table)
            {
                AmortizationListView.Items.Add(payment);
            }
        }
    }

    public class Payment
    {
        public double number { get; set; }
        public double principal { get; set; }
        public double interest { get; set; }
        public double remainingBalance { get; set; }
    }
}