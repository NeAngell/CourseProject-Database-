namespace Курсовой_проект.DBMigration.Migrations
{
    class _201805252336 : IMigration
    {
        public string ApplyQuery()
        {
            //your query for database
            string query = "CREATE TABLE Activation (Activation_Id int NOT NULL Primary Key IDENTITY, User_Id int NOT NULL, Activation_String varchar(32) NOT NULL, " +
                            "Activation bit NOT NULL DEFAULT 0, Activation_Date datetime NULL, " +
                            "CONSTRAINT UQ_Unique UNIQUE (User_Id, Activation_String));";

            return query;
        }
        public string RevertQuery()
        {
            //your revert query for database
            string query = "DROP TABLE Activation;";

            return query;
        }
    }
}
