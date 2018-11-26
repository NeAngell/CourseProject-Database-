namespace Курсовой_проект.DBMigration.Migrations
{
    class _201803251816 : IMigration
    {
        public string ApplyQuery()
        {
            //your query for database
            string query = "CREATE TABLE Marks (Mark_Id int NOT NULL Primary Key IDENTITY, Mark_Name varchar(15) NULL UNIQUE, Description varchar(MAX) NULL);";

            return query;
        }
        public string RevertQuery()
        {
            //your revert query for database
            string query = "DROP TABLE Marks;";

            return query;
        }
    }
}
