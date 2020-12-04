// 
// Epanet -- Epanet2 Toolkit hydraulics library C# Interface
//                                                                    
// Enumerations.cs -- Enumerations/constants used in Epanet 2 Toolkit
// 
// CREATED:    02/13/2014                                                                    
// VERSION:    2.00                                               
// DATE:         02/13/2014
//             
// AUTHOR:     slava           
// 
// This is free and unencumbered software released into the public domain.
// 
// Anyone is free to copy, modify, publish, use, compile, sell, or
// distribute this software, either in source code form or as a compiled
// binary, for any purpose, commercial or non-commercial, and by any
// means.
// 
// In jurisdictions that recognize copyright laws, the author or authors
// of this software dedicate any and all copyright interest in the
// software to the public domain. We make this dedication for the benefit
// of the public at large and to the detriment of our heirs and
// successors. We intend this dedication to be an overt act of
// relinquishment in perpetuity of all present and future rights to this
// software under copyright law.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org/>

namespace Epanet
{

    //These are codes used by the DLL functions

    /// <summary>Node value parameters.</summary>
    /// <remarks>
    ///     Parameters 9 - 13 (<see cref="Demand" /> through <see cref="SourceMass" />)
    ///     are computed values. The others are input design parameters.
    /// </remarks>
    /// <remarks>
    ///     Parameters 14 - 23 (<see cref="InitVolume" /> through <see cref="TankKbulk" />)
    ///     apply only to storage tank nodes.
    /// </remarks>
    public enum NodeValue
    {
        /// <summary>0 - Elevation</summary>
        Elevation = 0,

        /// <summary>1 - Base demand</summary>
        BaseDemand = 1,

        /// <summary>2 - Demand pattern index</summary>
        Pattern = 2,

        /// <summary>3 - Emitter coeff.</summary>
        Emitter = 3,

        /// <summary>4 - Initial quality</summary>
        InitQual = 4,

        /// <summary>5 - Source quality</summary>
        SourceQual = 5,

        /// <summary>6 - Source pattern index</summary>
        SourcePat = 6,

        /// <summary>7 - Source type (See <see cref="Epanet.SourceType" /></summary>
        SourceType = 7,

        /// <summary>8 - Initial water level in tank</summary>
        TankLevel = 8,

        /// <summary>9 - Actual demand</summary>
        Demand = 9,

        /// <summary>10 - Hydraulic head</summary>
        Head = 10,

        /// <summary>11 - Pressure</summary>
        Pressure = 11,

        /// <summary>12 - Actual quality</summary>
        Quality = 12,

        /// <summary>13 - Mass flow rate per minute of a chemical source</summary>
        SourceMass = 13,

        /// <summary>14 - Initial water volume</summary>
        InitVolume = 14,

        /// <summary>15 - Mixing model code (see <see cref="MixType" />)</summary>
        MixModel = 15,

        /// <summary>16 - Inlet/Outlet zone volume in a 2-compartment tank</summary>
        MixZonevol = 16,

        /// <summary>17 - Tank diameter</summary>
        TankDiam = 17,

        /// <summary>18 - Minimum water volume</summary>
        MinVolume = 18,

        /// <summary>19 - Index of volume versus depth curve (0 if none assigned)</summary>
        VolCurve = 19,

        /// <summary>20 - Minimum water level</summary>
        MinLevel = 20,

        /// <summary>21 - Maximum water level</summary>
        MaxLevel = 21,

        /// <summary>22 - Fraction of total volume occupied by the inlet/outlet zone in a 2-compartment tank</summary>
        MixFraction = 22,

        /// <summary>23 - Bulk reaction rate coefficient.</summary>
        TankKbulk = 23
    }

    /// <summary>Link value parameters.</summary>
    /// <remarks>
    ///     Parameters 8 - 13 (<see cref="Flow" /> through <see cref="Energy" />) are
    ///     computed values. The others are design parameters.
    /// </remarks>
    /// <remarks>
    ///     Flow rate is positive if the direction of flow is from the designated
    ///     start node of the link to its designated end node, and negative otherwise.
    /// </remarks>
    /// <remarks>
    ///     Values are returned in units which depend on the units used for flow rate
    ///     in the EPANET input file (see Units of Measurement).
    /// </remarks>
    public enum LinkValue
    {
        /// <summary>0 - Diameter</summary>
        Diameter = 0,

