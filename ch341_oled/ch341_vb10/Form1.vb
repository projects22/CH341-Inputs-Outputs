Imports System.Runtime.InteropServices
Imports System.Text
Public Class Form1

#Region "CONST, FONT"

    Public Const DLL_PATH As String = "C:\Windows\SysWOW64\CH341DLL.DLL" 'Public Const DLL_PATH As String = "c:\Temp\CH341DLL.DLL"

    ' standard ascii 5x7 font 
    Dim oledFont() As Byte = {
      &H0, &H0, &H0, &H0, &H0 _
    , &H3E, &H5B, &H4F, &H5B, &H3E _
    , &H3E, &H6B, &H4F, &H6B, &H3E _
    , &H1C, &H3E, &H7C, &H3E, &H1C _
    , &H18, &H3C, &H7E, &H3C, &H18 _
    , &H1C, &H57, &H7D, &H57, &H1C _
    , &H1C, &H5E, &H7F, &H5E, &H1C _
    , &H0, &H18, &H3C, &H18, &H0 _
    , &HFF, &HE7, &HC3, &HE7, &HFF _
    , &H0, &H18, &H24, &H18, &H0 _
    , &HFF, &HE7, &HDB, &HE7, &HFF _
    , &H30, &H48, &H3A, &H6, &HE _
    , &H26, &H29, &H79, &H29, &H26 _
    , &H40, &H7F, &H5, &H5, &H7 _
    , &H40, &H7F, &H5, &H25, &H3F _
    , &H5A, &H3C, &HE7, &H3C, &H5A _
    , &H7F, &H3E, &H1C, &H1C, &H8 _
    , &H8, &H1C, &H1C, &H3E, &H7F _
    , &H14, &H22, &H7F, &H22, &H14 _
    , &H5F, &H5F, &H0, &H5F, &H5F _
    , &H6, &H9, &H7F, &H1, &H7F _
    , &H0, &H66, &H89, &H95, &H6A _
    , &H60, &H60, &H60, &H60, &H60 _
    , &H94, &HA2, &HFF, &HA2, &H94 _
    , &H8, &H4, &H7E, &H4, &H8 _
    , &H10, &H20, &H7E, &H20, &H10 _
    , &H8, &H8, &H2A, &H1C, &H8 _
    , &H8, &H1C, &H2A, &H8, &H8 _
    , &H1E, &H10, &H10, &H10, &H10 _
    , &HC, &H1E, &HC, &H1E, &HC _
    , &H30, &H38, &H3E, &H38, &H30 _
    , &H6, &HE, &H3E, &HE, &H6 _
    , &H0, &H0, &H0, &H0, &H0 _
    , &H0, &H0, &H5F, &H0, &H0 _
    , &H0, &H7, &H0, &H7, &H0 _
    , &H14, &H7F, &H14, &H7F, &H14 _
    , &H24, &H2A, &H7F, &H2A, &H12 _
    , &H23, &H13, &H8, &H64, &H62 _
    , &H36, &H49, &H55, &H22, &H50 _
    , &H0, &H5, &H3, &H0, &H0 _
    , &H0, &H1C, &H22, &H41, &H0 _
    , &H0, &H41, &H22, &H1C, &H0 _
    , &H8, &H2A, &H1C, &H2A, &H8 _
    , &H8, &H8, &H3E, &H8, &H8 _
    , &H0, &H50, &H30, &H0, &H0 _
    , &H8, &H8, &H8, &H8, &H8 _
    , &H0, &H60, &H60, &H0, &H0 _
    , &H20, &H10, &H8, &H4, &H2 _
    , &H3E, &H51, &H49, &H45, &H3E _
    , &H0, &H42, &H7F, &H40, &H0 _
    , &H42, &H61, &H51, &H49, &H46 _
    , &H21, &H41, &H45, &H4B, &H31 _
    , &H18, &H14, &H12, &H7F, &H10 _
    , &H27, &H45, &H45, &H45, &H39 _
    , &H3C, &H4A, &H49, &H49, &H30 _
    , &H1, &H71, &H9, &H5, &H3 _
    , &H36, &H49, &H49, &H49, &H36 _
    , &H6, &H49, &H49, &H29, &H1E _
    , &H0, &H0, &H22, &H0, &H0 _
    , &H0, &H56, &H36, &H0, &H0 _
    , &H0, &H8, &H14, &H22, &H41 _
    , &H14, &H14, &H14, &H14, &H14 _
    , &H41, &H22, &H14, &H8, &H0 _
    , &H2, &H1, &H51, &H9, &H6 _
    , &H32, &H49, &H79, &H41, &H3E _
    , &H7E, &H11, &H11, &H11, &H7E _
    , &H7F, &H49, &H49, &H49, &H36 _
    , &H3E, &H41, &H41, &H41, &H22 _
    , &H7F, &H41, &H41, &H22, &H1C _
    , &H7F, &H49, &H49, &H49, &H41 _
    , &H7F, &H9, &H9, &H1, &H1 _
    , &H3E, &H41, &H41, &H51, &H32 _
    , &H7F, &H8, &H8, &H8, &H7F _
    , &H0, &H41, &H7F, &H41, &H0 _
    , &H20, &H40, &H41, &H3F, &H1 _
    , &H7F, &H8, &H14, &H22, &H41 _
    , &H7F, &H40, &H40, &H40, &H40 _
    , &H7F, &H2, &H4, &H2, &H7F _
    , &H7F, &H4, &H8, &H10, &H7F _
    , &H3E, &H41, &H41, &H41, &H3E _
    , &H7F, &H9, &H9, &H9, &H6 _
    , &H3E, &H41, &H51, &H21, &H5E _
    , &H7F, &H9, &H19, &H29, &H46 _
    , &H46, &H49, &H49, &H49, &H31 _
    , &H1, &H1, &H7F, &H1, &H1 _
    , &H3F, &H40, &H40, &H40, &H3F _
    , &H1F, &H20, &H40, &H20, &H1F _
    , &H7F, &H20, &H18, &H20, &H7F _
    , &H63, &H14, &H8, &H14, &H63 _
    , &H3, &H4, &H78, &H4, &H3 _
    , &H61, &H51, &H49, &H45, &H43 _
    , &H0, &H0, &H7F, &H41, &H41 _
    , &H2, &H4, &H8, &H10, &H20 _
    , &H41, &H41, &H7F, &H0, &H0 _
    , &H4, &H2, &H1, &H2, &H4 _
    , &H40, &H40, &H40, &H40, &H40 _
    , &H0, &H1, &H2, &H4, &H0 _
    , &H20, &H54, &H54, &H54, &H78 _
    , &H7F, &H48, &H44, &H44, &H38 _
    , &H38, &H44, &H44, &H44, &H20 _
    , &H38, &H44, &H44, &H48, &H7F _
    , &H38, &H54, &H54, &H54, &H18 _
    , &H8, &H7E, &H9, &H1, &H2 _
    , &H8, &H14, &H54, &H54, &H3C _
    , &H7F, &H8, &H4, &H4, &H78 _
    , &H0, &H44, &H7D, &H40, &H0 _
    , &H20, &H40, &H44, &H3D, &H0 _
    , &H0, &H7F, &H10, &H28, &H44 _
    , &H0, &H41, &H7F, &H40, &H0 _
    , &H7C, &H4, &H18, &H4, &H78 _
    , &H7C, &H8, &H4, &H4, &H78 _
    , &H38, &H44, &H44, &H44, &H38 _
    , &H7C, &H14, &H14, &H14, &H8 _
    , &H8, &H14, &H14, &H18, &H7C _
    , &H7C, &H8, &H4, &H4, &H8 _
    , &H48, &H54, &H54, &H54, &H20 _
    , &H4, &H3F, &H44, &H40, &H20 _
    , &H3C, &H40, &H40, &H20, &H7C _
    , &H1C, &H20, &H40, &H20, &H1C _
    , &H3C, &H40, &H30, &H40, &H3C _
    , &H44, &H28, &H10, &H28, &H44 _
    , &HC, &H50, &H50, &H50, &H3C _
    , &H44, &H64, &H54, &H4C, &H44 _
    , &H0, &H8, &H36, &H41, &H0 _
    , &H0, &H0, &H7F, &H0, &H0 _
    , &H0, &H41, &H36, &H8, &H0 _
    , &H8, &H8, &H2A, &H1C, &H8 _
    , &H8, &H1C, &H2A, &H8, &H8 _
    , &H0, &H0, &H0, &H0, &H0 _
    , &H70, &HF8, &HFF, &HFE, &H78
    }
