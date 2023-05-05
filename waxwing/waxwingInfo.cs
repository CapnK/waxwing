using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace waxwing
{
    public class waxwingInfo : GH_AssemblyInfo
    {
        public override string Name => "Waxwing";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("CA7AE2E6-60CF-4807-AE22-4FD1E046AD9E");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}