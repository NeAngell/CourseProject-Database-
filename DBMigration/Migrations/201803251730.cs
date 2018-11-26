namespace Курсовой_проект.DBMigration.Migrations
{
    class _201803251730 : IMigration
    {
        public string ApplyQuery()
        {
            //your query for database
            string query = "CREATE TABLE Users (User_Id int NOT NULL Primary Key IDENTITY, Username varchar(15) NULL UNIQUE, Phone varchar(15) NULL UNIQUE, Email varchar(254) NULL UNIQUE, "+
                            "First_Name varchar(15) NULL, Last_Name varchar(15) NULL, Patronymic varchar(15) NULL, Password varchar(50) NULL, Register_Date datetime NULL, User_Permissions varchar(10) NULL CHECK(User_Permissions IN('User', 'Admin', 'Manager')));";

            return query;
        }
        public string RevertQuery()
        {
            //your revert query for database
            string query = "DROP TABLE Users;";

            return query;
        }
    }
}