        /// <summary>1 - Length</summary>
        Length = 1,

        /// <summary>2 - Roughness coeff.</summary>
        Roughness = 2,

        /// <summary>3 - Minor loss coeff.</summary>
        MinorLoss = 3,

        /// <summary>4 - Initial link status (0 = closed, 1 = open)</summary>
        InitStatus = 4,

        /// <summary>5 - Roughness for pipes, initial speed for pumps, initial setting for valves</summary>
        InitSetting = 5,

        /// <summary>6 - Bulk reaction coeff.</summary>
        Kbulk = 6,

        /// <summary>7 - Wall reaction coeff.</summary>
        Kwall = 7,

        /// <summary>8 - Flow rate</summary>
        Flow = 8,

        /// <summary>9 - Flow velocity</summary>
        Velocity = 9,

        /// <summary>10 - Head loss</summary>
        HeadLoss = 10,

        /// <summary>11 - Actual link status (0 = closed, 1 = open)</summary>
        Status = 11,

        /// <summary>12 - Roughness for pipes, actual speed for pumps, actual setting for valves</summary>
        Setting = 12,

        /// <summary>13 - Energy expended in kwatts</summary>
        Energy = 13
    }

    /// <summary>Time parameters</summary>
    /// <remarks>
    ///     Do not change time parameters after calling <see cref="UnsafeNativeMethods.ENinitH" />
    ///     in a hydraulic analysis or <see cref="UnsafeNativeMethods.ENinitQ" /> in a water quality analysis.
    /// </remarks>
    public enum TimeParameter
    {
        /// <summary>0 - Simulation duration</summary>
        Duration = 0,

        /// <summary>1 - Hydraulic time step</summary>
        HydStep = 1,

        /// <summary>2 - Water quality time step</summary>
        QualStep = 2,

        /// <summary>3 - Time pattern time step</summary>
        PatternStep = 3,

        /// <summary>4 - Time pattern start time</summary>
        PatternStart = 4,

        /// <summary>5 - Reporting time step</summary>
        ReportStep = 5,

        /// <summary>6 - Report starting time</summary>
        ReportStart = 6,

        /// <summary>7 - Time step for evaluating rule-based controls</summary>
        RuleStep = 7,

        /// <summary>8 - Type of time series post-processing to use (see <see cref="TstatType" />)</summary>
        Statistic = 8,

        /// <summary>9 - Periods</summary>
        Periods = 9
    }

    /// <summary>Component counts.</summary>
    /// <remarks>
    ///     The number of junctions in a network equals the number of
    ///     nodes minus the number of tanks and reservoirs.
    /// </remarks>
    /// <remarks>
    ///     There is no facility within the Toolkit to add to or delete
    ///     from the components described in the Input file.
    /// </remarks>
    public enum CountType
    {
        /// <summary>0 - Nodes</summary>
        Node = 0,

        /// <summary>1 - Reservoirs and tank nodes</summary>
        Tank = 1,

        /// <summary>2 - Links</summary>
        Link = 2,

        /// <summary>3 - Time patterns</summary>
        Pattern = 3,

        /// <summary>4 - Curves</summary>
        Curve = 4,

        /// <summary>5 - Simple controls</summary>
        Control = 5
    }

    /// <summary>Node types</summary>
    public enum NodeType
    {
        /// <summary>0 - Junction node</summary>
        Junction = 0,

        /// <summary>1 - Reservoir node</summary>
        Reservoir = 1,

        /// <summary>2 - Tank node</summary>
        Tank = 2
    }

    /// <summary>Link types</summary>
    public enum LinkType
    {
        /// <summary>0 - pipe with check valve</summary>
        CVPipe = 0,

        /// <summary>1 - regular pipe</summary>
        Pipe = 1,

        /// <summary>2 - pump</summary>
        Pump = 2,

        /// <summary>3 - pressure reducing valve</summary>
        PRV = 3,

        /// <summary>4 - pressure sustaining valve</summary>
        PSV = 4,

        /// <summary>5 - pressure breaker valve</summary>
        PBV = 5,

        /// <summary>6 - flow control valve</summary>
        FCV = 6,

