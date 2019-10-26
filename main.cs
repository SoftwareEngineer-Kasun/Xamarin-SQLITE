// this check for db and place it outside
string dbName = "db.sqlite";
string dbPath = Path.Combine (Android.OS.Environment.ExternalStorageDirectory.ToString (), dbName);
// Check if your DB has already been extracted.
if (!File.Exists(dbPath))
{
    using (BinaryReader br = new BinaryReader(Android.App.Application.Context.Assets.Open(dbName)))
    {
        using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
        {
            byte[] buffer = new byte[2048];
            int len = 0;
            while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
            {
                bw.Write (buffer, 0, len);
            }
        }
    }
}


// open db connection
using (var conn = new SQLite.SQLiteConnection(dbPath))
{
        // Do stuff here...
}

// create tables with sqlite.net
public class Album
{
    [PrimaryKey, AutoIncrement]
    public int AlbumId { get; set; }
    public string Title { get; set; }
    public int ArtistId { get; set; }
}

//execute query
using (var conn = new SQLite.SQLiteConnection(dbPath))
{
    var cmd = new SQLite.SQLiteCommand (conn);
    cmd.CommandText = "select * from Album";
    var r = cmd.ExecuteQuery<Album> ();

    Console.Write (r);
}

//create table
using (var conn= new SQLite.SQLiteConnection(_pathToDatabase))
{
   conn.CreateTable<Person>();
}

//Add data to person table
var person = new Person { FirstName = "John " + DateTime.Now.Ticks, LastName = "Doe"};
using (var db = new SQLite.SQLiteConnection(_pathToDatabase ))
{
    db.Insert(person);
}
