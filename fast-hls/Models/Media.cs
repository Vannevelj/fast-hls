namespace FastHls.Models {
    public struct Media 
    {
        public MediaType MediaType { get; set; }
        public string GroupId { get; set; }
        public string Name { get; set; }
        public string? Uri { get; set; }
        public string? Language { get; set;}
        public string AssocLanguage {get;set;}
        public bool IsDefault {get;set;}
        public bool AutoSelect {get;set;}
        public bool Forced {get;set;}
        public string? InstreamId {get;set;}
        public string[]? Characteristics {get;set;}
    }
}