        /// <summary>7 - throttle control valve</summary>
        TCV = 7,

        /// <summary>8 - general purpose valve</summary>
        GPV = 8
    }

    /// <summary>Water quality analysis codes</summary>
    public enum QualType
    {
        /// <summary>0 - no quality analysis</summary>
        None = 0,

        /// <summary>1 - analyze a chemical</summary>
        Chem = 1,

        /// <summary>2 - analyze water age</summary>
        Age = 2,

        /// <summary>3 - trace % of flow from a source</summary>
        Trace = 3
    }

    /// <summary>Type of source quality input.</summary>
    /// <remarks>See <c>[SOURCES]</c> for a description of these source types.</remarks>
    /// <remarks>
    ///     Used in functions <see cref="UnsafeNativeMethods.ENgetnodevalue" /> and
    ///     <see cref="UnsafeNativeMethods.ENsetnodevalue" /> with <see cref="NodeValue.SourceType" />
    ///     parameter.
    /// </remarks>
    public enum SourceType
    {
        /// <summary>0 - inflow concentration</summary>
        Concen = 0,

        /// <summary>1 - mass inflow booster</summary>
        Mass = 1,

        /// <summary>2 - setpoint booster</summary>
        SetPoint = 2,

        /// <summary>3 - flow paced booster</summary>
        FlowPaced = 3
    }

    /// <summary>Flow units codes</summary>
    /// <remarks>
    ///     Flow units are specified in the <c>[OPTIONS]</c> section of the EPANET Input file.
    /// </remarks>
    /// <remarks>
    ///     Flow units in liters or cubic meters implies that metric
    ///     units are used for all other quantities in addition to flow.
    ///     Otherwise US units are employed. (See Units of Measurement).
    /// </remarks>
    public enum FlowUnitsType
    {
        /// <summary>0 - cubic feet per second</summary>
        Cfs = 0,

        /// <summary>1 - gallons per minute</summary>
        Gpm = 1,

        /// <summary>2 - million gallons per day</summary>
        Mgd = 2,

        /// <summary>3 - imperial million gal. per day</summary>
        Imgd = 3,

        /// <summary>4 - acre-feet per day</summary>
        Afd = 4,

        /// <summary>5 - liters per second</summary>
        Lps = 5,

        /// <summary>6 - liters per minute</summary>
        Lpm = 6,

        /// <summary>7 - megaliters per day</summary>
        Mld = 7,

        /// <summary>8 - cubic meters per hour</summary>
        Cmh = 8,

        /// <summary>9 - cubic meters per day</summary>
        Cmd = 9
    }

    /// <summary>Misc. options.</summary>
    public enum MiscOption
    {
        /// <summary>0 - Trials</summary>
        MaxTrials = 0,

        /// <summary>1 - Accuracy</summary>
        Accuracy = 1,

        /// <summary>2 - Tolerance</summary>
        Tolerance = 2,

        /// <summary>3 - Emitter exponent</summary>
        EmitExpon = 3,

        /// <summary>4 - Demandmult</summary>
        DemandMult = 4
    }

    /// <summary>Control types.</summary>
    /// <remarks>
    ///     For pipes, a setting of 0 means the pipe is closed
    ///     and 1 means it is open. For a pump, the setting contains
    ///     the pump's speed, with 0 meaning the pump is closed and 1
    ///     meaning it is open at its normal speed. For a valve, the
    ///     setting refers to the valve's pressure, flow, or loss
    ///     coefficient value, depending on valve type.
    /// </remarks>
    /// <remarks>For Timer or Time-of-Day controls the nindex parameter equals 0.</remarks>
    /// <remarks>
    ///     See <see cref="UnsafeNativeMethods.ENsetcontrol" /> for an example of
    ///     using this function.
    /// </remarks>
    public enum ControlType
    {
        /// <summary>0 - Low Level Control.</summary>
        /// <remarks>Applies when tank level or node pressure drops below specified level.</remarks>
        LowLevel = 0,

        /// <summary>1 - High Level Control.</summary>
        /// <remarks>Applies when tank level or node pressure rises above specified level.</remarks>
        HiLevel = 1,

        /// <summary>2 - Timer Control.</summary>
        /// <remarks>Applies at specific time into simulation.</remarks>
        Timer = 2,

