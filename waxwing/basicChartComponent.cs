using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using Plotly.NET;
using Plotly.NET.LayoutObjects;
using Microsoft.FSharp.Collections;
using Newtonsoft.Json;

namespace waxwing
{
    public class basicChartComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public basicChartComponent()
          : base("basicChartDisplay", "Basic Chart",
            "Display a basic Plotly Chart",
            "Waxwing", "Basics")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            // Use the pManager object to register your input parameters.
            // You can often supply default values when creating parameters.
            // All parameters must have the correct access type. If you want 
            // to import lists or trees of values, modify the ParamAccess flag.
            //pManager.AddPlaneParameter("Plane", "P", "Base plane for spiral", GH_ParamAccess.item, Plane.WorldXY);
            //pManager.AddNumberParameter("Outer Radius", "R1", "Outer radius for spiral", GH_ParamAccess.item, 10.0);
            //pManager.AddIntegerParameter("Turns", "T", "Number of turns between radii", GH_ParamAccess.item, 10);

            pManager.AddTextParameter("Title", "T", "Chart Title", GH_ParamAccess.item, "Basic Chart");

            // If you want to change properties of certain parameters, 
            // you can use the pManager instance to access them by index:
            //pManager[0].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            // Use the pManager object to register your output parameters.
            // Output parameters do not have default values, but they too must have the correct access type.
            //pManager.AddCurveParameter("Spiral", "S", "Spiral curve", GH_ParamAccess.item);

            pManager.AddTextParameter("Trace", "T", "Plotly.js 'trace' as JSON", GH_ParamAccess.item);
            pManager.AddTextParameter("Layout", "L", "Plotly.js 'layout' as JSON", GH_ParamAccess.item);
            pManager.AddTextParameter("Config", "C", "Plotly.js 'config' as JSON", GH_ParamAccess.item);

            // Sometimes you want to hide a specific parameter from the Rhino preview.
            // You can use the HideParameter() method as a quick way:
            //pManager.HideParameter(0);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // First, we need to retrieve all data from the input parameters.
            // We'll start by declaring variables and assigning them starting values.
            string title = "";

            // Then we need to access the input parameters individually. 
            // When data cannot be extracted from a parameter, we should abort this method.
            if (!DA.GetData(0, ref title)) return;
            

            // We should now validate the data and warn the user if invalid data is supplied.
            if (String.IsNullOrEmpty(title))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "Chart title is empty");
                title = "Default Chart Title";
                //return;
            }

            LinearAxis xAxis = new LinearAxis();
            xAxis.SetValue("title", "xAxis");
            xAxis.SetValue("zerolinecolor", "#ffff");
            xAxis.SetValue("gridcolor", "#ffff");
            xAxis.SetValue("showline", true);
            xAxis.SetValue("zerolinewidth", 2);

            LinearAxis yAxis = new LinearAxis();
            yAxis.SetValue("title", "yAxis");
            yAxis.SetValue("zerolinecolor", "#ffff");
            yAxis.SetValue("gridcolor", "#ffff");
            yAxis.SetValue("showline", true);
            yAxis.SetValue("zerolinewidth", 2);

            Layout layout = new Layout();
            layout.SetValue("xaxis", xAxis);
            layout.SetValue("yaxis", yAxis);
            layout.SetValue("title", title);
            layout.SetValue("plot_bgcolor", "#e5ecf6");
            layout.SetValue("showlegend", true);

            Trace trace = new Trace("bar");
            trace.SetValue("x", new[] { 1, 2, 3 });
            trace.SetValue("y", new[] { 1, 3, 2 });

            // THis didn't work because it require the Giraffe.ViewEngine to be strongly named
            // https://github.com/plotly/Plotly.NET/issues/371
            //var fig = GenericChart.Figure.create(ListModule.OfSeq(new[] { trace }), layout);
            //GenericChart.fromFigure(fig);

            // Finally assign the output parameters.
            DA.SetData(0, JsonConvert.SerializeObject(trace));
            DA.SetData(1, JsonConvert.SerializeObject(layout));
        }



        /// <summary>
        /// The Exposure property controls where in the panel a component icon 
        /// will appear. There are seven possible locations (primary to septenary), 
        /// each of which can be combined with the GH_Exposure.obscure flag, which 
        /// ensures the component will only be visible on panel dropdowns.
        /// </summary>
        public override GH_Exposure Exposure => GH_Exposure.primary;

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
        /// </summary>
        protected override System.Drawing.Bitmap Icon => null;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("A08E9229-01F6-46A8-B424-D911A02AD344");
    }
}