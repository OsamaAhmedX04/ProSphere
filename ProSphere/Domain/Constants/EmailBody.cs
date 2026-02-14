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

        public static string GetDeleteAccountBody(string email, string otp)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Confirm Account Deletion</title>
                </head>
                <body style='margin:0; padding:0; background-color:#f4f6f8; font-family:Arial, Helvetica, sans-serif;'>

                    <table width='100%' cellpadding='0' cellspacing='0'>
                        <tr>
                            <td align='center' style='padding:40px 0;'>

                                <table width='600' cellpadding='0' cellspacing='0' style='background:#ffffff; border-radius:12px; overflow:hidden; box-shadow:0 8px 30px rgba(0,0,0,0.08);'>

                                    <!-- Header -->
                                    <tr>
                                        <td style='background:#dc2626; padding:30px; text-align:center; color:#ffffff;'>
                                            <h1 style='margin:0; font-size:26px;'>Confirm Account Deletion ⚠️</h1>
                                            <p style='margin:8px 0 0; font-size:15px;'>This action is irreversible</p>
                                        </td>
                                    </tr>

                                    <!-- Body -->
                                    <tr>
                                        <td style='padding:35px 40px; color:#333333;'>

                                            <h2 style='margin-top:0;'>Hello,</h2>

                                            <p style='font-size:15px; line-height:1.6;'>
                                                We received a request to permanently delete the account associated with:
                                                <br />
                                                <strong>{email}</strong>
                                            </p>

                                            <p style='font-size:15px; line-height:1.6;'>
                                                To confirm this request, please use the verification code below.
                                            </p>

                                            <!-- OTP Box -->
                                            <div style='text-align:center; margin:35px 0;'>
                                                <div style='display:inline-block;
                                                            background:#fef2f2;
                                                            border:2px dashed #dc2626;
                                                            color:#dc2626;
                                                            padding:18px 30px;
                                                            font-size:26px;
                                                            font-weight:bold;
                                                            letter-spacing:6px;
                                                            border-radius:10px;'>
                                                    {otp}
                                                </div>
                                            </div>

                                            <p style='font-size:14px; color:#555555; text-align:center;'>
                                                This code will expire shortly for security reasons.
                                            </p>

                                            <p style='font-size:14px; margin-top:30px;'>
                                                If you did <strong>not</strong> request account deletion, please ignore this email.
                                                Your account will remain active.
                                            </p>

                                            <p style='margin-top:40px;'>
                                                Regards,<br />
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

        public static string GetVerifiedIdentityAcceptanceBody(string email, string firstName, string lastName)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Identity Verified</title>
                </head>
                <body style='margin:0; padding:0; background-color:#f4f6f8; font-family:Arial, Helvetica, sans-serif;'>

                    <table width='100%' cellpadding='0' cellspacing='0'>
                        <tr>
                            <td align='center' style='padding:40px 0;'>

                                <table width='600' cellpadding='0' cellspacing='0'
                                       style='background:#ffffff; border-radius:14px; overflow:hidden; box-shadow:0 10px 35px rgba(0,0,0,0.08);'>

                                    <!-- Header -->
                                    <tr>
                                        <td style='background:linear-gradient(135deg,#16a34a,#22c55e); padding:35px; text-align:center; color:#ffffff;'>
                                            <h1 style='margin:0; font-size:28px;'>Identity Verified ✅</h1>
                                            <p style='margin:10px 0 0; font-size:15px; opacity:0.95;'>
                                                Your account is now officially verified
                                            </p>
                                        </td>
                                    </tr>

                                    <!-- Body -->
                                    <tr>
                                        <td style='padding:40px; color:#333333;'>

                                            <h2 style='margin-top:0; font-size:20px;'>
                                                Hello {firstName} {lastName},
                                            </h2>

                                            <p style='font-size:15px; line-height:1.7;'>
                                                We’re happy to let you know that your identity verification has been
                                                <strong>successfully approved</strong>.
                                            </p>

                                            <p style='font-size:15px; line-height:1.7;'>
                                                This means your account associated with:
                                                <br />
                                                <strong>{email}</strong>
                                                <br />
                                                is now fully verified and trusted on <strong>ProSphere</strong>.
                                            </p>

                                            <!-- Highlight Box -->
                                            <div style='margin:30px 0;
                                                        padding:22px;
                                                        background:#f0fdf4;
                                                        border-left:5px solid #22c55e;
                                                        border-radius:8px;'>
                                                <p style='margin:0; font-size:14.5px; color:#166534; line-height:1.6;'>
                                                    ✔ Your profile appears as verified to others<br />
                                                </p>
                                            </div>

                                            <p style='font-size:14.5px; line-height:1.7;'>
                                                If you are an <strong>Investor</strong>, you may proceed with additional
                                                verification steps to unlock higher trust levels and investment capabilities.
                                            </p>

                                            <p style='margin-top:35px;'>
                                                Welcome aboard,<br />
                                                <strong>ProSphere Verification Team</strong>
                                            </p>
                                        </td>
                                    </tr>

                                    <!-- Footer -->
                                    <tr>
                                        <td style='background:#f4f6f8; padding:22px; text-align:center; font-size:12px; color:#777777;'>
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
        public static string GetVerifiedIdentityRejectionBody(string email, string firstName, string lastName, string rejectionReason)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Identity Verification Rejected</title>
                </head>
                <body style='margin:0; padding:0; background-color:#f4f6f8; font-family:Arial, Helvetica, sans-serif;'>

                <table width='100%' cellpadding='0' cellspacing='0'>
                <tr>
                <td align='center' style='padding:40px 0;'>

                <table width='600' cellpadding='0' cellspacing='0'
                       style='background:#ffffff; border-radius:14px; overflow:hidden; box-shadow:0 10px 35px rgba(0,0,0,0.08);'>

                    <!-- Header -->
                    <tr>
                        <td style='background:#dc2626; padding:35px; text-align:center; color:#ffffff;'>
                            <h1 style='margin:0; font-size:28px;'>Verification Update ❌</h1>
                            <p style='margin:10px 0 0; font-size:15px; opacity:0.95;'>
                                Action required to continue
                            </p>
                        </td>
                    </tr>

                    <!-- Body -->
                    <tr>
                        <td style='padding:40px; color:#333333;'>

                            <h2 style='margin-top:0; font-size:20px;'>
                                Hello {firstName} {lastName},
                            </h2>

                            <p style='font-size:15px; line-height:1.7;'>
                                Thank you for submitting your identity verification request.
                                After reviewing your documents, we were unable to approve your verification at this time.
                            </p>

                            <p style='font-size:15px; line-height:1.7;'>
                                <strong>Reason:</strong>
                            </p>

                            <!-- Reason Box -->
                            <div style='margin:20px 0;
                                        padding:18px;
                                        background:#fef2f2;
                                        border-left:5px solid #dc2626;
                                        border-radius:8px;'>
                                <p style='margin:0; font-size:14.5px; color:#7f1d1d; line-height:1.6;'>
                                    {rejectionReason}
                                </p>
                            </div>

                            <p style='font-size:14.5px; line-height:1.7;'>
                                You can update your documents and submit a new verification request at any time from your account dashboard.
                            </p>

                            <p style='font-size:14.5px; line-height:1.7;'>
                                If you believe this was a mistake, feel free to contact our support team.
                            </p>

                            <p style='margin-top:35px;'>
                                Regards,<br />
                                <strong>ProSphere Verification Team</strong>
                            </p>

                        </td>
                    </tr>

                    <!-- Footer -->
                    <tr>
                        <td style='background:#f4f6f8; padding:22px; text-align:center; font-size:12px; color:#777777;'>
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

        public static string GetVerifiedFinancialAcceptanceBody(string email, string firstName, string lastName)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Financial Verification Approved</title>
                </head>
                <body style='margin:0; padding:0; background:#f4f6f8; font-family:Arial, Helvetica, sans-serif;'>

                <table width='100%' cellpadding='0' cellspacing='0'>
                <tr>
                <td align='center' style='padding:50px 0;'>

                <table width='640' cellpadding='0' cellspacing='0'
                       style='background:#ffffff; border-radius:18px; overflow:hidden;
                              box-shadow:0 18px 50px rgba(0,0,0,0.12);'>

                <tr>
                <td style='background:linear-gradient(135deg,#0f172a,#1e293b);
                           padding:48px; text-align:center; color:#ffffff;'>
                <h1 style='margin:0; font-size:30px;'>Financial Verification Approved 💼</h1>
                <p style='margin-top:14px; font-size:16px; opacity:0.9;'>
                You’re now recognized as a verified investor
                </p>
                </td>
                </tr>

                <tr>
                <td style='padding:48px; color:#333333;'>

                <h2 style='margin-top:0; font-size:22px;'>
                Hello {firstName},
                </h2>

                <p style='font-size:15.8px; line-height:1.8;'>
                We’re pleased to inform you that your financial verification has been
                <strong>successfully approved</strong> after reviewing your submitted documents.
                </p>

                <div style='margin:32px 0; padding:26px;
                            background:#f8fafc;
                            border-radius:14px;
                            border-left:6px solid #0f172a;'>
                <p style='margin:0; font-size:15.5px; line-height:1.7; color:#0f172a;'>
                ✔ Your investor profile is now Financially verified<br />
                ✔ You can invest in projects with higher limits<br />
                ✔ Project creators will see you as a trusted investor
                </p>
                </div>

                <p style='font-size:15.8px; line-height:1.8;'>
                You can now explore and invest in available opportunities on
                <strong>ProSphere</strong> with confidence.
                </p>

                <p style='margin-top:42px;'>
                Welcome to the inner circle,<br />
                <strong>ProSphere Financial Compliance Team</strong>
                </p>

                </td>
                </tr>

                <tr>
                <td style='background:#f4f6f8; padding:26px;
                           text-align:center; font-size:12px; color:#777;'>
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
        public static string GetVerifiedFinancialRejectionBody(string email, string firstName, string lastName, string rejectionReason)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Financial Verification Update</title>
                </head>
                <body style='margin:0; padding:0; background:#f4f6f8; font-family:Arial, Helvetica, sans-serif;'>

                <table width='100%' cellpadding='0' cellspacing='0'>
                <tr>
                <td align='center' style='padding:50px 0;'>

                <table width='640' cellpadding='0' cellspacing='0'
                       style='background:#ffffff; border-radius:18px; overflow:hidden;
                              box-shadow:0 18px 50px rgba(0,0,0,0.12);'>

                <tr>
                <td style='background:#7c2d12; padding:48px; text-align:center; color:#ffffff;'>
                <h1 style='margin:0; font-size:30px;'>Financial Verification Required ⚠️</h1>
                <p style='margin-top:14px; font-size:16px; opacity:0.9;'>
                Additional information is needed
                </p>
                </td>
                </tr>

                <tr>
                <td style='padding:48px; color:#333333;'>

                <h2 style='margin-top:0; font-size:22px;'>
                Hello {firstName} {lastName},
                </h2>

                <p style='font-size:15.8px; line-height:1.8;'>
                Thank you for submitting your financial verification request.
                After reviewing your documents, we’re unable to approve it at this time.
                </p>

                <p style='font-size:15.8px; margin-top:26px;'>
                <strong>Reason:</strong>
                </p>

                <div style='margin:22px 0; padding:24px;
                            background:#fff7ed;
                            border-radius:14px;
                            border-left:6px solid #7c2d12;'>
                <p style='margin:0; font-size:15.5px; line-height:1.7; color:#7c2d12;'>
                {rejectionReason}
                </p>
                </div>

                <p style='font-size:15.8px; line-height:1.8;'>
                You can update your financial documents and submit a new verification request
                from your dashboard at any time.
                </p>

                <p style='font-size:15.8px; line-height:1.8;'>
                If you believe this decision was incorrect, our compliance team is ready to assist you.
                </p>

                <p style='margin-top:42px;'>
                Regards,<br />
                <strong>ProSphere Financial Compliance Team</strong>
                </p>

                </td>
                </tr>

                <tr>
                <td style='background:#f4f6f8; padding:26px;
                           text-align:center; font-size:12px; color:#777;'>
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

        public static string GetVerifiedProfessionalAcceptanceBody(string email, string firstName, string lastName)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Professional Verification Approved</title>
                </head>
                <body style='margin:0; padding:0; background:#f4f6f8; font-family:Arial, Helvetica, sans-serif;'>

                <table width='100%' cellpadding='0' cellspacing='0'>
                <tr>
                <td align='center' style='padding:50px 0;'>

                <table width='650' cellpadding='0' cellspacing='0'
                       style='background:#ffffff; border-radius:18px; overflow:hidden;
                              box-shadow:0 18px 55px rgba(0,0,0,0.12);'>

                <tr>
                <td style='background:linear-gradient(135deg,#1e3a8a,#2563eb);
                           padding:50px; text-align:center; color:#ffffff;'>
                <h1 style='margin:0; font-size:30px;'>Professional Investor Verified 🏢</h1>
                <p style='margin-top:14px; font-size:16px; opacity:0.95;'>
                Your professional status has been officially approved
                </p>
                </td>
                </tr>

                <tr>
                <td style='padding:50px; color:#333333;'>

                <h2 style='margin-top:0; font-size:22px;'>
                Hello {firstName},
                </h2>

                <p style='font-size:15.8px; line-height:1.8;'>
                We’re happy to inform you that your <strong>professional investor verification</strong>
                has been successfully approved after reviewing your submitted documents.
                </p>

                <div style='margin:34px 0; padding:28px;
                            background:#eff6ff;
                            border-radius:14px;
                            border-left:6px solid #2563eb;'>
                <p style='margin:0; font-size:15.5px; line-height:1.7; color:#1e3a8a;'>
                ✔ You are now recognized as a <strong>Professional Investor</strong><br />
                ✔ Higher investment limits are unlocked<br />
                ✔ Projects can see your verified professional status
                </p>
                </div>

                <p style='font-size:15.8px; line-height:1.8;'>
                You can now access advanced investment opportunities and participate
                in high-trust projects on <strong>ProSphere</strong>.
                </p>

                <p style='margin-top:45px;'>
                Welcome to the professional tier,<br />
                <strong>ProSphere Compliance & Verification Team</strong>
                </p>

                </td>
                </tr>

                <tr>
                <td style='background:#f4f6f8; padding:26px;
                           text-align:center; font-size:12px; color:#777;'>
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
        public static string GetVerifiedProfessionalRejectionBody(string email, string firstName, string lastName, string rejectionReason)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Professional Verification Update</title>
                </head>
                <body style='margin:0; padding:0; background:#f4f6f8; font-family:Arial, Helvetica, sans-serif;'>

                <table width='100%' cellpadding='0' cellspacing='0'>
                <tr>
                <td align='center' style='padding:50px 0;'>

                <table width='650' cellpadding='0' cellspacing='0'
                       style='background:#ffffff; border-radius:18px; overflow:hidden;
                              box-shadow:0 18px 55px rgba(0,0,0,0.12);'>

                <tr>
                <td style='background:#991b1b; padding:50px; text-align:center; color:#ffffff;'>
                <h1 style='margin:0; font-size:30px;'>Professional Verification Required ⚠️</h1>
                <p style='margin-top:14px; font-size:16px; opacity:0.95;'>
                Additional documentation is needed
                </p>
                </td>
                </tr>

                <tr>
                <td style='padding:50px; color:#333333;'>

                <h2 style='margin-top:0; font-size:22px;'>
                Hello {firstName},
                </h2>

                <p style='font-size:15.8px; line-height:1.8;'>
                Thank you for submitting your professional investor verification request.
                After carefully reviewing your documents, we were unable to approve it at this time.
                </p>

                <p style='font-size:15.8px; margin-top:26px;'>
                <strong>Reason for rejection:</strong>
                </p>

                <div style='margin:22px 0; padding:26px;
                            background:#fef2f2;
                            border-radius:14px;
                            border-left:6px solid #991b1b;'>
                <p style='margin:0; font-size:15.5px; line-height:1.7; color:#7f1d1d;'>
                {rejectionReason}
                </p>
                </div>

                <p style='font-size:15.8px; line-height:1.8;'>
                You can update your professional documents and submit a new verification request
                from your account dashboard at any time.
                </p>

                <p style='font-size:15.8px; line-height:1.8;'>
                If you believe this decision was made in error, our compliance team is ready to assist you.
                </p>

                <p style='margin-top:45px;'>
                Regards,<br />
                <strong>ProSphere Compliance & Verification Team</strong>
                </p>

                </td>
                </tr>

                <tr>
                <td style='background:#f4f6f8; padding:26px;
                           text-align:center; font-size:12px; color:#777;'>
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


        public static string GetWelcomeNewEmployeeBody(string email, string name)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Welcome to ProSphere Team</title>
                </head>
                <body style='margin:0; padding:0; background:#f4f6f8; font-family:Arial, Helvetica, sans-serif;'>

                <table width='100%' cellpadding='0' cellspacing='0'>
                <tr>
                <td align='center' style='padding:50px 0;'>

                <table width='640' cellpadding='0' cellspacing='0'
                       style='background:#ffffff; border-radius:16px; overflow:hidden;
                              box-shadow:0 15px 45px rgba(0,0,0,0.1);'>

                <!-- Header -->
                <tr>
                <td style='background:linear-gradient(135deg,#0f172a,#1e293b);
                           padding:45px; text-align:center; color:#ffffff;'>
                <h1 style='margin:0; font-size:28px;'>Welcome to ProSphere 👋</h1>
                <p style='margin-top:12px; font-size:15px; opacity:0.9;'>
                You’ve been assigned as a Moderator
                </p>
                </td>
                </tr>

                <!-- Body -->
                <tr>
                <td style='padding:45px; color:#333;'>

                <h2 style='margin-top:0; font-size:20px;'>
                Hello {name},
                </h2>

                <p style='font-size:15.5px; line-height:1.8;'>
                Your account <strong>{email}</strong> has been successfully created
                and assigned the <strong>Moderator</strong> role within ProSphere.
                </p>

                <div style='margin:30px 0; padding:24px;
                            background:#f1f5f9;
                            border-left:6px solid #0f172a;
                            border-radius:12px;'>
                <p style='margin:0; font-size:15px; line-height:1.7; color:#0f172a;'>
                To activate your account:<br /><br />
                1. Please contact the <strong>ProSphere Team</strong>.<br />
                2. You will receive your login email and temporary password.<br />
                3. Sign in and change your password to activate your account.
                </p>
                </div>

                <p style='font-size:15.5px; line-height:1.8;'>
                For security reasons, your account will remain inactive
                until you complete the password change process.
                </p>

                <p style='margin-top:40px;'>
                We’re glad to have you on board.<br />
                <strong>ProSphere Administration Team</strong>
                </p>

                </td>
                </tr>

                <!-- Footer -->
                <tr>
                <td style='background:#f4f6f8; padding:22px;
                           text-align:center; font-size:12px; color:#777;'>
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