        /// <summary>3 - Time-of-Day Control.</summary>
        /// <remarks>Applies at specific time of day.</remarks>
        TimeOfDay = 3
    }

    /// <summary>Time series statistics types.</summary>
    /// <remarks>
    ///     Used as value in <see cref="UnsafeNativeMethods.ENgettimeparam" /> and
    ///     <see cref="UnsafeNativeMethods.ENsettimeparam" /> functions
    ///     with <see cref="TimeParameter.Statistic" /> code.
    /// </remarks>
    public enum TstatType
    {
        /// <summary>0 - none</summary>
        None = 0,

        /// <summary>1 - time-averages</summary>
        Average = 1,

        /// <summary>2 - minimum values</summary>
        Minimum = 2,

        /// <summary>3 - maximum values</summary>
        Maximum = 3,

        /// <summary>4 - max - min values</summary>
        Range = 4
    }

    /// <summary>
    ///     Tank mixing models.
    ///     The codes for the various tank mixing model choices are as follows:
    /// </summary>
    public enum MixType
    {
        /// <summary>0 - Single compartment, complete mix model</summary>
        Mix1 = 0,

        /// <summary>1 - Two-compartment, complete mix model</summary>
        Mix2 = 1,

        /// <summary>2 - Plug flow, first in, first out model</summary>
        Fifo = 2,

        /// <summary>3 - Stacked plug flow, last in, first out model</summary>
        Lifo = 3
    }

    /// <summary>Re-initialize flow flags</summary>
    /// <remarks>
    ///     2-digit flag where 1st (left) digit indicates if link flows should be re-initialized (1) or
    ///     not (0) and 2nd digit indicates if hydraulic results should be saved to file (1) or not (0)
    /// </remarks>
    [System.Flags]
    public enum SaveOptions
    {
        /// <summary>None</summary>
        None = 0,

        /// <summary>Save-results-to-file flag</summary>
        SaveToFile = 1,

        /// <summary>Re-initialize flow flag</summary>
        InitFlow = 10,
    }

    /// <summary>StatusLevel</summary>
    public enum StatusLevel
    {
        /// <summary>None</summary>
        None = 0,

        /// <summary>Normal</summary>
        Normal = 1,

        /// <summary>Full</summary>
        Full = 2
    }

    /// <summary>Result for link properties Initstatus and Status. (0 = closed, 1 = open)</summary>
    public enum LinkStatus
    {
        Closed = 0,
        Open = 1
    }

    /// <summary>Epanet error codes</summary>
    public enum ErrorCode
    {
        /// <summary>No error</summary>
        Ok = 0,

        /// <summary> WARNING: System hydraulically unbalanced.</summary>
        Warn1 = 1,

        /// <summary> WARNING: System may be hydraulically unstable.</summary>
        Warn2 = 2,

        /// <summary> WARNING: System disconnected.</summary>
        Warn3 = 3,

        /// <summary> WARNING: Pumps cannot deliver enough flow or head.</summary>
        Warn4 = 4,

        /// <summary> WARNING: Valves cannot deliver enough flow.</summary>
        Warn5 = 5,

        /// <summary> WARNING: System has negative pressures.</summary>
        Warn6 = 6,

        /// <summary>System Error 101: insufficient memory available.</summary>
        Err101 = 101,

        /// <summary>System Error 102: no network data available.</summary>
        Err102 = 102,

        /// <summary>System Error 103: hydraulics not initialized.</summary>
        Err103 = 103,

        /// <summary>System Error 104: no hydraulics for water quality analysis.</summary>
        Err104 = 104,

        /// <summary>System Error 105: water quality not initialized.</summary>
        Err105 = 105,

        /// <summary>System Error 106: no results saved to report on.</summary>
        Err106 = 106,

        /// <summary>System Error 107: hydraulics supplied from external file.</summary>
        Err107 = 107,

        /// <summary>System Error 108: cannot use external file while hydraulics solver is active.</summary>
        Err108 = 108,

        /// <summary>System Error 109: cannot change time parameter when solver is active.</summary>
        Err109 = 109,

        /// <summary>System Error 110: cannot solve network hydraulic equations.</summary>
        Err110 = 110,

        /// <summary>System Error 120: cannot solve water quality transport equations.</summary>
        Err120 = 120,

