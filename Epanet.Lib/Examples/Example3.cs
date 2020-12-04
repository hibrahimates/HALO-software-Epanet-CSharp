namespace Epanet.Examples
{

    /// <summary>Example #2 from "The EPANET Programmer's Toolkit" help.</summary>
    public static class Example3
    {
        /// <summary>
        ///     This example illustrates how the Toolkit could be used to determine
        ///     the lowest dose of chlorine applied at the entrance to a
        ///     distribution system needed to ensure that a minimum residual is met
        ///     throughout the system. We assume that the EPANET input file
        ///     contains the proper set of kinetic coefficients that describe the
        ///     rate at which chlorine will decay in the system being studied. In
        ///     the example code, the ID label of the source node is contained in
        ///     <paramref name="SourceID" />, the minimum residual target is given
        ///     by <paramref name="Ctarget" />, and the target is only checked after
        ///     a start-up duration of 5 days (432,000 seconds). To keep the code
        ///     more readable, no error checking is made on the results returned
        ///     from the Toolkit function calls.
        /// </summary>
        public static float cl2dose(string SourceID, float Ctarget)
        {
            int i, nnodes, sourceindex;
            bool violation;
            float c, csource;
            int t, tstep;

            /* Open the toolkit & obtain a hydraulic solution */
            UnsafeNativeMethods.ENopen("example3.inp", "example3.rpt", "");
            UnsafeNativeMethods.ENsolveH();

            /* Get the number of nodes & */
            /* the source node's index   */
            UnsafeNativeMethods.ENgetcount(CountType.Node, out nnodes);
            UnsafeNativeMethods.ENgetnodeindex(SourceID, out sourceindex);

            /* Setup system to analyze for chlorine */
            /* (in case it was not done in the input file.) */
            UnsafeNativeMethods.ENsetqualtype(QualType.Chem, "Chlorine", "mg/L", "");

            /* Open the water quality solver */
            UnsafeNativeMethods.ENopenQ();

            /* Begin the search for the source concentration */
            csource = 0.0f;
            do
            {
                /* Update source concentration to next level */
                csource = csource + 0.1f;
                UnsafeNativeMethods.ENsetnodevalue(sourceindex, NodeValue.SourceQual, csource);

                /* Run WQ simulation checking for target violations */
                violation = false;
                UnsafeNativeMethods.ENinitQ(false);
                do
                {
                    UnsafeNativeMethods.ENrunQ(out t);
                    if (t > 432000)
                    {
                        for (i = 1; i <= nnodes; i++)
                        {
                            UnsafeNativeMethods.ENgetnodevalue(i, NodeValue.Quality, out c);
                            if (c < Ctarget)
                            {
                                violation = true;
                                break;
                            }
                        }
                    }
                    UnsafeNativeMethods.ENnextQ(out tstep);

                    /* End WQ run if violation found */
                }
                while (!violation && tstep > 0);

                /* Continue search if violation found */
            }
            while (violation && csource <= 4.0);

            /* Close up the WQ solver and toolkit */
            UnsafeNativeMethods.ENcloseQ();
            UnsafeNativeMethods.ENclose();
            return csource;
        }
    }

}
