using Clonked.Basecamp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestingApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Api api = new Api(
                  new BasicAuthenticationCredentials()
                  {
                      AccountId = 1234567890,
                      UserName = "user@brandview.com",
                      Password = "password"
                  });

            Project releasesProject = api.Projects.Get(1234567890);

            var peeps = releasesProject.People.Select(s => s.Id).ToList();
            
            Debug.WriteLine("Peeps: " + peeps.Count);
        }
    }
}
