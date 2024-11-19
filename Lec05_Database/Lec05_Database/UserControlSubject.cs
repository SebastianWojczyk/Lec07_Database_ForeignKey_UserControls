using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lec05_Database
{
    public partial class UserControlSubject : UserControl
    {
        public Subject SubjectToDelete
        {
            get
            {
                //buttonDelete clicked
                //subject is from database
                if(!buttonDelete.Enabled && subject.Person!=null)
                {
                    return subject;
                }
                return null;
            }
        }
        Subject subject;
        public UserControlSubject()
        {
            InitializeComponent();
            subject = new Subject();
            //p.Subjects.Add(subject);
            //subject.Person = p;
        }

        public UserControlSubject(Subject s)
        {
            InitializeComponent();

            subject = s;    
            textBoxName.Text = s.Name;
            numericUpDownECTS.Value = s.ECTS;
        }

        public void UpdateSubject(Person p)
        {
            if (buttonDelete.Enabled)
            {
                subject.Person = p;
                subject.Name = textBoxName.Text;
                subject.ECTS = (int)numericUpDownECTS.Value;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            groupBox1.Text += " to delete";
            buttonDelete.Enabled = false;
            textBoxName.Enabled = false;
            numericUpDownECTS.Enabled = false;
        }
    }
}
