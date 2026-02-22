using System;

namespace BMICalculatorApp;

public partial class MainPage : ContentPage
{
    private bool isUpdating = false; // prevents infinite loop

    public MainPage()
    {
        InitializeComponent();
    }

    // When meters change → update feet
    private void OnMeterChanged(object sender, TextChangedEventArgs e)
    {
        if (isUpdating) return;

        if (double.TryParse(MeterEntry.Text, out double meters))
        {
            isUpdating = true;
            double feet = meters * 3.28084;
            FeetEntry.Text = feet.ToString("F2");
            isUpdating = false;
        }
    }

    // When feet change → update meters
    private void OnFeetChanged(object sender, TextChangedEventArgs e)
    {
        if (isUpdating) return;

        if (double.TryParse(FeetEntry.Text, out double feet))
        {
            isUpdating = true;
            double meters = feet / 3.28084;
            MeterEntry.Text = meters.ToString("F2");
            isUpdating = false;
        }
    }

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        if (double.TryParse(MeterEntry.Text, out double height) &&
            double.TryParse(WeightEntry.Text, out double weight))
        {
            if (height <= 0 || weight <= 0)
            {
                ResultLabel.Text = "Enter valid values.";
                CategoryLabel.Text = "";
                return;
            }

            double bmi = weight / (height * height);
            string category = GetBMICategory(bmi);

            ResultLabel.Text = $"BMI: {bmi:F2}";
            CategoryLabel.Text = category;
            SetCategoryColor(category);
        }
        else
        {
            ResultLabel.Text = "Invalid input.";
            CategoryLabel.Text = "";
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

    private void SetCategoryColor(string category)
    {
        switch (category)
        {
            case "Underweight":
                CategoryLabel.TextColor = Colors.Blue;
                break;
            case "Normal weight":
                CategoryLabel.TextColor = Colors.Green;
                break;
            case "Overweight":
                CategoryLabel.TextColor = Colors.Orange;
                break;
            case "Obese":
                CategoryLabel.TextColor = Colors.Red;
                break;
        }
    }
}