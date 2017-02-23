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
    public partial class DiaryForm : Form
    {
        private RecordSerializer _serializer;
        private BindingSource bsInput;

        public DiaryForm()
        {
            InitializeComponent();
            _serializer = new RecordSerializer(new DiaryRecordFactory());
            bsInput = new BindingSource();
            bsInput.DataSource = new List<DiaryRecord>();
            dataGridView1.DataSource = bsInput;
        }

        private void button3_Click(Object sender, EventArgs e)
        {
            var curRow = dataGridView1.CurrentRow;
            if (curRow == null)
            {
                MessageBox.Show("Запись не выбрана");
                return;
            }
            var curRecord = curRow.DataBoundItem as DiaryRecord;
            curRecord.Content = textBox1.Text;
            curRecord.RecordDateTime = dateTimePicker2.Value;
            dataGridView1.Refresh();
        }

        private void button1_Click(Object sender, EventArgs e)
        {
            try
            {
                var ofd = new OpenFileDialog();
                if (DialogResult.OK == ofd.ShowDialog())
                {
                    var loadedRecords = _serializer.LoadFromFile(ofd.FileName);
                    bsInput.DataSource = loadedRecords.Cast<DiaryRecord>().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при загрузке файла");
            }
        }

        private void button2_Click(Object sender, EventArgs e)
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

        private void button4_Click(Object sender, EventArgs e)
        {
            bsInput.RemoveCurrent();
        }

        private void button5_Click(Object sender, EventArgs e)
        {
            bsInput.AddNew();
        }

        private void checkBox1_CheckedChanged(Object sender, EventArgs e)
        {
            dateTimePicker1_ValueChanged(sender, e);
        }

        private void dateTimePicker1_ValueChanged(Object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                var cur = dateTimePicker1.Value;
                var dayStart = new DateTime(cur.Year, cur.Month, cur.Day);
                var dayEnd = dayStart.AddDays(1);
                dataGridView1.DataSource = new BindingSource()
                {
                    DataSource = bsInput.List.OfType<DiaryRecord>().Where(diary =>
                            dayStart < diary.RecordDateTime && diary.RecordDateTime < dayEnd).ToList()
                };
            }
            else
            {
                dataGridView1.DataSource = bsInput;
            }
            button3.Enabled = button4.Enabled = button5.Enabled = button1.Enabled = !checkBox1.Checked;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}