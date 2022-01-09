using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace IncomePlanner
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //Edit Texts
        EditText incomePerHourEditText;
        EditText workHourPerDayEditText;
        EditText taxRateEditText;
        EditText savingRateEditText;

        //Text Views
        TextView workSummaryTextView;
        TextView grossIncomeTextView;
        TextView taxPayableTextView;
        TextView savingsTextView;
        TextView spendableIncomeTextView;

        Button calculateButton;
        RelativeLayout resultLayout;

        bool inputCalculated = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            ConnectViews();
        }

        void ConnectViews()
        {
            incomePerHourEditText = FindViewById<EditText>(Resource.Id.incomePerHourEditText);
            workHourPerDayEditText = (EditText)FindViewById(Resource.Id.hoursPerDayEditText);
            taxRateEditText = (EditText)FindViewById(Resource.Id.taxRateEditText);
            savingRateEditText = (EditText)FindViewById(Resource.Id.savingRateEditText);

            workSummaryTextView = (TextView)FindViewById(Resource.Id.workSummaryTextView);
            grossIncomeTextView = (TextView)FindViewById(Resource.Id.grossIncomeTextView);
            taxPayableTextView = (TextView)FindViewById(Resource.Id.taxPayableTextView);
            savingsTextView = (TextView)FindViewById(Resource.Id.savingsTextView);
            spendableIncomeTextView = (TextView)FindViewById(Resource.Id.spendableIncomeTextView);

            calculateButton = (Button)FindViewById(Resource.Id.calculateButton);
            resultLayout = (RelativeLayout)FindViewById(Resource.Id.resultLayout);

            calculateButton.Click += CalculateButton_Click;
        }

        private void CalculateButton_Click(object sender, System.EventArgs e)
        {
            if (inputCalculated)
            {
                inputCalculated = false;
                calculateButton.Text = "Calculate";
                ClearInput();
                return;
            }
            // Take inputs from user
            double incomePerHour = double.Parse(incomePerHourEditText.Text);
            double workHourPerDay = double.Parse(workHourPerDayEditText.Text);
            double taxRate = double.Parse(taxRateEditText.Text);
            double savingRate = double.Parse(savingRateEditText.Text);

            // Calculate annual income, tax and savings
            double annualWorkHourSummary = workHourPerDay * 5 * 50; // 52 weeks in a year, but user has 2 weeks off
            double annualIncome = incomePerHour * annualWorkHourSummary;
            double taxPayable = (taxRate / 100) * annualIncome;
            double annualSavings = (savingRate / 100) * annualIncome;
            double spendableIncome = annualIncome - annualSavings - taxPayable;

            //Display results of the calculation
            workSummaryTextView.Text = annualWorkHourSummary.ToString("#,##") + "HRS";
            grossIncomeTextView.Text = annualIncome.ToString("#,##") + "$";
            taxPayableTextView.Text = taxPayable.ToString("#,##") + "$";
            savingsTextView.Text = annualSavings.ToString("#,##") + "$";
            spendableIncomeTextView.Text = spendableIncome.ToString("#,##") + "$";

            //Publishing our results - make a resultLayout visible
            resultLayout.Visibility = Android.Views.ViewStates.Visible;

            //Setting a calculated flag to true - because calculating is done
            inputCalculated = true;

            //Setting a button as "Clear" instead of "Calculate"
            calculateButton.Text = "Clear"; 
        }

        void ClearInput()
        {
            incomePerHourEditText.Text = "";
            workHourPerDayEditText.Text = "";
            taxRateEditText.Text = "";
            savingRateEditText.Text = "";

            resultLayout.Visibility = Android.Views.ViewStates.Invisible;
        }
    }
}