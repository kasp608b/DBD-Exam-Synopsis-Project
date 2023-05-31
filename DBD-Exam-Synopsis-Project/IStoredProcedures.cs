namespace DBD_Exam_Synopsis_Project
{
    internal interface IStoredProcedures
    {
        void GetAllPersons();

        void GetPerson(int PId);

        void GetAllFriendsOfPerson(string PName);

        void GetAllFriendsOfPersonReciprocal(string PName);

        void usp_GetAllFriendsOfFriendsPerson(string PName);
        int CreatePerson(string PName);

        int CreatePersonFriend(int PId1, int PId2);

        int UpdatePersonName(int PId, string PName);

        int DeletePerson(int PId);


    }
}
