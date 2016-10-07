namespace IB.Db.Enum
{
    
    public enum GeneralEnum
    {
        UnhandeledError = 10000,
        AuthenticationFail = 0,
        UserNameCannotBeEmpty = 1,
        PasswordCannotBeEmpty = 2,
        UserNotFound = 3,
        UserPasswordLocked = 4,
        UserNameOrPasswordInvalid = 5,
        UserOrGroupPermissionIsNotDefined = 6,
        UserNameExist = 7,
        EmailTemplateNotFound= 8,
        UserIsNotActivated = 9,
        UserIsNotActive = 10,
        SearchTextIsShort = 11,
        ItemNotFound = 12,// Dağıtım sırasında LeadUserId boş veya 0 olamaz.
        UserPushIdIsInvalid = 14,
        EmptyList = 15,
        TokenInvalid = 16,
        TokenExpired = 17,
        PasswordsAreNotEqual
    }
}