using System.Windows;
using System.Windows.Controls;

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
            
            Payment.amount = monthlyPayment;

            double remainingBalance = principal;

            for (int paymentNumber = 1; paymentNumber <= numberOfPayments; paymentNumber++)
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

            MonthlyPaymentLabel.Content = "Monthly Payment";
            MonthlyPaymentAmountLabel.Content = "$" + Payment.amount.ToString();

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            AmortizationListView.Items.Clear();
            loanAmountBox.Clear();
            interestRateBox.Clear();
            termBox.Clear();
            MonthlyPaymentLabel.Content = "";
            MonthlyPaymentAmountLabel.Content = "";
        }

        private void populateAmortizationTable(List<Payment> table)
        {
            AmortizationListView.Items.Clear();
            
            foreach (Payment payment in table)
            {
                AmortizationListView.Items.Add(payment);
            }
        }

        private void AmortizationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    public class Payment
    {
        public static double amount {  get; set; }
        public double number { get; set; }
        public double principal { get; set; }
        public double interest { get; set; }
        public double remainingBalance { get; set; }

    }
    // ideas:
    // monthly/annual amortization selection
    // column for cumulative interest paid
}