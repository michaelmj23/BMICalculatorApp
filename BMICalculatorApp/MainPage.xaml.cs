using System;

namespace BMICalculatorApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        if (double.TryParse(HeightEntry.Text, out double height) &&
            double.TryParse(WeightEntry.Text, out double weight))
        {
            if (height <= 0 || weight <= 0)
            {
                ResultLabel.Text = "Please enter valid positive numbers.";
                return;
            }

            double bmi = weight / (height * height);
            string category = GetBMICategory(bmi);

            ResultLabel.Text = $"Your BMI: {bmi:F2}\nCategory: {category}";
        }
        else
        {
            ResultLabel.Text = "Invalid input. Please enter numbers only.";
        }
    }

    private string GetBMICategory(double bmi)
    {
        if (bmi < 18.5)
            return "Underweight";
        else if (bmi < 24.9)
            return "Normal weight";
        else if (bmi < 29.9)
            return "Overweight";
        else
            return "Obese";
    }
}