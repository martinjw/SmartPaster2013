// Guids.cs
// MUST match guids.h
using System;

namespace SmartPaster
{
    static class GuidList
    {
        public const string guidSmartPasterPkgString = "233fade6-f9e1-4a0d-8784-089004c574fc";
        public const string guidSmartPasterCmdSetString = "1dba502a-a6ce-4857-a13e-d9936e61ec66";

        public static readonly Guid guidSmartPasterCmdSet = new Guid(guidSmartPasterCmdSetString);
    };
}