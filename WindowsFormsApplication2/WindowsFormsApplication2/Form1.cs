using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private LinkedList ls = new LinkedList();
        private LinkedList lsrr = new LinkedList();
        public Form1()
        {
            InitializeComponent();
            numericUpDown1.Minimum = 0;
            numericUpDown2.Minimum = 1;
            numericUpDown3.Minimum = 1;
            numericUpDown4.Minimum = 1;

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        int pi = 1;
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                Form2 m = new Form2();
                m.Show();
                return;
            }
            ls.Add(textBox1.Text,
                (int)numericUpDown1.Value,
                (int)numericUpDown2.Value,
               (int)numericUpDown3.Value);

            lsrr.Add(textBox1.Text,
                (int)numericUpDown1.Value,
                (int)numericUpDown2.Value,
               (int)numericUpDown3.Value);
            process p = ls.get_process();

            listBox1.Items.Add(p.get_processName() + "\t" +
                      p.get_aT() + "\t" + p.get_bT() + "\t" + p.get_priority());
            pi++;
            textBox1.Text = "p"+pi;
            

        }

        private void button2_Click(object sender, EventArgs e)
        {

            int size = ls.size();
            if (size == 0) { Form2 m = new Form2();
                m.Show();
                return;
            }
            listBox2.Items.Clear();
            float waiting=0;
            float turna = 0;
            string gantt="";
            if (comboBox1.Text == "FCFS")
            {
                ls.sort(0, size, ls.get_process());
                process p = ls.get_process();
                int begin = p.get_aT();
                for (int i = 0; i < ls.size(); i++)
                    if (i == 0)
                    {
                        listBox2.Items.Add(p.get_processName() + " "
                            + "waiting=0" + " " + "Departure=" + (p.get_aT() + p.get_bT()));
                        gantt = gantt + "| "+ p.get_processName()+" |";
                        begin = p.get_bT() + p.get_aT();
                    }

                    else
                    {
                        
                        p = p.get_next();
                        int d;
                        if (begin >= p.get_aT())
                        {
                            d = (begin + p.get_bT());
                            waiting = waiting + (begin - p.get_aT());
                            listBox2.Items.Add(p.get_processName() + " "
                                + "waiting=" + (begin - p.get_aT()) + " " + "Departure=" + d);
                            gantt = gantt + " " + p.get_processName() + " |";
                        }
                        else {
                            d = (p.get_aT() + p.get_bT());
                            listBox2.Items.Add(p.get_processName() + " "
                                + "waiting=0" + " " + "Departure=" + d);
                            gantt = gantt + " "+"| " + p.get_processName() + " |";
                        }
                        begin = d;

                    }
            }
            else if (comboBox1.Text == "SJF")
            {
                bool first = true;
                if (radioButton2.Checked)
                {
                    ls.sort(0, size, ls.get_process());
                    process p = ls.get_process();
                    int begin = p.get_aT();
                    while (p != null)
                    {
                        if (p.get_next() != null)
                        {
                            int ctp = p.get_timePassed();
                            int ntp = p.get_next().get_timePassed();
                            int c = p.get_aT() - ctp;
                            int n = p.get_next().get_aT() - ntp;
                            int loop = 1;
                            if (c == n)
                            {
                                loop = ls.check(c, p);
                            }
                            if (loop > 1)
                            {
                                ls.sort(1, loop, p);
                            }
                        }
                        int b = p.get_bT();
                        int d;
                        if (begin >= p.get_aT()&&!first) {
                            d = begin + b;
                            waiting = waiting + (begin - p.get_aT());
                            listBox2.Items.Add(p.get_processName() + " "
                                + "waiting=" + (begin - p.get_aT()) + " Departure=" + d);
                            gantt = gantt + " " + p.get_processName() + " |";
                        }
                        else {
                            d = p.get_aT() + b;
                            listBox2.Items.Add(p.get_processName() + " "
                                + "waiting=0" + " Departure=" + d);
                            gantt = gantt + " " + "| " + p.get_processName() + " |";
                            first = false;
                        }
                        begin = d;
                        p = p.get_next();
                        ls.elmiTDeff(b, p);
                    }
                    ls.restPassedT();
                }
                else
                {
                    ls.sort(0, size, ls.get_process());
                    process p = ls.get_process();
                    int begin = p.get_aT();
                    while (begin<ls.tBT())
                    {
                        if (p == null) { p = ls.get_process(); }
                        if (p.get_next() != null)
                        {
                            int ctp = p.get_timePassed();
                            int ntp = p.get_next().get_timePassed();
                            int c = p.get_aT() - ctp;
                            int n = p.get_next().get_aT() - ntp;
                            int loop = 1;
                            if (c == n)
                            {
                                loop = ls.check(c, p);
                            }
                            if (loop > 1)
                            {
                                ls.sort(1, loop, p);
                            }
                        }
                        
                        int bt = p.get_bT();
                        int btP = p.get_bTimePassed();

                        while (p != null)
                        {
                            if (bt==btP)
                            {
                                p = p.get_next();
                                bt = p.get_bT();
                                btP = p.get_bTimePassed();
                            }

                            else { break; }
                        }
                        

                        bt = p.get_bT();
                        btP = p.get_bTimePassed();
                        if (bt != btP)
                        {
                            if (bt - btP > 1)
                            {
                                p.set_bTimePassed(btP + 1);
                                begin++;
                            }
                            else
                            {
                                int b = p.get_bT();
                                begin++;
                                waiting = waiting + (begin - (p.get_aT() + b));
                                listBox2.Items.Add(p.get_processName() + " "
                                    + "waiting=" + (begin - (p.get_aT() + b)) + " Departure=" + begin);
                                p.set_bTimePassed(btP + 1);
                            }
                            if (!first)
                            {
                                gantt = gantt + " " + p.get_processName() + " |";
                            }
                            else
                            {
                                gantt = gantt + "| " + p.get_processName() + " |";
                                first = false;
                            }
                            ls.elmiTDeff(1, p);
                        }
                    }
                    ls.restPassedT();

                }
            }
            else if (comboBox1.Text == "Priority")
            {

                bool first = true;
                if (radioButton2.Checked)
                {
                    ls.sort(0, size, ls.get_process());
                    process p = ls.get_process();
                    int begin = 0;
                    while (p != null)
                    {
                        if (p.get_next() != null)
                        {
                            int ctp = p.get_timePassed();
                            int ntp = p.get_next().get_timePassed();
                            int c = p.get_aT() - ctp;
                            int n = p.get_next().get_aT() - ntp;
                            int loop = 1;
                            if (c == n)
                            {
                                loop = ls.check(c, p);
                            }
                            if (loop > 1)
                            {
                                ls.sort(2, loop, p);
                            }
                        }
                        int b = p.get_bT();
                        int d ;
                        if (begin>=p.get_aT()&&!first)
                        {
                            d = begin + b;
                            waiting = waiting + (begin - p.get_aT());
                            listBox2.Items.Add(p.get_processName() + " "
                                + "waiting=" + (begin - p.get_aT()) + " Departure=" + d);
                            gantt = gantt + " " + p.get_processName() + " |";
                            begin = d;
                        }
                        else
                        {
                            d = p.get_aT() + b;
                            listBox2.Items.Add(p.get_processName() + " "
                                + "waiting=0" + " Departure=" + d);
                            gantt = gantt + " " + "| " + p.get_processName() + " |";
                            first = false;
                            begin = d;
                        }
                        p = p.get_next();
                        ls.elmiTDeff(b, p);
                    }
                    ls.restPassedT();
                }
                else
                {

                    ls.sort(0, size, ls.get_process());
                    process p = ls.get_process();
                    int begin = p.get_aT();
                    while (begin < ls.tBT())
                    {
                        if (p == null) { p = ls.get_process(); }
                        if (p.get_next() != null)
                        {
                            int ctp = p.get_timePassed();
                            int ntp = p.get_next().get_timePassed();
                            int c = p.get_aT() - ctp;
                            int n = p.get_next().get_aT() - ntp;
                            int loop = 1;
                            if (c == n)
                            {
                                loop = ls.check(c, p);
                            }
                            if (loop > 1)
                            {
                                ls.sort(2, loop, p);
                            }
                        }

                        int bt = p.get_bT();
                        int btP = p.get_bTimePassed();

                        while (p != null)
                        {
                            if (bt == btP)
                            {
                                p = p.get_next();
                                bt = p.get_bT();
                                btP = p.get_bTimePassed();
                            }

                            else { break; }
                        }


                        bt = p.get_bT();
                        btP = p.get_bTimePassed();
                        if (bt != btP)
                        {
                            if (bt - btP > 1)
                            {
                                p.set_bTimePassed(btP + 1);
                                begin++;
                            }
                            else
                            {
                                int b = p.get_bT();
                                waiting = waiting + (begin - (p.get_aT() + b));
                                begin++;
                                listBox2.Items.Add(p.get_processName() + " "
                                    + "waiting=" + (begin - (p.get_aT() + b)) + " Departure=" + begin);
                                p.set_bTimePassed(btP + 1);
                            }
                            if (!first)
                            {
                                gantt = gantt + " " + p.get_processName() + " |";
                            }
                            else
                            {
                                gantt = gantt + "| " + p.get_processName() + " |";
                                first = false;
                            }
                            ls.elmiTDeff(1, p);
                        }
                    }
                    ls.restPassedT();
                }
            }
            else if (comboBox1.Text == "Round Roben")
            {
                bool first = true;
                int q = (int)numericUpDown4.Value;
                lsrr.sort(0, size, lsrr.get_process());
                process p = lsrr.get_process();
                int begin = p.get_aT();
                while (begin< lsrr.tBT())
                {

                    if (p == null) { p = lsrr.get_process(); }

                    int loop = check(p);

                    for (int i = 0; i < loop; i++)
                    {
                        int cpbt = p.get_bTimePassed();
                        int bt = p.get_bT();
                        if (cpbt != bt)
                        {
                            if (bt - cpbt > q)
                            {
                                p.set_bTimePassed(cpbt + q);
                                begin = begin + q;
                            }
                            else
                            {
                                p.set_bTimePassed(bt);
                                begin = begin + (bt - cpbt);
                                waiting = waiting + (begin - (p.get_bT() + p.get_aT()));
                                listBox2.Items.Add(p.get_processName() + " "
                                + "waiting=" + (begin -(p.get_bT()+ p.get_aT())) + " Departure=" + begin);
                            }
                            if (!first)
                            {
                                gantt = gantt + " "+p.get_processName()+" |";
                            }
                            else {
                                gantt = gantt + "| " + p.get_processName() + " |";
                                first = false;
                            }
                            lsrr.elmiTDeff(q, p);
                            check(p);
                        }
                        if (p.get_next() != null)
                        {
                            if (p.get_next().get_aT() == p.get_next().get_timePassed())
                                p = p.get_next();
                        }
                        else p = null;
                        
                    }
                }

            }
            label7.Text = ""+(waiting / size);
            listBox3.Items.Clear();
            listBox3.Items.Add(gantt);          
            lsrr.restPassedT();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            pi = 1;
            textBox1.Text = "p" + pi;
            ls = new LinkedList();
            lsrr = new LinkedList();
        }
        public int check(process p)
        {
            int loop = 1;
            if (p.get_next() != null)
            {
                int ctp = p.get_timePassed();
                int ntp = p.get_next().get_timePassed();
                int c = p.get_aT() - ctp;
                int n = p.get_next().get_aT() - ntp;
                if (c == n)
                {
                    loop = ls.check(c, p);
                }
            }
            return loop;
        }
        
    }
}
