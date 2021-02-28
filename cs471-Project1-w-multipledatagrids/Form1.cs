using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace cs471_Project1_w_multipledatagrids
{
    public partial class Form1 : Form
    {
        private Dispatcher d = new Dispatcher();
        private int spawncounter = 0;
        private int completedProcesses = 0;
        private bool running = false;
        private bool switchit = true;
        string[] completedProc = new string[6];
        Process previousProc;
        private bool processesCreated = false;

        private String id_tbTerm;

      

        public Form1()
        {
            this.Load += new EventHandler(Form1_Load);
            InitializeComponent();
        }

        private void Form1_Load(System.Object sender, System.EventArgs e)
        {
            SetupLayout();
            SetupDataGridView();

        }

        private void Run()
        {
            
            running = true;
            timer1.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e) //run
        {
            if (!processesCreated)
            {
                processesCreated = true;
            }
            FillGridWithProcesses();
            dataGridView1.Sort(dataGridView1.Columns["Priority"],
                 System.ComponentModel.ListSortDirection.Ascending);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(processesCreated==true)
            {
                Run();
            }
            else
                MessageBox.Show("Please create a process or click AutoCreate", "Wait a second!"); //show message box
        }

        //Terminates the currently running process. Does not work if the Queue isnt running.
        private void button_Terminate_Click(object sender, EventArgs e)
        {

            if (SanityCheck2(id_tbTerm)&&running)
            {
                
                if (id_tbTerm == d.GetProcess().getID().ToString())
                {
                    d.runningProcess.terminate();

                }
            }
        }

        private void createProcess_Button_Click(object sender, EventArgs e)
        {
            
            String processName = newProcessName_TextBox.Text;
            String processPriority = newProcessPriority_Textbox.Text;
            String processBurstTime = newProcessBurstTime_TextBox.Text;
            
            if(SanityCheck1(processName, processPriority, processBurstTime))
            {
                spawncounter++;
                string[] row0 = new string[6];
                Process process0 = new Process(spawncounter, processName, Convert.ToDouble(processPriority), Convert.ToDouble(processBurstTime));
                d.AddProcess(process0);

                row0[0] = process0.getID().ToString();
                row0[1] = process0.getName();
                row0[2] = process0.getPriority().ToString();
                row0[3] = process0.getBurstTime().ToString();

                this.dataGridView1.Rows.Add(row0);
                dataGridView1.Sort(dataGridView1.Columns["Priority"],
                     System.ComponentModel.ListSortDirection.Ascending);

                if (!processesCreated)
                {
                    processesCreated = true;
                }
            }
            
        }
        //Error Checking
        private bool SanityCheck1(String name, String pri, String bur)
        {
            bool priflag = false;
            bool burflag = false;
            double Num;
            bool isNum;
            isNum = double.TryParse(pri, out Num);
            if (!isNum)
            {
                MessageBox.Show("Invalid Priority Value", "Wait a second!");
            }
            else
                priflag = true;

            isNum = double.TryParse(bur, out Num);
            if (!isNum)
            {
                MessageBox.Show("Invalid Burst Value", "Wait a second!");
            }
            else
                burflag = true;

            return priflag && burflag;
        }
        //Error Checking
        private bool SanityCheck2(String id)
        {
            bool idflag = false;
            
            double Num;
            bool isNum;
            isNum = double.TryParse(id, out Num);
            if (!isNum)
            {
                MessageBox.Show("Invalid ID", "Wait a second!");
            }
            else
                idflag = true;

            

            return idflag;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            id_tbTerm = textBox1.Text.ToString();
        }

        private void SetupLayout()
        {
            this.Size = new Size(1450, 650);
            button1.Text = "AutoCreate";
            button2.Text = "Run";
        }

        private void SetupDataGridView()
        {
            dataGridView1.Name = "Blocked List";
            dataGridView1.ColumnCount = 4;

            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Name";
            dataGridView1.Columns[2].Name = "Priority";
            dataGridView1.Columns[3].Name = "Burst Time";
            dataGridView1.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView1.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            dataGridView1.RowHeadersVisible = false;


            dataGridView2.Name = "Priority Queue";
            dataGridView2.ColumnCount = 3;

            dataGridView2.Columns[0].Name = "ID";
            dataGridView2.Columns[1].Name = "Name";
            dataGridView2.Columns[2].Name = "Priority";
            dataGridView2.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView2.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            dataGridView2.RowHeadersVisible = false;

            dataGridView3.Name = "Currently Running";
            dataGridView3.ColumnCount = 5;

            dataGridView3.Columns[0].Name = "ID";
            dataGridView3.Columns[1].Name = "Name";
            dataGridView3.Columns[2].Name = "Priority";
            dataGridView3.Columns[3].Name = "Burst Time";
            dataGridView3.Columns[4].Name = "Remaining Time";
            dataGridView3.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView3.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            dataGridView3.RowHeadersVisible = false;



        }

        private void FillGridWithProcesses()
        {
            var rand = new Random();

            Process process0 = new Process(spawncounter, "gaming" + spawncounter, rand.NextDouble() * 5, rand.Next(1,5));
            d.AddProcess(process0);
            string[] row0 = new string[6];
            spawncounter++;

            row0[0] = process0.getID().ToString();
            row0[1] = process0.getName();
            row0[2] = process0.getPriority().ToString();
            row0[3] = process0.getBurstTime().ToString();

            this.dataGridView1.Rows.Add(row0);

            Process process1 = new Process(spawncounter, "gaming" + spawncounter, rand.NextDouble() * 5, rand.Next(1, 5));
            d.AddProcess(process1);
            string[] row1 = new string[6];
            spawncounter++;

            row1[0] = process1.getID().ToString();
            row1[1] = process1.getName();
            row1[2] = process1.getPriority().ToString();
            row1[3] = process1.getBurstTime().ToString();

            this.dataGridView1.Rows.Add(row1);

            Process process2 = new Process(spawncounter, "gaming" + spawncounter, rand.NextDouble() * 5, rand.Next(1, 5));
            d.AddProcess(process2);
            string[] row2 = new string[6];
            spawncounter++;

            row2[0] = process2.getID().ToString();
            row2[1] = process2.getName();
            row2[2] = process2.getPriority().ToString();
            row2[3] = process2.getBurstTime().ToString();

            this.dataGridView1.Rows.Add(row2);

            Process process3 = new Process(spawncounter, "gaming" + spawncounter, rand.NextDouble() * 5, rand.Next(1, 5));
            d.AddProcess(process3);
            string[] row3 = new string[6];
            spawncounter++;

            row3[0] = process3.getID().ToString();
            row3[1] = process3.getName();
            row3[2] = process3.getPriority().ToString();
            row3[3] = process3.getBurstTime().ToString();

            this.dataGridView1.Rows.Add(row3);

            Process process4 = new Process(spawncounter, "gaming" + spawncounter, rand.NextDouble() * 5, rand.Next(1, 5));
            d.AddProcess(process4);
            string[] row4 = new string[6];
            spawncounter++;

            row4[0] = process4.getID().ToString();
            row4[1] = process4.getName();
            row4[2] = process4.getPriority().ToString();
            row4[3] = process4.getBurstTime().ToString();

            this.dataGridView1.Rows.Add(row4);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool completed = d.checkRunning();

            if (completed && completedProcesses != spawncounter)
            {
                completedProcesses++;
                switchit = true;

                completedProc[0] = previousProc.getID().ToString();
                completedProc[1] = previousProc.getName();
                completedProc[2] = previousProc.getPriority().ToString();
                completedProc[3] = previousProc.getBurstTime().ToString();

                this.dataGridView2.Rows.Add(completedProc);
                richTextBox1.Text += d.Context();
            }
            Process processTBD = d.GetProcess();
            previousProc = processTBD;

            this.dataGridView3[0, 0].Value = processTBD.getID().ToString();
            this.dataGridView3[1, 0].Value = processTBD.getName();
            this.dataGridView3[2, 0].Value = processTBD.getPriority().ToString();
            this.dataGridView3[3, 0].Value = processTBD.getBurstTime().ToString();
            this.dataGridView3[4, 0].Value = (processTBD.getBurstTime() - processTBD.getElapsedTime()).ToString();

            if (processTBD.getID().ToString() ==
               dataGridView3[0, 0].Value.ToString() && switchit == true && spawncounter != completedProcesses)
            {
                dataGridView1.Rows.RemoveAt(0);
                switchit = false;
            }
            
        }

    }
}


