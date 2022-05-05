using System;
using Azure.Data.Tables;

namespace Company.Function {
    public class Player : ITableEntity {
        public string PartitionKey {get; set;}
        public string RowKey {get; set;}
        public Azure.ETag ETag {get; set;}
        public DateTimeOffset? Timestamp {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string EmailAddress {get; set;}
    }
}