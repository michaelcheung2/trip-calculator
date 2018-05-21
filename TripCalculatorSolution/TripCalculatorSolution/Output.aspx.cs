// MICHAEL CHEUNG - 5/21/2018
// This is a program that calculates the expenses for a group of students who like to go on road trips.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TripCalculatorSolution
{
    public partial class Output : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Retrieve the final output result from Session, and display it on the page.
            if (Session["TripCalculatorResults"] != null)
            {
                List<string> oResultsList = (List<string>)Session["TripCalculatorResults"];
                StringBuilder sbResult = new StringBuilder();

                foreach (string sResult in oResultsList)
                {
                    sbResult.Append(sResult + "<br />");
                }
                divContentOutput.InnerHtml = sbResult.ToString();

                Session.Remove("TripCalculatorResults"); // Clean up session.
            }
        }
    }
}