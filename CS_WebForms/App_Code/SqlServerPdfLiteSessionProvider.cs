using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using RadPdf.Lite;

// Not in use by default
// Uncomment the approprate line in CustomPdfIntegrationProvider.cs
public class SqlServerPdfLiteSessionProvider : PdfLiteSessionProvider
{
    /*
     This class assumes a SQL Server table called "sessions":
     
        CREATE TABLE [dbo].[sessions](
            [id] [varchar](255) COLLATE Latin1_General_CS_AS NOT NULL,
            [value] [varbinary](max) NOT NULL,
            CONSTRAINT [PK_sessions] PRIMARY KEY CLUSTERED 
            (
                [id] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    */

    private readonly SqlConnection _connection;

    // This example uses an a simple SQL Server database for sessions
    public SqlServerPdfLiteSessionProvider(string connectionString)
        : base()
    {
        _connection = new SqlConnection(connectionString);
        _connection.Open();
    }

    public override string AddSession(PdfLiteSession session)
    {
        // Generate a session key using RAD PDF's default generator.
        // Your own generator can be used here instead.
        string key = GenerateKey();

        using (SqlCommand command = _connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO sessions (id, value) VALUES (@id, @value)";

            command.Parameters.Add("@id", SqlDbType.VarChar, 255).Value = key;
            command.Parameters.Add("@value", SqlDbType.VarBinary, -1).Value = session.Serialize();

            command.ExecuteNonQuery();
        }

        return key;
    }

    public override PdfLiteSession GetSession(string key)
    {
        using (SqlCommand command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT value FROM sessions WHERE id = @id";

            command.Parameters.Add("@id", SqlDbType.VarChar, 255).Value = key;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    byte[] data = (byte[])reader.GetValue(0);

                    return PdfLiteSession.Deserialize(data);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
