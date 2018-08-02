using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BitseriesType = System.Collections.Generic.List<int>;//user defined type
using RawDataType = System.Collections.Generic.List<double>;//user defined type for the raw data
namespace BitClustering
{
    public partial class Form1 : Form
    {
        private List<rec_and_ctrl> resize_list;
        private Size originalForm;

        public Form1()
        {
            InitializeComponent();
        }

       
        private void getValuesFromUsers(out int N_LENGTH, out int W_LENGTH, out int K_CENTERS, out string file_name)
        {
            N_LENGTH = Convert.ToInt16(this.txt_N_LENGTH.Text);
            W_LENGTH = Convert.ToInt16(this.txt_W_LENGTH.Text);
            K_CENTERS = Convert.ToInt16(this.txt_K_CENTERS.Text);
            file_name = this.txt_FileName.Text;
        }

        private void getValuesFromUsers_Enhancement(out int N_LENGTH, out int W_LENGTH, out double threshold, out string file_name)
        {
            N_LENGTH = Convert.ToInt16(this.txt_N_LENGTH.Text);
            W_LENGTH = Convert.ToInt16(this.txt_W_LENGTH.Text);
            threshold = Convert.ToDouble(this.txt_Threshold.Text);
            file_name = this.txt_FileName.Text;
        }

        private void getValuesFromUsers_HOTSAX(out int N_LENGTH, out int W_LENGTH, out string file_name)
        {
            N_LENGTH = Convert.ToInt16(this.txt_N_LENGTH.Text);
            W_LENGTH = Convert.ToInt16(this.txt_W_LENGTH.Text);
            file_name = this.txt_FileName.Text;
        }

        private void btn_algorithm2_Click(object sender, EventArgs e)
        {
            
            //get values frome user:
            int N_LENGTH, W_LENGTH, K_CENTERS;
            string file_name;

            //starting time
            getValuesFromUsers(out N_LENGTH, out W_LENGTH, out K_CENTERS, out file_name);

            var watch = System.Diagnostics.Stopwatch.StartNew();///calc execution time
            //call algorithm2
            Console.WriteLine("Calling BitClustering Li_Bray...");
            RawDataType data;
            List<int> cluster_belong;
            Tuple<double, int> result = Discord_Time_Series.bitClusterDiscord(out cluster_belong, out data, N_LENGTH, W_LENGTH, K_CENTERS, file_name);

            watch.Stop();//stop timer
            var elapsedMs = watch.ElapsedMilliseconds;

            this.txt_time.Text = elapsedMs.ToString();
            this.txt_best_so_far_dist.Text = result.Item1.ToString();
            this.txt_best_so_far_loc.Text = result.Item2.ToString();

            Console.WriteLine("Writing to file...");
            IOFunctions.writeFile_Cluster(data, N_LENGTH, result.Item2, cluster_belong);
            //Done
            Console.WriteLine(this.Text + " : \n\tDone Algorithm 2. Best_so_far_loc = "+ result.Item2 +". Time: "+ elapsedMs);
            Console.WriteLine("====================================================================");
        }

        private void btn_Squeezer_Click(object sender, EventArgs e)
        {
            //get values frome user:
            int N_LENGTH, W_LENGTH;
            double threshold;
            string file_name;

            //starting time
            getValuesFromUsers_Enhancement(out N_LENGTH, out W_LENGTH, out threshold, out file_name);

            var watch = System.Diagnostics.Stopwatch.StartNew();///calc execution time
            //call the function
            Console.WriteLine("Calling BitClustering + Squeezer ...");

            RawDataType data;
            List<int> cluster_belong;
            Tuple<double, int> result = Discord_Time_Series.squeezer(out cluster_belong, out data, N_LENGTH, W_LENGTH, threshold, file_name, Discord_Time_Series.BIT_SERIES);

            watch.Stop();//stop timer
            var elapsedMs = watch.ElapsedMilliseconds;

            this.txt_time.Text = elapsedMs.ToString();
            this.txt_best_so_far_dist.Text = result.Item1.ToString();
            this.txt_best_so_far_loc.Text = result.Item2.ToString();

            Console.WriteLine("Writing to file...");
            IOFunctions.writeFile_Cluster(data, N_LENGTH, result.Item2, cluster_belong);
            //Done
            Console.WriteLine(this.Text + " : \n\tDone \"BitClustering + Squeezer\" algorithm. Best_so_far_loc = " + result.Item2 + ". Time: " + elapsedMs);
            Console.WriteLine("====================================================================");
        }

       

