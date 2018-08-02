using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitseriesType = System.Collections.Generic.List<int>;//user defined type
using RawDataType = System.Collections.Generic.List<double>;//user defined type for the raw data


namespace BitClustering
{
    static class BitCluster
    {
        static private Random random_obj = new Random();// helping for making a random order
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


        static private List<int> getKRandomCenter(int bit_series_data_length, int NUMBER_OF_CENTERS)
        {
            List<int> k_centers = new List<int>(); //store K_centers
            Random r = new Random();

            while (k_centers.Count < NUMBER_OF_CENTERS)
            {
                int index = r.Next(bit_series_data_length);
                if (k_centers.Contains(index) == false)
                {
                    k_centers.Add(index);
                }
            }

            return k_centers;
        }

        static public List<int> getKCentersContinuously(int NUMBER_OF_CENTERS)
        {
            List<int> k_indice = new List<int>();

            for (int i = 0; i < NUMBER_OF_CENTERS; i++)
                k_indice.Add(i);
            return k_indice;
        }

        static public void bitCluster(out List<Cluster> b_cluster, out List<int> cluster_belong, int K_CENTERS, int N_LENGTH, int W_LENGTH, string file_name)
        {

            //read data 
            RawDataType data = IOFunctions.readFile(file_name);

            Dictionary<int, BitseriesType> bit_series_data;//store Bit series data

            bit_series_data = HelperFunctions.bitSeriesDataset(data, N_LENGTH, W_LENGTH);//get bit series data from original data


            //set some variables:
            int bit_series_data_length = bit_series_data.Count;



            b_cluster = new List<Cluster>();//store k clusters

            List<int> k_centers;//store k centers

            cluster_belong = new List<int>(); //store the cluster whose each point belong to

            double dist_i, dist_p;

            for (int i = 0; i < bit_series_data_length; i++)
            {
                cluster_belong.Add(-1);
            }

            //initialize K centers
            k_centers = getKRandomCenter(bit_series_data_length, K_CENTERS);
            //k_centers = getKCentersContinuously(K_CENTERS);

            
            //(line 1 - 4 of the paper): initialize k cluster, then append them to 'b_cluster' 
            for (int i = 0; i < K_CENTERS; i++)
            {
                Cluster one_cluster = new Cluster(k_centers[i], 1);
                b_cluster.Add(one_cluster);
                b_cluster[i].addToListMemberIndice(k_centers[i]);
                cluster_belong[k_centers[i]] = i;
            }


            //(line 5-15 in the paper): 
            for (int j = 0; j < bit_series_data_length; j++)//go through the BD data
            {
                //get each element:
                if (cluster_belong[j] == (-1)) //if the point hasn't belong any cluster yet
                {
                    int p = 0;
                    dist_p = HelperFunctions.bitSeriesDistance(bit_series_data[j], bit_series_data[b_cluster[0].getCenterIndex()]);
                    for (int i = 1; i < K_CENTERS; i++)
                    {
                        dist_i = HelperFunctions.bitSeriesDistance(bit_series_data[j], bit_series_data[b_cluster[i].getCenterIndex()]);
                        
                        if (dist_i < dist_p)
                        {
                            p = i;
                            dist_p = dist_i;
                        }
                           
                    }//end for

                    // line 12-14 in the paper
                    cluster_belong[j] = p;
                    b_cluster[p].plusOneToNumberOfMembers();
                    b_cluster[p].addToListMemberIndice(j);

                }
            }//end of for

            foreach (Cluster list_members in b_cluster)
            {
                list_members.getListMemberIndice().Shuffle();
            }

            Console.WriteLine("End shuffle.");

            //double radius;
            // List<double> center_value;
            // for (int i = 0; i < K_CENTERS; i++)
            // {
            //     center_value = data.GetRange(k_centers[i], N_LENGTH);
            //     radius = HelperFunctions.calculateRadius(data, b_cluster[i].getListMemberIndice(), center_value, N_LENGTH);
            //     b_cluster[i].setRadius(radius);
            // }

        }//end of function


        static public double support(Cluster_Squeezer cluster_Squeezer, int element, int pos)
        {
            List<Dictionary<int, int>> count_elements = cluster_Squeezer.getCountElements();

            if (!((count_elements[pos]).ContainsKey(element)))
                return 0;
            else
                return  (double)((count_elements[pos])[element]) / cluster_Squeezer.getCluster().getNumberOfMembers();

        }


