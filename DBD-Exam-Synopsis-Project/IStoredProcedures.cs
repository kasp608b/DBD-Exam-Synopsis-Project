namespace DBD_Comp_StoredProcedures
{
    internal interface IStoredProcedures
    {
        int CreatePerson(string PName);

        void CreatePersonFriend(int PId1, int PId2);

        void GetAllPersons();

        void GetPerson(int PId);

        void GetAllFriendsOfPerson(string PName);

        void GetAllFriendsOfPersonReciprocal(string PName);

        void usp_GetAllFriendsOfFriendsPerson(string PName);

        int UpdatePersonName(int PId, string PName);

        int DeletePerson(int PId);


    }
}
