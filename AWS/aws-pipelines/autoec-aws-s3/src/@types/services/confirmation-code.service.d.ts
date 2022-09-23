
declare namespace ConfirmationCode {
  type ContactType = 'phone' | 'email';

  type PostSendConfirmationCodeParams = {
    contactType: ContactType;
    email: string;
    phone: string;
    resend: boolean;
  };

  type PatchConfirmCodeParams = {
    contactType: ContactType;
    email: string;
    phone: string;
    code: string;
  };
}
