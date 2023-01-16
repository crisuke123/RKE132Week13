using System.Data.SQLite;

ReadData(crateconnection());
InsertCustomer(crateconnection());
removeCostomer(crateconnection());
static SQLiteConnection crateconnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version = 3; New = True; Compress = true;");

    try
    {
        connection.Open();
        Console.WriteLine("db found.");
    }
    catch
    {
        Console.WriteLine("db not found.");
    }
    return connection;
}


static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, * FROM customer";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowid = reader["rowid"].ToString();
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringDoB = reader.GetString(3);

        Console.WriteLine($"{readerRowid}. Full Name {readerStringFirstName} {readerStringLastName}; dob: {readerStringDoB}");
    }

    myConnection.Close();
}

static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fName, LName, dob;

    Console.WriteLine("Enter First name: ");
    fName = Console.ReadLine();
    Console.WriteLine("Enter last name: ");
    LName = Console.ReadLine();
    Console.WriteLine("Enter date of birth (mm-dd-yyyy) ");
    dob = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer (FirstName, lastName, dateOfBirth) " +
        $"VALUES ('{fName}', '{LName}', '{dob}')";

    int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Row inserted: {rowInserted}");

   
    ReadData(myConnection);

    

}

static void removeCostomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string idToDelete;
    Console.WriteLine("Enter an id To Delete a customer :");
   idToDelete = Console.ReadLine(); 

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}"; 
    int rowRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{rowRemoved} was removed from the table customer.");

    ReadData(myConnection);
}

