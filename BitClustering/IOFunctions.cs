using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawDataType = System.Collections.Generic.List<double>;//user defined type for the raw data

namespace BitClustering
{
    class IOFunctions
    {
        static public RawDataType readFile(string file_name, int lines_to_skip = 1)
        {
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\", file_name);
            RawDataType data = new RawDataType();

            //read file
            string[] lines = System.IO.File.ReadAllLines(path);

            for (int i = 0; i < lines_to_skip; i ++)
                //skip one line from the file
                lines = lines.Skip(1).ToArray();


            foreach (string line in lines)
            {
                data.Add(Convert.ToDouble(line)); //convert into a double list then add to 'data'
            }

            return data;
        }//end ReadFileIntoList function

        static public void writeFile(RawDataType data, int N_LENGTH, int best_so_far_loc)
        {
            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Output\", "output_BitClustering.csv");
            string delimiter = ",";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Join(delimiter, new string[] { "index", "data", "is_discord" }));
            for (int index = 0; index < data.Count; index++)
            {
                string is_discord = "N";
                if (index >= best_so_far_loc && index <= best_so_far_loc + N_LENGTH - 1)
                    is_discord = "Y";
                sb.AppendLine(string.Join(delimiter, new string[] { index.ToString(), data[index].ToString(), is_discord }));
            }
            System.IO.File.WriteAllText(filePath, sb.ToString());
        }


        static public void writeFile_Cluster(RawDataType data, int N_LENGTH, int best_so_far_loc, List<int> cluster_belong)
        {
            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Output\", "output_Cluster.csv");
            string delimiter = ",";

            List<int> cluster_belong_2 = new List<int>();
            for (int i =0; i < data.Count - cluster_belong.Count; i++ )
            {
                cluster_belong_2.Add(-1);
            }
            cluster_belong_2 = cluster_belong_2.Concat(cluster_belong).ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Join(delimiter, new string[] { "index", "data", "is_discord", "cluster_belong" }));
            for (int index = 0; index < data.Count; index++)
            {
                string is_discord = "N";
                if (index >= best_so_far_loc && index <= best_so_far_loc + N_LENGTH - 1)
                    is_discord = "Y";
                sb.AppendLine(string.Join(delimiter, new string[] { index.ToString(), data[index].ToString(), is_discord, cluster_belong_2[index].ToString() }));
            }
            System.IO.File.WriteAllText(filePath, sb.ToString());
        }


    }
}
