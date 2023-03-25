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
        SelectLoginUserStatsByUserName,
        UpdateLoginUserStats,
        DeleteFromLoginPageStats,
        DeleteFromLoginUserStats
    }

    public enum Views
    {
        SelectAllLoginPageStats,
        UserStats_User,
        SelectAllLoginUserStats,
        SelectPageStatsWithUserName,
     
    }
    public enum Functions
    {
        SelectLoginUserStatsByUserNameFunction
    }


}
