/*
 * Algorithm 2
 * /
 * */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitseriesType = System.Collections.Generic.List<int>;//user defined type 
using RawDataType = System.Collections.Generic.List<double>;//user defined type for the raw data

namespace BitClustering
{
    static class Discord_Time_Series
    {
        private const double INFINITE = 99999;
        public const int PAA = 3;
        public const int PLA = 2;
        public const int BIT_SERIES = 1;

        static private Random random_obj = new Random();// helping for making a random order

        //static private void Shuffle_Generic<T>(this IList<T> list)
        //{
        //    int n = list.Count;
        //    while (n > 1)
        //    {
        //        n--;
        //        int k = random_obj.Next(n + 1);
        //        T value = list[k];
        //        list[k] = list[n];
        //        list[n] = value;
        //    }
        //}//end Shuffle function

        private static void Shuffle2(this List<int> list)
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

        static public List<int> getOuterLoop(List<Cluster> b_cluster, int K_CLUSTERS)
        {
            List<Cluster> ordered_bCluster = b_cluster.OrderBy(obj => obj.getNumberOfMembers()).ToList();

            List<int> outer_loop = new List<int>();

           // outer_loop.AddRange(ordered_bCluster[0].getListMemberIndice());
           // outer_loop.Shuffle2();

            for (int i = 0; i < K_CLUSTERS; i++)
            {
                outer_loop.AddRange(ordered_bCluster[i].getListMemberIndice());
            }
            return outer_loop;
        }

        //note: 'cluster_of_cur_outer' is a value of the cluster containing current outer element. 
        //( 'cluster_of_cur_outer'runs from 0 to (K_CLUSTERS-1) );
        static List<int> getInnerLoop(List<Cluster> b_cluster, int K_CLUSTERS, int cluster_of_cur_outer)
        {
            List<int> inner_loop = new List<int>();

            //append the same cluster first
            inner_loop.AddRange(b_cluster[cluster_of_cur_outer].getListMemberIndice());
            //inner_loop.Shuffle2();

            for (int cluster_remainder = 0; cluster_remainder < K_CLUSTERS; cluster_remainder++)
            {
                if (cluster_remainder != cluster_of_cur_outer)
                    inner_loop.AddRange(b_cluster[cluster_remainder].getListMemberIndice());
            }
            return inner_loop;
        }


        static public List<int> getOuterLoop(List<Cluster_Squeezer> b_cluster, int K_CLUSTERS)
        {
            List<Cluster_Squeezer> ordered_bCluster = b_cluster.OrderBy(obj => obj.getCluster().getNumberOfMembers()).ToList();

            List<int> outer_loop = new List<int>();

            //outer_loop.AddRange(ordered_bCluster[0].getCluster().getListMemberIndice());
            //outer_loop.Shuffle2();

            for (int i = 0; i < K_CLUSTERS; i++)
            {
                outer_loop.AddRange(ordered_bCluster[i].getCluster().getListMemberIndice());
            }
            return outer_loop;
        }

        //note: 'cluster_of_cur_outer' is a value of the cluster containing current outer element. 
        //( 'cluster_of_cur_outer'runs from 0 to (K_CLUSTERS-1) );
        static List<int> getInnerLoop(List<Cluster_Squeezer> b_cluster, int K_CLUSTERS, int cluster_of_cur_outer)
        {
            List<int> inner_loop = new List<int>();

            //append the same cluster first
            inner_loop.AddRange(b_cluster[cluster_of_cur_outer].getCluster().getListMemberIndice());
            inner_loop.Shuffle2();

            for (int cluster_remainder = 0; cluster_remainder < K_CLUSTERS; cluster_remainder++)
            {
                if (cluster_remainder != cluster_of_cur_outer)
                    inner_loop.AddRange(b_cluster[cluster_remainder].getCluster().getListMemberIndice());
            }
      
            return inner_loop;
        }


        static double distanceBetween2Clusters(RawDataType p_center, RawDataType q_center, double radius_p, double radius_q)
        {
            double dist = Math.Sqrt(p_center.Zip(q_center, (a, b) => (a - b) * (a - b)).Sum()) - radius_p - radius_q;
            return dist;

        }

