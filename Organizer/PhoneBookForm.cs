using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Organizer
{
    public partial class PhoneBookForm : Form
    {
        private RecordSerializer _serializer;
        private BindingSource bsInput;

        public PhoneBookForm()
        {
            InitializeComponent();            
            _serializer = new RecordSerializer(new PhoneBookRecordFactory());
            bsInput = new BindingSource();
            bsInput.DataSource = new List<PhoneBookRecord>();
            dataGridView1.DataSource = bsInput;
        }

        private void button1_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                var ofd = new OpenFileDialog();
                if (DialogResult.OK == ofd.ShowDialog())
                {
                    var loadedRecords = _serializer.LoadFromFile(ofd.FileName);
                    bsInput.DataSource = loadedRecords.Cast<PhoneBookRecord>().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при загрузке файла");
            }
        }

        private void button2_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                var sfd = new SaveFileDialog();
                if (DialogResult.OK == sfd.ShowDialog())
                {
                    _serializer.SaveToFile(sfd.FileName,
                        (dataGridView1.DataSource as BindingSource)
                            .List.Cast<AbstractRecord>());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при сохранении файла");
            }
        }

        private void dataGridView1_Validating(Object sender, CancelEventArgs e)
        {            
            foreach (DataGridViewRow curRow in dataGridView1.Rows)
            {
                var dto = curRow.DataBoundItem as AbstractRecord;
                if (dto != null)
                {
                    var errorMessages = dto.Validate();
                    if (errorMessages.Any())
                    {
                        MessageBox.Show(String.Join(
                                Environment.NewLine + ";",
                                errorMessages.Select(msg => msg.Text).ToArray()),
                            $@"Возникли ошибки при редактировании строки {curRow.Index+1}");
                        e.Cancel = true;
                        break;
                    }
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = false;
            base.OnFormClosing(e);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_CLOSE = 0x10;
            if (m.Msg == WM_CLOSE)
            {
                base.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            }
            base.WndProc(ref m);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
