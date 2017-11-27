Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

'Ejemplo uso de la clase: Crypto.Encrypt(TextoOrigen,ClaveEncriptacion), Crypto.Decrypt(TextoEncriptado,ClaveEncriptacion)
Public Class Crypto
    Private Shared DES As New TripleDESCryptoServiceProvider
    Private Shared MD5 As New MD5CryptoServiceProvider
    Private Shared Function MD5Hash(ByVal value As String) As Byte()
        Return MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value))
    End Function

    Public Shared Function Encrypt(ByVal stringToEncrypt As String, ByVal key As String) As String
        DES.Key = Crypto.MD5Hash(key)
        DES.Mode = CipherMode.ECB
        Dim Buffer As Byte() = ASCIIEncoding.ASCII.GetBytes(stringToEncrypt)
        Return Convert.ToBase64String(DES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length))
    End Function

    Public Shared Function Decrypt(ByVal encryptedString As String, ByVal key As String) As String
        Try
            DES.Key = Crypto.MD5Hash(key)
            DES.Mode = CipherMode.ECB
            Dim Buffer As Byte() = Convert.FromBase64String(encryptedString)
            Return ASCIIEncoding.ASCII.GetString(DES.CreateDecryptor().TransformFinalBlock(Buffer, 0, Buffer.Length))
        Catch ex As Exception
            'MsgBox.Show("Invalid Key", "Decryption Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return ""
        End Try
    End Function

    Private Shared skey As String = "Q|&#8@$+("

    Public Shared Function Encriptar(ByVal value As String)

        Dim des As New TripleDESCryptoServiceProvider

        des.IV = New Byte(7) {}

        Dim pdb As New Security.Cryptography.PasswordDeriveBytes(skey, New Byte(-1) {})

        des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, New Byte(7) {})

        Dim ms As New IO.MemoryStream((value.Length * 2) - 1)

        Dim encStream As New CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write)

        Dim plainBytes As Byte() = Encoding.Default.GetBytes(value)

        encStream.Write(plainBytes, 0, plainBytes.Length)

        encStream.FlushFinalBlock()

        Dim encryptedBytes(CInt(ms.Length - 1)) As Byte

        ms.Position = 0

        ms.Read(encryptedBytes, 0, CInt(ms.Length))

        encStream.Close()

        Return Convert.ToBase64String(encryptedBytes)

    End Function

    Public Shared Function DesEncriptar(ByVal value As String) As String

        Dim des As New TripleDESCryptoServiceProvider

        des.IV = New Byte(7) {}

        Dim pdb As New Security.Cryptography.PasswordDeriveBytes(skey, New Byte(-1) {})

        des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, New Byte(7) {})

        Dim encryptedBytes As Byte() = Convert.FromBase64String(value)

        Dim ms As New IO.MemoryStream(value.Length)

        Dim decStream As New CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write)

        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)

        decStream.FlushFinalBlock()

        Dim plainBytes(CInt(ms.Length - 1)) As Byte

        ms.Position = 0

        ms.Read(plainBytes, 0, CInt(ms.Length))

        decStream.Close()

        Return Encoding.UTF8.GetString(plainBytes)
    End Function

End Class


