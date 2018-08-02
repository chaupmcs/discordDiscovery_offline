using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitClustering
{
    static class Heuristic
    {

        static private Random random_obj = new Random();// helping for making a random order
        private const double NEW_MAX = 0.84;
        private const double NEW_MIN = -0.84;
        public const double GAUSS_1 = -0.43;
        public const double GAUSS_2 = 0.43;
        private const double INFINITE = 99999;

        static public double MinMax_Normalization(double value, double min, double max)
        {
            return ((value - min) / (max - min)) * (NEW_MAX - NEW_MIN) + NEW_MIN;
        }

        static public double Distance(List<double> t1, List<double> t2)
        {
            double dist = Math.Sqrt(t1.Zip(t2, (a, b) => (a - b) * (a - b)).Sum());
            return dist;
        }

        // Make the tree and tables function
        static public void MakeTree_And_Table_Function(List<double> data, int N_LENGTH, out AugmentedTrie tree, 
            out Dictionary<string, int> count_table, out Dictionary<int, string> total_table, int W_DIMENSION)
        {
            double min = data.Min();
            double max = data.Max();


            count_table = new Dictionary<string, int>();
            total_table = new Dictionary<int, string>();

            List<double> C_n;
            List<double> C_w = new List<double>();//  w dimension


            //convert time series C of length into a w dimensional, then transform to SAX word:
            for (int start_point_subtimeseries = 0; start_point_subtimeseries + N_LENGTH - 1 <= data.Count - 1; start_point_subtimeseries++)//go through timeseries by the window "N_LENGTH"
            {
                C_n = data.GetRange(start_point_subtimeseries, N_LENGTH);//get the sub_timeseries
                C_w.Clear();// reset C_w
                string s = String.Empty;//initialize the SAX word


                //if  N_LENGTH % Constants.W_DIMENSION != 0, then we do PAA as the formala (quite complex and take time):
                if (N_LENGTH % W_DIMENSION != 0)
                {
                    for (int i = 0; i < W_DIMENSION; i++)
                        C_w.Add(0);//set initial value for C_w

                    for (int i = 0; i < N_LENGTH * W_DIMENSION; i++)
                    {
                        C_w[i / N_LENGTH] += C_n[i / W_DIMENSION];
                    }

                    for (int i = 0; i < W_DIMENSION; i++)
                    {
                        C_w[i] = MinMax_Normalization(C_w[i], min, max);
                        //Convert  c_i to SAX
                        if (C_w[i] <= GAUSS_1)
                        {
                            s += "a";
                        }
                        else if (C_w[i] >= GAUSS_2)
                        {
                            s += "c";
                        }
                        else
                        {
                            s += "b";
                        }
                    }
                }
                else//else, we can simple split it into the same size, this help to reduce the execution time*/
                {

                    double c_i;
                    int from_index, to_index;

                    //Calculate C_w 
                    for (int start_point_w = 0; start_point_w < W_DIMENSION; start_point_w++)
                    {
                        from_index = (N_LENGTH / W_DIMENSION) * start_point_w;
                        to_index = (N_LENGTH / W_DIMENSION) * (start_point_w + 1) - 1;

                        c_i = 0;
                        for (int i = from_index; i <= to_index; i++)
                        {
                            c_i += C_n.ElementAt(i);
                        }
                        c_i = c_i * (W_DIMENSION / (double)(N_LENGTH));

                        c_i = MinMax_Normalization(c_i, min, max);

                        //Convert  c_i to SAX
                        if (c_i <= GAUSS_1)
                        {
                            s += "a";
                        }
                        else if (c_i >= GAUSS_2)
                        {
                            s += "c";
                        }
                        else
                        {
                            s += "b";
                        }

                        C_w.Add(c_i);

                    }//end for loop-calc SAX word     
                }//end else


                total_table.Add(start_point_subtimeseries, s);

                if (count_table.ContainsKey(s))
                {
                    count_table[s]++;//if it did have, just plus 1 to 'count_table'
                }
                else
                {
                    count_table.Add(s, 1);// else, we make a new one
                }
            }// end for _ go through time series  by the window

            //Making the root node:
            TreeNode root = new TreeNode('R');

            //init the path (to print the tree later)
            List<char> path = new List<char>();

            // Making the augmented tree:
            tree = new AugmentedTrie(root, path);

            tree.CreateTheAugmentedTrie();

            // appending the indice to the tree.
            foreach (KeyValuePair<int, string> pair in total_table)
            {
                tree.AddTheDataToLeaf(pair.Value, pair.Key);
            }


        } //end MakeTree_AND_Table function


        //Shuffle function for List<int>
        private static void Shuffle(this List<int> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random_obj.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }//end Shuffle function


        // outer loop
        private static List<int> OuterArrangement(Dictionary<int, string> total_table, Dictionary<string, int> count_table)
        {
            List<int> outter_list = new List<int>();
            List<int> outer_random = new List<int>();

            foreach (KeyValuePair<int, string> pair in total_table)
            {
                if (count_table[pair.Value] == 1)
                    outter_list.Add(pair.Key);
                else
                    outer_random.Add(pair.Key);
            }

            //make outer_random randomly
            outer_random.Shuffle();

            outter_list = outter_list.Concat(outer_random).ToList();
            return outter_list;
        }//end OuterArrangement


        // inner loop
        private static List<int> InnerArrangement(Dictionary<int, string> total_table, Dictionary<string, int> count_table, AugmentedTrie tree, string word)
        {
            TreeNode the_leaf = tree.FindtheLeaf(word);
            List<int> inner_list = new List<int>(the_leaf.GetDataNode());//go through the data first

            bool[] over_arr = new bool[total_table.Count];
            for (int i = 0; i < total_table.Count; i++)
                over_arr[i] = false;

            foreach (int element in inner_list)
            {
                over_arr[element] = true;
            }

            List<int> inner_random = new List<int>();

            for (int i = 0; i < total_table.Count; i++)
            {
                if (over_arr[i] == false)
                    inner_random.Add(i);
            }

            //make random_inner randomly
            inner_random.Shuffle();

            inner_list = inner_list.Concat(inner_random).ToList();

            return inner_list;
        }//end innerArrangement


        // Heuristic function
        public static Tuple<double, int> Heuristic_Function(int N_LENGTH, int W_DIMENSION, string file_name, out List<double> data)
        {
            data = IOFunctions.readFile(file_name);
            Dictionary<string, int> count_table;
            Dictionary<int, string> total_table;
            AugmentedTrie tree;

            MakeTree_And_Table_Function(data, N_LENGTH, out tree, out count_table, out total_table, W_DIMENSION);

            double best_so_far_dist = 0;
            int best_so_far_loc = 0;

            double nearest_neighbor_dist = 0;
            double dist = 0;

            List<int> outer_list, inner_list;

            outer_list = OuterArrangement(total_table, count_table);
            bool break_to_outer_loop = false;

            bool[] is_skip_at_p = new bool[outer_list.Count];
            for (int i = 0; i < outer_list.Count; i++)
                is_skip_at_p[i] = false;

            foreach (int p in outer_list)
            {
                //if (is_skip_at_p[p] || Math.Abs(p - 10867) < N_LENGTH || Math.Abs(p - 3994) < N_LENGTH 
                //    || Math.Abs(p - 13492) < N_LENGTH)// 

                if (is_skip_at_p[p])
                {
                    //p was visited at inner loop before
                    continue;
                }
                else
                {
                    nearest_neighbor_dist = INFINITE;
                    string word = total_table[p];

                    inner_list = InnerArrangement(total_table, count_table, tree, word);

                    foreach (int q in inner_list)// inner loop
                    {
                        if (Math.Abs(p - q) < N_LENGTH)
                        {
                            continue;// self-match => skip to the next one
                        }
                        else
                        {
                            //calculate the Distance between p and q
                            dist = Distance(data.GetRange(p, N_LENGTH), data.GetRange(q, N_LENGTH));

                            if (dist < best_so_far_dist)
                            {
                                //skip the element q at oute_loop, 'cuz if (p,q) is not a solution, so does (q,p).
                                is_skip_at_p[q] = true;

                                break_to_outer_loop = true; //break, to the next loop at outer_loop
                                break;// break at inner_loop first
                            }

                            if (dist < nearest_neighbor_dist)
                            {
                                nearest_neighbor_dist = dist;
                            }
                        }
                    }//end inner
                    if (break_to_outer_loop)
                    {
                        break_to_outer_loop = false;//reset
                        continue;//go to the next p in outer loop
                    }

                    if (nearest_neighbor_dist > best_so_far_dist)
                    {
                        best_so_far_dist = nearest_neighbor_dist;
                        best_so_far_loc = p;
                    }
                }//end else


            }//end outter

            Tuple<double, int> result = new Tuple<double, int>(best_so_far_dist, best_so_far_loc);
            //Console.WriteLine("skip = " + number_skip);

            return result;
        }// end Heuristic function



        /*
        private static void Shuffle_Generic<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random_obj.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }//end Shuffle function
        */

    }
}
