// MICHAEL CHEUNG - 5/21/2018
// This is a program that calculates the expenses for a group of students who like to go on road trips.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TripCalculatorSolution
{
    public partial class Input : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Run this only once - the first time the page loads.
            if (!IsPostBack)
            {
                // Generate the initial person section containing all the necessary data entry fields.
                AddPerson_Click(btnAddPerson, e);
            }
        }

        // Utilize Page_PreInit to recreate the dynamically generated controls on the page.
        protected void Page_PreInit(object sender, EventArgs e)
        {
            string[] oFormKeys = Request.Form.AllKeys; // Fetches list of control IDs from the form.
            List<string> oPersonControlIDs = new List<string>();
            for (var i = 0; i < oFormKeys.Length; i++)
            {
                if (oFormKeys[i].Contains("txtPerson"))
                {
                    oPersonControlIDs.Add(oFormKeys[i]);
                }
            }
            int nPersonIndex = 1;
            foreach (string sPersonControlID in oPersonControlIDs)
            {
                this.CreatePersonSection(sPersonControlID, nPersonIndex);
                nPersonIndex++;
            }
        }

        // Creates a person's section with all the necessary data entry fields.
        protected void AddPerson_Click(object sender, EventArgs e)
        {
            int nCount = 0;
            List<TextBox> oTextBoxes = pnlContentInput.Controls.OfType<TextBox>().ToList();
            foreach (TextBox oTextBox in oTextBoxes)
            {
                // Tally up how many PersonSections have already been created.
                if (!String.IsNullOrEmpty(oTextBox.ID) && oTextBox.ID.Contains("txtPersonName"))
                {
                    nCount++;
                }
            }
            nCount = nCount + 1;
            // Create a new PersonSection with a unique ID.
            this.CreatePersonSection("txtPersonName" + nCount, nCount);
        }

        // Dynamically generates all the necessary data entry fields.
        protected void CreatePersonSection(string sID, int nIndex)
        {
            Literal lt1 = new Literal();
            lt1.Text = "<hr />";
            pnlContentInput.Controls.Add(lt1);

            Label lblPersonName = new Label();
            lblPersonName.Text = "Person Name:&nbsp;";
            pnlContentInput.Controls.Add(lblPersonName);

            TextBox txtPersonName = new TextBox();
            txtPersonName.ID = sID;
            pnlContentInput.Controls.Add(txtPersonName);

            Literal lt2 = new Literal();
            lt2.Text = "<br /><br />";
            pnlContentInput.Controls.Add(lt2);

            Label lblExpense = new Label();
            lblExpense.Text = "Expense Amount:&nbsp;";
            pnlContentInput.Controls.Add(lblExpense);

            TextBox txtExpense = new TextBox();
            txtExpense.ID = "txtExpense" + nIndex;
            txtExpense.Attributes.Add("type", "number");
            txtExpense.Attributes.Add("step", "0.01");
            pnlContentInput.Controls.Add(txtExpense);

            Button btnAddExpense = new Button();
            btnAddExpense.ID = "btnAddExpense" + nIndex;
            btnAddExpense.Width = Unit.Pixel(120);
            btnAddExpense.Text = "Add Expense";
            btnAddExpense.Click += new EventHandler(AddExpense_Click);
            pnlContentInput.Controls.Add(btnAddExpense);

            Literal lt3 = new Literal();
            lt3.Text = "<br /><br />";
            pnlContentInput.Controls.Add(lt3);

            Label lblTotalLabel = new Label();
            lblTotalLabel.Text = "Total:&nbsp;";
            pnlContentInput.Controls.Add(lblTotalLabel);

            Label lblTotalValue = new Label();
            lblTotalValue.ID = "lblTotalValue" + nIndex;
            lblTotalValue.Text = "$0.00";
            pnlContentInput.Controls.Add(lblTotalValue);
        }

        // Takes user input from "Expense Amount" textbox, and adds the amount to the specified person's running total.
        protected void AddExpense_Click(object sender, EventArgs e)
        {
            // Initialize variables and controls
            Button btnAddExpense = (Button)sender;
            string sIndex = btnAddExpense.ID.Substring(13);
            TextBox txtExpense = (TextBox)pnlContentInput.FindControl("txtExpense" + sIndex);
            Label lblTotalValue = (Label)pnlContentInput.FindControl("lblTotalValue" + sIndex);
            decimal dInputExpense = 0;
            decimal dTotal = 0;
            string sInputExpense = txtExpense.Text.Trim();

            if (!String.IsNullOrEmpty(sInputExpense))
            {
                dTotal = decimal.Parse(lblTotalValue.Text.Replace("$", ""));
                if (decimal.TryParse(sInputExpense, out dInputExpense))
                {
                    dTotal += dInputExpense; // Add the amount to the running total. 
                }
                lblTotalValue.Text = "$" + dTotal.ToString();
            }
        }

        // Calculate the expenses so that each person shares expenses equally (split evenly amongst the grand total).
        protected void ComputeDistribution_Click(object sender, EventArgs e)
        {
            // Initialize variables and controls
            Dictionary<string, decimal> oDictionaryAllPeople = new Dictionary<string, decimal>();
            List<Label> oLabels = pnlContentInput.Controls.OfType<Label>().ToList();
            List<string> oResultsList = new List<string>();
            string sCurrentPersonName = string.Empty;
            decimal dGrandTotal = 0;
            decimal dDistributedTotal = 0;

            // Calculates the grand total in expenses between all the people.
            dGrandTotal = CalculateGrandTotal(oLabels);

            // Store data of each person's name and their running total in a Dictionary.
            oDictionaryAllPeople = EstablishAllPeopleData(oLabels);

            // Calculates the total expenses to be evenly distributed amongst each person.
            dDistributedTotal = CalculateDistribution(dGrandTotal, oDictionaryAllPeople.Keys.Count);

            // Determine who "owes" money and who "is owed" money. Concatenate the final output result.
            oResultsList.Add("The grand total of " + FormatCurrency(dGrandTotal) + " was split between " + oDictionaryAllPeople.Keys.Count + " people, so each person is to pay " + FormatCurrency(dDistributedTotal) + ".");
            foreach (KeyValuePair<string, decimal> oKeyValuePair in oDictionaryAllPeople)
            {
                sCurrentPersonName = oKeyValuePair.Key;
                decimal dCurrentPersonExpenses = oKeyValuePair.Value;
                decimal dRemainingDifference = CalculateDifference(dCurrentPersonExpenses, dDistributedTotal);
                if (dCurrentPersonExpenses > dDistributedTotal)
                {
                    oResultsList.Add(sCurrentPersonName + " incurred expenses of " + FormatCurrency(dCurrentPersonExpenses) + ", so " + sCurrentPersonName + " is owed " + FormatCurrency(dRemainingDifference));
                }
                if (dCurrentPersonExpenses < dDistributedTotal)
                {
                    oResultsList.Add(sCurrentPersonName + " incurred expenses of " + FormatCurrency(dCurrentPersonExpenses) + ", so " + sCurrentPersonName + " owes " + FormatCurrency(dRemainingDifference));
                }
            }
            // Store the results in Session to persist the data across to the output page, then redirect user to the output page.
            Session.Add("TripCalculatorResults", oResultsList);
            Response.Redirect("Output.aspx");
        }

        // Calculates and returns the grand total in expenses between all the people.
        protected decimal CalculateGrandTotal(List<Label> oLabels)
        {
            string sSectionNumber = string.Empty;
            string sCurrentPersonName = string.Empty;
            decimal dTemp;
            decimal dGrandTotal = 0;

            foreach (Label oLabel in oLabels)
            {
                if (!String.IsNullOrEmpty(oLabel.ID) && oLabel.ID.Contains("lblTotalValue")) // Skip over "Person Name" textboxes.
                {
                    if (decimal.TryParse(oLabel.Text.Trim().Replace("$", ""), out dTemp))
                    {
                        dGrandTotal += dTemp;
                    }
                }
            }
            return dGrandTotal;
        }

        // Returns a Dictionary containing each person's name and their running total.
        protected Dictionary<string, decimal> EstablishAllPeopleData(List<Label> oLabels)
        {
            Dictionary<string, decimal> oDictionaryAllPeople = new Dictionary<string, decimal>();
            string sSectionNumber = string.Empty;
            string sCurrentPersonName = string.Empty;
            decimal dTemp;

            foreach (Label oLabel in oLabels)
            {
                if (!String.IsNullOrEmpty(oLabel.ID) && oLabel.ID.Contains("lblTotalValue")) // Skip over "Person Name" textboxes.
                {
                    if (decimal.TryParse(oLabel.Text.Trim().Replace("$", ""), out dTemp))
                    {
                        sSectionNumber = oLabel.ID.Substring(13); // Cut off "lblTotalValue"
                        Control oControl = pnlContentInput.FindControl("txtPersonName" + sSectionNumber);
                        if (oControl != null)
                        {
                            TextBox txtPersonName = (TextBox)oControl;
                            sCurrentPersonName = txtPersonName.Text.Trim();
                            if (String.IsNullOrEmpty(sCurrentPersonName)) { sCurrentPersonName = "(Person " + sSectionNumber + ")"; }
                            oDictionaryAllPeople.Add(sCurrentPersonName, dTemp); // Store data of each person's name and their running total.
                        }
                    }
                }
            }
            return oDictionaryAllPeople;
        }

        // Calculates the total expenses to be evenly distributed amongst each person.
        protected decimal CalculateDistribution(decimal dGrandTotal, int nPersonsCount)
        {
            decimal dResult = dGrandTotal / nPersonsCount;
            string sResult = dResult.ToString("#.##");
            if (String.IsNullOrEmpty(sResult)) { sResult = "0.00"; }
            return Convert.ToDecimal(sResult);
        }

        // Calculates the difference to determine remaining debt or remaining owed.
        protected decimal CalculateDifference(decimal dCurrentPersonExpenses, decimal dDistributedTotal)
        {
            decimal dDifference = 0;
            if (dCurrentPersonExpenses > dDistributedTotal)
            {
                dDifference = dCurrentPersonExpenses - dDistributedTotal;
            }
            if (dCurrentPersonExpenses < dDistributedTotal)
            {
                dDifference = dDistributedTotal - dCurrentPersonExpenses;
            }
            return dDifference;
        }

        // Helper function to format the decimal into a string with a dollar sign. 
        protected string FormatCurrency(decimal dInput)
        {
            return String.Format("{0:C}", dInput);
        }
    }
}