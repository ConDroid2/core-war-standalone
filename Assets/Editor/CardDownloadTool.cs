//using Amazon.DynamoDBv2;
//using Amazon.DynamoDBv2.DataModel;
//using Amazon.DynamoDBv2.Model;
//using System.Threading.Tasks;

//public class CardDownloadTool
//{
//    AmazonDynamoDBClient client;
//    DynamoDBContext context;

//    public CardDownloadTool()
//    {
//        client = new AmazonDynamoDBClient("AKIATDE2CZXI46DNH35K", "m481Ws6QMy1CAACX9XtXj8HiM1SIixJgllYi2YFt", Amazon.RegionEndpoint.USEast1);
//        context = new DynamoDBContext(client);
//    }

//    private static void GetBook(DynamoDBContext context, string name)
//    {
//        RawCard cardData = new RawCard();
//        System.Exception exception = new System.Exception("Failed to load card " + name);
//        AmazonDynamoDBResult<RawCard> result = new AmazonDynamoDBResult<RawCard>(cardData, exception, null);
//        AmazonDynamoDBCallback<RawCard> callback = new AmazonDynamoDBCallback<RawCard>(CallbackResult);
//        context.LoadAsync<RawCard>(name, callback);
//        client.ListTablesAsync()

//        Console.WriteLine("\nGetBook: Printing result.....");
//        Console.WriteLine("Title: {0} \n No.Of threads:{1} \n No. of messages: {2}",
//                  bookItem.Title, bookItem.ISBN, bookItem.PageCount);
//    }

    

//    [DynamoDBTable("core-card-server")]
//    public class RawCard
//    {
//        [DynamoDBHashKey] //Partition key
//        public string name { get; set; }    
//        public string imagePath { get; set; }
//        public string type { get; set; }
//        public int costBlack { get; set; }
//        public int costRed { get; set; }
//        public int costGreen { get; set; }
//        public int costBlue { get; set; }
//        public int costNeutral { get; set; }
//        public string attack { get; set; }
//        public string resitance { get; set; }
//        public string keywords { get; set; }
//        public string description { get; set; }
//        public string action { get; set; }
//    }
//}
