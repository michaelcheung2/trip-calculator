// MICHAEL CHEUNG - 5/21/2018
// These are the unit tests for a program that calculates the expenses for a group of students who like to go on road trips.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace TripCalculatorTest
{
    [TestClass]
    public class TripCalculatorUnitTest : TripCalculatorSolution.Input
    {
        [TestMethod]
        // Calculates and returns the grand total in expenses between three expenses.
        public void testCalculateGrandTotalWithThreeExpenses()
        {
            List<Label> oLabels = new List<Label>();

            Label oLabel1 = new Label();
            oLabel1.ID = "lblTotalValue1";
            oLabel1.Text = "$53.54";
            oLabels.Add(oLabel1);

            Label oLabel2 = new Label();
            oLabel2.ID = "lblTotalValue2";
            oLabel2.Text = "$50.23";
            oLabels.Add(oLabel2);

            Label oLabel3 = new Label();
            oLabel3.ID = "lblTotalValue3";
            oLabel3.Text = "$113.41";
            oLabels.Add(oLabel3);

            decimal dResult = CalculateGrandTotal(oLabels);
            Assert.AreEqual(Convert.ToDecimal(217.18), dResult);
        }

        [TestMethod]
        // Calculates and returns the grand total in expenses between two expenses, where one expense is negative because one person loaned money to someone else.
        public void testCalculateGrandTotalWithTwoExpensesAndNegativeAmount()
        {
            List<Label> oLabels = new List<Label>();

            Label oLabel1 = new Label();
            oLabel1.ID = "lblTotalValue1";
            oLabel1.Text = "$225.00";
            oLabels.Add(oLabel1);

            Label oLabel2 = new Label();
            oLabel2.ID = "lblTotalValue2";
            oLabel2.Text = "-$75.00";
            oLabels.Add(oLabel2);

            decimal dResult = CalculateGrandTotal(oLabels);
            Assert.AreEqual(Convert.ToDecimal(150.00), dResult);
        }

        [TestMethod]
        // Calculates the total expenses to be evenly distributed amongst three people that made typical expenses.
        public void testCalculateDistributionWithThreePeopleAndNormalExpenses()
        {
            decimal dGrandTotal = 217.18M;
            int nPersonsCount = 3;
            decimal dResult = CalculateDistribution(dGrandTotal, nPersonsCount);
            Assert.AreEqual(Convert.ToDecimal(72.39), dResult);
        }

        [TestMethod]
        // Calculates the total expenses to be evenly distributed amongst three people that made no expenses.
        public void testCalculateDistributionWithThreePeopleAndNoExpenses()
        {
            decimal dGrandTotal = 0;
            int nPersonsCount = 3;
            decimal dResult = CalculateDistribution(dGrandTotal, nPersonsCount);
            Assert.AreEqual(0, dResult);
        }

        [TestMethod]
        // Calculates the total expenses to be evenly distributed amongst 20 people that made a large amount of expenses.
        public void testCalculateDistributionWithTwentyPeopleAndLargeExpenses()
        {
            decimal dGrandTotal = 35089.97M;
            int nPersonsCount = 20;
            decimal dResult = CalculateDistribution(dGrandTotal, nPersonsCount);
            // 35089.97 / 20 = 1754.4985. This should round up to 1754.50.
            Assert.AreEqual(Convert.ToDecimal(1754.50), dResult);
        }

        [TestMethod]
        // Calculates the difference to determine remaining debt (when a person's expenses is greater than the distributed total).
        public void testCalculateDifferenceWhenPersonOwesMoney()
        {
            decimal dCurrentPersonExpenses = 72.39M;
            decimal dDistributedTotal = 53.54M;
            decimal dResult = CalculateDifference(dCurrentPersonExpenses, dDistributedTotal);
            Assert.AreEqual(Convert.ToDecimal(18.85), dResult);
        }

        [TestMethod]
        // Calculates the difference to determine remaining owed to a person (when a person's expenses is lower than the distributed total).
        public void testCalculateDifferenceWhenPersonIsOwedMoney()
        {
            decimal dCurrentPersonExpenses = 50.23M;
            decimal dDistributedTotal = 72.39M;
            decimal dResult = CalculateDifference(dCurrentPersonExpenses, dDistributedTotal);
            Assert.AreEqual(Convert.ToDecimal(22.16), dResult);
        }

        [TestMethod]
        // Helper function to format the decimal into a string with a dollar sign. 
        public void testFormatCurrency()
        {
            decimal dTemp = 10.00M;
            string sResult = FormatCurrency(dTemp);
            Assert.AreEqual("$10.00", sResult);
        }
    }
}
