﻿namespace GestorEventos.Core
{
    public class Constants
    {
        public const string RoleNameAdmin = "Role_Admin";
        public const string RoleNameOrganizer = "Role_Organizer";
        public const string RoleNameCreditor = "Role_Creditor";
        public const string RoleNameParticipant = "Role_Participant";

        public const string DefaultAdmin_Email = "default@admin.com";
        public const string DefaultAdmin_Password = "Password01";

        public const string SendGridAuthLink_ConfirmEmail = "?userID={0}&code={1}";
        public const string SendGridAuthLink_ResetPassword = "?userID={0}&code={1}";
        public const string SendGrid_NewEventSubject = "Check out this new Event";
    }
}