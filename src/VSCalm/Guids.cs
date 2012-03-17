// Guids.cs
// MUST match guids.h
using System;

namespace Algenta.VSCalm
{
    static class GuidList
    {
        public const string guidVSCalmPkgString = "56448fef-be24-4ece-a353-b94ab996b45a";
        public const string guidVSCalmCmdSetString = "320dcd93-0d66-480c-94cb-bd3e3c708435";

        public static readonly Guid guidVSCalmCmdSet = new Guid(guidVSCalmCmdSetString);
    };
}