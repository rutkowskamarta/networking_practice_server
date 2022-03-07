namespace ServerPlugin.Tags
{
	public class Tags
	{
        //request tags
        public static readonly ushort PlayerDataRequest = 0;
        public static readonly ushort JoinRoomRequest = 1;
        public static readonly ushort LeaveRoomRequest = 2;
        public static readonly ushort CreateRoomRequest = 3;

        public static readonly ushort StartGameRequest = 4;

        public static readonly ushort AddCategoryRequest = 5;
        public static readonly ushort RemoveCategoryRequest = 6;
        public static readonly ushort SetRoundsNumberRequest = 7;
        public static readonly ushort PlayerReadyRequest = 8;
        public static readonly ushort PlayerUnreadyRequest = 9;
        public static readonly ushort GenerateLetterRequest = 10;


        //response tags
        public static readonly ushort CreateRoomResponseSucess = 100;
        public static readonly ushort CreateRoomResponseFail = 101;
        public static readonly ushort UpdateRoomStateNotification = 102;

        public static readonly ushort JoinRoomResponseSucess = 103;
        public static readonly ushort JoinRoomResponseFail = 104;

        public static readonly ushort GameStartedResponseSucess = 105;
        public static readonly ushort GameStartedResponseFail = 106;

        public static readonly ushort GameCategoryAddedNotification = 107;
        public static readonly ushort GameCategoryRemovedNotification = 108;

        public static readonly ushort RoundsModifiedResponse = 109;
        public static readonly ushort EveryoneReadyNotification = 110;

        public static readonly ushort ReadyStateChangedResponse = 111;
        public static readonly ushort LetterGeneratedResponse = 112;

    }
}
