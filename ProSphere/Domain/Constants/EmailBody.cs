namespace ProSphere.Domain.Constants
{
    public static class EmailBody
    {
        public static string GetEmailConfirmationBody(
            string email, string confirmationLink, string firstName, string lastName, string role)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Confirm Your Email</title>
                </head>
                <body style='margin:0; padding:0; background-color:#f4f6f8; font-family:Arial, Helvetica, sans-serif;'>

                    <table width='100%' cellpadding='0' cellspacing='0'>
                        <tr>
                            <td align='center' style='padding:40px 0;'>

                                <table width='600' cellpadding='0' cellspacing='0' style='background:#ffffff; border-radius:12px; overflow:hidden; box-shadow:0 8px 30px rgba(0,0,0,0.08);'>
                    
                                    <!-- Header -->
                                    <tr>
                                        <td style='background:#4f46e5; padding:30px; text-align:center; color:#ffffff;'>
                                            <h1 style='margin:0; font-size:26px;'>Welcome to ProSphere 🚀</h1>
                                            <p style='margin:8px 0 0; font-size:15px;'>One more step to get started</p>
                                        </td>
                                    </tr>

                                    <!-- Body -->
                                    <tr>
                                        <td style='padding:35px 40px; color:#333333;'>
                                            <h2 style='margin-top:0;'>Hello {firstName} {lastName},</h2>

                                            <p style='font-size:15px; line-height:1.6;'>
                                                You’ve successfully registered as <strong>{role}</strong> using the email:
                                                <br />
                                                <strong>{email}</strong>
                                            </p>

                                            <p style='font-size:15px; line-height:1.6;'>
                                                Please confirm your email address by clicking the button below.
                                                This link will expire for security reasons.
                                            </p>

                                            <!-- Button -->
                                            <div style='text-align:center; margin:35px 0;'>
                                                <a href='{confirmationLink}'
                                                   style='background:#4f46e5;
                                                          color:#ffffff;
                                                          padding:14px 28px;
                                                          text-decoration:none;
                                                          font-size:16px;
                                                          border-radius:8px;
                                                          display:inline-block;
                                                          font-weight:bold;'>
                                                    Confirm Email
                                                </a>
                                            </div>

                                            <p style='font-size:14px; color:#555555;'>
                                                If the button doesn’t work, copy and paste this link into your browser:
                                            </p>

                                            <p style='font-size:13px; word-break:break-all; color:#4f46e5;'>
                                                {confirmationLink}
                                            </p>

                                            <p style='font-size:14px; margin-top:30px;'>
                                                If you didn’t create this account, you can safely ignore this email.
                                            </p>

                                            <p style='margin-top:40px;'>
                                                Cheers,<br />
                                                <strong>ProSphere Team</strong>
                                            </p>
                                        </td>
                                    </tr>

                                    <!-- Footer -->
                                    <tr>
                                        <td style='background:#f4f6f8; padding:20px; text-align:center; font-size:12px; color:#777777;'>
                                            © {DateTime.UtcNow.Year} ProSphere. All rights reserved.
                                        </td>
                                    </tr>

                                </table>

                            </td>
                        </tr>
                    </table>

                </body>
                </html>";
        }

        public static string GetResetPasswordBody(string email, string resetPasswordLink)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Reset Your Password</title>
                </head>
                <body style='margin:0; padding:0; background-color:#f4f6f8; font-family:Arial, Helvetica, sans-serif;'>

                    <table width='100%' cellpadding='0' cellspacing='0'>
                        <tr>
                            <td align='center' style='padding:40px 0;'>

                                <table width='600' cellpadding='0' cellspacing='0' style='background:#ffffff; border-radius:12px; overflow:hidden; box-shadow:0 8px 30px rgba(0,0,0,0.08);'>
                    
                                    <!-- Header -->
                                    <tr>
                                        <td style='background:#ef4444; padding:30px; text-align:center; color:#ffffff;'>
                                            <h1 style='margin:0; font-size:26px;'>Reset Your Password 🔐</h1>
                                            <p style='margin:8px 0 0; font-size:15px;'>Secure your account access</p>
                                        </td>
                                    </tr>

                                    <!-- Body -->
                                    <tr>
                                        <td style='padding:35px 40px; color:#333333;'>

                                            <h2 style='margin-top:0;'>Hello,</h2>

                                            <p style='font-size:15px; line-height:1.6;'>
                                                We received a request to reset the password for the account associated with:
                                                <br />
                                                <strong>{email}</strong>
                                            </p>

                                            <p style='font-size:15px; line-height:1.6;'>
                                                Click the button below to create a new password.
                                                This link will expire for security reasons.
                                            </p>

                                            <!-- Button -->
                                            <div style='text-align:center; margin:35px 0;'>
                                                <a href='{resetPasswordLink}'
                                                   style='background:#ef4444;
                                                          color:#ffffff;
                                                          padding:14px 28px;
                                                          text-decoration:none;
                                                          font-size:16px;
                                                          border-radius:8px;
                                                          display:inline-block;
                                                          font-weight:bold;'>
                                                    Reset Password
                                                </a>
                                            </div>

                                            <p style='font-size:14px; color:#555555;'>
                                                If the button doesn’t work, copy and paste this link into your browser:
                                            </p>

                                            <p style='font-size:13px; word-break:break-all; color:#ef4444;'>
                                                {resetPasswordLink}
                                            </p>

                                            <p style='font-size:14px; margin-top:30px;'>
                                                If you didn’t request a password reset, you can safely ignore this email.
                                                Your password will remain unchanged.
                                            </p>

                                            <p style='margin-top:40px;'>
                                                Stay safe,<br />
                                                <strong>ProSphere Security Team</strong>
                                            </p>

                                        </td>
                                    </tr>

                                    <!-- Footer -->
                                    <tr>
                                        <td style='background:#f4f6f8; padding:20px; text-align:center; font-size:12px; color:#777777;'>
                                            © {DateTime.UtcNow.Year} ProSphere. All rights reserved.
                                        </td>
                                    </tr>

                                </table>

                            </td>
                        </tr>
                    </table>

                </body>
                </html>";
        }
    }
}
