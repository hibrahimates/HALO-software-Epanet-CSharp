using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epanet
{
    class NodeOp
    {
        public static int Get_numofNode()
        {
            UnsafeNativeMethods.ENgetcount(CountType.Node, out int numofNode);
            return numofNode;
        }
        public static float[] Get_NodeElev()
        {
            //float NodeElev;
            float[] NodeElevs = new float[Get_numofNode()];
            for (int i = 0; i < Get_numofNode(); i++)
            {
                UnsafeNativeMethods.ENgetnodevalue(i + 1, NodeValue.Elevation, out float NodeElev);
                NodeElevs[i] = NodeElev;
            }
            return NodeElevs;
        }
        public static float[] Get_NodeHead()
        {
            //float NodeHead;
            float[] NodeHeads = new float[Get_numofNode()];
            for (int i = 0; i < Get_numofNode(); i++)
            {
                UnsafeNativeMethods.ENgetnodevalue(i + 1, NodeValue.Head, out float NodeHead);
                // UnsafeNativeMethods.ENgetnodevalue(i + 1, NodeValue.Elevation, out float Nodelevation); kot mu yükseklik mi? Ben kotu kullandım....
                NodeHeads[i] = NodeHead;
            }
            return NodeHeads;
        }

        public static float[] Get_Pressure()
        {
            //float Pressure;
            float[] Pressures = new float[Get_numofNode()];
            for (int i = 0; i < Get_numofNode(); i++)
            {
                UnsafeNativeMethods.ENgetnodevalue(i + 1, NodeValue.Pressure, out float Pressure);
                // UnsafeNativeMethods.ENgetnodevalue(i + 1, NodeValue.Elevation, out float Nodelevation); kot mu yükseklik mi? Ben kotu kullandım....
                Pressures[i] = Pressure;
            }
            return Pressures;
        }
        public static float[] Get_NodeDemand()
        {
            /// float NodeDemand;
            float[] NodeDemands = new float[Get_numofNode()];
            for (int i = 0; i < Get_numofNode(); i++)
            {
                UnsafeNativeMethods.ENgetnodevalue(i + 1, NodeValue.Demand, out float NodeDemand);
                NodeDemands[i] = NodeDemand * ((float)3.6);
            }
            return NodeDemands;
        }
        public static float[] Get_Hdesign()
        {
            //Hdesign=nodeHead+ MinHead;
            //float Hdesign;
            float[] Hdesigns = new float[Get_numofNode()];
            for (int i = 0; i < Get_numofNode(); i++)
            {
                UnsafeNativeMethods.ENgetnodevalue(i + 1, NodeValue.Elevation, out float Hdesign);
                Hdesigns[i] = Hdesign + Sabitler.MinHead;
            }
            return Hdesigns;
        }
    }
}