#End Region

#Region "Device"

    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341GetVersion() As Integer
    End Function

    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341GetDrvVersion() As Integer
    End Function

    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341OpenDevice(ByVal index As Integer) As IntPtr
    End Function

    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Sub CH341CloseDevice(ByVal index As Integer)
    End Sub

    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341ResetDevice(ByVal index As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

#End Region

#Region "I2C"

    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Protected Friend Shared Function CH341SetStream(ByVal iIndex As Integer, ByVal iMode As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341SetDelaymS(ByVal iIndex As Integer, ByVal iDelay As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341StreamI2C(ByVal iIndex As Integer, ByVal iWriteLength As Integer, ByVal iWriteBuffer As Byte(), ByVal iReadLength As Integer, ByVal oReadBuffer As Byte()) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341ReadI2C(ByVal iIndex As Integer, ByVal iDevice As Byte, ByVal iAddr As Byte, ByRef oByte As Byte) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341WriteI2C(ByVal iIndex As Integer, ByVal iDevice As Byte, ByVal iAddr As Byte, ByVal iByte As Byte) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

#End Region


    Private Sub btnConnect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConnect.Click

        Label1.Text = "DLL version: " & CH341GetVersion()
        If CH341OpenDevice(0) Then
            Label2.Text = "Device is Open"
        Else
            Label2.Text = "Device failed to open"
        End If
        'CH341Label2.Text = ResetDevice(0)
        Label3.Text = "Driver version: " & CH341GetDrvVersion()
        If CH341SetStream(0, &H81) Then
            Label4.Text = "I2C set to 100KHz" '0x81=MSB first, 100KHz
        Else
            Label4.Text = "I2C settings failed"
        End If
        'CH341SetDelaymS(0, 2)
        text1.MaxLength = 144
        oledInit()
        clear()

    End Sub

    Private Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim text As String
        Dim row, col As Byte

        text = text1.Text
        For i = 1 To text.Length
            drawChar(Asc(Mid(text, i, 1)), row, col)
            col = col + 1
            If col > 17 Then
                col = 0
                row = row + 1
                If row > 7 And col > 17 Then
                    row = 0
                    col = 0
                End If
            End If

        Next
    End Sub

    Private Sub i2cWrite(ByVal data As Byte)
        Dim addr(3), ret(3) As Byte
        addr(0) = &H78
        addr(1) = 0
        addr(2) = data

        CH341StreamI2C(0, 3, addr, 3, ret)
    End Sub
    Private Sub oledInit()
        i2cWrite(&HAE)   ' DISPLAYOFF
        i2cWrite(&H8D)         ' CHARGEPUMP *
        i2cWrite(&H14)     '&H14-pump on
        i2cWrite(&H20)         ' MEMORYMODE
        i2cWrite(&H0)      '&H0=horizontal, &H01=vertical, &H02=page
        i2cWrite(&HA1)        'SEGREMAP * A0/A1=top/bottom 
        i2cWrite(&HC8)     'COMSCANDEC * C0/C8=left/right
        i2cWrite(&HDA)         ' SETCOMPINS *
        i2cWrite(&H12)   '&H22=4rows, &H12=8rows
        i2cWrite(&H81)        ' SETCONTRAST
        i2cWrite(&H9F)     '&H8F

        i2cWrite(&HAF)          'DISPLAYON
    End Sub

    Private Sub clear()
        Dim x As Byte = 130
        Dim addr(x), ret(x), y As Byte
        addr(0) = &H78
        addr(1) = &H40

        For y = 0 To 7
            i2cWrite(&HB0 + y)   ' 0 to 7 pages
            i2cWrite(0)  ' low nibble
            i2cWrite(&H10)  'high nibble
            CH341StreamI2C(0, x, addr, x, ret)
        Next

    End Sub

    Private Sub drawChar(ByVal fig As Byte, ByVal y As Byte, ByVal x As Byte)
        Dim addr(7), ret(7) As Byte
        addr(0) = &H78
        addr(1) = &H40
        For i = 0 To 4
            'addr(i + 2) = oledFont(5 * (fig - 32) + i)
            addr(i + 2) = oledFont(5 * fig + i)
        Next

        i2cWrite(&H21)   'col addr
        i2cWrite(7 * x)   'col start
        i2cWrite(7 * x + 4)      'col end
        i2cWrite(&H22)   '
        i2cWrite(y)   'Page start
        i2cWrite(y)      'Page end
        CH341StreamI2C(0, 7, addr, 7, ret)

    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CH341CloseDevice(0)
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        text1.Text = ""
        clear()
    End Sub
End Class
