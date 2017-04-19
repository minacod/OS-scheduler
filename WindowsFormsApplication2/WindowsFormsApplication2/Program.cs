using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public class process {
        private process next;
        private string processName;
        private int aT;
        private int bT;
        private int priority;
        private int timePassed;
        private int bTimePassed;
        public process(string str, int at, int bt, int pri) {
            next = null;
            processName = str;
            aT = at;
            bT = bt;
            priority = pri;
            timePassed = 0;
            bTimePassed = 0;
            }
        public void set_timePassed(int i) { timePassed = i; }
        public int get_timePassed() { return timePassed; }
        public void set_bTimePassed(int i) { bTimePassed = i; }
        public int get_bTimePassed() { return bTimePassed; }
        public void set_next(process p) { next = p; }
        public process get_next() { return next; }
        public void set_processName(string s) { processName = s; }
        public string get_processName() { return processName; }
        public void set_aT(int i) { aT = i; }
        public int get_aT() { return aT; }
        public void set_bT(int i) { bT = i; }
        public int get_bT() { return bT; }
        public void set_priority(int i) { priority = i; }
        public int get_priority() { return priority; }
    }

    public class LinkedList
    {
        private process head;

        public LinkedList() {
            head = null;
        }
        public bool isEmpty() {
            if (head == null) return true;
            else return false;
        }
        public void Add(string str,int aT,int bT,int priority)
        {
            process nw = new process(str, aT, bT, priority);
            if (isEmpty()){ head = nw; }
            else{
                process tmp=head;
                head = nw;
                head.set_next(tmp);
            }
        }

        public void sort(int sel, int siz, process p)
        {
            process tmp = p;
            int n = siz;
            int x, y;
            for (x = 0; x < n; x++)
            {
                process tmp2 = tmp.get_next();
                for (y = 0; y < n-x- 1; y++)
                {
                    if (sel == 0) {
                        int c = tmp.get_aT()-tmp.get_timePassed();
                        int nn = tmp2.get_aT()-tmp.get_timePassed();
                        if (c > nn)
                        {
                            swap(tmp, tmp2);
                        }
                    }
                    else if (sel == 1)
                    {
                        
                        int c = tmp.get_bT()-tmp.get_bTimePassed();
                        int nn = tmp2.get_bT()-tmp2.get_bTimePassed();
                        if (c > nn)
                        {
                            swap(tmp, tmp2);
                        }
                    }
                    else if (sel == 2)
                    {

                        int c = tmp.get_priority();
                        int nn = tmp2.get_priority();
                        if (c > nn)
                        {
                            swap(tmp, tmp2);
                        }
                    }
                    tmp2 =tmp2.get_next();
                }
                tmp=tmp.get_next();
            }
        }
        
        public void swap(process tmp,process tmp2) {
            string tmpStr = tmp2.get_processName();
            int tmpAT = tmp2.get_aT();
            int tmpBT = tmp2.get_bT();
            int tmpPir = tmp2.get_priority();
            int tmpatp = tmp2.get_timePassed();
            int tmpbtp = tmp2.get_bTimePassed();
            tmp2.set_aT(tmp.get_aT());
            tmp2.set_bT(tmp.get_bT());
            tmp2.set_processName(tmp.get_processName());
            tmp2.set_priority(tmp.get_priority());
            tmp2.set_timePassed(tmp.get_timePassed());
            tmp2.set_bTimePassed(tmp.get_bTimePassed());
            tmp.set_aT(tmpAT);
            tmp.set_bT(tmpBT);
            tmp.set_processName(tmpStr);
            tmp.set_priority(tmpPir);
            tmp.set_timePassed(tmpatp);
            tmp.set_bTimePassed(tmpbtp);

        }
        public int size()
        {
            if (isEmpty()) return 0;
            int i = 1;
            process tmp = head.get_next();
            while (tmp!=null) {
                i++;
                tmp=tmp.get_next();
            }
            return i;
        }
        public int tBT()
        {
            if (isEmpty()) return 0;
            process tmp = head;
            int i = 0;
            while (tmp != null)
            {
                i = i + tmp.get_bT();
                tmp = tmp.get_next();
            }
            return i;
        }
        public void elmiTDeff(int bbT,process p) {
            process tmp = p;
            while (tmp != null) {
                int at = tmp.get_aT();
                int tbbT = bbT + tmp.get_timePassed();
                if (at- tmp.get_timePassed() != 0) {

                    if (at > tbbT)
                        tmp.set_timePassed(tbbT);
                    else
                        tmp.set_timePassed(at);
                    
                }
                tmp = tmp.get_next();
            }
        }
        int[] allEle() {
            process tmp = head;
            int s = size();
            int[] arr = new int[s];
            for(int i = 0; i < s; i++)
            {
                arr[i] = tmp.get_aT();
                tmp = tmp.get_next();
            }
            return arr;
        }
        public void restPassedT()
        {
            if (isEmpty()) return;
            head.set_timePassed(0);
            head.set_bTimePassed(0);
            process tmp = head.get_next();
            while (tmp != null)
            {
                tmp.set_timePassed(0);
                tmp.set_bTimePassed(0);
                tmp = tmp.get_next();
            }
        }
        public int check(int x,process p)
        {
            int i = 0;
            process tmp=p;
            while (tmp != null)
            {
                if (tmp.get_aT()-tmp.get_timePassed() == x) 
                    i++;
                tmp=tmp.get_next();
            }
            return i;
        }
        public process get_process() {
            return head;
        }


    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
