using System;
using Azure.Data.Tables;

namespace Company.Function {
    public class Match : ITableEntity {
        public string PartitionKey {get; set;}
        public string RowKey {get; set;}
        public Azure.ETag ETag {get; set;}
        public DateTimeOffset? Timestamp {get; set;}
    }
}