        static public double simComputation(Cluster_Squeezer cluster, BitseriesType bit_data)
        {
            int bit_len = bit_data.Count;
            double sim = 0;

            for (int i = 0; i < bit_len; i++)
            {
                sim += support(cluster, bit_data[i], i);
            }
            return (sim/ bit_len);
        }


        static public void squeezerCluster(RawDataType data, int type_convert_raw_data,  out List<Cluster_Squeezer> b_cluster, out List<int> cluster_belong, double threshold, int N_LENGTH, int W_LENGTH, string file_name)
        {
            //store Bit series data
            Dictionary<int, BitseriesType> bit_series_data;
           

            if (type_convert_raw_data == Discord_Time_Series.BIT_SERIES)
            {
                //get bit series data from original data
                bit_series_data = HelperFunctions.bitSeriesDataset(data, N_LENGTH, W_LENGTH);
            }
            else
            {
                if (type_convert_raw_data == Discord_Time_Series.PLA)
                {
                    bit_series_data = HelperFunctions.PLA(data, N_LENGTH, W_LENGTH);
                }

                else
                {
                    bit_series_data = HelperFunctions.PAA(data, N_LENGTH, W_LENGTH);
                }


            }

         
            //set some variables:
            int bit_series_data_length = bit_series_data.Count;

            b_cluster = new List<Cluster_Squeezer>();//store clusters

            cluster_belong = new List<int>(); //store the Cluster_Squeezer whose each point belong to

            //set initial values for cluster_belong: '-1'  here imply they dont lie in any clusters
            for (int i = 0; i < bit_series_data_length; i++)
            {
                cluster_belong.Add(-1);
            }


            //initialize the first data as a center point
            b_cluster.Add(new Cluster_Squeezer(new Cluster(0, 1), new List<Dictionary<int, int>>()));// or 0 ??
            b_cluster[0].getCluster().addToListMemberIndice(0);
            b_cluster[0].updateCountElements(bit_series_data[0]);
            cluster_belong[0] = 0;


            double sim_max = 0;
            double sim_value = 0;
            int cluster_index = 0;//the Cluster_Squeezer whose point j belong to.

            for (int j = 1; j < bit_series_data_length; j++)//go through the BD data,  except the first one
            {

                sim_max = 0;
                cluster_index = 0;
                //for each existed Cluster_Squeezer C
                for (int i=0; i< b_cluster.Count; i++)
                {
                    sim_value = simComputation(b_cluster[i], bit_series_data[j]);
                    if (sim_max < sim_value)
                    {
                        cluster_index = i;
                        sim_max = sim_value;
                    }
                        
                }

             
                if (sim_max >= threshold)
                {
                    cluster_belong[j] = cluster_index;
                    b_cluster[cluster_index].getCluster().plusOneToNumberOfMembers();
                    b_cluster[cluster_index].getCluster().addToListMemberIndice(j);
                    b_cluster[cluster_index].updateCountElements(bit_series_data[j]);
                }
                else
                {
                   
                    //make a new Cluster_Squeezer then add it to b_cluster
                    b_cluster.Add(new Cluster_Squeezer(new Cluster(j, 1), new List<Dictionary<int, int>>()));
                    cluster_belong[j] = b_cluster.Count-1;
                    b_cluster[b_cluster.Count - 1].getCluster().addToListMemberIndice(j);
                    b_cluster[b_cluster.Count - 1].updateCountElements(bit_series_data[j]);

                }

            }//end of for

            Console.WriteLine("The number of clusters is " + b_cluster.Count);

            foreach(Cluster_Squeezer list_members in b_cluster)
            {
                list_members.getCluster().getListMemberIndice().Shuffle();
            }

            Console.WriteLine("End shuffle.");

            //double radius;
            //List<double> center_value;
            //for (int i = 0; i < b_cluster.Count; i++)
            //{
            //    center_value = data.GetRange(b_cluster[i].getCluster().getCenterIndex(), N_LENGTH);
            //    radius = HelperFunctions.calculateRadius(data, b_cluster[i].getCluster().getListMemberIndice(), center_value, N_LENGTH);
            //    b_cluster[i].getCluster().setRadius(radius);
            //}




        }//end squeezerCluster() function

    }//end class
}