        //algorithm 2:
        static public Tuple<double, int> bitClusterDiscord(out List<int> cluster_belong, out RawDataType data, int N_LENGTH, int W_LENGTH, int K_CENTERS, string file_name)
        {
          
            //read data 
            data = IOFunctions.readFile(file_name); //raw timeseries data

            //store Bit series data
           // Dictionary<int, BitseriesType> bit_series_data = HelperFunctions.bitSeriesDataset(data, N_LENGTH, W_LENGTH);//get bit series data from original data

            //line 6: run algorithm 1 to have b_cluster
            List<Cluster> b_cluster;


            var watch = System.Diagnostics.Stopwatch.StartNew();///calc execution time
            //call the function
          
            BitCluster.bitCluster(out b_cluster, out cluster_belong, K_CENTERS, N_LENGTH, W_LENGTH, file_name);

            watch.Stop();//stop timer
            var elapsedMs = watch.ElapsedMilliseconds;

          

            Console.WriteLine("_________bitCluster is Done in "+ elapsedMs + ".\nKeep going...Please wait");
            

            // get Outer loop:
            List<int> outer_loop = getOuterLoop(b_cluster, K_CENTERS); //get outer

            List<int> inner_loop;
            bool continue_to_outer_loop = false;

            double nearest_neighbor_dist = 0;
            double dist = 0;

            double best_so_far_dist = 0;
            int best_so_far_loc = 0;

            RawDataType p_center, q_center;

            bool[] is_skip_at_p = new bool[outer_loop.Count];
            for (int i = 0; i < outer_loop.Count; i++)
                is_skip_at_p[i] = false;

            int cluster_of_cur_outer;//thang p dang nam o cluster nao
            int cluster_of_cur_inner;// tracing for q's cluster at inner loop

            //test
            int count_skip_at_p = 0;
            int count_break_sub_2_dis = 0;
            int count_dis_smaller_best_so_far = 0;
            int count_center_p_diff_center_q = 0;

            foreach (int p in outer_loop)
            {
                if (is_skip_at_p[p])
                {
                    count_skip_at_p++;
                    //p was visited at inner loop before
                    continue;
                }
                else
                {
                    nearest_neighbor_dist = INFINITE;

                    cluster_of_cur_outer = cluster_belong[p];

                    inner_loop = getInnerLoop(b_cluster, K_CENTERS, cluster_of_cur_outer);

                    foreach (int q in inner_loop)// inner loop
                    {
                        if (Math.Abs(p - q) < N_LENGTH)
                        {
                            continue;// self-match => skip to the next one
                        }
                        else
                        {

                           /* cluster_of_cur_inner = cluster_belong[q];
                            if (cluster_of_cur_inner  != cluster_of_cur_outer)
                            {
                                count_center_p_diff_center_q++;
                                p_center = data.GetRange(b_cluster[cluster_of_cur_outer].getCenterIndex(), N_LENGTH);
                                q_center = data.GetRange(b_cluster[cluster_of_cur_inner].getCenterIndex(), N_LENGTH);

                                if (distanceBetween2Clusters(p_center, q_center, b_cluster[cluster_of_cur_outer].getRadius(), b_cluster[cluster_of_cur_inner].getRadius()) >= best_so_far_dist)
                                {
                                    count_break_sub_2_dis++;
                                    continue_to_outer_loop = true;
                                    break;
                                }
                            }*/

                         
                            //calculate the Distance between p and q
                            dist = HelperFunctions.gaussDistance(data.GetRange(p, N_LENGTH), data.GetRange(q, N_LENGTH));

                            if (dist < best_so_far_dist)
                            {
                                //skip the element q at oute_loop, 'cuz if (p,q) is not a solution, so does (q,p).
                                is_skip_at_p[q] = true;
                                count_dis_smaller_best_so_far++;
                                continue_to_outer_loop = true; //break, to the next loop at outer_loop
                                break;// break at inner_loop first
                            }

                            if (dist < nearest_neighbor_dist)
                            {
                                nearest_neighbor_dist = dist;
                            }
                        }//end else
                    }//end inner
                    if (continue_to_outer_loop)
                    {
                        continue_to_outer_loop = false;//reset
                        continue;//go to the next p in outer loop
                    }

                    if (nearest_neighbor_dist > best_so_far_dist)
                    {
                        best_so_far_dist = nearest_neighbor_dist;
                        best_so_far_loc = p;
                    }
                }//end else


            }//end outter

            Console.WriteLine("count_skip_at_p =" + count_skip_at_p);
            Console.WriteLine("======count_break_sub_2_dis =" + count_break_sub_2_dis);
            Console.WriteLine("count_dis_smaller_best_so_far =" + count_dis_smaller_best_so_far);
            Console.WriteLine("count_center_p_diff_center_q=" + count_center_p_diff_center_q);


            return new Tuple<double, int>(best_so_far_dist, best_so_far_loc);
        }//end bitClusterDiscord()





