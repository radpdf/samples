using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using RadPdf.Lite;

// Not in use by default
// Uncomment the approprate line in CustomPdfIntegrationProvider.cs
public class SqlServerPdfLiteStorageProvider : PdfLiteStorageProvider
{
    /*
     This class assumes a SQL Server table called "data":

        CREATE TABLE [dbo].[data](
            [id] [uniqueidentifier] NOT NULL,
            [subtype] [int] NOT NULL,
            [value] [varbinary](max) NOT NULL,
            CONSTRAINT [PK_data] PRIMARY KEY CLUSTERED 
            (
                [id] ASC,
                [subtype] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    */

    private readonly SqlConnection _connection;

    public SqlServerPdfLiteStorageProvider(string connectionString)
        : base()
    {
        _connection = new SqlConnection(connectionString);
        _connection.Open();
    }

    public override void DeleteData(PdfLiteSession session)
    {
        using (SqlCommand command = _connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM data WHERE id = @id";

            command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = session.ID;

            command.ExecuteNonQuery();
        }
    }

    public override byte[] GetData(PdfLiteSession session, int subtype)
    {
        using (SqlCommand command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT value FROM data WHERE id = @id AND subtype = @subtype";

            command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = session.ID;
            command.Parameters.Add("@subtype", SqlDbType.Int).Value = subtype;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return (byte[]) reader.GetValue(0);
                }
                else
                {
                    return null;
                }
            }
        }
    }

    public override void SetData(PdfLiteSession session, int subtype, byte[] value)
    {
        using (SqlCommand command = _connection.CreateCommand())
        {
            // Do an "upsert"
            command.CommandText = "IF NOT EXISTS (SELECT id FROM data WHERE id = @id AND subtype = @subtype)\nINSERT INTO data(id, subtype, value) VALUES(@id, @subtype, @value)\nELSE\nUPDATE data SET value = @value WHERE id = @id AND subtype = @subtype";

            command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = session.ID;
            command.Parameters.Add("@subtype", SqlDbType.Int).Value = subtype;
            command.Parameters.Add("@value", SqlDbType.VarBinary, -1).Value = value;

            command.ExecuteNonQuery();
        }
    }
}
