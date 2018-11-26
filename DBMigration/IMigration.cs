namespace Курсовой_проект.DBMigration
{
    interface IMigration
    {
        string ApplyQuery();
        string RevertQuery();
    }
}
