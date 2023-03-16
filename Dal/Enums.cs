namespace Dal.Enums
{
    public enum StoredProcedures
    {
        InsertLoginUserStats ,
        InsertLoginPageStats,
        SelectUserByEmailAndPassword,
        SelectLoginUserStatsById,
        SelectLoginPageStatsById,
        SelectLoginPageStatsByLoginUserStatsId,
        UpdateLoginUserStats,
        DeleteFromLoginPageStats,
        DeleteFromLoginUserStats
    }

    public enum Views
    {
        SelectAllLoginPageStats,
        UserStats_User,
        SelectAllLoginUserStats
    }


}
