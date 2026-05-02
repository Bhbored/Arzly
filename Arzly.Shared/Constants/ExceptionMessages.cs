using System;
using System.Collections.Generic;
using System.Text;

namespace Arzly.Shared.Constants
{
    public static class ExceptionMessages
    {
        public static readonly string EmptyAddRequest = "Create Request is Empty , Make sure All it's Properties are Provided";
        public static readonly string EmptyUpdateRequest = "Update Request is Empty , Make sure All it's Properties are Provided";
        public static readonly string MissingPickUpLocation = "The Assigned Location isn't Found";
        public static readonly string InvalidFirebaseId = "No User Found with This FireBaseId";
        public static readonly string MissingFirebaseId = "FireBaseId wasn't provided";
        public static readonly string MissingId = "No ID Provided";
        public static readonly string NoObjectWithId = "No Object with this ID Found";
    }
}
