using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitClustering
{
    class Cluster
    {
        protected int center_index;
        protected int number_of_members;
        protected List<int> list_members_index;
        protected double radius;

        //Constructor
        public Cluster(int center, int number_of_members)
        {
            this.center_index = center;
            this.number_of_members = number_of_members;
            this.list_members_index = new List<int>();
            this.radius = 0;
        }


        //get set functions:
        public int getCenterIndex()
        {
            return this.center_index;
        }


        public double getRadius()
        {
            return this.radius;
        }

        public List<int> getListMemberIndice()
        {
            return this.list_members_index;
        }

        public int getNumberOfMembers()
        {
            return this.number_of_members;
        }

        // set radius:
        public void setRadius(double radius)
        {
            this.radius = radius;
        }

        //when appending a element:
        public void addToListMemberIndice(int index)
        {
            this.list_members_index.Add(index);
        }

        public void plusOneToNumberOfMembers()
        {
            this.number_of_members++;
        }


    }
}
