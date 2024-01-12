namespace User_Management_Application.Utilities
{
    public class Response<T>
    {
        internal List<string> Errors;
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
        public T? Data { get; set; }
    }


    public class Response
    {
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
    }



    public class ResponseStatusCode
    {
        public string SUCCESS { get; set; } = "00";
        public string FAILED { get; set; } = "01";
        public string PENDING { get; set; } = "02";
        public string INACTIVE { get; set; } = "04";
        public string NORECORDFOUND { get; set; } = "06";
        public string INCOMPLETEPROCESS { get; set; } = "07";
        public string USERPROFILINGFAILED { get; set; } = "08";
        public string BADREQUEST { get; set; } = "400";
        public string EXCEPTIONOCCOURED { get; set; } = "002";
        public string NOTQUALIFIED { get; set; } = "003";
        public string RECORDEXIST { get; set; } = "004";
        public string ACCESSDENIED { get; set; } = "005";
        public string NULLDATAPOINT { get; set; } = "006";
        public string SERVICEUNAVAILABLE { get; set; } = "503";
        public string NOTFOUND { get; set; } = "404";
        public string INTERNALSERVERERROR { get; set; } = "500";

    }


}