        private void btn_Squeezer_PLA_Click(object sender, EventArgs e)
        {
            //get values frome user:
            int N_LENGTH, W_LENGTH;
            double threshold;
            string file_name;

            //starting time
            getValuesFromUsers_Enhancement(out N_LENGTH, out W_LENGTH, out threshold, out file_name);

            var watch = System.Diagnostics.Stopwatch.StartNew();///calc execution time
            //call the function
            Console.WriteLine("Calling BitClustering + Squeezer + PLA ...");

            RawDataType data;
            List<int> cluster_belong;
            Tuple<double, int> result = Discord_Time_Series.squeezer(out cluster_belong, out data, N_LENGTH, W_LENGTH, threshold, file_name, Discord_Time_Series.PLA);

            watch.Stop();//stop timer
            var elapsedMs = watch.ElapsedMilliseconds;

            this.txt_time.Text = elapsedMs.ToString();
            this.txt_best_so_far_dist.Text = result.Item1.ToString();
            this.txt_best_so_far_loc.Text = result.Item2.ToString();

            Console.WriteLine("Writing to file...");
            IOFunctions.writeFile_Cluster(data, N_LENGTH, result.Item2, cluster_belong);
          
            //Done
            Console.WriteLine(this.Text + " : \n\tDone BitClustering + Squeezer + PLA. Best_so_far_loc = " + result.Item2 + ". Time: " + elapsedMs);
            Console.WriteLine("====================================================================");
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            //Application.Restart();
            this.txt_time.Clear();
            this.txt_best_so_far_dist.Clear();
            this.txt_best_so_far_loc.Clear();
        }

        private void btn_HotSax_Click(object sender, EventArgs e)
        {
            //get values frome user:
            int N_LENGTH, W_LENGTH;
            string file_name;

            //starting time
            getValuesFromUsers_HOTSAX(out N_LENGTH, out W_LENGTH, out file_name);

            //fixed: hard code
            W_LENGTH = 3;
            
            var watch = System.Diagnostics.Stopwatch.StartNew();///calc execution time
            //call the function
            Console.WriteLine("Calling HOTSAX ...");

            RawDataType data;
            Tuple<double, int> result = Heuristic.Heuristic_Function(N_LENGTH, W_LENGTH, file_name, out data);

            watch.Stop();//stop timer
            var elapsedMs = watch.ElapsedMilliseconds;

            this.txt_time.Text = elapsedMs.ToString();
            this.txt_best_so_far_dist.Text = result.Item1.ToString();
            this.txt_best_so_far_loc.Text = result.Item2.ToString();

            Console.WriteLine("Writing to file...");
            IOFunctions.writeFile(data, N_LENGTH, result.Item2);

            //Done
            Console.WriteLine(this.Text + " : \n\tDone HOTSAX. Best_so_far_loc = " + result.Item2 + ". Time: " + elapsedMs);
            Console.WriteLine("====================================================================");
        }

        private void btn_Hotsax_Squeezer_Click(object sender, EventArgs e)
        {
            //get values frome user:
            int N_LENGTH, W_LENGTH;
            double threshold;
            string file_name;

            //starting time
            getValuesFromUsers_Enhancement(out N_LENGTH, out W_LENGTH, out threshold, out file_name);

            var watch = System.Diagnostics.Stopwatch.StartNew();///calc execution time
            //call the function
            Console.WriteLine("Calling Hotsax + Squeezer...");

            RawDataType data;
            List<int> cluster_belong;
            Tuple<double, int> result = Discord_Time_Series.squeezer(out cluster_belong, out data, N_LENGTH, W_LENGTH, threshold, file_name, Discord_Time_Series.PAA);

            watch.Stop();//stop timer
            var elapsedMs = watch.ElapsedMilliseconds;
            
            this.txt_time.Text = elapsedMs.ToString();
            this.txt_best_so_far_dist.Text = result.Item1.ToString();
            this.txt_best_so_far_loc.Text = result.Item2.ToString();

            Console.WriteLine("Writing to file...");
            IOFunctions.writeFile_Cluster(data, N_LENGTH, result.Item2, cluster_belong);

            //Done
            Console.WriteLine(this.Text + " : \n\tDone Hotsax + Squeezer. Best_so_far_loc = " + result.Item2 + ". Time: " + elapsedMs);
            Console.WriteLine("====================================================================");
        }

