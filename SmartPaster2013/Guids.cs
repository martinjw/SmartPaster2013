// Guids.cs
// MUST match guids.h
using System;

namespace SmartPaster2013
{
    static class GuidList
    {
        public const string guidSmartPaster2013PkgString = "233fade6-f9e1-4a0d-8784-089004c574fc";
        public const string guidSmartPaster2013CmdSetString = "1dba502a-a6ce-4857-a13e-d9936e61ec66";

        public static readonly Guid guidSmartPaster2013CmdSet = new Guid(guidSmartPaster2013CmdSetString);
    };
}