        /// <summary>Input Error 200: one or more errors in input file.</summary>
        Err200 = 200,

        /// <summary>Input Error 201: syntax error in following line of [%s] section:</summary>
        Err201 = 201,

        /// <summary>Input Error 202: %s %s contains illegal numeric value.</summary>
        Err202 = 202,

        /// <summary>Input Error 203: %s %s refers to undefined node.</summary>
        Err203 = 203,

        /// <summary>Input Error 204: %s %s refers to undefined link.</summary>
        Err204 = 204,

        /// <summary>Input Error 205: %s %s refers to undefined time pattern.</summary>
        Err205 = 205,

        /// <summary>Input Error 206: %s %s refers to undefined curve.</summary>
        Err206 = 206,

        /// <summary>Input Error 207: %s %s attempts to control a CV.</summary>
        Err207 = 207,

        /// <summary>Input Error 208: %s specified for undefined Node %s.</summary>
        Err208 = 208,

        /// <summary>Input Error 209: illegal %s value for Node %s.</summary>
        Err209 = 209,

        /// <summary>Input Error 210: %s specified for undefined Link %s.</summary>
        Err210 = 210,

        /// <summary>Input Error 211: illegal %s value for Link %s.</summary>
        Err211 = 211,

        /// <summary>Input Error 212: trace node %.0s %s is undefined.</summary>
        Err212 = 212,

        /// <summary>Input Error 213: illegal option value in [%s] section:</summary>
        Err213 = 213,

        /// <summary>Input Error 214: following line of [%s] section contains too many characters:</summary>
        Err214 = 214,

        /// <summary>Input Error 215: %s %s is a duplicate ID.</summary>
        Err215 = 215,

        /// <summary>Input Error 216: %s data specified for undefined Pump %s.</summary>
        Err216 = 216,

        /// <summary>Input Error 217: invalid %s data for Pump %s.</summary>
        Err217 = 217,

        /// <summary>Input Error 219: %s %s illegally connected to a tank.</summary>
        Err219 = 219,

        /// <summary>Input Error 220: %s %s illegally connected to another valve.</summary>
        Err220 = 220,

        /*** Updated on 10/25/00 ***/

        /// <summary>Input Error 222: %s %s has same start and end nodes.</summary>
        Err222 = 222,

        /// <summary>Input Error 223: not enough nodes in network</summary>
        Err223 = 223,

        /// <summary>Input Error 224: no tanks or reservoirs in network.</summary>
        Err224 = 224,

        /// <summary>Input Error 225: invalid lower/upper levels for Tank %s.</summary>
        Err225 = 225,

        /// <summary>Input Error 226: no head curve supplied for Pump %s.</summary>
        Err226 = 226,

        /// <summary>Input Error 227: invalid head curve for Pump %s.</summary>
        Err227 = 227,

        /// <summary>Input Error 230: Curve %s has nonincreasing x-values.</summary>
        Err230 = 230,

        /// <summary>Input Error 233: Node %s is unconnected.</summary>
        Err233 = 233,

        /// <summary>Input Error 240: %s %s refers to undefined source.</summary>
        Err240 = 240,

        /// <summary>Input Error 241: %s %s refers to undefined control.</summary>
        Err241 = 241,

        /// <summary>Input Error 250: function call contains invalid format.</summary>
        Err250 = 250,

        /// <summary>Input Error 251: function call contains invalid parameter code.</summary>
        Err251 = 251,

        /// <summary>File Error 301: identical file names.</summary>
        Err301 = 301,

        /// <summary>File Error 302: cannot open input file.</summary>
        Err302 = 302,

        /// <summary>File Error 303: cannot open report file.</summary>
        Err303 = 303,

        /// <summary>File Error 304: cannot open binary output file.</summary>
        Err304 = 304,

        /// <summary>File Error 305: cannot open hydraulics file.</summary>
        Err305 = 305,

        /// <summary>File Error 306: hydraulics file does not match network data.</summary>
        Err306 = 306,

        /// <summary>File Error 307: cannot read hydraulics file.</summary>
        Err307 = 307,

        /// <summary>File Error 308: cannot save results to file.</summary>
        Err308 = 308,

        /// <summary>File Error 309: cannot save results to report file.</summary>
        Err309 = 309,
    }

}