        private void setValues(string txt_FileName, string txt_K_CENTERS, string txt_W_LENGTH, string txt_Threshold, string txt_N_LENGTH)
        {
            this.txt_FileName.Text = txt_FileName;
            this.txt_K_CENTERS.Text = txt_K_CENTERS;
            this.txt_W_LENGTH.Text = txt_W_LENGTH;
            this.txt_Threshold.Text = txt_Threshold;
            this.txt_N_LENGTH.Text = txt_N_LENGTH;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBox1.SelectedItem.ToString())
            {
                case "TEK16.txt":
                    setValues("TEK16.txt", "28", "32", "0.85", "128");
                    break;
                case "TEK17.txt":
                    setValues("TEK17.txt", "28", "32", "0.85", "128");
                    break;
                case "chfdb_275.txt":
                    setValues("chfdb_275.txt", "28", "10", "0.85", "40");
                    break;
                case "chf01_275.txt":
                    setValues("chf01_275.txt", "28", "10", "0.85", "40");
                    break;
                case "chfdb_chf13_45590.txt":
                    setValues("chfdb_chf13_45590.txt", "28", "10", "0.85", "40");
                    break;
                case "ECG.txt":
                    setValues("ECG.txt", "28", "10", "0.85", "40");
                    break;
                case "mitdb__100_180.txt":
                    setValues("mitdb__100_180.txt", "28", "30", "0.85", "120");
                    break;
                case "nprs43.txt":
                    setValues("nprs43.txt", "28", "40", "0.85", "160");
                    break;
                case "nprs44.txt":
                    setValues("nprs44.txt", "28", "40", "0.85", "160");
                    break;
                case "power_data.txt":
                    setValues("power_data.txt", "28", "50", "0.85", "200");
                    break;
                case "ERP.csv":
                    setValues("ERP.csv", "28", "16", "0.85", "64");
                    break;

                default:
                    setValues("filename", "0", "0", "0", "0");
                    break;
            }//end  switch           

        }
        
  
        private void btn_RunTest_Click(object sender, EventArgs e)
        {
            btn_RunTest.Text = "Running...";
            btn_RunTest.Enabled = false;
            
            string[] method_names = new string[comboBox_Method.Items.Count];

            for (int i = 0; i < comboBox_Method.Items.Count; i++)
            {
                method_names[i] = comboBox_Method.Items[i].ToString();
            }

            if (string.IsNullOrEmpty(comboBox_Method.Text))
            {
                MessageBox.Show("Please select a Method.");
            }
            else
            {
                switch (comboBox_Method.SelectedItem.ToString())
                {
                    case "BitClustering Li_Braysy":
                        btn_algorithm2_Click(sender, e);
                        break;
                    case "BitCluster + Squeezer":
                        btn_Squeezer_Click(sender, e);
                        break;
                    case "BitCluster + Squeezer + PLA":
                        btn_Squeezer_PLA_Click(sender, e);
                        break;
                    case "HOT_SAX":
                        btn_HotSax_Click(sender, e);
                        break;
                    case "HOT_SAX + Squeezer":
                        btn_Hotsax_Squeezer_Click(sender, e);
                        break;

                    default:
                        {
                            MessageBox.Show("The method is not found", "Error !",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                }
             
            }//end else

            btn_RunTest.Enabled = true;
            btn_RunTest.Text = "Run Test";

        }//end func

        public IEnumerable<Control> GetAll(Control control, Type type = null)
        {
            var controls = control.Controls.Cast<Control>();
            if (type == null)
            {
                return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls);
            }
            else
                return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // set filename_values for combobox1
            string link = System.IO.Path.Combine(System.Environment.CurrentDirectory, @"Data\");
            string[] fileArray = System.IO.Directory.GetFiles(@link);
            foreach (string file in fileArray)
                this.comboBox1.Items.Add(System.IO.Path.GetFileName(file));//get all data filenames in the directory


            //dynamic control size
            var ctrl_list = GetAll(this);

            resize_list = new List<rec_and_ctrl>();
            foreach (Control control in ctrl_list)
            {
                resize_list.Add(new rec_and_ctrl(new Rectangle(control.Location.X, control.Location.Y, control.Width, control.Height), control));
            }

            originalForm = this.Size;

        }

        private void resizeChildren()
        {
            foreach (rec_and_ctrl resize_element in resize_list)
                resizeChildren_Control(resize_element.rec, resize_element.ctrl);

        }
        private void resizeChildren_Control(Rectangle rec, Control control)
        {
            float Xratio = this.Width / (float)(originalForm.Width);
            float Yratio = this.Height / (float)(originalForm.Height);

            int new_width = (int)(rec.Width * Xratio);
            int new_height = (int)(rec.Height * Yratio);
            int newX = (int)(rec.X * Xratio);
            int newY = (int)(rec.Y * Yratio);

            control.Location = new Point(newX, newY);
            control.Size = new Size(new_width, new_height);

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            resizeChildren();
        }
    }//end class
}//end file
