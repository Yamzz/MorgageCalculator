using System;
using System.Windows;
using static System.Int32;
using static System.MidpointRounding;
using static System.Math;

namespace MorgageCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// https://en.wikipedia.org/wiki/Mortgage_calculator
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate string CalcMortgageDelegate(double amountBorrowed, double interestRate, int mortgagePeriod);

        public MainWindow()
        {
            InitializeComponent();
        }

        static public double amountBorrowed { get; set; }
        static public double interestRate { get; set; }
        static public int mortgagePeriod { get; set; }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            amountBorrowed = (double)Parse(txtMonthlyAmt.Text);

            decimal result;
            if (Decimal.TryParse(txtInterest.Text, out result))
                interestRate = (double)result;

            mortgagePeriod = Parse(txtPeriod.Text);

            CalcMortgageDelegate del = new CalcMortgageDelegate(CalcMortgage);
            txtMonthlyPayments.Text = del(amountBorrowed, interestRate, mortgagePeriod);
        }

        private string CalcMortgage(double amountBorrowed, double interestRate, int mortgagePeriod)
        {
            double p = amountBorrowed;
            double r = ConvertToMonthlyInterest(interestRate);
            double n = YearsToMonths(mortgagePeriod);

            var c = (decimal)(((r * p) * Pow((1 + r), n)) / (Pow((1 + r), n) - 1));

            return ($"${Round(c, AwayFromZero)}");
        }


        private int YearsToMonths(int years)
        {
            return (12 * years);
        }

        private double ConvertToMonthlyInterest(double percent)
        {
            return (percent / 12) / 100;
        }



    }
}
