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
    public partial class Form1 : Form
    {
        DatabaseDataContext db_dc = new DatabaseDataContext();
        public Form1()
        {
            InitializeComponent();
            ReadDatabase();
            buttonUpdate.Enabled = false;
            buttonDelete.Enabled = false;
        }

        private void ReadDatabase()
        {
            listBoxPersons.Items.Clear();
            foreach (Person person in db_dc.Persons)
            //foreach (Person person in db_dc.Persons.Where(o => o.Age<20))
            {
                listBoxPersons.Items.Add(person);
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            Person p = new Person();
            p.Name = textBoxName.Text;
            p.Age = (int)numericUpDownAge.Value;

            db_dc.Persons.InsertOnSubmit(p);

            foreach (UserControlSubject ucs in flowLayoutPanelSubjects.Controls)
            {
                ucs.UpdateSubject(p);
            }

            db_dc.SubmitChanges();
            ReadDatabase();

            textBoxName.Text = "";
            numericUpDownAge.Value = 0;

            flowLayoutPanelSubjects.Controls.Clear();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxPersons.SelectedItem != null)
            {
                if (listBoxPersons.SelectedItem is Person)
                {
                    Person personToDelete = listBoxPersons.SelectedItem as Person;

                    db_dc.Subjects.DeleteAllOnSubmit(personToDelete.Subjects);
                    db_dc.Persons.DeleteOnSubmit(personToDelete);

                    db_dc.SubmitChanges();
                    ReadDatabase();

                    textBoxName.Text = "";
                    numericUpDownAge.Value = 0;
                    flowLayoutPanelSubjects.Controls.Clear();

                    buttonInsert.Enabled = true;
                    buttonUpdate.Enabled = false;
                    buttonDelete.Enabled = false;
                }
            }
        }

        private void listBoxPersons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPersons.SelectedItem != null)
            {
                if (listBoxPersons.SelectedItem is Person)
                {
                    Person personToUpdate = listBoxPersons.SelectedItem as Person;

                    textBoxName.Text = personToUpdate.Name;
                    numericUpDownAge.Value = personToUpdate.Age;

                    flowLayoutPanelSubjects.Controls.Clear();
                    foreach (Subject s in personToUpdate.Subjects)
                    {
                        UserControlSubject userControlSubject = new UserControlSubject(s);   
                        flowLayoutPanelSubjects.Controls.Add(userControlSubject);
                    }

                    buttonInsert.Enabled = false;
                    buttonUpdate.Enabled = true;
                    buttonDelete.Enabled = true;
                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (listBoxPersons.SelectedItem != null)
            {
                if (listBoxPersons.SelectedItem is Person)
                {
                    Person personToUpdate = listBoxPersons.SelectedItem as Person;

                    personToUpdate.Name = textBoxName.Text;
                    personToUpdate.Age = (int) numericUpDownAge.Value;

                    foreach(UserControlSubject ucs in flowLayoutPanelSubjects.Controls)
                    {
                        if (ucs.SubjectToDelete != null)
                        {
                            db_dc.Subjects.DeleteOnSubmit(ucs.SubjectToDelete);
                        }
                        ucs.UpdateSubject(personToUpdate);
                    }
                    db_dc.SubmitChanges();
                    ReadDatabase();

                    textBoxName.Text = "";
                    numericUpDownAge.Value = 0;

                    flowLayoutPanelSubjects.Controls.Clear() ;
                    buttonInsert.Enabled = true;
                    buttonUpdate.Enabled = false;
                    buttonDelete.Enabled = false;
                }
            }
        }

        private void buttonAddSubject_Click(object sender, EventArgs e)
        {
            UserControlSubject ucs = new UserControlSubject();
            flowLayoutPanelSubjects.Controls.Add(ucs);
        }
    }
}