        static public Tuple<double, int> squeezer(out List<int> cluster_belong, out RawDataType data, int N_LENGTH, int W_LENGTH, double threshold, string file_name, int type_convert_raw_data)
        {

            //read data 
            data = IOFunctions.readFile(file_name); //raw timeseries data

            List<Cluster_Squeezer> b_cluster;


            var watch = System.Diagnostics.Stopwatch.StartNew();///calc execution time
            //call the function
            BitCluster.squeezerCluster(data, type_convert_raw_data, out b_cluster, out cluster_belong, threshold, N_LENGTH, W_LENGTH, file_name);

            watch.Stop();//stop timer
            var elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("________squeezerCluster is Done in " + elapsedMs + ".\nKeep going...Please wait");
         
           
            
            // get Outer loop:
            List<int> outer_loop = getOuterLoop(b_cluster, b_cluster.Count); //get outer

            List<int> inner_loop;
            bool continue_to_outer_loop = false;

            double nearest_neighbor_dist = 0;
            double dist = 0;

            double best_so_far_dist = 0;
            int best_so_far_loc = 0;

            RawDataType p_center, q_center;

            bool[] is_skip_at_p = new bool[outer_loop.Count];
            for (int i = 0; i < outer_loop.Count; i++)
                is_skip_at_p[i] = false;

            int cluster_of_cur_outer;//thang p dang nam o cluster nao
            int cluster_of_cur_inner;// tracking for q's cluster at inner loop

            //test
            int count_skip_at_p = 0;
            int count_break_sub_2_dis = 0;
            int count_dis_smaller_best_so_far = 0;
            int count_center_p_diff_center_q = 0;

            foreach (int p in outer_loop)
            {

                if (is_skip_at_p[p])
                {
                    count_skip_at_p++;
                    //p was visited at inner loop before
                    continue;
                }
                else
                {
                    nearest_neighbor_dist = INFINITE;

                    cluster_of_cur_outer = cluster_belong[p];

                    inner_loop = getInnerLoop(b_cluster, b_cluster.Count, cluster_of_cur_outer);

                    foreach (int q in inner_loop)// inner loop
                    {
                        if (Math.Abs(p - q) < N_LENGTH)
                        {
                            continue;// self-match => skip to the next one
                        }
                        else
                        {
                            /*cluster_of_cur_inner = cluster_belong[q];
							
							if (cluster_of_cur_inner != cluster_of_cur_outer)
							{
                                count_center_p_diff_center_q++;
								p_center = data.GetRange(b_cluster[cluster_of_cur_outer].getCluster().getCenterIndex(), N_LENGTH);
								q_center = data.GetRange(b_cluster[cluster_of_cur_inner].getCluster().getCenterIndex(), N_LENGTH);

								if (distanceBetween2Clusters(p_center, q_center, b_cluster[cluster_of_cur_outer].getCluster().getRadius(), b_cluster[cluster_of_cur_inner].getCluster().getRadius()) >= best_so_far_dist)
								{
									continue_to_outer_loop = true;
                                    count_break_sub_2_dis++;
									break;
								}
							}
                            */

                            //calculate the Distance between p and q
                            dist = HelperFunctions.gaussDistance(data.GetRange(p, N_LENGTH), data.GetRange(q, N_LENGTH));

                            if (dist < best_so_far_dist)
                            {
                                //skip the element q at oute_loop, 'cuz if (p,q) is not a solution, so does (q,p).
                                is_skip_at_p[q] = true;
                                count_dis_smaller_best_so_far++;

                                continue_to_outer_loop = true; //break, to the next loop at outer_loop
                                break;// break at inner_loop first
                            }

                            if (dist < nearest_neighbor_dist)
                            {
                                nearest_neighbor_dist = dist;
                            }
                        }//end else
                    }//end inner
                    if (continue_to_outer_loop)
                    {
                        continue_to_outer_loop = false;//reset
                        continue;//go to the next p in outer loop
                    }

                    if (nearest_neighbor_dist > best_so_far_dist)
                    {
                        best_so_far_dist = nearest_neighbor_dist;
                        best_so_far_loc = p;
                    }
                }//end else


            }//end outter

            Console.WriteLine("count_skip_at_p =" + count_skip_at_p);
            Console.WriteLine("=======count_break_sub_2_dis =" + count_break_sub_2_dis);
            Console.WriteLine("count_dis_smaller_best_so_far =" + count_dis_smaller_best_so_far);
            Console.WriteLine("count_center_p_diff_center_q=" + count_center_p_diff_center_q);


            return new Tuple<double, int>(best_so_far_dist, best_so_far_loc);
        }//end bitClusterDiscord_Enhancement()


    }
}
