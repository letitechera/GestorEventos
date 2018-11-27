namespace GestorEventos.Core
{
    public class Constants
    {
        public const string RoleNameAdmin = "Role_Admin";
        public const string RoleNameOrganizer = "Role_Organizer";
        public const string RoleNameCreditor = "Role_Creditor";
        public const string RoleNameParticipant = "Role_Participant";

        public const string DefaultAdmin_Email = "default@admin.com";
        public const string DefaultAdmin_Password = "Password01";

        //MailChimp
        public const int MailChimp_ContactListId = 1234;
        public const string MailChimp_MergeFieldType_Url = "url";
        public const string MailChimp_MergeFieldType_Text = "text";
        public const string MailChimp_MergeFieldHelp_Event = "Field for events distribution";
    }
}